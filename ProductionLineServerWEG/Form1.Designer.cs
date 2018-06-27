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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Terminal = new System.Windows.Forms.RichTextBox();
            this.BtnPreLoadProcess = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabTal = new System.Windows.Forms.TabPage();
            this.BtnTesteProcessManager = new System.Windows.Forms.Button();
            this.tabControle = new System.Windows.Forms.TabPage();
            this.BtnInsertEinE = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.listBox6 = new System.Windows.Forms.ListBox();
            this.BtnDesligarE = new System.Windows.Forms.Button();
            this.BtnLigarE = new System.Windows.Forms.Button();
            this.listBox5 = new System.Windows.Forms.ListBox();
            this.BtnInsertPiece = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BtnStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.BtnPreLoadEsteiras = new System.Windows.Forms.Button();
            this.tabEsteiras = new System.Windows.Forms.TabPage();
            this.NomeE = new System.Windows.Forms.TextBox();
            this.InLimitE = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BtnCriarEsteira = new System.Windows.Forms.Button();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.listBox4 = new System.Windows.Forms.ListBox();
            this.BtnInserirPinE = new System.Windows.Forms.Button();
            this.tabProcesso = new System.Windows.Forms.TabPage();
            this.BtnCriaProcesso = new System.Windows.Forms.Button();
            this.nomeP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DescP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RuntimeP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnInsertProcesso = new System.Windows.Forms.Button();
            this.BtnListProcesso = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Terminal2 = new System.Windows.Forms.RichTextBox();
            this.BtnListEsterias = new System.Windows.Forms.Button();
            this.tabControl2.SuspendLayout();
            this.tabTal.SuspendLayout();
            this.tabControle.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabEsteiras.SuspendLayout();
            this.tabProcesso.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Terminal
            // 
            this.Terminal.BackColor = System.Drawing.SystemColors.InfoText;
            this.Terminal.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.Terminal.ForeColor = System.Drawing.SystemColors.Info;
            this.Terminal.Location = new System.Drawing.Point(566, 28);
            this.Terminal.Name = "Terminal";
            this.Terminal.ReadOnly = true;
            this.Terminal.Size = new System.Drawing.Size(497, 359);
            this.Terminal.TabIndex = 1;
            this.Terminal.Text = "";
            this.Terminal.UseWaitCursor = true;
            this.Terminal.TextChanged += new System.EventHandler(this.Terminal_TextChanged);
            // 
            // BtnPreLoadProcess
            // 
            this.BtnPreLoadProcess.Location = new System.Drawing.Point(53, 6);
            this.BtnPreLoadProcess.Name = "BtnPreLoadProcess";
            this.BtnPreLoadProcess.Size = new System.Drawing.Size(158, 23);
            this.BtnPreLoadProcess.TabIndex = 26;
            this.BtnPreLoadProcess.Text = "Pré Load Processos";
            this.BtnPreLoadProcess.UseVisualStyleBackColor = true;
            this.BtnPreLoadProcess.Click += new System.EventHandler(this.BtnPreLoadProcess_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabTal);
            this.tabControl2.Controls.Add(this.tabControle);
            this.tabControl2.Location = new System.Drawing.Point(12, 28);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(271, 463);
            this.tabControl2.TabIndex = 14;
            // 
            // tabTal
            // 
            this.tabTal.Controls.Add(this.BtnPreLoadEsteiras);
            this.tabTal.Controls.Add(this.BtnTesteProcessManager);
            this.tabTal.Controls.Add(this.BtnPreLoadProcess);
            this.tabTal.Location = new System.Drawing.Point(4, 22);
            this.tabTal.Name = "tabTal";
            this.tabTal.Padding = new System.Windows.Forms.Padding(3);
            this.tabTal.Size = new System.Drawing.Size(263, 437);
            this.tabTal.TabIndex = 0;
            this.tabTal.Text = "TabTal";
            this.tabTal.UseVisualStyleBackColor = true;
            // 
            // BtnTesteProcessManager
            // 
            this.BtnTesteProcessManager.Location = new System.Drawing.Point(53, 408);
            this.BtnTesteProcessManager.Name = "BtnTesteProcessManager";
            this.BtnTesteProcessManager.Size = new System.Drawing.Size(158, 23);
            this.BtnTesteProcessManager.TabIndex = 27;
            this.BtnTesteProcessManager.Text = "Teste ProcessManager (P a)";
            this.BtnTesteProcessManager.UseVisualStyleBackColor = true;
            this.BtnTesteProcessManager.Click += new System.EventHandler(this.BtnTesteProcessManager_Click);
            // 
            // tabControle
            // 
            this.tabControle.Controls.Add(this.BtnInsertEinE);
            this.tabControle.Controls.Add(this.label9);
            this.tabControle.Controls.Add(this.listBox6);
            this.tabControle.Controls.Add(this.BtnDesligarE);
            this.tabControle.Controls.Add(this.BtnLigarE);
            this.tabControle.Controls.Add(this.listBox5);
            this.tabControle.Controls.Add(this.BtnInsertPiece);
            this.tabControle.Location = new System.Drawing.Point(4, 22);
            this.tabControle.Name = "tabControle";
            this.tabControle.Padding = new System.Windows.Forms.Padding(3);
            this.tabControle.Size = new System.Drawing.Size(263, 437);
            this.tabControle.TabIndex = 1;
            this.tabControle.Text = "Controle";
            this.tabControle.UseVisualStyleBackColor = true;
            // 
            // BtnInsertEinE
            // 
            this.BtnInsertEinE.Location = new System.Drawing.Point(53, 255);
            this.BtnInsertEinE.Name = "BtnInsertEinE";
            this.BtnInsertEinE.Size = new System.Drawing.Size(158, 23);
            this.BtnInsertEinE.TabIndex = 29;
            this.BtnInsertEinE.Text = "Insert";
            this.BtnInsertEinE.UseVisualStyleBackColor = true;
            this.BtnInsertEinE.Click += new System.EventHandler(this.BtnInsertEinE_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(123, 150);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "in";
            // 
            // listBox6
            // 
            this.listBox6.FormattingEnabled = true;
            this.listBox6.Location = new System.Drawing.Point(53, 167);
            this.listBox6.Name = "listBox6";
            this.listBox6.Size = new System.Drawing.Size(158, 82);
            this.listBox6.TabIndex = 28;
            // 
            // BtnDesligarE
            // 
            this.BtnDesligarE.Location = new System.Drawing.Point(135, 123);
            this.BtnDesligarE.Name = "BtnDesligarE";
            this.BtnDesligarE.Size = new System.Drawing.Size(76, 23);
            this.BtnDesligarE.TabIndex = 27;
            this.BtnDesligarE.Text = "Desligar E";
            this.BtnDesligarE.UseVisualStyleBackColor = true;
            this.BtnDesligarE.Click += new System.EventHandler(this.BtnDesligarE_Click);
            // 
            // BtnLigarE
            // 
            this.BtnLigarE.Location = new System.Drawing.Point(53, 123);
            this.BtnLigarE.Name = "BtnLigarE";
            this.BtnLigarE.Size = new System.Drawing.Size(76, 23);
            this.BtnLigarE.TabIndex = 26;
            this.BtnLigarE.Text = "Ligar E";
            this.BtnLigarE.UseVisualStyleBackColor = true;
            this.BtnLigarE.Click += new System.EventHandler(this.BtnLigarE_Click);
            // 
            // listBox5
            // 
            this.listBox5.FormattingEnabled = true;
            this.listBox5.Location = new System.Drawing.Point(53, 6);
            this.listBox5.Name = "listBox5";
            this.listBox5.Size = new System.Drawing.Size(158, 82);
            this.listBox5.TabIndex = 25;
            // 
            // BtnInsertPiece
            // 
            this.BtnInsertPiece.Location = new System.Drawing.Point(53, 94);
            this.BtnInsertPiece.Name = "BtnInsertPiece";
            this.BtnInsertPiece.Size = new System.Drawing.Size(158, 23);
            this.BtnInsertPiece.TabIndex = 25;
            this.BtnInsertPiece.Text = "Inserir Peça";
            this.BtnInsertPiece.UseVisualStyleBackColor = true;
            this.BtnInsertPiece.Click += new System.EventHandler(this.BtnInsertPiece_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnStart,
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this.BtnStop,
            this.toolStripLabel2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1075, 25);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BtnStart
            // 
            this.BtnStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnStart.Image = ((System.Drawing.Image)(resources.GetObject("BtnStart.Image")));
            this.BtnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(23, 22);
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(91, 22);
            this.toolStripLabel1.Text = "Start Simulation";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // BtnStop
            // 
            this.BtnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnStop.Image = ((System.Drawing.Image)(resources.GetObject("BtnStop.Image")));
            this.BtnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(23, 22);
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(91, 22);
            this.toolStripLabel2.Text = "Stop Simulation";
            // 
            // BtnPreLoadEsteiras
            // 
            this.BtnPreLoadEsteiras.Location = new System.Drawing.Point(53, 35);
            this.BtnPreLoadEsteiras.Name = "BtnPreLoadEsteiras";
            this.BtnPreLoadEsteiras.Size = new System.Drawing.Size(158, 23);
            this.BtnPreLoadEsteiras.TabIndex = 28;
            this.BtnPreLoadEsteiras.Text = "Pré Load Esteiras";
            this.BtnPreLoadEsteiras.UseVisualStyleBackColor = true;
            this.BtnPreLoadEsteiras.Click += new System.EventHandler(this.BtnPreLoadEsteiras_Click);
            // 
            // tabEsteiras
            // 
            this.tabEsteiras.Controls.Add(this.BtnListEsterias);
            this.tabEsteiras.Controls.Add(this.BtnInserirPinE);
            this.tabEsteiras.Controls.Add(this.listBox4);
            this.tabEsteiras.Controls.Add(this.label8);
            this.tabEsteiras.Controls.Add(this.label7);
            this.tabEsteiras.Controls.Add(this.listBox3);
            this.tabEsteiras.Controls.Add(this.BtnCriarEsteira);
            this.tabEsteiras.Controls.Add(this.label6);
            this.tabEsteiras.Controls.Add(this.label5);
            this.tabEsteiras.Controls.Add(this.InLimitE);
            this.tabEsteiras.Controls.Add(this.NomeE);
            this.tabEsteiras.Location = new System.Drawing.Point(4, 22);
            this.tabEsteiras.Name = "tabEsteiras";
            this.tabEsteiras.Padding = new System.Windows.Forms.Padding(3);
            this.tabEsteiras.Size = new System.Drawing.Size(263, 437);
            this.tabEsteiras.TabIndex = 1;
            this.tabEsteiras.Text = "Esteiras";
            this.tabEsteiras.UseVisualStyleBackColor = true;
            // 
            // NomeE
            // 
            this.NomeE.Location = new System.Drawing.Point(111, 6);
            this.NomeE.Name = "NomeE";
            this.NomeE.Size = new System.Drawing.Size(100, 20);
            this.NomeE.TabIndex = 1;
            // 
            // InLimitE
            // 
            this.InLimitE.Location = new System.Drawing.Point(111, 32);
            this.InLimitE.Name = "InLimitE";
            this.InLimitE.Size = new System.Drawing.Size(100, 20);
            this.InLimitE.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Nome";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Limite de Entrada";
            // 
            // BtnCriarEsteira
            // 
            this.BtnCriarEsteira.Location = new System.Drawing.Point(53, 58);
            this.BtnCriarEsteira.Name = "BtnCriarEsteira";
            this.BtnCriarEsteira.Size = new System.Drawing.Size(158, 23);
            this.BtnCriarEsteira.TabIndex = 18;
            this.BtnCriarEsteira.Text = "Criar Esteira";
            this.BtnCriarEsteira.UseVisualStyleBackColor = true;
            this.BtnCriarEsteira.Click += new System.EventHandler(this.BtnCriarEsteira_Click);
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(53, 123);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(158, 82);
            this.listBox3.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(50, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(165, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Inserir Processo mestre na esteira";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(125, 209);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "in";
            // 
            // listBox4
            // 
            this.listBox4.FormattingEnabled = true;
            this.listBox4.Location = new System.Drawing.Point(53, 226);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(158, 82);
            this.listBox4.TabIndex = 24;
            // 
            // BtnInserirPinE
            // 
            this.BtnInserirPinE.Location = new System.Drawing.Point(53, 314);
            this.BtnInserirPinE.Name = "BtnInserirPinE";
            this.BtnInserirPinE.Size = new System.Drawing.Size(158, 23);
            this.BtnInserirPinE.TabIndex = 25;
            this.BtnInserirPinE.Text = "Inserir";
            this.BtnInserirPinE.UseVisualStyleBackColor = true;
            this.BtnInserirPinE.Click += new System.EventHandler(this.BtnInserirPinE_Click);
            // 
            // tabProcesso
            // 
            this.tabProcesso.Controls.Add(this.BtnListProcesso);
            this.tabProcesso.Controls.Add(this.BtnInsertProcesso);
            this.tabProcesso.Controls.Add(this.label4);
            this.tabProcesso.Controls.Add(this.listBox2);
            this.tabProcesso.Controls.Add(this.listBox1);
            this.tabProcesso.Controls.Add(this.label3);
            this.tabProcesso.Controls.Add(this.RuntimeP);
            this.tabProcesso.Controls.Add(this.DescP);
            this.tabProcesso.Controls.Add(this.nomeP);
            this.tabProcesso.Controls.Add(this.label2);
            this.tabProcesso.Controls.Add(this.label1);
            this.tabProcesso.Controls.Add(this.BtnCriaProcesso);
            this.tabProcesso.Location = new System.Drawing.Point(4, 22);
            this.tabProcesso.Name = "tabProcesso";
            this.tabProcesso.Padding = new System.Windows.Forms.Padding(3);
            this.tabProcesso.Size = new System.Drawing.Size(263, 437);
            this.tabProcesso.TabIndex = 0;
            this.tabProcesso.Text = "Processo";
            this.tabProcesso.UseVisualStyleBackColor = true;
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
            // nomeP
            // 
            this.nomeP.Location = new System.Drawing.Point(111, 6);
            this.nomeP.Name = "nomeP";
            this.nomeP.Size = new System.Drawing.Size(100, 20);
            this.nomeP.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Nome";
            // 
            // DescP
            // 
            this.DescP.Location = new System.Drawing.Point(111, 32);
            this.DescP.Name = "DescP";
            this.DescP.Size = new System.Drawing.Size(100, 20);
            this.DescP.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Descrição";
            // 
            // RuntimeP
            // 
            this.RuntimeP.Location = new System.Drawing.Point(111, 58);
            this.RuntimeP.Name = "RuntimeP";
            this.RuntimeP.Size = new System.Drawing.Size(100, 20);
            this.RuntimeP.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Runtime";
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
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(53, 233);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(158, 82);
            this.listBox2.TabIndex = 21;
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabProcesso);
            this.tabControl1.Controls.Add(this.tabEsteiras);
            this.tabControl1.Location = new System.Drawing.Point(289, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(271, 463);
            this.tabControl1.TabIndex = 13;
            // 
            // Terminal2
            // 
            this.Terminal2.BackColor = System.Drawing.SystemColors.InfoText;
            this.Terminal2.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.Terminal2.ForeColor = System.Drawing.SystemColors.Info;
            this.Terminal2.Location = new System.Drawing.Point(566, 393);
            this.Terminal2.Name = "Terminal2";
            this.Terminal2.ReadOnly = true;
            this.Terminal2.Size = new System.Drawing.Size(497, 98);
            this.Terminal2.TabIndex = 16;
            this.Terminal2.Text = "";
            this.Terminal2.UseWaitCursor = true;
            this.Terminal2.TextChanged += new System.EventHandler(this.Terminal2_TextChanged);
            // 
            // BtnListEsterias
            // 
            this.BtnListEsterias.Location = new System.Drawing.Point(53, 343);
            this.BtnListEsterias.Name = "BtnListEsterias";
            this.BtnListEsterias.Size = new System.Drawing.Size(158, 23);
            this.BtnListEsterias.TabIndex = 26;
            this.BtnListEsterias.Text = "List";
            this.BtnListEsterias.UseVisualStyleBackColor = true;
            this.BtnListEsterias.Click += new System.EventHandler(this.BtnListEsterias_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 503);
            this.Controls.Add(this.Terminal2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Terminal);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl2.ResumeLayout(false);
            this.tabTal.ResumeLayout(false);
            this.tabControle.ResumeLayout(false);
            this.tabControle.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabEsteiras.ResumeLayout(false);
            this.tabEsteiras.PerformLayout();
            this.tabProcesso.ResumeLayout(false);
            this.tabProcesso.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox Terminal;
        private System.Windows.Forms.Button BtnPreLoadProcess;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabTal;
        private System.Windows.Forms.TabPage tabControle;
        private System.Windows.Forms.ListBox listBox5;
        private System.Windows.Forms.Button BtnInsertPiece;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton BtnStop;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton BtnStart;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button BtnDesligarE;
        private System.Windows.Forms.Button BtnLigarE;
        private System.Windows.Forms.Button BtnTesteProcessManager;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListBox listBox6;
        private System.Windows.Forms.Button BtnInsertEinE;
        private System.Windows.Forms.Button BtnPreLoadEsteiras;
        private System.Windows.Forms.TabPage tabEsteiras;
        private System.Windows.Forms.Button BtnInserirPinE;
        private System.Windows.Forms.ListBox listBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.Button BtnCriarEsteira;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox InLimitE;
        private System.Windows.Forms.TextBox NomeE;
        private System.Windows.Forms.TabPage tabProcesso;
        private System.Windows.Forms.Button BtnListProcesso;
        private System.Windows.Forms.Button BtnInsertProcesso;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox RuntimeP;
        private System.Windows.Forms.TextBox DescP;
        private System.Windows.Forms.TextBox nomeP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnCriaProcesso;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.RichTextBox Terminal2;
        private System.Windows.Forms.Button BtnListEsterias;
    }
}