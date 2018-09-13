namespace StolotoParser_v2
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.fileDataCurrrent = new System.Windows.Forms.Label();
            this.fileDataAll = new System.Windows.Forms.Label();
            this.labelLastDraw = new System.Windows.Forms.Label();
            this.getLastDraw = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.elementButton10 = new StolotoParser_v2.UserControls.ElementButton();
            this.elementButton11 = new StolotoParser_v2.UserControls.ElementButton();
            this.elementButton12 = new StolotoParser_v2.UserControls.ElementButton();
            this.elementButton7 = new StolotoParser_v2.UserControls.ElementButton();
            this.elementButton8 = new StolotoParser_v2.UserControls.ElementButton();
            this.elementButton9 = new StolotoParser_v2.UserControls.ElementButton();
            this.elementButton4 = new StolotoParser_v2.UserControls.ElementButton();
            this.elementButton5 = new StolotoParser_v2.UserControls.ElementButton();
            this.elementButton6 = new StolotoParser_v2.UserControls.ElementButton();
            this.elementButton3 = new StolotoParser_v2.UserControls.ElementButton();
            this.elementButton2 = new StolotoParser_v2.UserControls.ElementButton();
            this.elementButton1 = new StolotoParser_v2.UserControls.ElementButton();
            this.buttonTest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(512, 302);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "(от 0 д о 0)";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown1.Location = new System.Drawing.Point(483, 318);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(117, 20);
            this.numericUpDown1.TabIndex = 14;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(708, 20);
            this.textBox1.TabIndex = 0;
            // 
            // progressBar2
            // 
            this.progressBar2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar2.Location = new System.Drawing.Point(12, 38);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(465, 20);
            this.progressBar2.TabIndex = 18;
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 96);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(465, 199);
            this.listBox1.TabIndex = 19;
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Enabled = false;
            this.buttonStart.Location = new System.Drawing.Point(12, 305);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(173, 35);
            this.buttonStart.TabIndex = 20;
            this.buttonStart.Text = "Старт";
            this.buttonStart.UseVisualStyleBackColor = true;
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(194, 305);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(140, 35);
            this.buttonStop.TabIndex = 21;
            this.buttonStop.Text = "Стоп";
            this.buttonStop.UseVisualStyleBackColor = true;
            // 
            // fileDataCurrrent
            // 
            this.fileDataCurrrent.AutoSize = true;
            this.fileDataCurrrent.Location = new System.Drawing.Point(93, 71);
            this.fileDataCurrrent.Name = "fileDataCurrrent";
            this.fileDataCurrrent.Size = new System.Drawing.Size(0, 13);
            this.fileDataCurrrent.TabIndex = 22;
            // 
            // fileDataAll
            // 
            this.fileDataAll.AutoSize = true;
            this.fileDataAll.Location = new System.Drawing.Point(191, 71);
            this.fileDataAll.Name = "fileDataAll";
            this.fileDataAll.Size = new System.Drawing.Size(0, 13);
            this.fileDataAll.TabIndex = 23;
            // 
            // labelLastDraw
            // 
            this.labelLastDraw.AutoSize = true;
            this.labelLastDraw.Location = new System.Drawing.Point(411, 71);
            this.labelLastDraw.Name = "labelLastDraw";
            this.labelLastDraw.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelLastDraw.Size = new System.Drawing.Size(0, 13);
            this.labelLastDraw.TabIndex = 27;
            this.labelLastDraw.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // getLastDraw
            // 
            this.getLastDraw.Enabled = false;
            this.getLastDraw.Location = new System.Drawing.Point(273, 64);
            this.getLastDraw.Name = "getLastDraw";
            this.getLastDraw.Size = new System.Drawing.Size(132, 26);
            this.getLastDraw.TabIndex = 28;
            this.getLastDraw.Text = "Последний тираж";
            this.getLastDraw.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(483, 200);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(157, 17);
            this.checkBox1.TabIndex = 29;
            this.checkBox1.Text = "Дополнить текущий фаил";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Enabled = false;
            this.checkBox2.Location = new System.Drawing.Point(483, 223);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(147, 17);
            this.checkBox2.TabIndex = 30;
            this.checkBox2.Text = "Дополнить общий фаил";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Текущий фаил:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "All:";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(483, 246);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(198, 17);
            this.checkBox3.TabIndex = 33;
            this.checkBox3.Text = "Парсить дополнительные номера";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // elementButton10
            // 
            this.elementButton10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.elementButton10.Element = null;
            this.elementButton10.Location = new System.Drawing.Point(645, 161);
            this.elementButton10.Name = "elementButton10";
            this.elementButton10.Size = new System.Drawing.Size(75, 35);
            this.elementButton10.TabIndex = 11;
            this.elementButton10.Text = " ";
            this.elementButton10.UseVisualStyleBackColor = true;
            // 
            // elementButton11
            // 
            this.elementButton11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.elementButton11.Element = null;
            this.elementButton11.Location = new System.Drawing.Point(564, 161);
            this.elementButton11.Name = "elementButton11";
            this.elementButton11.Size = new System.Drawing.Size(75, 35);
            this.elementButton11.TabIndex = 10;
            this.elementButton11.Text = " ";
            this.elementButton11.UseVisualStyleBackColor = true;
            // 
            // elementButton12
            // 
            this.elementButton12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.elementButton12.Element = null;
            this.elementButton12.Location = new System.Drawing.Point(483, 161);
            this.elementButton12.Name = "elementButton12";
            this.elementButton12.Size = new System.Drawing.Size(75, 35);
            this.elementButton12.TabIndex = 9;
            this.elementButton12.Text = " ";
            this.elementButton12.UseVisualStyleBackColor = true;
            // 
            // elementButton7
            // 
            this.elementButton7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.elementButton7.Element = null;
            this.elementButton7.Location = new System.Drawing.Point(645, 120);
            this.elementButton7.Name = "elementButton7";
            this.elementButton7.Size = new System.Drawing.Size(75, 35);
            this.elementButton7.TabIndex = 8;
            this.elementButton7.Text = " ";
            this.elementButton7.UseVisualStyleBackColor = true;
            // 
            // elementButton8
            // 
            this.elementButton8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.elementButton8.Element = null;
            this.elementButton8.Location = new System.Drawing.Point(564, 120);
            this.elementButton8.Name = "elementButton8";
            this.elementButton8.Size = new System.Drawing.Size(75, 35);
            this.elementButton8.TabIndex = 7;
            this.elementButton8.Text = " ";
            this.elementButton8.UseVisualStyleBackColor = true;
            // 
            // elementButton9
            // 
            this.elementButton9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.elementButton9.Element = null;
            this.elementButton9.Location = new System.Drawing.Point(483, 120);
            this.elementButton9.Name = "elementButton9";
            this.elementButton9.Size = new System.Drawing.Size(75, 35);
            this.elementButton9.TabIndex = 6;
            this.elementButton9.Text = " ";
            this.elementButton9.UseVisualStyleBackColor = true;
            // 
            // elementButton4
            // 
            this.elementButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.elementButton4.Element = null;
            this.elementButton4.Location = new System.Drawing.Point(645, 79);
            this.elementButton4.Name = "elementButton4";
            this.elementButton4.Size = new System.Drawing.Size(75, 35);
            this.elementButton4.TabIndex = 5;
            this.elementButton4.Text = " ";
            this.elementButton4.UseVisualStyleBackColor = true;
            // 
            // elementButton5
            // 
            this.elementButton5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.elementButton5.Element = null;
            this.elementButton5.Location = new System.Drawing.Point(564, 79);
            this.elementButton5.Name = "elementButton5";
            this.elementButton5.Size = new System.Drawing.Size(75, 35);
            this.elementButton5.TabIndex = 4;
            this.elementButton5.Text = " ";
            this.elementButton5.UseVisualStyleBackColor = true;
            // 
            // elementButton6
            // 
            this.elementButton6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.elementButton6.Element = null;
            this.elementButton6.Location = new System.Drawing.Point(483, 79);
            this.elementButton6.Name = "elementButton6";
            this.elementButton6.Size = new System.Drawing.Size(75, 35);
            this.elementButton6.TabIndex = 3;
            this.elementButton6.Text = " ";
            this.elementButton6.UseVisualStyleBackColor = true;
            // 
            // elementButton3
            // 
            this.elementButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.elementButton3.Element = null;
            this.elementButton3.Location = new System.Drawing.Point(645, 38);
            this.elementButton3.Name = "elementButton3";
            this.elementButton3.Size = new System.Drawing.Size(75, 35);
            this.elementButton3.TabIndex = 2;
            this.elementButton3.Text = " ";
            this.elementButton3.UseVisualStyleBackColor = true;
            // 
            // elementButton2
            // 
            this.elementButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.elementButton2.Element = null;
            this.elementButton2.Location = new System.Drawing.Point(564, 38);
            this.elementButton2.Name = "elementButton2";
            this.elementButton2.Size = new System.Drawing.Size(75, 35);
            this.elementButton2.TabIndex = 1;
            this.elementButton2.Text = " ";
            this.elementButton2.UseVisualStyleBackColor = true;
            // 
            // elementButton1
            // 
            this.elementButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.elementButton1.Element = null;
            this.elementButton1.Location = new System.Drawing.Point(483, 38);
            this.elementButton1.Name = "elementButton1";
            this.elementButton1.Size = new System.Drawing.Size(75, 35);
            this.elementButton1.TabIndex = 0;
            this.elementButton1.Text = " ";
            this.elementButton1.UseVisualStyleBackColor = true;
            // 
            // buttonTest
            // 
            this.buttonTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTest.Enabled = false;
            this.buttonTest.Location = new System.Drawing.Point(340, 305);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(137, 35);
            this.buttonTest.TabIndex = 34;
            this.buttonTest.Text = "Тест";
            this.buttonTest.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 346);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.getLastDraw);
            this.Controls.Add(this.labelLastDraw);
            this.Controls.Add(this.fileDataAll);
            this.Controls.Add(this.fileDataCurrrent);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.elementButton10);
            this.Controls.Add(this.elementButton11);
            this.Controls.Add(this.elementButton12);
            this.Controls.Add(this.elementButton7);
            this.Controls.Add(this.elementButton8);
            this.Controls.Add(this.elementButton9);
            this.Controls.Add(this.elementButton4);
            this.Controls.Add(this.elementButton5);
            this.Controls.Add(this.elementButton6);
            this.Controls.Add(this.elementButton3);
            this.Controls.Add(this.elementButton2);
            this.Controls.Add(this.elementButton1);
            this.MinimumSize = new System.Drawing.Size(600, 310);
            this.Name = "MainForm";
            this.Text = "StolotoParser";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.ElementButton elementButton1;
        private UserControls.ElementButton elementButton2;
        private UserControls.ElementButton elementButton3;
        private UserControls.ElementButton elementButton4;
        private UserControls.ElementButton elementButton5;
        private UserControls.ElementButton elementButton6;
        private UserControls.ElementButton elementButton7;
        private UserControls.ElementButton elementButton8;
        private UserControls.ElementButton elementButton9;
        private UserControls.ElementButton elementButton10;
        private UserControls.ElementButton elementButton11;
        private UserControls.ElementButton elementButton12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Label fileDataCurrrent;
        private System.Windows.Forms.Label fileDataAll;
        private System.Windows.Forms.Label labelLastDraw;
        private System.Windows.Forms.Button getLastDraw;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button buttonTest;
    }
}

