namespace ProductionLineServerWEG
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Terminal = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.BtnListProcesso = new System.Windows.Forms.Button();
            this.BtnInsertProcesso = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.RuntimeP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DescP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nomeP = new System.Windows.Forms.TextBox();
            this.BtnCriaProcesso = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Terminal
            // 
            this.Terminal.BackColor = System.Drawing.SystemColors.InfoText;
            this.Terminal.ForeColor = System.Drawing.SystemColors.Info;
            this.Terminal.Location = new System.Drawing.Point(551, 12);
            this.Terminal.Name = "Terminal";
            this.Terminal.Size = new System.Drawing.Size(512, 479);
            this.Terminal.TabIndex = 1;
            this.Terminal.Text = "";
            this.Terminal.TextChanged += new System.EventHandler(this.Terminal_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(274, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(271, 479);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.BtnListProcesso);
            this.tabPage1.Controls.Add(this.BtnInsertProcesso);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.listBox2);
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.RuntimeP);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.DescP);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.nomeP);
            this.tabPage1.Controls.Add(this.BtnCriaProcesso);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(263, 453);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Processo";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // BtnListProcesso
            // 
            this.BtnListProcesso.Location = new System.Drawing.Point(53, 350);
            this.BtnListProcesso.Name = "BtnListProcesso";
            this.BtnListProcesso.Size = new System.Drawing.Size(158, 23);
            this.BtnListProcesso.TabIndex = 24;
            this.BtnListProcesso.Text = "List";
            this.BtnListProcesso.UseVisualStyleBackColor = true;
            this.BtnListProcesso.Click += new System.EventHandler(this.BtnListProcesso_Click);
            // 
            // BtnInsertProcesso
            // 
            this.BtnInsertProcesso.Location = new System.Drawing.Point(53, 321);
            this.BtnInsertProcesso.Name = "BtnInsertProcesso";
            this.BtnInsertProcesso.Size = new System.Drawing.Size(158, 23);
            this.BtnInsertProcesso.TabIndex = 23;
            this.BtnInsertProcesso.Text = "Insert";
            this.BtnInsertProcesso.UseVisualStyleBackColor = true;
            this.BtnInsertProcesso.Click += new System.EventHandler(this.BtnInsertProcesso_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(124, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "in";
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(53, 233);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(158, 82);
            this.listBox2.TabIndex = 21;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(53, 130);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(158, 82);
            this.listBox1.TabIndex = 20;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Runtime";
            // 
            // RuntimeP
            // 
            this.RuntimeP.Location = new System.Drawing.Point(111, 58);
            this.RuntimeP.Name = "RuntimeP";
            this.RuntimeP.Size = new System.Drawing.Size(100, 20);
            this.RuntimeP.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Descrição";
            // 
            // DescP
            // 
            this.DescP.Location = new System.Drawing.Point(111, 32);
            this.DescP.Name = "DescP";
            this.DescP.Size = new System.Drawing.Size(100, 20);
            this.DescP.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Nome";
            // 
            // nomeP
            // 
            this.nomeP.Location = new System.Drawing.Point(111, 6);
            this.nomeP.Name = "nomeP";
            this.nomeP.Size = new System.Drawing.Size(100, 20);
            this.nomeP.TabIndex = 14;
            // 
            // BtnCriaProcesso
            // 
            this.BtnCriaProcesso.Location = new System.Drawing.Point(53, 84);
            this.BtnCriaProcesso.Name = "BtnCriaProcesso";
            this.BtnCriaProcesso.Size = new System.Drawing.Size(158, 23);
            this.BtnCriaProcesso.TabIndex = 13;
            this.BtnCriaProcesso.Text = "Criar Processo";
            this.BtnCriaProcesso.UseVisualStyleBackColor = true;
            this.BtnCriaProcesso.Click += new System.EventHandler(this.BtnCriaProcesso_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(263, 453);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Esteiras";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(111, 32);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(111, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 503);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Terminal);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox Terminal;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button BtnListProcesso;
        private System.Windows.Forms.Button BtnInsertProcesso;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox RuntimeP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DescP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nomeP;
        private System.Windows.Forms.Button BtnCriaProcesso;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
    }
}