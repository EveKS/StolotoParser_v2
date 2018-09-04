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

        event EventHandler NewElementEdded;

        void SetButtons(Element[] elements);

        void SetLoaded();

        void UpdateSelectedStatuses(Element element, string statusText);
    }

    public partial class MainForm : Form, IMainForm
    {
        private int _step = 50;

        private int _defaultValue = 50;

        private int _maximum = 30000;

        public event EventHandler OnFormLoad;

        public event EventHandler StartButtonClick;

        public event EventHandler StopButtonClick;

        public event EventHandler NewElementEdded;

        private IElementButton _selectedButton;

        public MainForm()
        {
            InitializeComponent();

            this.numericUpDown1.Maximum = this._maximum;

            this.numericUpDown1.Increment = this._step;

            this.numericUpDown1.Value = this._defaultValue;

            this.Load += new EventHandler(this.MainForm_Load);

            this.numericUpDown1.ValueChanged += this.NumericUpDown1_ValueChanged;

            this.buttonStop.Click += ButtonStop_Click;

            this.buttonStart.Click += ButtonStart_Click;
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            this.InvoceAction(new Action(() =>
            {
                (sender as Button).Enabled = false;

                buttonStop.Enabled = true;

                var buttons = this.Controls.OfType<IElementButton>()
                    .OrderBy(bt => (bt as Button).TabIndex);

                foreach (var btn in buttons)
                {
                    if (btn == this._selectedButton) continue;

                    (btn as Button).Enabled = false;
                }

                if (this.StartButtonClick != null)
                {
                    this.StartButtonClick.Invoke(this._selectedButton, EventArgs.Empty);
                }
            }));
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            this.InvoceAction(new Action(() =>
            {
                (sender as Button).Enabled = false;

                buttonStart.Enabled = true;

                var buttons = this.Controls.OfType<IElementButton>()
                    .OrderBy(bt => (bt as Button).TabIndex);

                foreach (var btn in buttons)
                {
                    if (string.IsNullOrWhiteSpace((btn as Button).Text)) continue;

                    (btn as Button).Enabled = true;
                }
            }));

            if (this.StopButtonClick != null)
            {
                this.StopButtonClick.Invoke(null, EventArgs.Empty);
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

        public void SetLoaded()
        {
            this.InvoceAction(new Action(() =>
            {
                this.buttonStart.Enabled = true;

                this.buttonStop.Enabled = false;

                var buttons = this.Controls.OfType<IElementButton>()
                    .OrderBy(bt => (bt as Button).TabIndex);

                foreach (var btn in buttons)
                {
                    if (string.IsNullOrWhiteSpace((btn as Button).Text)) continue;

                    (btn as Button).Enabled = true;
                }
            }));
        }

        public void UpdateSelectedStatuses(Element element, string statusText)
        {
            this.InvoceAction(new Action(() =>
            {
                this.buttonStart.Enabled = true;

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
