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
        int SetLastDraw { set; }

        ParsingSettings ParsingSettings { get; }

        event EventHandler OnFormLoad;

        event EventHandler StopButtonClick;

        event EventHandler StartButtonClick;

        event EventHandler NewElementEdded;

        event EventHandler GetLastDrawClick;

        event EventHandler ButtonTestClick;

        void SetButtons(Element[] elements);

        void SetLoaded();

        void AppTextListBox(string text);

        void ClearListBox();

        void UpdateDatas(FilesData filesData);

        void UpdateProgres(int value, int max);

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

        public event EventHandler GetLastDrawClick;

        public event EventHandler ButtonTestClick;

        private IElementButton _selectedButton; 

        public MainForm()
        {
            InitializeComponent();

            this.numericUpDown1.Maximum = this._maximum;

            this.numericUpDown1.Increment = this._step;

            this.numericUpDown1.Value = this._defaultValue;

            this.progressBar2.Maximum = this._defaultValue;

            this.Load += new EventHandler(this.MainForm_Load);

            this.numericUpDown1.ValueChanged += this.NumericUpDown1_ValueChanged;

            this.buttonStop.Click += ButtonStop_Click;

            this.buttonStart.Click += ButtonStart_Click;

            this.getLastDraw.Click += GetLastDraw_Click;

            this.buttonTest.Click += ButtonTest_Click;
        }

        private void ButtonTest_Click(object sender, EventArgs e)
        {
            if (this.ButtonTestClick != null)
            {
                this.ButtonTestClick.Invoke(this._selectedButton.Element, EventArgs.Empty);
            }
        }

        private void GetLastDraw_Click(object sender, EventArgs e)
        {
            this.buttonStart.Enabled = false;

            this.buttonStop.Enabled = false;

            this.buttonTest.Enabled = false;

            var buttons = this.Controls.OfType<IElementButton>()
                .OrderBy(bt => (bt as Button).TabIndex);

            foreach (var btn in buttons)
            {
                (btn as Button).Enabled = false;
            }

            if (this.GetLastDrawClick != null)
            {
                this.GetLastDrawClick.Invoke(this._selectedButton, EventArgs.Empty);
            }
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            this.InvoceAction(new Action(() =>
            {
                this.progressBar2.Value = 0;

                (sender as Button).Enabled = false;

                buttonStop.Enabled = true;

                this.buttonTest.Enabled = false;

                var buttons = this.Controls.OfType<IElementButton>()
                    .OrderBy(bt => (bt as Button).TabIndex);

                foreach (var btn in buttons)
                {
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

                this.buttonTest.Enabled = true;

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
            var value = (int)(sender as NumericUpDown).Value;

            foreach (var btn in this.Controls.OfType<IElementButton>())
            {
                if (btn.Element == null) continue;

                this.progressBar2.Maximum = value;

                btn.Element.TotalCount = value;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (this.OnFormLoad != null)
            {
                this.OnFormLoad(sender, e);
            }
        }

        public int SetLastDraw { set { this.InvoceAction(() => { this.labelLastDraw.Text = Convert.ToString(value); }); } }

        public void SetLoaded()
        {
            this.InvoceAction(new Action(() =>
            {
                this.buttonStart.Enabled = true;

                this.buttonTest.Enabled = true;

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

        public void UpdateDatas(FilesData filesData)
        {
            this.InvoceAction(() =>
            {
                if (filesData != null)
                {
                    if (filesData.LastDrawCurrent != 0)
                    {
                        this.fileDataCurrrent.Text = Convert.ToString(filesData.LastDrawCurrent);
                    }

                    if (filesData.LastDrawFull != 0)
                    {
                        this.fileDataAll.Text = Convert.ToString(filesData.LastDrawFull);
                    }
                }
            });
        }

        public void UpdateSelectedStatuses(Element element, string statusText)
        {
            this.InvoceAction(new Action(() =>
            {
                this.buttonStart.Enabled = true;

                this.buttonTest.Enabled = true;

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

        public ParsingSettings ParsingSettings
        {
            get
            {
                return new ParsingSettings()
                {
                    AddToAll = this.checkBox2.Checked,
                    AddToCurrent = this.checkBox1.Checked,
                    ParsingExtraNumbers = this.checkBox3.Checked
                };
            }
        }

        private void Button_ElementButtonClick(object sender, EventArgs e)
        {
            this.InvoceAction(new Action(() =>
            {
                this.progressBar2.Value = 0;

                this.fileDataCurrrent.Text = string.Empty;

                this.fileDataAll.Text = string.Empty;

                this.labelLastDraw.Text = string.Empty;

                this.getLastDraw.Enabled = true;

                var btn = sender as IElementButton;

                this._selectedButton = btn;

                this.checkBox2.Enabled = !string.IsNullOrEmpty(btn.Element.FileAllName);
            }));
        }

        public void AppTextListBox(string text)
        {
            this.InvoceAction(() => listBox1.Items.Add(text));
        }

        public void ClearListBox()
        {
            this.InvoceAction(() => this.listBox1.Items.Clear());
        }

        public void UpdateProgres(int value, int max)
        {
            this.InvoceAction(() => 
            {
                this.progressBar2.Maximum = max;

                this.progressBar2.Value = value;
            });
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
