using StolotoParser_v2.Models;
using StolotoParser_v2.Services;
using StolotoParser_v2.UserControls;
using System;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;

namespace StolotoParser_v2.Presenters
{
    public class MainPresenter
    {
        private int _startDraw = -1;

        private int _loadedPage = -1;

        private int _drawInPage = 50;

        private int _lastDrawCurrent;

        private int _lastDrawAll;

        private Task _task;

        private CancellationTokenSource _cancellationTokenSource;

        private AppSettings _appSettings;

        private IElementButton _selectedButton;

        private Element _selectedElement;

        private readonly IMainForm _mainForm;

        private readonly IJsonService _jsonService;

        private readonly IHtmlService _htmlService;

        private readonly IHtmlParser _htmlParser;

        private readonly  ILoggerFactory _loggerFactory;

        public MainPresenter(IMainForm mainForm, IJsonService jsonService, IHtmlService htmlService,
            IHtmlParser htmlParser, ILoggerFactory loggerFactory)
        {
            this._mainForm = mainForm;

            this._jsonService = jsonService;

            this._htmlService = htmlService;

            this._htmlParser = htmlParser;

            this._loggerFactory = loggerFactory;

            this._mainForm.OnFormLoad += new EventHandler(this._mainForm_OnLoad);

            this._mainForm.NewElementEdded += new EventHandler(this._mainForm_NewElementEdded);

            this._mainForm.StartButtonClick += this._mainForm_StartButtonClick;

            this._mainForm.StopButtonClick += this._mainForm_StopButtonClick;

            this._mainForm.GetLastDrawClick += _mainForm_GetLastDrawClick;

            this._mainForm.ButtonTestClick += _mainForm_ButtonTestClick;

            this._mainForm.MainFormFormClosing += _mainForm_MainFormFormClosing;
        }

        private void _mainForm_MainFormFormClosing(object sender, EventArgs e)
        {
            this._loggerFactory.CloseProgramLogged();
        }

        private void _mainForm_ButtonTestClick(object sender, EventArgs e)
        {
            var selectedElement = sender as Element;

            if (!string.IsNullOrWhiteSpace(selectedElement.FileName))
            {
                this.TestFile(selectedElement.FileName);
            }

            if (!string.IsNullOrWhiteSpace(selectedElement.FileAllName))
            {
                this.TestFile(selectedElement.FileAllName);
            }
        }

        private void TestFile(string filePath)
        {
            this._mainForm.AppTextListBox(string.Format("Проверяется {0} фаил", filePath));

            var stolotoParseResults = this.GetData(filePath);

            if (stolotoParseResults == null)
            {
                this._mainForm.AppTextListBox(string.Format("Фаил {0} отсутствует", filePath));
            }
            else if (stolotoParseResults.Count() == 0)
            {
                this._mainForm.AppTextListBox("Фаил пуст");
            }
            else
            {
                this.GetDetails(stolotoParseResults);

                this.AbsenceTest(stolotoParseResults);

                this.OrderTest(stolotoParseResults);

                this.DuplicateTest(stolotoParseResults);
            }
        }

        private void OrderTest(IEnumerable<StolotoParseResult> stolotoParseResults)
        {
            StolotoParseResult old = null;

            foreach (var item in stolotoParseResults)
            {
                if (old != null && (old.Draw - 1) != item.Draw)
                {
                    this._mainForm.AppTextListBox(string.Format("Нарушен порядок: {0} {1} тиражей", old.Draw, item.Draw));
                }

                old = item;
            }
        }

        private void AbsenceTest(IEnumerable<StolotoParseResult> stolotoParseResults)
        {
            var tempData = stolotoParseResults.Distinct(new BoxEqualityComparer()).OrderBy(s => s.Draw).ToList();

            var fistDraw = tempData.FirstOrDefault().Draw;

            var lastDraw = tempData.LastOrDefault().Draw;

            for (int i = 0, j = fistDraw; i < tempData.Count; j++)
            {
                var draw = tempData[i].Draw;

                if (draw != j)
                {
                    this._mainForm.AppTextListBox(string.Format("Тираж {0} отсутствует", j));
                }
                else
                {
                    i++;
                }

                if (j == lastDraw) break;
            }
        }

        private void DuplicateTest(IEnumerable<StolotoParseResult> stolotoParseResults)
        {
            foreach (var item in stolotoParseResults.GroupBy(s => s.Draw).Where(g => g.Count() > 1))
            {
                this._mainForm.AppTextListBox(string.Format("Тираж {0} встречается: {1} раза", item.Key, item.Count()));
            }
        }

        private void GetDetails(IEnumerable<StolotoParseResult> stolotoParseResults)
        {
            this._mainForm.AppTextListBox(string.Format("Всего тиражей в файле {0}, из них уникальных: {1}", stolotoParseResults.Count(), stolotoParseResults.Distinct(new BoxEqualityComparer()).Count()));

            this._mainForm.AppTextListBox(string.Format("Первый тираж в файле {0}, минимальный: {1}", stolotoParseResults.LastOrDefault().Draw, stolotoParseResults.Min(s => s.Draw)));

            this._mainForm.AppTextListBox(string.Format("Последний тираж в файле {0}, максимальный: {1}", stolotoParseResults.FirstOrDefault().Draw, stolotoParseResults.Max(s => s.Draw)));
        }

        private List<StolotoParseResult> GetData(string filePath)
        {
            if (!File.Exists(filePath)) return null;

            var stolotoParseResults = new List<StolotoParseResult>();

            using (StreamReader sr = new StreamReader(Path.Combine(Application.StartupPath, filePath)))
            {
                while (!sr.EndOfStream)
                {
                    string firstRow = sr.ReadLine();

                    var datas = firstRow.Split('_');

                    var draw = Convert.ToInt32(datas[0].Trim());

                    var numbers = datas[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(n =>
                    {
                        var tmp = n.TrimStart('0');

                        return Convert.ToInt32(string.IsNullOrEmpty(tmp) ? "0" : tmp);
                    });

                    stolotoParseResults.Add(new StolotoParseResult() { Draw = draw, Numbers = numbers.ToList() });
                }
            }

            return stolotoParseResults;
        }

        private void _mainForm_GetLastDrawClick(object sender, EventArgs e)
        {
            try
            {
            this._selectedButton = sender as IElementButton;

            this._selectedElement = this._selectedButton.Element;

            if (this._cancellationTokenSource != null)
            {
                this._cancellationTokenSource.Cancel();
            }

            this._cancellationTokenSource = new CancellationTokenSource();

            this._task = Task.Factory.StartNew(() => this.GetDraw(), this._cancellationTokenSource.Token)
                .ContinueWith(task =>
                {
                    this._mainForm.SetLastDraw = task.Result;

                    this._mainForm.SetLoaded();
                });

            }
            catch (Exception ex)
            {
                this._loggerFactory.ErrorLogged(ex);
            }
        }

        private void _mainForm_StopButtonClick(object sender, EventArgs e)
        {
            this._loadedPage = -1;

            this._startDraw = -1;

            this._cancellationTokenSource.Cancel();
        }

        private void _mainForm_StartButtonClick(object sender, EventArgs e)
        {
            this._selectedButton = sender as IElementButton;

            this._selectedElement = this._selectedButton.Element;

            if (this._cancellationTokenSource != null)
            {
                this._cancellationTokenSource.Cancel();
            }

            this._cancellationTokenSource = new CancellationTokenSource();

            this._mainForm.ClearListBox();

            this._task = Task.Factory.StartNew(() => this.TaskBody(), this._cancellationTokenSource.Token);
        }

        private void _mainForm_NewElementEdded(object sender, EventArgs e)
        {
            if (sender is IElementButton)
            {
                (sender as IElementButton).ElementButtonClick += MainPresenter_ElementButtonClick;
            }
        }

        private void MainPresenter_ElementButtonClick(object sender, EventArgs e)
        {
            this._mainForm.ClearListBox();

            var element = (sender as IElementButton).Element;

            this._mainForm.UpdateSelectedStatuses((sender as IElementButton).Element, string.Format(this._appSettings.Format, (sender as IElementButton).Element.PathName, 1));

            this._mainForm.UpdateDatas(this.GetDatas(element));
        }

        private void _mainForm_OnLoad(object sender, EventArgs e)
        {
            this._loggerFactory.RunProgramLogged();

            this.GetSettings();

            this._mainForm.SetButtons(this._appSettings.Elements);
        }

        private void GetSettings()
        {
            using (StreamReader sr = new StreamReader(Path.Combine(Application.StartupPath, "appsettings.json")))
            {
                this._appSettings = this._jsonService.JsonConvertDeserializeObject<AppSettings>(sr.ReadToEnd());
            }
        }

        private FilesData GetDatas(Element element)
        {
            FilesData filesData = null;

            if (File.Exists(Path.Combine(Application.StartupPath, element.FileName)))
            {
                filesData = new FilesData() { DataName = element.BtnName };

                using (StreamReader sr = new StreamReader(Path.Combine(Application.StartupPath, element.FileName)))
                {
                    string firstRow = sr.ReadLine();

                    if (!string.IsNullOrEmpty(firstRow))
                    {
                        var datas = firstRow.Split('_');

                        this._lastDrawCurrent = Convert.ToInt32(datas[0].Trim());

                        filesData.LastDrawCurrent = this._lastDrawCurrent;
                    }
                    else
                    {
                        this._lastDrawCurrent = 0;
                    }
                }
            }

            if (!string.IsNullOrEmpty(element.FileAllName) && File.Exists(Path.Combine(Application.StartupPath, element.FileAllName)))
            {
                if (filesData == null) filesData = new FilesData() { DataName = element.BtnName };

                using (StreamReader sr = new StreamReader(Path.Combine(Application.StartupPath, element.FileAllName)))
                {
                    string firstRow = sr.ReadLine();

                    if (!string.IsNullOrEmpty(firstRow))
                    {
                        var datas = firstRow.Split('_');

                        this._lastDrawAll = Convert.ToInt32(datas[0].Trim());

                        filesData.LastDrawFull = this._lastDrawAll;
                    }
                }
            }

            return filesData;
        }

        private void TaskBody()
        {
            this.GetDatas(this._selectedElement);

            var parsingSettings = this._mainForm.ParsingSettings;

            var newFormat = "";

            var total = this._selectedElement.TotalCount.HasValue && this._selectedElement.TotalCount.Value > 0
                ? this._selectedElement.TotalCount.Value
                : this._drawInPage;

            var stopTakeDrawCurrent = parsingSettings.AddToCurrent ? this._lastDrawCurrent : total;

            IFileWriteService fileWriteService = new FileWriteService(parsingSettings, this._selectedElement, this._cancellationTokenSource.Token, this._mainForm.UpdateProgres, this._mainForm.AppTextListBox, stopTakeDrawCurrent, this._lastDrawAll, this._loggerFactory.ErrorLogged);

            fileWriteService.InitialOldData();

            var page = 1;

            var setData = true;

            this._startDraw = this._lastDrawCurrent + 1;

            try
            {
                var iteration = 0;

                var maximum = total;

                var loaded = 0;

                while (((parsingSettings.AddToAll ? true : (parsingSettings.AddToCurrent ? (this._startDraw > this._lastDrawCurrent || total > 0) : total > 0))))
                {
                    if (this._loadedPage == -1)
                    {
                        this._loadedPage = 1;

                        newFormat = string.Format(this._appSettings.Format, this._selectedElement.PathName, 1);
                    }
                    else
                    {
                        newFormat = string.Format(this._appSettings.ContinueFormat, this._selectedElement.PathName, 1, this._startDraw);
                    }

                    total = total - this._drawInPage >= this._drawInPage ? total - this._drawInPage : 0;

                    if (this._cancellationTokenSource.IsCancellationRequested) return;

                    var json = this._htmlService.GetStringContent(newFormat);

                    if (this._cancellationTokenSource.IsCancellationRequested) return;

                    var postResulModel = this._jsonService.JsonConvertDeserializeObject<PostResulModel>(json);

                    if (this._cancellationTokenSource.IsCancellationRequested) return;

                    var stolotoParseResults = this._htmlParser.ParseHtml(postResulModel.Data, parsingSettings.ParsingExtraNumbers);

                    if (this._cancellationTokenSource.IsCancellationRequested) return;

                    if(iteration == 0)
                    {
                        maximum = parsingSettings.AddToCurrent ? stolotoParseResults.Max(val => val.Draw) - this._lastDrawCurrent : maximum;
                    }

                    loaded += stolotoParseResults.Count;

                    this._mainForm.SetLoadingProgress(loaded, maximum > loaded ? maximum : loaded);

                    fileWriteService.AppendResults(stolotoParseResults);

                    if (setData)
                    {
                        setData = false;

                        this._mainForm.UpdateDatas((new FilesData() { LastDrawCurrent = stolotoParseResults.Where(val => val.Numbers.Count > 0).Max(val => val.Draw) }));
                    }

                    this._selectedButton.ToolTip = new LotaryToolTip() { Status = postResulModel.Status, Page = page };

                    var breakIteration = (parsingSettings.AddToCurrent ? stolotoParseResults.Any(val => val.Draw <= this._lastDrawCurrent) : total == 0)
                        && (parsingSettings.AddToAll ? stolotoParseResults.Any(val => val.Draw <= this._lastDrawAll) : true);

                    if (postResulModel.Stop || breakIteration) break;

                    this._startDraw = stolotoParseResults.Min(val => val.Draw) - 1;

                    page++;

                    this._loadedPage++;

                    iteration++;
                }
            }
            catch (Exception ex)
            {
                this._loggerFactory.ErrorLogged(ex);
            }

            fileWriteService.Finalize();

            this._loadedPage = -1;

            this._startDraw = -1;

            this._mainForm.SetLoaded();
        }

        private int GetDraw()
        {
            var json = this._htmlService.GetStringContent(string.Format(this._appSettings.Format, this._selectedElement.PathName, 1));

            var postResulModel = this._jsonService.JsonConvertDeserializeObject<PostResulModel>(json);

            var stolotoParseResults = this._htmlParser.ParseHtml(postResulModel.Data, false);

            var max = stolotoParseResults.Max(val => val.Draw);

            var maxVal = stolotoParseResults.FirstOrDefault(val => val.Draw == max);

            if (maxVal.Numbers == null || maxVal.Numbers.Count == 0)
            {
                this._mainForm.AppTextListBox(string.Format("{0} тираж ожидается", max--));
            }

            return max;
        }
    }
}
