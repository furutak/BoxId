namespace BoxIdDb
{
    partial class frmSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearch));
            this.dgvProductSerial = new System.Windows.Forms.DataGridView();
            this.txtBoxIdFrom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProductSerialFrom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtBoxIdTo = new System.Windows.Forms.TextBox();
            this.txtProductSerialTo = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpPrintDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpPrintDateFrom = new System.Windows.Forms.DateTimePicker();
            this.txtInvoiceFrom = new System.Windows.Forms.TextBox();
            this.txtInvoiceTo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpPackDateFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpPackDateTo = new System.Windows.Forms.DateTimePicker();
            this.txtCartonIdFrom = new System.Windows.Forms.TextBox();
            this.txtCartonIdTo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkInvoice = new System.Windows.Forms.CheckBox();
            this.chkCartonId = new System.Windows.Forms.CheckBox();
            this.chkPackDate = new System.Windows.Forms.CheckBox();
            this.chkBoxId = new System.Windows.Forms.CheckBox();
            this.chkPrintDate = new System.Windows.Forms.CheckBox();
            this.chkProductSerial = new System.Windows.Forms.CheckBox();
            this.cmbReturn = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkReturn = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductSerial)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProductSerial
            // 
            this.dgvProductSerial.AllowUserToAddRows = false;
            this.dgvProductSerial.AllowUserToDeleteRows = false;
            this.dgvProductSerial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductSerial.Location = new System.Drawing.Point(13, 229);
            this.dgvProductSerial.Name = "dgvProductSerial";
            this.dgvProductSerial.ReadOnly = true;
            this.dgvProductSerial.RowTemplate.Height = 21;
            this.dgvProductSerial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvProductSerial.Size = new System.Drawing.Size(962, 455);
            this.dgvProductSerial.TabIndex = 9;
            // 
            // txtBoxIdFrom
            // 
            this.txtBoxIdFrom.Location = new System.Drawing.Point(187, 122);
            this.txtBoxIdFrom.Name = "txtBoxIdFrom";
            this.txtBoxIdFrom.Size = new System.Drawing.Size(213, 19);
            this.txtBoxIdFrom.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Box ID: ";
            // 
            // txtProductSerialFrom
            // 
            this.txtProductSerialFrom.Location = new System.Drawing.Point(187, 190);
            this.txtProductSerialFrom.Name = "txtProductSerialFrom";
            this.txtProductSerialFrom.Size = new System.Drawing.Size(213, 19);
            this.txtProductSerialFrom.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Product Serial: ";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(815, 171);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(129, 22);
            this.btnSearch.TabIndex = 21;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtBoxIdTo
            // 
            this.txtBoxIdTo.Location = new System.Drawing.Point(474, 122);
            this.txtBoxIdTo.Name = "txtBoxIdTo";
            this.txtBoxIdTo.Size = new System.Drawing.Size(213, 19);
            this.txtBoxIdTo.TabIndex = 11;
            // 
            // txtProductSerialTo
            // 
            this.txtProductSerialTo.Location = new System.Drawing.Point(474, 190);
            this.txtProductSerialTo.Name = "txtProductSerialTo";
            this.txtProductSerialTo.Size = new System.Drawing.Size(213, 19);
            this.txtProductSerialTo.TabIndex = 17;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(815, 116);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(129, 22);
            this.btnExport.TabIndex = 20;
            this.btnExport.Text = "CSV Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Print Date: ";
            // 
            // dtpPrintDateTo
            // 
            this.dtpPrintDateTo.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtpPrintDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPrintDateTo.Location = new System.Drawing.Point(474, 156);
            this.dtpPrintDateTo.Name = "dtpPrintDateTo";
            this.dtpPrintDateTo.ShowUpDown = true;
            this.dtpPrintDateTo.Size = new System.Drawing.Size(213, 19);
            this.dtpPrintDateTo.TabIndex = 14;
            // 
            // dtpPrintDateFrom
            // 
            this.dtpPrintDateFrom.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtpPrintDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPrintDateFrom.Location = new System.Drawing.Point(187, 156);
            this.dtpPrintDateFrom.Name = "dtpPrintDateFrom";
            this.dtpPrintDateFrom.ShowUpDown = true;
            this.dtpPrintDateFrom.Size = new System.Drawing.Size(213, 19);
            this.dtpPrintDateFrom.TabIndex = 13;
            // 
            // txtInvoiceFrom
            // 
            this.txtInvoiceFrom.Location = new System.Drawing.Point(187, 24);
            this.txtInvoiceFrom.Name = "txtInvoiceFrom";
            this.txtInvoiceFrom.Size = new System.Drawing.Size(213, 19);
            this.txtInvoiceFrom.TabIndex = 1;
            // 
            // txtInvoiceTo
            // 
            this.txtInvoiceTo.Location = new System.Drawing.Point(474, 24);
            this.txtInvoiceTo.Name = "txtInvoiceTo";
            this.txtInvoiceTo.Size = new System.Drawing.Size(213, 19);
            this.txtInvoiceTo.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "Pack Date: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "Invoice: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(139, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "From: ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(440, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 12);
            this.label9.TabIndex = 6;
            this.label9.Text = "To: ";
            // 
            // dtpPackDateFrom
            // 
            this.dtpPackDateFrom.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtpPackDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPackDateFrom.Location = new System.Drawing.Point(187, 89);
            this.dtpPackDateFrom.Name = "dtpPackDateFrom";
            this.dtpPackDateFrom.ShowUpDown = true;
            this.dtpPackDateFrom.Size = new System.Drawing.Size(213, 19);
            this.dtpPackDateFrom.TabIndex = 7;
            // 
            // dtpPackDateTo
            // 
            this.dtpPackDateTo.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtpPackDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPackDateTo.Location = new System.Drawing.Point(474, 89);
            this.dtpPackDateTo.Name = "dtpPackDateTo";
            this.dtpPackDateTo.ShowUpDown = true;
            this.dtpPackDateTo.Size = new System.Drawing.Size(213, 19);
            this.dtpPackDateTo.TabIndex = 8;
            // 
            // txtCartonIdFrom
            // 
            this.txtCartonIdFrom.Location = new System.Drawing.Point(187, 56);
            this.txtCartonIdFrom.Name = "txtCartonIdFrom";
            this.txtCartonIdFrom.Size = new System.Drawing.Size(213, 19);
            this.txtCartonIdFrom.TabIndex = 4;
            // 
            // txtCartonIdTo
            // 
            this.txtCartonIdTo.Location = new System.Drawing.Point(474, 56);
            this.txtCartonIdTo.Name = "txtCartonIdTo";
            this.txtCartonIdTo.Size = new System.Drawing.Size(213, 19);
            this.txtCartonIdTo.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Carton ID: ";
            // 
            // chkInvoice
            // 
            this.chkInvoice.AutoSize = true;
            this.chkInvoice.Location = new System.Drawing.Point(733, 27);
            this.chkInvoice.Name = "chkInvoice";
            this.chkInvoice.Size = new System.Drawing.Size(15, 14);
            this.chkInvoice.TabIndex = 3;
            this.chkInvoice.UseVisualStyleBackColor = true;
            // 
            // chkCartonId
            // 
            this.chkCartonId.AutoSize = true;
            this.chkCartonId.Location = new System.Drawing.Point(733, 59);
            this.chkCartonId.Name = "chkCartonId";
            this.chkCartonId.Size = new System.Drawing.Size(15, 14);
            this.chkCartonId.TabIndex = 6;
            this.chkCartonId.UseVisualStyleBackColor = true;
            // 
            // chkPackDate
            // 
            this.chkPackDate.AutoSize = true;
            this.chkPackDate.Location = new System.Drawing.Point(733, 93);
            this.chkPackDate.Name = "chkPackDate";
            this.chkPackDate.Size = new System.Drawing.Size(15, 14);
            this.chkPackDate.TabIndex = 9;
            this.chkPackDate.UseVisualStyleBackColor = true;
            // 
            // chkBoxId
            // 
            this.chkBoxId.AutoSize = true;
            this.chkBoxId.Location = new System.Drawing.Point(733, 125);
            this.chkBoxId.Name = "chkBoxId";
            this.chkBoxId.Size = new System.Drawing.Size(15, 14);
            this.chkBoxId.TabIndex = 12;
            this.chkBoxId.UseVisualStyleBackColor = true;
            // 
            // chkPrintDate
            // 
            this.chkPrintDate.AutoSize = true;
            this.chkPrintDate.Location = new System.Drawing.Point(733, 160);
            this.chkPrintDate.Name = "chkPrintDate";
            this.chkPrintDate.Size = new System.Drawing.Size(15, 14);
            this.chkPrintDate.TabIndex = 15;
            this.chkPrintDate.UseVisualStyleBackColor = true;
            // 
            // chkProductSerial
            // 
            this.chkProductSerial.AutoSize = true;
            this.chkProductSerial.Location = new System.Drawing.Point(733, 193);
            this.chkProductSerial.Name = "chkProductSerial";
            this.chkProductSerial.Size = new System.Drawing.Size(15, 14);
            this.chkProductSerial.TabIndex = 18;
            this.chkProductSerial.UseVisualStyleBackColor = true;
            // 
            // cmbReturn
            // 
            this.cmbReturn.FormattingEnabled = true;
            this.cmbReturn.Items.AddRange(new object[] {
            "N",
            "R"});
            this.cmbReturn.Location = new System.Drawing.Point(848, 56);
            this.cmbReturn.Name = "cmbReturn";
            this.cmbReturn.Size = new System.Drawing.Size(66, 20);
            this.cmbReturn.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(791, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "Return: ";
            // 
            // chkReturn
            // 
            this.chkReturn.AutoSize = true;
            this.chkReturn.Location = new System.Drawing.Point(935, 59);
            this.chkReturn.Name = "chkReturn";
            this.chkReturn.Size = new System.Drawing.Size(15, 14);
            this.chkReturn.TabIndex = 6;
            this.chkReturn.UseVisualStyleBackColor = true;
            // 
            // frmSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(987, 696);
            this.Controls.Add(this.cmbReturn);
            this.Controls.Add(this.chkProductSerial);
            this.Controls.Add(this.chkPrintDate);
            this.Controls.Add(this.chkBoxId);
            this.Controls.Add(this.chkPackDate);
            this.Controls.Add(this.chkReturn);
            this.Controls.Add(this.chkCartonId);
            this.Controls.Add(this.chkInvoice);
            this.Controls.Add(this.dtpPackDateTo);
            this.Controls.Add(this.dtpPrintDateTo);
            this.Controls.Add(this.dtpPackDateFrom);
            this.Controls.Add(this.dtpPrintDateFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCartonIdTo);
            this.Controls.Add(this.txtInvoiceTo);
            this.Controls.Add(this.txtProductSerialTo);
            this.Controls.Add(this.txtCartonIdFrom);
            this.Controls.Add(this.txtBoxIdTo);
            this.Controls.Add(this.txtInvoiceFrom);
            this.Controls.Add(this.txtProductSerialFrom);
            this.Controls.Add(this.txtBoxIdFrom);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvProductSerial);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product Serial";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSearch_FormClosed);
            this.Load += new System.EventHandler(this.frmModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductSerial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProductSerial;
        private System.Windows.Forms.TextBox txtBoxIdFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProductSerialFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtBoxIdTo;
        private System.Windows.Forms.TextBox txtProductSerialTo;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpPrintDateTo;
        private System.Windows.Forms.DateTimePicker dtpPrintDateFrom;
        private System.Windows.Forms.TextBox txtInvoiceFrom;
        private System.Windows.Forms.TextBox txtInvoiceTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpPackDateFrom;
        private System.Windows.Forms.DateTimePicker dtpPackDateTo;
        private System.Windows.Forms.TextBox txtCartonIdFrom;
        private System.Windows.Forms.TextBox txtCartonIdTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkInvoice;
        private System.Windows.Forms.CheckBox chkCartonId;
        private System.Windows.Forms.CheckBox chkPackDate;
        private System.Windows.Forms.CheckBox chkBoxId;
        private System.Windows.Forms.CheckBox chkPrintDate;
        private System.Windows.Forms.CheckBox chkProductSerial;
        private System.Windows.Forms.ComboBox cmbReturn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkReturn;
    }
}

