namespace StolotoParser_v2.UserControls
{
    partial class LotaryInfoControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnLotary = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripLoaded = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.statusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLotary
            // 
            this.btnLotary.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnLotary.Location = new System.Drawing.Point(0, 0);
            this.btnLotary.Name = "btnLotary";
            this.btnLotary.Size = new System.Drawing.Size(200, 24);
            this.btnLotary.TabIndex = 4;
            this.btnLotary.Text = "lotary";
            this.btnLotary.UseVisualStyleBackColor = true;
            // 
            // toolStripLoaded
            // 
            this.toolStripLoaded.AutoSize = false;
            this.toolStripLoaded.AutoToolTip = true;
            this.toolStripLoaded.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripLoaded.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripLoaded.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.toolStripLoaded.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripLoaded.Name = "toolStripLoaded";
            this.toolStripLoaded.Size = new System.Drawing.Size(75, 17);
            this.toolStripLoaded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripLoaded.ToolTipText = "Loaded";
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLoaded,
            this.toolStripTotal,
            this.toolStripProgressBar});
            this.statusBar.Location = new System.Drawing.Point(0, 24);
            this.statusBar.Name = "statusBar";
            this.statusBar.ShowItemToolTips = true;
            this.statusBar.Size = new System.Drawing.Size(298, 22);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 3;
            this.statusBar.Text = "statusStrip";
            // 
            // toolStripTotal
            // 
            this.toolStripTotal.AutoSize = false;
            this.toolStripTotal.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripTotal.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.toolStripTotal.Name = "toolStripTotal";
            this.toolStripTotal.Size = new System.Drawing.Size(75, 17);
            this.toolStripTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripTotal.ToolTipText = "Total";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(145, 16);
            // 
            // numericUpDown
            // 
            this.numericUpDown.Location = new System.Drawing.Point(206, 1);
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(94, 20);
            this.numericUpDown.TabIndex = 5;
            // 
            // LotaryInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.numericUpDown);
            this.Controls.Add(this.btnLotary);
            this.Controls.Add(this.statusBar);
            this.Name = "LotaryInfoControl";
            this.Size = new System.Drawing.Size(298, 46);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnLotary;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLoaded;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripTotal;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.NumericUpDown numericUpDown;
    }
}
