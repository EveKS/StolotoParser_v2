using StolotoParser_v2.Models;
using StolotoParser_v2.Services;
using StolotoParser_v2.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StolotoParser_v2.Presenters
{
    public class ElementPresenter
    {
        private bool _get;

        private Task _task;

        private CancellationTokenSource _cancellationTokenSource;

        private readonly string _pathFormat;

        private readonly ILotaryInfoControl _lotaryInfoControl;

        private readonly Element _element;

        private readonly IJsonService _jsonService;

        private readonly IHtmlService _htmlService;

        private readonly IHtmlParser _htmlParser;

        private readonly IFileWriteService _fileWriteService;

        public ElementPresenter(ILotaryInfoControl lotaryInfoControl, IJsonService jsonService, IHtmlService htmlService,
            IHtmlParser htmlParser, IFileWriteService fileWriteService, string pathFormat)
        {
            this._pathFormat = pathFormat;

            this._lotaryInfoControl = lotaryInfoControl;

            this._element = lotaryInfoControl.Element;

            this._jsonService = jsonService;

            this._htmlService = htmlService;

            this._htmlParser = htmlParser;

            this._fileWriteService = fileWriteService;

            this._lotaryInfoControl.BtnLotaryClick += this._lotaryInfoControl_BtnLotaryClick;
        }

        private void _lotaryInfoControl_BtnLotaryClick(object sender, EventArgs e)
        {
            this._get = (bool)sender;

            if (this._get)
            {
                this._cancellationTokenSource = new CancellationTokenSource();

                this._task = Task.Factory.StartNew(() => this.TaskBody(), this._cancellationTokenSource.Token);
            }
            else if (this._cancellationTokenSource != null)
            {
                this._cancellationTokenSource.Cancel();
            }
        }

        private void TaskBody()
        {
            var maximum = 0;

            var total = this._element.TotalCount.HasValue && this._element.TotalCount.Value > 0
                ? this._element.TotalCount.Value
                : 50;

            maximum = total;

            var page = 1;

            this._fileWriteService.ClearFile(this._element);

            while(total > 0)
            {
                total = total - 50 > 50 ? total - 50 : 0;

                var json = this._htmlService.GetStringContent(string.Format(this._pathFormat, this._element.PathName, page));

                var postResulModel = this._jsonService.JsonConvertDeserializeObject<PostResulModel>(json);

                var stolotoParseResults = this._htmlParser.ParseHtml(postResulModel.Data);

                this._fileWriteService.WriteStolotoResult(stolotoParseResults, this._element);

                this._lotaryInfoControl.ToolTip = new LotaryToolTip() { Status = postResulModel.Status, Page = page };

                this._lotaryInfoControl.ProcessInfo = new ProcessInfo() { Loaded = maximum - total, Total = maximum };

                if (postResulModel.Stop) break;

                page++;
            }

            this._lotaryInfoControl.Canceled = true;
        }
    }
}
