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

        private readonly IJsonService _jsonService;

        private readonly IHtmlService _htmlService;

        private readonly IHtmlParser _htmlParser;

        public ElementPresenter(ILotaryInfoControl lotaryInfoControl, IJsonService jsonService, IHtmlService htmlService,
            IHtmlParser htmlParser, string pathFormat)
        {
            this._pathFormat = pathFormat;

            this._lotaryInfoControl = lotaryInfoControl;

            this._jsonService = jsonService;

            this._htmlService = htmlService;

            this._htmlParser = htmlParser;

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
            else if(this._cancellationTokenSource != null)
            {
                this._cancellationTokenSource.Cancel();
            }
        }

        private void TaskBody()
        {

        }
    }
}
