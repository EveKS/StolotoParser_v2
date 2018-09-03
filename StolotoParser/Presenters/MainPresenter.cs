using StolotoParser_v2.Models;
using StolotoParser_v2.Services;
using StolotoParser_v2.UserControls;
using System;
using System.IO;
using System.Windows.Forms;

namespace StolotoParser_v2.Presenters
{
    public class MainPresenter
    {
        private AppSettings _appSettings;

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
        }

        private void _mainForm_NewElementEdded(object sender, EventArgs e)
        {
            if(sender is IElementButton)
            {
                new ElementPresenter(sender as IElementButton, this._jsonService, this._htmlService,
                    this._htmlParser, this._fileWriteService, this._appSettings.Format);
            }
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
    }
}
