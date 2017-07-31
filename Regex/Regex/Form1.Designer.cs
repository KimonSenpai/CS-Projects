namespace Regexp
{
    partial class Form1
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.Replace = new System.Windows.Forms.RadioButton();
            this.Search = new System.Windows.Forms.RadioButton();
            this.Match = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Reg = new System.Windows.Forms.TextBox();
            this.Str = new System.Windows.Forms.TextBox();
            this.Res = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.iCase = new System.Windows.Forms.CheckBox();
            this.What = new System.Windows.Forms.TextBox();
            this.gr1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 377);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(270, 72);
            this.button1.TabIndex = 0;
            this.button1.Text = "Старт";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gr1
            // 
            this.gr1.Controls.Add(this.What);
            this.gr1.Controls.Add(this.iCase);
            this.gr1.Controls.Add(this.Replace);
            this.gr1.Controls.Add(this.Search);
            this.gr1.Controls.Add(this.Match);
            this.gr1.Location = new System.Drawing.Point(13, 60);
            this.gr1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gr1.Name = "gr1";
            this.gr1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gr1.Size = new System.Drawing.Size(270, 90);
            this.gr1.TabIndex = 1;
            this.gr1.TabStop = false;
            this.gr1.Text = "Метод";
            // 
            // Replace
            // 
            this.Replace.AutoSize = true;
            this.Replace.Location = new System.Drawing.Point(174, 29);
            this.Replace.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Replace.Name = "Replace";
            this.Replace.Size = new System.Drawing.Size(86, 24);
            this.Replace.TabIndex = 2;
            this.Replace.Text = "Replace";
            this.Replace.UseVisualStyleBackColor = true;
            this.Replace.CheckedChanged += new System.EventHandler(this.Replace_CheckedChanged);
            // 
            // Search
            // 
            this.Search.AutoSize = true;
            this.Search.Location = new System.Drawing.Point(88, 29);
            this.Search.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(78, 24);
            this.Search.TabIndex = 1;
            this.Search.Text = "Search";
            this.Search.UseVisualStyleBackColor = true;
            // 
            // Match
            // 
            this.Match.AutoSize = true;
            this.Match.Checked = true;
            this.Match.Location = new System.Drawing.Point(9, 29);
            this.Match.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Match.Name = "Match";
            this.Match.Size = new System.Drawing.Size(71, 24);
            this.Match.TabIndex = 0;
            this.Match.TabStop = true;
            this.Match.Text = "Match";
            this.Match.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Строка:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Регулярное выражение:";
            // 
            // Reg
            // 
            this.Reg.Location = new System.Drawing.Point(22, 33);
            this.Reg.Name = "Reg";
            this.Reg.Size = new System.Drawing.Size(261, 26);
            this.Reg.TabIndex = 4;
            // 
            // Str
            // 
            this.Str.Location = new System.Drawing.Point(13, 178);
            this.Str.Multiline = true;
            this.Str.Name = "Str";
            this.Str.Size = new System.Drawing.Size(269, 80);
            this.Str.TabIndex = 5;
            // 
            // Res
            // 
            this.Res.Location = new System.Drawing.Point(13, 289);
            this.Res.Multiline = true;
            this.Res.Name = "Res";
            this.Res.Size = new System.Drawing.Size(269, 80);
            this.Res.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 266);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Результат:";
            // 
            // iCase
            // 
            this.iCase.AutoSize = true;
            this.iCase.Location = new System.Drawing.Point(9, 58);
            this.iCase.Name = "iCase";
            this.iCase.Size = new System.Drawing.Size(112, 24);
            this.iCase.TabIndex = 3;
            this.iCase.Text = "Ignore case";
            this.iCase.UseVisualStyleBackColor = true;
            // 
            // What
            // 
            this.What.Location = new System.Drawing.Point(127, 56);
            this.What.Name = "What";
            this.What.Size = new System.Drawing.Size(133, 26);
            this.What.TabIndex = 4;
            this.What.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 458);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Res);
            this.Controls.Add(this.Str);
            this.Controls.Add(this.Reg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gr1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Regex";
            this.gr1.ResumeLayout(false);
            this.gr1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox gr1;
        private System.Windows.Forms.RadioButton Match;
        private System.Windows.Forms.RadioButton Replace;
        private System.Windows.Forms.RadioButton Search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Reg;
        private System.Windows.Forms.TextBox Str;
        private System.Windows.Forms.TextBox Res;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox iCase;
        private System.Windows.Forms.TextBox What;
    }
}

