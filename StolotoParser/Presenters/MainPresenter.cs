using StolotoParser_v2.Models;
using StolotoParser_v2.Services;
using StolotoParser_v2.UserControls;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StolotoParser_v2.Presenters
{
    public class MainPresenter
    {
        private Task _task;

        private CancellationTokenSource _cancellationTokenSource;

        private AppSettings _appSettings;

        private IElementButton _selectedButton;

        private Element _selectedElement;

        private readonly IMainForm _mainForm;

        private readonly IJsonService _jsonService;

        private readonly IHtmlService _htmlService;

        private readonly IHtmlParser _htmlParser;

        private readonly IFileWriteService _fileWriteService;

        public MainPresenter(IMainForm mainForm, IJsonService jsonService, IHtmlService htmlService,
            IHtmlParser htmlParser, IFileWriteService fileWriteService)
        {
            this._mainForm = mainForm;

            this._jsonService = jsonService;

            this._htmlService = htmlService;

            this._htmlParser = htmlParser;

            this._fileWriteService = fileWriteService;

            this._mainForm.OnFormLoad += new EventHandler(this._mainForm_OnLoad);

            this._mainForm.NewElementEdded += new EventHandler(this._mainForm_NewElementEdded);

            this._mainForm.StartButtonClick += _mainForm_StartButtonClick;
        }

        private void _mainForm_StartButtonClick(object sender, EventArgs e)
        {
            this._selectedButton = sender as IElementButton;

            this._selectedElement = this._selectedButton.Element;


            this._cancellationTokenSource = new CancellationTokenSource();

            this._task = Task.Factory.StartNew(() => this.TaskBody(), this._cancellationTokenSource.Token);
        }

        private void _mainForm_NewElementEdded(object sender, EventArgs e)
        {
            if(sender is IElementButton)
            {
                (sender as IElementButton).ElementButtonClick += MainPresenter_ElementButtonClick;
            }
        }

        private void MainPresenter_ElementButtonClick(object sender, EventArgs e)
        {
            this._mainForm.UpdateSelectedStatuses((sender as IElementButton).Element, string.Format(this._appSettings.Format, (sender as IElementButton).Element.PathName, 1));
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

        private void TaskBody()
        {
            var maximum = 0;

            var total = this._selectedElement.TotalCount.HasValue && this._selectedElement.TotalCount.Value > 0
                ? this._selectedElement.TotalCount.Value
                : 50;

            maximum = total;

            var page = 1;

            this._fileWriteService.ClearFile(this._selectedElement);

            while (total > 0)
            {
                total = total - 50 > 50 ? total - 50 : 0;

                if (this._cancellationTokenSource.IsCancellationRequested) return;

                var json = this._htmlService.GetStringContent(string.Format(this._appSettings.Format, this._selectedElement.PathName, page));

                if (this._cancellationTokenSource.IsCancellationRequested) return;

                var postResulModel = this._jsonService.JsonConvertDeserializeObject<PostResulModel>(json);

                if (this._cancellationTokenSource.IsCancellationRequested) return;

                var stolotoParseResults = this._htmlParser.ParseHtml(postResulModel.Data);

                if (this._cancellationTokenSource.IsCancellationRequested) return;

                this._fileWriteService.WriteStolotoResult(stolotoParseResults, this._selectedElement);

                if (this._cancellationTokenSource.IsCancellationRequested) return;

                this._selectedButton.ToolTip = new LotaryToolTip() { Status = postResulModel.Status, Page = page };

                if (postResulModel.Stop) break;

                page++;
            }

            this._selectedButton.Loaded = true;

            this._mainForm.SetLoadedStatus();
        }
    }
}
