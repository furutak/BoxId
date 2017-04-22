namespace BoxIdDb
{
    partial class frmCartonContent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCartonContent));
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvBoxId = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCartonId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpPackDate = new System.Windows.Forms.DateTimePicker();
            this.txtBoxId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRegisterCartonId = new System.Windows.Forms.Button();
            this.dgvLot = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dgvShaft = new System.Windows.Forms.DataGridView();
            this.dgvModel = new System.Windows.Forms.DataGridView();
            this.dgvOverlay = new System.Windows.Forms.DataGridView();
            this.label12 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.dgvInvoice = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.txtInvoice = new System.Windows.Forms.TextBox();
            this.btnRegisterInvoice = new System.Windows.Forms.Button();
            this.btnClearBoxId = new System.Windows.Forms.Button();
            this.btnClearInvoice = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbStage = new System.Windows.Forms.ComboBox();
            this.dgvReturn = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoxId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShaft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturn)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(710, 56);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(130, 22);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvBoxId
            // 
            this.dgvBoxId.AllowUserToAddRows = false;
            this.dgvBoxId.AllowUserToDeleteRows = false;
            this.dgvBoxId.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBoxId.Location = new System.Drawing.Point(23, 160);
            this.dgvBoxId.Name = "dgvBoxId";
            this.dgvBoxId.ReadOnly = true;
            this.dgvBoxId.RowTemplate.Height = 21;
            this.dgvBoxId.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvBoxId.Size = new System.Drawing.Size(282, 50);
            this.dgvBoxId.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Pack Date: ";
            // 
            // txtCartonId
            // 
            this.txtCartonId.Enabled = false;
            this.txtCartonId.Location = new System.Drawing.Point(116, 23);
            this.txtCartonId.Name = "txtCartonId";
            this.txtCartonId.Size = new System.Drawing.Size(188, 19);
            this.txtCartonId.TabIndex = 5;
            this.txtCartonId.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Carton ID: ";
            // 
            // dtpPackDate
            // 
            this.dtpPackDate.Enabled = false;
            this.dtpPackDate.Location = new System.Drawing.Point(117, 62);
            this.dtpPackDate.Name = "dtpPackDate";
            this.dtpPackDate.Size = new System.Drawing.Size(188, 19);
            this.dtpPackDate.TabIndex = 12;
            this.dtpPackDate.TabStop = false;
            // 
            // txtBoxId
            // 
            this.txtBoxId.Enabled = false;
            this.txtBoxId.Location = new System.Drawing.Point(116, 119);
            this.txtBoxId.Name = "txtBoxId";
            this.txtBoxId.Size = new System.Drawing.Size(188, 19);
            this.txtBoxId.TabIndex = 1;
            this.txtBoxId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxId_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Box ID: ";
            // 
            // btnRegisterCartonId
            // 
            this.btnRegisterCartonId.Location = new System.Drawing.Point(332, 160);
            this.btnRegisterCartonId.Name = "btnRegisterCartonId";
            this.btnRegisterCartonId.Size = new System.Drawing.Size(106, 22);
            this.btnRegisterCartonId.TabIndex = 2;
            this.btnRegisterCartonId.Text = "Register Carton";
            this.btnRegisterCartonId.UseVisualStyleBackColor = true;
            this.btnRegisterCartonId.Click += new System.EventHandler(this.btnRegisterBoxId_Click);
            // 
            // dgvLot
            // 
            this.dgvLot.AllowUserToAddRows = false;
            this.dgvLot.AllowUserToDeleteRows = false;
            this.dgvLot.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgvLot.Location = new System.Drawing.Point(704, 122);
            this.dgvLot.Name = "dgvLot";
            this.dgvLot.ReadOnly = true;
            this.dgvLot.RowTemplate.Height = 21;
            this.dgvLot.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvLot.Size = new System.Drawing.Size(162, 216);
            this.dgvLot.TabIndex = 9;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(710, 21);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(130, 22);
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "Reprint";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dgvShaft
            // 
            this.dgvShaft.AllowUserToAddRows = false;
            this.dgvShaft.AllowUserToDeleteRows = false;
            this.dgvShaft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShaft.Location = new System.Drawing.Point(519, 181);
            this.dgvShaft.Name = "dgvShaft";
            this.dgvShaft.ReadOnly = true;
            this.dgvShaft.RowTemplate.Height = 21;
            this.dgvShaft.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvShaft.Size = new System.Drawing.Size(134, 43);
            this.dgvShaft.TabIndex = 9;
            // 
            // dgvModel
            // 
            this.dgvModel.AllowUserToAddRows = false;
            this.dgvModel.AllowUserToDeleteRows = false;
            this.dgvModel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvModel.Location = new System.Drawing.Point(519, 124);
            this.dgvModel.Name = "dgvModel";
            this.dgvModel.ReadOnly = true;
            this.dgvModel.RowTemplate.Height = 21;
            this.dgvModel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvModel.Size = new System.Drawing.Size(134, 43);
            this.dgvModel.TabIndex = 9;
            // 
            // dgvOverlay
            // 
            this.dgvOverlay.AllowUserToAddRows = false;
            this.dgvOverlay.AllowUserToDeleteRows = false;
            this.dgvOverlay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOverlay.Location = new System.Drawing.Point(519, 238);
            this.dgvOverlay.Name = "dgvOverlay";
            this.dgvOverlay.ReadOnly = true;
            this.dgvOverlay.RowTemplate.Height = 21;
            this.dgvOverlay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvOverlay.Size = new System.Drawing.Size(134, 43);
            this.dgvOverlay.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(367, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 12);
            this.label12.TabIndex = 6;
            this.label12.Text = "User: ";
            // 
            // txtUser
            // 
            this.txtUser.Enabled = false;
            this.txtUser.Location = new System.Drawing.Point(441, 25);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(188, 19);
            this.txtUser.TabIndex = 5;
            this.txtUser.TabStop = false;
            // 
            // dgvInvoice
            // 
            this.dgvInvoice.AllowUserToAddRows = false;
            this.dgvInvoice.AllowUserToDeleteRows = false;
            this.dgvInvoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvoice.Location = new System.Drawing.Point(23, 279);
            this.dgvInvoice.Name = "dgvInvoice";
            this.dgvInvoice.ReadOnly = true;
            this.dgvInvoice.RowTemplate.Height = 21;
            this.dgvInvoice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvInvoice.Size = new System.Drawing.Size(281, 50);
            this.dgvInvoice.TabIndex = 45;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 249);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 44;
            this.label4.Text = "Invoice: ";
            // 
            // txtInvoice
            // 
            this.txtInvoice.Location = new System.Drawing.Point(117, 244);
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.Size = new System.Drawing.Size(188, 19);
            this.txtInvoice.TabIndex = 4;
            this.txtInvoice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInvoice_KeyDown);
            // 
            // btnRegisterInvoice
            // 
            this.btnRegisterInvoice.Enabled = false;
            this.btnRegisterInvoice.Location = new System.Drawing.Point(332, 279);
            this.btnRegisterInvoice.Name = "btnRegisterInvoice";
            this.btnRegisterInvoice.Size = new System.Drawing.Size(106, 22);
            this.btnRegisterInvoice.TabIndex = 5;
            this.btnRegisterInvoice.Text = "Register Invoice";
            this.btnRegisterInvoice.UseVisualStyleBackColor = true;
            this.btnRegisterInvoice.Click += new System.EventHandler(this.btnRegisterInvoice_Click);
            // 
            // btnClearBoxId
            // 
            this.btnClearBoxId.Location = new System.Drawing.Point(332, 189);
            this.btnClearBoxId.Name = "btnClearBoxId";
            this.btnClearBoxId.Size = new System.Drawing.Size(106, 22);
            this.btnClearBoxId.TabIndex = 3;
            this.btnClearBoxId.Text = "Clear Box ID";
            this.btnClearBoxId.UseVisualStyleBackColor = true;
            this.btnClearBoxId.Click += new System.EventHandler(this.btnClearBoxId_Click);
            // 
            // btnClearInvoice
            // 
            this.btnClearInvoice.Location = new System.Drawing.Point(332, 309);
            this.btnClearInvoice.Name = "btnClearInvoice";
            this.btnClearInvoice.Size = new System.Drawing.Size(106, 22);
            this.btnClearInvoice.TabIndex = 6;
            this.btnClearInvoice.Text = "Clear Invoice";
            this.btnClearInvoice.UseVisualStyleBackColor = true;
            this.btnClearInvoice.Click += new System.EventHandler(this.btnClearInvoice_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(467, 241);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "Overlay:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(467, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "Shaft: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(467, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "Model:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(668, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "Lot: ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(367, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 12);
            this.label9.TabIndex = 6;
            this.label9.Text = "Stage: ";
            // 
            // cmbStage
            // 
            this.cmbStage.FormattingEnabled = true;
            this.cmbStage.Items.AddRange(new object[] {
            "MP"});
            this.cmbStage.Location = new System.Drawing.Point(441, 60);
            this.cmbStage.Name = "cmbStage";
            this.cmbStage.Size = new System.Drawing.Size(188, 20);
            this.cmbStage.TabIndex = 46;
            // 
            // dgvReturn
            // 
            this.dgvReturn.AllowUserToAddRows = false;
            this.dgvReturn.AllowUserToDeleteRows = false;
            this.dgvReturn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReturn.Location = new System.Drawing.Point(519, 295);
            this.dgvReturn.Name = "dgvReturn";
            this.dgvReturn.ReadOnly = true;
            this.dgvReturn.RowTemplate.Height = 21;
            this.dgvReturn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvReturn.Size = new System.Drawing.Size(134, 43);
            this.dgvReturn.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(467, 298);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "Return:";
            // 
            // frmCartonContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(885, 365);
            this.Controls.Add(this.cmbStage);
            this.Controls.Add(this.btnClearInvoice);
            this.Controls.Add(this.btnClearBoxId);
            this.Controls.Add(this.btnRegisterInvoice);
            this.Controls.Add(this.dgvInvoice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtInvoice);
            this.Controls.Add(this.dgvBoxId);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.dgvLot);
            this.Controls.Add(this.dtpPackDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtBoxId);
            this.Controls.Add(this.txtCartonId);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.btnRegisterCartonId);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgvReturn);
            this.Controls.Add(this.dgvOverlay);
            this.Controls.Add(this.dgvModel);
            this.Controls.Add(this.dgvShaft);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCartonContent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Carton Content";
            this.Load += new System.EventHandler(this.frmModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoxId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShaft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvBoxId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCartonId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpPackDate;
        private System.Windows.Forms.TextBox txtBoxId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRegisterCartonId;
        private System.Windows.Forms.DataGridView dgvLot;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridView dgvShaft;
        private System.Windows.Forms.DataGridView dgvModel;
        private System.Windows.Forms.DataGridView dgvOverlay;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.DataGridView dgvInvoice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtInvoice;
        private System.Windows.Forms.Button btnRegisterInvoice;
        private System.Windows.Forms.Button btnClearBoxId;
        private System.Windows.Forms.Button btnClearInvoice;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbStage;
        private System.Windows.Forms.DataGridView dgvReturn;
        private System.Windows.Forms.Label label10;
    }
}

