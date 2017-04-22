namespace BoxIdDb
{
    partial class frmModule
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModule));
            this.btnCancel = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.dgvInline = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpPrintDate = new System.Windows.Forms.DateTimePicker();
            this.txtProductSerial = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvLine = new System.Windows.Forms.DataGridView();
            this.dgvConfig = new System.Windows.Forms.DataGridView();
            this.dgvPassFail = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnRegisterBoxId = new System.Windows.Forms.Button();
            this.txtOkCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDeleteSelection = new System.Windows.Forms.Button();
            this.btnChangeLimit = new System.Windows.Forms.Button();
            this.txtLimit = new System.Windows.Forms.TextBox();
            this.dgvDateCode = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvDateCode2 = new System.Windows.Forms.DataGridView();
            this.txtBoxIdPrint = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbShaft = new System.Windows.Forms.ComboBox();
            this.cmbOverlay = new System.Windows.Forms.ComboBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabInline = new System.Windows.Forms.TabPage();
            this.tabOqc = new System.Windows.Forms.TabPage();
            this.dgvOqc = new System.Windows.Forms.DataGridView();
            this.btnCancelBoxid = new System.Windows.Forms.Button();
            this.btnDeleteSerial = new System.Windows.Forms.Button();
            this.btnAddSerial = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPassFail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDateCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDateCode2)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabInline.SuspendLayout();
            this.tabOqc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOqc)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(838, 211);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 22);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(630, 144);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 12);
            this.label12.TabIndex = 6;
            this.label12.Text = "User: ";
            // 
            // txtUser
            // 
            this.txtUser.Enabled = false;
            this.txtUser.Location = new System.Drawing.Point(703, 141);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(105, 19);
            this.txtUser.TabIndex = 5;
            // 
            // dgvInline
            // 
            this.dgvInline.AllowUserToAddRows = false;
            this.dgvInline.AllowUserToDeleteRows = false;
            this.dgvInline.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.dgvInline.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInline.Location = new System.Drawing.Point(4, 6);
            this.dgvInline.Name = "dgvInline";
            this.dgvInline.ReadOnly = true;
            this.dgvInline.RowTemplate.Height = 21;
            this.dgvInline.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvInline.Size = new System.Drawing.Size(962, 420);
            this.dgvInline.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Print Date: ";
            // 
            // txtBoxId
            // 
            this.txtBoxId.Enabled = false;
            this.txtBoxId.Location = new System.Drawing.Point(165, 141);
            this.txtBoxId.Name = "txtBoxId";
            this.txtBoxId.Size = new System.Drawing.Size(188, 19);
            this.txtBoxId.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Box ID: ";
            // 
            // dtpPrintDate
            // 
            this.dtpPrintDate.Enabled = false;
            this.dtpPrintDate.Location = new System.Drawing.Point(165, 176);
            this.dtpPrintDate.Name = "dtpPrintDate";
            this.dtpPrintDate.Size = new System.Drawing.Size(188, 19);
            this.dtpPrintDate.TabIndex = 12;
            // 
            // txtProductSerial
            // 
            this.txtProductSerial.Location = new System.Drawing.Point(165, 211);
            this.txtProductSerial.Name = "txtProductSerial";
            this.txtProductSerial.Size = new System.Drawing.Size(188, 19);
            this.txtProductSerial.TabIndex = 5;
            this.txtProductSerial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductSerial_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Product Serial: ";
            // 
            // dgvLine
            // 
            this.dgvLine.AllowUserToAddRows = false;
            this.dgvLine.AllowUserToDeleteRows = false;
            this.dgvLine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLine.Location = new System.Drawing.Point(109, 78);
            this.dgvLine.Name = "dgvLine";
            this.dgvLine.ReadOnly = true;
            this.dgvLine.RowTemplate.Height = 21;
            this.dgvLine.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvLine.Size = new System.Drawing.Size(350, 43);
            this.dgvLine.TabIndex = 9;
            // 
            // dgvConfig
            // 
            this.dgvConfig.AllowUserToAddRows = false;
            this.dgvConfig.AllowUserToDeleteRows = false;
            this.dgvConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConfig.Location = new System.Drawing.Point(109, 11);
            this.dgvConfig.Name = "dgvConfig";
            this.dgvConfig.ReadOnly = true;
            this.dgvConfig.RowTemplate.Height = 21;
            this.dgvConfig.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvConfig.Size = new System.Drawing.Size(350, 61);
            this.dgvConfig.TabIndex = 9;
            // 
            // dgvPassFail
            // 
            this.dgvPassFail.AllowUserToAddRows = false;
            this.dgvPassFail.AllowUserToDeleteRows = false;
            this.dgvPassFail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPassFail.Location = new System.Drawing.Point(563, 78);
            this.dgvPassFail.Name = "dgvPassFail";
            this.dgvPassFail.ReadOnly = true;
            this.dgvPassFail.RowTemplate.Height = 21;
            this.dgvPassFail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvPassFail.Size = new System.Drawing.Size(350, 43);
            this.dgvPassFail.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "By Line: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "By Model:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(495, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "By Judge:";
            // 
            // btnRegisterBoxId
            // 
            this.btnRegisterBoxId.Enabled = false;
            this.btnRegisterBoxId.Location = new System.Drawing.Point(397, 211);
            this.btnRegisterBoxId.Name = "btnRegisterBoxId";
            this.btnRegisterBoxId.Size = new System.Drawing.Size(100, 22);
            this.btnRegisterBoxId.TabIndex = 11;
            this.btnRegisterBoxId.Text = "Register Box ID";
            this.btnRegisterBoxId.UseVisualStyleBackColor = true;
            this.btnRegisterBoxId.Click += new System.EventHandler(this.btnRegisterBoxId_Click);
            // 
            // txtOkCount
            // 
            this.txtOkCount.Enabled = false;
            this.txtOkCount.Location = new System.Drawing.Point(703, 176);
            this.txtOkCount.Name = "txtOkCount";
            this.txtOkCount.Size = new System.Drawing.Size(105, 19);
            this.txtOkCount.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(630, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "OK Count: ";
            // 
            // btnDeleteSelection
            // 
            this.btnDeleteSelection.Location = new System.Drawing.Point(541, 211);
            this.btnDeleteSelection.Name = "btnDeleteSelection";
            this.btnDeleteSelection.Size = new System.Drawing.Size(100, 22);
            this.btnDeleteSelection.TabIndex = 11;
            this.btnDeleteSelection.Text = "Delete Selection";
            this.btnDeleteSelection.UseVisualStyleBackColor = true;
            this.btnDeleteSelection.Click += new System.EventHandler(this.btnDeleteSelection_Click);
            // 
            // btnChangeLimit
            // 
            this.btnChangeLimit.Location = new System.Drawing.Point(847, 176);
            this.btnChangeLimit.Name = "btnChangeLimit";
            this.btnChangeLimit.Size = new System.Drawing.Size(57, 22);
            this.btnChangeLimit.TabIndex = 11;
            this.btnChangeLimit.Text = "Limit";
            this.btnChangeLimit.UseVisualStyleBackColor = true;
            this.btnChangeLimit.Visible = false;
            this.btnChangeLimit.Click += new System.EventHandler(this.btnChangeLimit_Click);
            // 
            // txtLimit
            // 
            this.txtLimit.Enabled = false;
            this.txtLimit.Location = new System.Drawing.Point(846, 141);
            this.txtLimit.Name = "txtLimit";
            this.txtLimit.Size = new System.Drawing.Size(58, 19);
            this.txtLimit.TabIndex = 5;
            this.txtLimit.Visible = false;
            // 
            // dgvDateCode
            // 
            this.dgvDateCode.AllowUserToAddRows = false;
            this.dgvDateCode.AllowUserToDeleteRows = false;
            this.dgvDateCode.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgvDateCode.Location = new System.Drawing.Point(563, 11);
            this.dgvDateCode.Name = "dgvDateCode";
            this.dgvDateCode.ReadOnly = true;
            this.dgvDateCode.RowTemplate.Height = 21;
            this.dgvDateCode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvDateCode.Size = new System.Drawing.Size(350, 61);
            this.dgvDateCode.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(495, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "By Lot: ";
            // 
            // dgvDateCode2
            // 
            this.dgvDateCode2.AllowUserToAddRows = false;
            this.dgvDateCode2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dgvDateCode2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDateCode2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDateCode2.ColumnHeadersHeight = 33;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDateCode2.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDateCode2.EnableHeadersVisualStyles = false;
            this.dgvDateCode2.GridColor = System.Drawing.Color.White;
            this.dgvDateCode2.Location = new System.Drawing.Point(135, 0);
            this.dgvDateCode2.Name = "dgvDateCode2";
            this.dgvDateCode2.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDateCode2.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDateCode2.RowHeadersVisible = false;
            this.dgvDateCode2.RowHeadersWidth = 40;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDateCode2.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDateCode2.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dgvDateCode2.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.dgvDateCode2.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDateCode2.RowTemplate.Height = 33;
            this.dgvDateCode2.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDateCode2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvDateCode2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvDateCode2.Size = new System.Drawing.Size(344, 66);
            this.dgvDateCode2.TabIndex = 13;
            this.dgvDateCode2.Visible = false;
            // 
            // txtBoxIdPrint
            // 
            this.txtBoxIdPrint.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtBoxIdPrint.Location = new System.Drawing.Point(289, 0);
            this.txtBoxIdPrint.Multiline = true;
            this.txtBoxIdPrint.Name = "txtBoxIdPrint";
            this.txtBoxIdPrint.Size = new System.Drawing.Size(190, 40);
            this.txtBoxIdPrint.TabIndex = 5;
            this.txtBoxIdPrint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBoxIdPrint.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(402, 144);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 12);
            this.label9.TabIndex = 6;
            this.label9.Text = "Shaft: ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(402, 179);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "Overlay: ";
            // 
            // cmbShaft
            // 
            this.cmbShaft.Enabled = false;
            this.cmbShaft.FormattingEnabled = true;
            this.cmbShaft.Location = new System.Drawing.Point(460, 141);
            this.cmbShaft.Name = "cmbShaft";
            this.cmbShaft.Size = new System.Drawing.Size(125, 20);
            this.cmbShaft.TabIndex = 16;
            this.cmbShaft.Enter += new System.EventHandler(this.cmbShaft_Enter);
            this.cmbShaft.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbShaft_KeyDown);
            // 
            // cmbOverlay
            // 
            this.cmbOverlay.Enabled = false;
            this.cmbOverlay.FormattingEnabled = true;
            this.cmbOverlay.Location = new System.Drawing.Point(460, 176);
            this.cmbOverlay.Name = "cmbOverlay";
            this.cmbOverlay.Size = new System.Drawing.Size(125, 20);
            this.cmbOverlay.TabIndex = 16;
            this.cmbOverlay.Enter += new System.EventHandler(this.cmbOverlay_Enter);
            this.cmbOverlay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbOverlay_KeyDown);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(684, 211);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 22);
            this.btnExport.TabIndex = 41;
            this.btnExport.Text = "Excel Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(397, 211);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 22);
            this.btnPrint.TabIndex = 42;
            this.btnPrint.Text = "Reprint";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabInline);
            this.tabControl1.Controls.Add(this.tabOqc);
            this.tabControl1.Location = new System.Drawing.Point(6, 236);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(978, 455);
            this.tabControl1.TabIndex = 44;
            // 
            // tabInline
            // 
            this.tabInline.Controls.Add(this.dgvInline);
            this.tabInline.Location = new System.Drawing.Point(4, 22);
            this.tabInline.Name = "tabInline";
            this.tabInline.Padding = new System.Windows.Forms.Padding(3);
            this.tabInline.Size = new System.Drawing.Size(970, 429);
            this.tabInline.TabIndex = 0;
            this.tabInline.Text = "In-Line";
            this.tabInline.UseVisualStyleBackColor = true;
            // 
            // tabOqc
            // 
            this.tabOqc.Controls.Add(this.dgvOqc);
            this.tabOqc.Location = new System.Drawing.Point(4, 22);
            this.tabOqc.Name = "tabOqc";
            this.tabOqc.Padding = new System.Windows.Forms.Padding(3);
            this.tabOqc.Size = new System.Drawing.Size(970, 429);
            this.tabOqc.TabIndex = 1;
            this.tabOqc.Text = "OQC";
            this.tabOqc.UseVisualStyleBackColor = true;
            // 
            // dgvOqc
            // 
            this.dgvOqc.AllowUserToAddRows = false;
            this.dgvOqc.AllowUserToDeleteRows = false;
            this.dgvOqc.BackgroundColor = System.Drawing.Color.Orange;
            this.dgvOqc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOqc.Location = new System.Drawing.Point(5, 4);
            this.dgvOqc.Name = "dgvOqc";
            this.dgvOqc.ReadOnly = true;
            this.dgvOqc.RowTemplate.Height = 21;
            this.dgvOqc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvOqc.Size = new System.Drawing.Size(961, 420);
            this.dgvOqc.TabIndex = 10;
            // 
            // btnCancelBoxid
            // 
            this.btnCancelBoxid.Location = new System.Drawing.Point(838, 176);
            this.btnCancelBoxid.Name = "btnCancelBoxid";
            this.btnCancelBoxid.Size = new System.Drawing.Size(100, 22);
            this.btnCancelBoxid.TabIndex = 41;
            this.btnCancelBoxid.Text = "Cancel Boxid";
            this.btnCancelBoxid.UseVisualStyleBackColor = true;
            this.btnCancelBoxid.Visible = false;
            this.btnCancelBoxid.Click += new System.EventHandler(this.btnCancelBoxid_Click);
            // 
            // btnDeleteSerial
            // 
            this.btnDeleteSerial.Location = new System.Drawing.Point(838, 139);
            this.btnDeleteSerial.Name = "btnDeleteSerial";
            this.btnDeleteSerial.Size = new System.Drawing.Size(100, 22);
            this.btnDeleteSerial.TabIndex = 41;
            this.btnDeleteSerial.Text = "Delete Product";
            this.btnDeleteSerial.UseVisualStyleBackColor = true;
            this.btnDeleteSerial.Visible = false;
            this.btnDeleteSerial.Click += new System.EventHandler(this.btnDeleteSerial_Click);
            // 
            // btnAddSerial
            // 
            this.btnAddSerial.Location = new System.Drawing.Point(541, 211);
            this.btnAddSerial.Name = "btnAddSerial";
            this.btnAddSerial.Size = new System.Drawing.Size(100, 22);
            this.btnAddSerial.TabIndex = 41;
            this.btnAddSerial.Text = "Add Product";
            this.btnAddSerial.UseVisualStyleBackColor = true;
            this.btnAddSerial.Visible = false;
            this.btnAddSerial.Click += new System.EventHandler(this.btnAddSerial_Click);
            // 
            // frmModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(988, 701);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnAddSerial);
            this.Controls.Add(this.btnDeleteSerial);
            this.Controls.Add(this.btnCancelBoxid);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.cmbOverlay);
            this.Controls.Add(this.cmbShaft);
            this.Controls.Add(this.dgvDateCode);
            this.Controls.Add(this.dtpPrintDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtProductSerial);
            this.Controls.Add(this.txtBoxIdPrint);
            this.Controls.Add(this.txtBoxId);
            this.Controls.Add(this.txtLimit);
            this.Controls.Add(this.txtOkCount);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.btnRegisterBoxId);
            this.Controls.Add(this.btnDeleteSelection);
            this.Controls.Add(this.btnChangeLimit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgvPassFail);
            this.Controls.Add(this.dgvConfig);
            this.Controls.Add(this.dgvLine);
            this.Controls.Add(this.dgvDateCode2);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmModule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product Serial";
            this.Load += new System.EventHandler(this.frmModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPassFail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDateCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDateCode2)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabInline.ResumeLayout(false);
            this.tabOqc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOqc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvInline;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpPrintDate;
        private System.Windows.Forms.TextBox txtProductSerial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvLine;
        private System.Windows.Forms.DataGridView dgvConfig;
        private System.Windows.Forms.DataGridView dgvPassFail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnRegisterBoxId;
        private System.Windows.Forms.TextBox txtOkCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDeleteSelection;
        private System.Windows.Forms.Button btnChangeLimit;
        private System.Windows.Forms.TextBox txtLimit;
        private System.Windows.Forms.DataGridView dgvDateCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvDateCode2;
        private System.Windows.Forms.TextBox txtBoxIdPrint;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbShaft;
        private System.Windows.Forms.ComboBox cmbOverlay;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabInline;
        private System.Windows.Forms.TabPage tabOqc;
        private System.Windows.Forms.DataGridView dgvOqc;
        private System.Windows.Forms.Button btnCancelBoxid;
        private System.Windows.Forms.Button btnDeleteSerial;
        private System.Windows.Forms.Button btnAddSerial;
    }
}

