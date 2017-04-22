namespace BoxIdDb
{
    partial class frmCarton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCarton));
            this.btnAddBoxId = new System.Windows.Forms.Button();
            this.btnSearchBoxId = new System.Windows.Forms.Button();
            this.dgvCartonId = new System.Windows.Forms.DataGridView();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCtnIdFrom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProductSerial = new System.Windows.Forms.TextBox();
            this.dtpPackDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rdbCartonId = new System.Windows.Forms.RadioButton();
            this.rdbPackDate = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.rdbShipDate = new System.Windows.Forms.RadioButton();
            this.btnEditShipping = new System.Windows.Forms.Button();
            this.dtpShipDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCtnIdTo = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBoxId = new System.Windows.Forms.TextBox();
            this.rdbBoxId = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.txtInvoice = new System.Windows.Forms.TextBox();
            this.rdbInvoice = new System.Windows.Forms.RadioButton();
            this.rdbSerno = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartonId)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddBoxId
            // 
            this.btnAddBoxId.Location = new System.Drawing.Point(222, 164);
            this.btnAddBoxId.Name = "btnAddBoxId";
            this.btnAddBoxId.Size = new System.Drawing.Size(100, 23);
            this.btnAddBoxId.TabIndex = 10;
            this.btnAddBoxId.Text = "Add Carton ID";
            this.btnAddBoxId.UseVisualStyleBackColor = true;
            this.btnAddBoxId.Click += new System.EventHandler(this.btnAddCartonId_Click);
            // 
            // btnSearchBoxId
            // 
            this.btnSearchBoxId.Location = new System.Drawing.Point(98, 164);
            this.btnSearchBoxId.Name = "btnSearchBoxId";
            this.btnSearchBoxId.Size = new System.Drawing.Size(100, 23);
            this.btnSearchBoxId.TabIndex = 9;
            this.btnSearchBoxId.Text = "Search";
            this.btnSearchBoxId.UseVisualStyleBackColor = true;
            this.btnSearchBoxId.Click += new System.EventHandler(this.btnSearchBoxId_Click);
            // 
            // dgvCartonId
            // 
            this.dgvCartonId.AllowUserToAddRows = false;
            this.dgvCartonId.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCartonId.Location = new System.Drawing.Point(12, 206);
            this.dgvCartonId.Name = "dgvCartonId";
            this.dgvCartonId.ReadOnly = true;
            this.dgvCartonId.RowTemplate.Height = 21;
            this.dgvCartonId.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCartonId.Size = new System.Drawing.Size(857, 463);
            this.dgvCartonId.TabIndex = 9;
            this.dgvCartonId.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBoxId_CellContentClick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(78, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 12);
            this.label12.TabIndex = 6;
            this.label12.Text = "Pack Date: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Carton ID from: ";
            // 
            // txtCtnIdFrom
            // 
            this.txtCtnIdFrom.Location = new System.Drawing.Point(176, 21);
            this.txtCtnIdFrom.Name = "txtCtnIdFrom";
            this.txtCtnIdFrom.Size = new System.Drawing.Size(166, 19);
            this.txtCtnIdFrom.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(448, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Product Serial: ";
            // 
            // txtProductSerial
            // 
            this.txtProductSerial.Location = new System.Drawing.Point(546, 88);
            this.txtProductSerial.Name = "txtProductSerial";
            this.txtProductSerial.Size = new System.Drawing.Size(166, 19);
            this.txtProductSerial.TabIndex = 7;
            // 
            // dtpPackDate
            // 
            this.dtpPackDate.CustomFormat = "yyyy/MM/dd";
            this.dtpPackDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPackDate.Location = new System.Drawing.Point(176, 53);
            this.dtpPackDate.Name = "dtpPackDate";
            this.dtpPackDate.Size = new System.Drawing.Size(166, 19);
            this.dtpPackDate.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(450, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "User: ";
            // 
            // txtUser
            // 
            this.txtUser.Enabled = false;
            this.txtUser.Location = new System.Drawing.Point(546, 122);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(166, 19);
            this.txtUser.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(628, 164);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rdbCartonId
            // 
            this.rdbCartonId.AutoSize = true;
            this.rdbCartonId.Location = new System.Drawing.Point(363, 23);
            this.rdbCartonId.Name = "rdbCartonId";
            this.rdbCartonId.Size = new System.Drawing.Size(14, 13);
            this.rdbCartonId.TabIndex = 14;
            this.rdbCartonId.UseVisualStyleBackColor = true;
            this.rdbCartonId.CheckedChanged += new System.EventHandler(this.rdbCartonId_CheckedChanged);
            // 
            // rdbPackDate
            // 
            this.rdbPackDate.AutoSize = true;
            this.rdbPackDate.Checked = true;
            this.rdbPackDate.Location = new System.Drawing.Point(364, 56);
            this.rdbPackDate.Name = "rdbPackDate";
            this.rdbPackDate.Size = new System.Drawing.Size(14, 13);
            this.rdbPackDate.TabIndex = 14;
            this.rdbPackDate.TabStop = true;
            this.rdbPackDate.UseVisualStyleBackColor = true;
            this.rdbPackDate.CheckedChanged += new System.EventHandler(this.rdbPackDate_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(448, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "PC Ship Date: ";
            // 
            // rdbShipDate
            // 
            this.rdbShipDate.AutoSize = true;
            this.rdbShipDate.Location = new System.Drawing.Point(734, 53);
            this.rdbShipDate.Name = "rdbShipDate";
            this.rdbShipDate.Size = new System.Drawing.Size(14, 13);
            this.rdbShipDate.TabIndex = 14;
            this.rdbShipDate.UseVisualStyleBackColor = true;
            this.rdbShipDate.CheckedChanged += new System.EventHandler(this.rdbShipDate_CheckedChanged);
            // 
            // btnEditShipping
            // 
            this.btnEditShipping.Location = new System.Drawing.Point(504, 164);
            this.btnEditShipping.Name = "btnEditShipping";
            this.btnEditShipping.Size = new System.Drawing.Size(100, 23);
            this.btnEditShipping.TabIndex = 12;
            this.btnEditShipping.Text = "Edit Shipping";
            this.btnEditShipping.UseVisualStyleBackColor = true;
            this.btnEditShipping.Click += new System.EventHandler(this.btnEditShipping_Click);
            // 
            // dtpShipDate
            // 
            this.dtpShipDate.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtpShipDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpShipDate.Location = new System.Drawing.Point(546, 52);
            this.dtpShipDate.Name = "dtpShipDate";
            this.dtpShipDate.ShowUpDown = true;
            this.dtpShipDate.Size = new System.Drawing.Size(166, 19);
            this.dtpShipDate.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(448, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "Carton ID  to: ";
            // 
            // txtCtnIdTo
            // 
            this.txtCtnIdTo.Location = new System.Drawing.Point(546, 20);
            this.txtCtnIdTo.Name = "txtCtnIdTo";
            this.txtCtnIdTo.Size = new System.Drawing.Size(166, 19);
            this.txtCtnIdTo.TabIndex = 5;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(346, 164);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 23);
            this.btnExport.TabIndex = 11;
            this.btnExport.Text = "Excel Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(78, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "Box ID: ";
            // 
            // txtBoxId
            // 
            this.txtBoxId.Location = new System.Drawing.Point(176, 89);
            this.txtBoxId.Name = "txtBoxId";
            this.txtBoxId.Size = new System.Drawing.Size(166, 19);
            this.txtBoxId.TabIndex = 3;
            // 
            // rdbBoxId
            // 
            this.rdbBoxId.AutoSize = true;
            this.rdbBoxId.Location = new System.Drawing.Point(364, 90);
            this.rdbBoxId.Name = "rdbBoxId";
            this.rdbBoxId.Size = new System.Drawing.Size(14, 13);
            this.rdbBoxId.TabIndex = 14;
            this.rdbBoxId.UseVisualStyleBackColor = true;
            this.rdbBoxId.CheckedChanged += new System.EventHandler(this.rdbBoxId_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(78, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "Invoice: ";
            // 
            // txtInvoice
            // 
            this.txtInvoice.Location = new System.Drawing.Point(176, 123);
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.Size = new System.Drawing.Size(166, 19);
            this.txtInvoice.TabIndex = 4;
            // 
            // rdbInvoice
            // 
            this.rdbInvoice.AutoSize = true;
            this.rdbInvoice.Location = new System.Drawing.Point(364, 124);
            this.rdbInvoice.Name = "rdbInvoice";
            this.rdbInvoice.Size = new System.Drawing.Size(14, 13);
            this.rdbInvoice.TabIndex = 14;
            this.rdbInvoice.UseVisualStyleBackColor = true;
            this.rdbInvoice.CheckedChanged += new System.EventHandler(this.rdbInvoice_CheckedChanged);
            // 
            // rdbSerno
            // 
            this.rdbSerno.AutoSize = true;
            this.rdbSerno.Location = new System.Drawing.Point(734, 89);
            this.rdbSerno.Name = "rdbSerno";
            this.rdbSerno.Size = new System.Drawing.Size(14, 13);
            this.rdbSerno.TabIndex = 15;
            this.rdbSerno.TabStop = true;
            this.rdbSerno.UseVisualStyleBackColor = true;
            this.rdbSerno.CheckedChanged += new System.EventHandler(this.rdbSerno_CheckedChanged);
            // 
            // frmCarton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(881, 681);
            this.Controls.Add(this.rdbSerno);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.rdbShipDate);
            this.Controls.Add(this.rdbInvoice);
            this.Controls.Add(this.rdbBoxId);
            this.Controls.Add(this.rdbPackDate);
            this.Controls.Add(this.rdbCartonId);
            this.Controls.Add(this.dtpShipDate);
            this.Controls.Add(this.dtpPackDate);
            this.Controls.Add(this.txtCtnIdTo);
            this.Controls.Add(this.txtCtnIdFrom);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtInvoice);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtBoxId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtProductSerial);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnEditShipping);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddBoxId);
            this.Controls.Add(this.btnSearchBoxId);
            this.Controls.Add(this.dgvCartonId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCarton";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Carton ID";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCarton_FormClosed);
            this.Load += new System.EventHandler(this.frmCarton_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartonId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView dgvCartonId;
        private System.Windows.Forms.Button btnSearchBoxId;
        private System.Windows.Forms.Button btnAddBoxId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCtnIdFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProductSerial;
        private System.Windows.Forms.DateTimePicker dtpPackDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rdbCartonId;
        private System.Windows.Forms.RadioButton rdbPackDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdbShipDate;
        private System.Windows.Forms.Button btnEditShipping;
        private System.Windows.Forms.DateTimePicker dtpShipDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCtnIdTo;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBoxId;
        private System.Windows.Forms.RadioButton rdbBoxId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtInvoice;
        private System.Windows.Forms.RadioButton rdbInvoice;
        private System.Windows.Forms.RadioButton rdbSerno;
    }
}

