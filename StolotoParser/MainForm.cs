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
        public event EventHandler OnFormLoad;

        public event EventHandler NewElementEdded;

        public MainForm()
        {
            InitializeComponent();

            this.Load += new EventHandler(MainForm_Load);
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
            for (int index = 0; index < elements.Length; index++)
            {
                var element = elements[index];

                var lotaryInfoControl = new LotaryInfoControl()
                {
                    Element = element,
                    Location = new Point(0, index * 50),
                    Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right)))
                };

                this.Controls.Add(lotaryInfoControl);

                if (this.NewElementEdded != null)
                {
                    this.NewElementEdded.Invoke(lotaryInfoControl, EventArgs.Empty);
                }
            }
        }
    }
}
