using StolotoParser_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StolotoParser_v2.UserControls
{
    public class ElementButton : Button, IElementButton
    {
        private readonly ToolTip _toolTip;

        public event EventHandler ElementButtonClick;

        private bool _toggle;

        private Element _element;

        public ElementButton()
        {
            this._toolTip = new ToolTip();

            this._toolTip.SetToolTip(this, "");

            this.Click += ElementButton_Click;
        }

        private void ElementButton_Click(object sender, EventArgs e)
        {
            this._toggle ^= true;

            if (this._toggle)
            {
                this.Text = "Started";
            }

            if (this.ElementButtonClick != null)
            {
                this.ElementButtonClick.Invoke(this._toggle, e);
            }
        }

        public Element Element
        {
            get
            {
                return this._element;
            }
            set
            {
                this.InvoceAction(new Action(() =>
                {
                    this._element = value;

                    this.Text = value.BtnName;

                    this._toolTip.SetToolTip(this, value.BtnName);
                }));
            }
        }

        public void Continue()
        {
            this.InvoceAction(new Action(() =>
            {
                this._toggle = false;

                this.Text = "Paused";
            }));
        }

        public void Pause()
        {
            this.InvoceAction(new Action(() =>
            {
                this._toggle = false;

                this.Text = "Paused";
            }));
        }

        public void Canceled()
        {
            this.InvoceAction(new Action(() =>
                {
                    this._toggle = false;

                    this.Text = this._element.BtnName;

                    this.Enabled = true;
                }));
        }

        public LotaryToolTip ToolTip
        {
            set
            {
                this.InvoceAction(new Action(() =>
                {
                    var tTip = string.Format("Name: {0}\nStarus:\t{1}\nPage:\t{2}", this._element.BtnName, value.Status, value.Page);

                    this._toolTip.SetToolTip(this, tTip);
                }));
            }
        }

        private void InvoceAction(Action action)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
