using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StolotoParser_v2.Models;

namespace StolotoParser_v2.UserControls
{
    public partial class LotaryInfoControl : UserControl, ILotaryInfoControl
    {
        private bool _toggle;

        private int _step = 50;

        private int _defaultValue = 50;

        private int _maximum = 30000;

        private Element _element;

        public LotaryInfoControl()
        {
            InitializeComponent();

            this.numericUpDown.Value = this._defaultValue;

            this.numericUpDown.Increment = this._step;

            this.numericUpDown.Maximum = this._maximum;

            this.toolStripTotal.Text = Convert.ToString(this._defaultValue);

            this.numericUpDown.ValueChanged += this.NumericUpDown_ValueChanged;

            this.btnLotary.Click += BtnLotary_Click;
        }

        public event EventHandler BtnLotaryClick;

        public bool Canceled
        {
            set
            {
                var setTextAction = new Action(() =>
                {
                    this._toggle = !value;

                    this.btnLotary.Text = string.Format("Loaded {0}, start new", this._element.BtnName);

                    this.numericUpDown.Enabled = value;
                });

                if (this.InvokeRequired)
                {
                    this.Invoke(setTextAction);
                }
                else
                {
                    setTextAction();
                }
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
                this._element = value;

                var setTextAction = new Action(() =>
                {
                    this.btnLotary.Text = value.BtnName;

                    if (value.TotalCount.HasValue)
                    {
                        this.numericUpDown.Value = value.TotalCount.Value;
                    }
                });

                if (btnLotary.InvokeRequired)
                {
                    this.btnLotary.Invoke(setTextAction);
                }
                else
                {
                    setTextAction();
                }
            }
        }

        public ProcessInfo ProcessInfo
        {
            set
            {
                var loadedAction = new Action(() => { this.toolStripLoaded.Text = Convert.ToString(value.Loaded); });

                var loadedTotal = new Action(() => { this.toolStripTotal.Text = Convert.ToString(value.Total); });

                var progressAction = new Action(() => 
                {
                    this.toolStripProgressBar.Maximum = value.Total;

                    this.toolStripProgressBar.Value = value.Loaded;
                });


                if (statusBar.InvokeRequired)
                {
                    this.statusBar.Invoke(loadedAction);

                    this.statusBar.Invoke(loadedTotal);

                    this.statusBar.Invoke(progressAction);
                }
                else
                {
                    loadedAction();

                    loadedTotal();

                    progressAction();
                }
            }
        }

        public LotaryToolTip ToolTip
        {
            set
            {
                var settextAction = new Action(() =>
                {
                    var tTip = string.Format("Starus:\t{0}\nPage:\t{1}", value.Status, value.Page);

                    toolTip.SetToolTip(this.btnLotary, tTip);
                });

                if (btnLotary.InvokeRequired)
                {
                    btnLotary.Invoke(settextAction);
                }
                else
                {
                    settextAction();
                }
            }
        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            var numericUpDown = (sender as NumericUpDown);

            this._element.TotalCount = (int?)(Math.Floor(numericUpDown.Value));

            this.toolStripTotal.Text = Convert.ToString(numericUpDown.Value);
        }

        private void BtnLotary_Click(object sender, EventArgs e)
        {
            this._toggle ^= true;

            this.numericUpDown.Enabled = !this._toggle;

            (sender as Button).Text = this._toggle 
                ? string.Format("Stop load from {0}", this._element.BtnName)
                : this._element.BtnName;

            if (this.BtnLotaryClick != null)
            {
                this.toolStripProgressBar.Value = 0;

                this.BtnLotaryClick.Invoke(this._toggle, e);
            }
        }
    }
}
