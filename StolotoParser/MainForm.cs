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

        event EventHandler NewElementEdded;

        void SetButtons(Element[] elements);
    }

    public partial class MainForm : Form, IMainForm
    {
        private int _step = 50;

        private int _defaultValue = 50;

        private int _maximum = 30000;

        public event EventHandler OnFormLoad;

        public event EventHandler NewElementEdded;

        public MainForm()
        {
            InitializeComponent();

            this.numericUpDown1.Maximum = this._maximum;

            this.numericUpDown1.Increment = this._step;

            this.numericUpDown1.Value = this._defaultValue;

            this.Load += new EventHandler(this.MainForm_Load);

            this.numericUpDown1.ValueChanged += this.NumericUpDown1_ValueChanged;
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

        void IMainForm.SetButtons(Element[] elements)
        {
            var buttons = this.Controls.OfType<IElementButton>().OrderBy(bt => (bt as Button).TabIndex).ToList();

            for (int index = 0; index < elements.Length; index++)
            {
                var element = elements[index];

                var button = buttons[index];

                button.Element = element;

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
    }
}
