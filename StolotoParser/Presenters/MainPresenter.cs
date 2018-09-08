﻿using StolotoParser_v2.Models;
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

        public MainPresenter(IMainForm mainForm, IJsonService jsonService, IHtmlService htmlService,
            IHtmlParser htmlParser)
        {
            this._mainForm = mainForm;

            this._jsonService = jsonService;

            this._htmlService = htmlService;

            this._htmlParser = htmlParser;

            this._mainForm.OnFormLoad += new EventHandler(this._mainForm_OnLoad);

            this._mainForm.NewElementEdded += new EventHandler(this._mainForm_NewElementEdded);

            this._mainForm.StartButtonClick += this._mainForm_StartButtonClick;

            this._mainForm.StopButtonClick += this._mainForm_StopButtonClick;

            this._mainForm.GetLastDrawClick += _mainForm_GetLastDrawClick;
        }

        private void _mainForm_GetLastDrawClick(object sender, EventArgs e)
        {
            this._selectedButton = sender as IElementButton;

            this._selectedElement = this._selectedButton.Element;

            this._cancellationTokenSource = new CancellationTokenSource();

            this._task = Task.Factory.StartNew(() => this.GetDraw(), this._cancellationTokenSource.Token)
                .ContinueWith(task =>
                {
                    this._mainForm.SetLastDraw = task.Result;

                    this._mainForm.SetLoaded();
                });
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
            var parsingSettings = this._mainForm.ParsingSettings;

            IFileWriteService fileWriteService = new FileWriteService(parsingSettings, this._selectedElement, this._cancellationTokenSource.Token, this._mainForm.UpdateProgres, this._mainForm.AppTextListBox, this._lastDrawCurrent, this._lastDrawAll);

            fileWriteService.InitialOldData();

            var newFormat = "";

            var total = this._selectedElement.TotalCount.HasValue && this._selectedElement.TotalCount.Value > 0
                ? this._selectedElement.TotalCount.Value
                : this._drawInPage;

            var max = total;

            var page = 1;

            if (!parsingSettings.AddToCurrent) fileWriteService.ClearFile();

            var setData = true;

            this._startDraw = this._lastDrawCurrent + 1;

            while (((parsingSettings.AddToAll ? true : (parsingSettings.AddToCurrent ? (this._startDraw > this._lastDrawCurrent || total > 0) : total > 0))))
            {
                if(total == 0)
                {
                    this._mainForm.AppTextListBox("Идет запись в общий фаил");
                }

                if (this._loadedPage == -1)
                {
                    this._loadedPage = 1;

                    newFormat = string.Format(this._appSettings.Format, this._selectedElement.PathName, 1);
                }
                else
                {
                    newFormat = string.Format(this._appSettings.ContinueFormat, this._selectedElement.PathName, 1, this._startDraw);
                }

                total = total - this._drawInPage > this._drawInPage ? total - this._drawInPage : 0;

                if (this._cancellationTokenSource.IsCancellationRequested) return;

                var json = this._htmlService.GetStringContent(newFormat);

                if (this._cancellationTokenSource.IsCancellationRequested) return;

                var postResulModel = this._jsonService.JsonConvertDeserializeObject<PostResulModel>(json);

                if (this._cancellationTokenSource.IsCancellationRequested) return;

                var stolotoParseResults = this._htmlParser.ParseHtml(postResulModel.Data, parsingSettings.ParsingExtraNumbers);

                if (this._cancellationTokenSource.IsCancellationRequested) return;

                fileWriteService.WriteStolotoResult(stolotoParseResults, max - (total + this._drawInPage));

                if (setData)
                {
                    setData = false;

                    this._mainForm.UpdateDatas((new FilesData() { LastDrawCurrent = stolotoParseResults.Where(val => val.Numbers.Count > 0).Max(val => val.Draw) }));
                }

                this._selectedButton.ToolTip = new LotaryToolTip() { Status = postResulModel.Status, Page = page };

                var breakIteration = (parsingSettings.AddToCurrent ? stolotoParseResults.Any(val => val.Draw == this._lastDrawCurrent) : total == 0)
                    && (parsingSettings.AddToAll ? stolotoParseResults.Any(val => val.Draw == this._lastDrawAll) : true);

                if (postResulModel.Stop || breakIteration) break;

                this._startDraw = stolotoParseResults.Min(val => val.Draw);

                page++;

                this._loadedPage++;
            }

            fileWriteService.WritOldData();

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
