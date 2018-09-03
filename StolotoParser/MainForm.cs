using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StolotoParser_v2.Models;
using StolotoParser_v2.UserControls;

namespace StolotoParser_v2
{
    public interface IMainForm
    {
        event EventHandler OnFormLoad;

        event EventHandler StopButtonClick;

        event EventHandler StartButtonClick;

        event EventHandler PauseButtonClick;

        event EventHandler ContinueButtonClick;

        event EventHandler NewElementEdded;

        void SetButtons(Element[] elements);

        void SetLoadedStatus();

        void UpdateSelectedStatuses(Element element, string statusText);
    }

    public partial class MainForm : Form, IMainForm
    {
        private Status _status;

        private int _step = 50;

        private int _defaultValue = 50;

        private int _maximum = 30000;

        private Status _buttonStartStatus
        {
            get
            {
                return this._status;
            }
            set
            {
                this.InvoceAction(new Action(() =>
                {
                    this.buttonContinue.Text = value.ToString();

                    this._status = value;
                }));
            }
        }

        public event EventHandler OnFormLoad;

        public event EventHandler StartButtonClick;

        public event EventHandler PauseButtonClick;

        public event EventHandler ContinueButtonClick;

        public event EventHandler StopButtonClick;

        public event EventHandler NewElementEdded;

        private IElementButton _selectedButton;

        public MainForm()
        {
            InitializeComponent();

            this._buttonStartStatus = Status.Старт;

            this.numericUpDown1.Maximum = this._maximum;

            this.numericUpDown1.Increment = this._step;

            this.numericUpDown1.Value = this._defaultValue;

            this.Load += new EventHandler(this.MainForm_Load);

            this.numericUpDown1.ValueChanged += this.NumericUpDown1_ValueChanged;

            this.buttonContinue.Click += ButtonContinue_Click;

            this.buttonStop.Click += ButtonStop_Click;
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            this.buttonContinue.Text = Status.Старт.ToString();

            if (this.StopButtonClick != null)
            {
                this.StopButtonClick.Invoke(null, EventArgs.Empty);
            }
        }

        private void ButtonContinue_Click(object sender, EventArgs e)
        {
            if (this._selectedButton.Loaded) this._buttonStartStatus = Status.Старт;

            switch (this._buttonStartStatus)
            {
                case Status.Старт:
                    this._buttonStartStatus = Status.Приостановить;

                    this._selectedButton.Loaded = false;

                    if (this.StartButtonClick != null)
                    {
                        this.StartButtonClick.Invoke(this._selectedButton, EventArgs.Empty);
                    }
                    break;
                case Status.Приостановить:
                    if (this._selectedButton.Loaded)
                    {
                        this._buttonStartStatus = Status.Старт;

                        if (this.StopButtonClick != null)
                        {
                            this.StopButtonClick.Invoke(null, EventArgs.Empty);
                        }
                    }
                    else
                    {
                        this._buttonStartStatus = Status.Продолжить;

                        if (this.PauseButtonClick != null)
                        {
                            this.PauseButtonClick.Invoke(null, EventArgs.Empty);
                        }
                    }
                    break;
                case Status.Продолжить:
                    this._buttonStartStatus = Status.Приостановить;

                    if (this.ContinueButtonClick != null)
                    {
                        this.ContinueButtonClick.Invoke(null, EventArgs.Empty);
                    }
                    break;
                default:
                    break;
            }
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            foreach (var btn in this.Controls.OfType<IElementButton>())
            {
                if (btn.Element == null) continue;

                btn.Element.TotalCount = (int)(sender as NumericUpDown).Value;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (this.OnFormLoad != null)
            {
                this.OnFormLoad(sender, e);
            }
        }

        public void SetLoadedStatus()
        {
            this._buttonStartStatus = Status.Старт;
        }

        public void UpdateSelectedStatuses(Element element, string statusText)
        {
            this.InvoceAction(new Action(() =>
            {
                this.buttonContinue.Enabled = true;

                var total = element.TotalCount.HasValue ? (int)element.TotalCount : 50;

                this.progressBar2.Value = 0;

                this.progressBar2.Maximum = total > 0 ? total : total;

                this.textBox1.Text = statusText;
            }));
        }

        void IMainForm.SetButtons(Element[] elements)
        {
            var buttons = this.Controls.OfType<IElementButton>()
                .OrderBy(bt => (bt as Button).TabIndex).ToList();

            for (int index = 0; index < elements.Length; index++)
            {
                var element = elements[index];

                var button = buttons[index];

                button.Element = element;

                button.ElementButtonClick += Button_ElementButtonClick;

                if (this.NewElementEdded != null)
                {
                    this.NewElementEdded.Invoke(button, EventArgs.Empty);
                }
            }

            foreach (var btn in buttons.Skip(elements.Length))
            {
                (btn as Button).Enabled = false;
            }
        }

        private void Button_ElementButtonClick(object sender, EventArgs e)
        {
            this.InvoceAction(new Action(() =>
            {
                var btn = sender as IElementButton;

                this._selectedButton = btn;
            }));
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
