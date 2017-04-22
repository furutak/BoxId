using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Security.Permissions;
using Npgsql;

namespace BoxIdDb
{
    public partial class frmCarton : Form
    {
        //�e�t�H�[��frmLogin�ցA�C�x���g������A���i�f���Q�[�g�j
        public delegate void RefreshEventHandler(object sender, EventArgs e);
        public event RefreshEventHandler RefreshEvent;

        //�f�[�^�O���b�h�r���[�p�{�^��
        DataGridViewButtonColumn updateId;
        DataGridViewButtonColumn replaceId;
        DataGridViewButtonColumn editShip;

        //���̑��񃍁[�J���ϐ�
        DataTable dtCarton;

        // �R���X�g���N�^
        public frmCarton()
        {
            InitializeComponent();
        }

        // ���[�h���̏���
        private void frmCarton_Load(object sender, EventArgs e)
        {
            //�t�H�[���̏ꏊ���w��
            this.Left = 50;
            this.Top = 10;

            dtCarton = new DataTable();
            defineDatatable(ref dtCarton);
            updateDataGridView(ref dgvCartonId, dtCarton, true);

            // �c�`�s�d�s�h�l�d�o�h�b�j�d�q�̕��ȉ���������
            dtpRounddownHour(dtpShipDate);

            // �o�ד��̈ꊇ�o�^�́A���[�U�[�X�݂̂��\
            if (txtUser.Text == "User_9")
            {
                btnEditShipping.Enabled = true;
                txtCtnIdTo.Enabled = true;
            }
            else
            {
                btnEditShipping.Enabled = false;
                txtCtnIdTo.Enabled = false;
            }
        }

        // �T�u�v���V�[�W���F�f�[�^�O���b�g�r���[�̍X�V�B�e�t�H�[���ŌĂяo���A�e�t�H�[���̏��������p��
        public void updateControls(string user)
        {
            txtUser.Text = user;
        }

        // �T�u�v���V�[�W���F�f�[�^�e�[�u���̒�`
        private void defineDatatable(ref DataTable dt)
        {
            dt.Columns.Add("cartonid", Type.GetType("System.String"));
            dt.Columns.Add("boxid1", Type.GetType("System.String"));
            dt.Columns.Add("boxid2", Type.GetType("System.String"));
            dt.Columns.Add("packdate", Type.GetType("System.DateTime"));
            dt.Columns.Add("suser", Type.GetType("System.String"));
            dt.Columns.Add("invoice", Type.GetType("System.String"));
            dt.Columns.Add("invctnno", Type.GetType("System.String"));
            dt.Columns.Add("pcshipdate", Type.GetType("System.DateTime"));
        }

        // �T�u�v���V�[�W���F�f�[�^�O���b�g�r���[�̍X�V
        public void updateDataGridView(ref DataGridView dgv, DataTable dt, bool load)
        {
            string cartnId = txtCtnIdFrom.Text;
            string boxId = txtBoxId.Text;
            string invoice = txtInvoice.Text;
            string serialNo = txtProductSerial.Text;
            DateTime packDate = dtpPackDate.Value;
            DateTime shipDate = dtpShipDate.Value;
            TfSQL tf = new TfSQL();
            string sql = String.Empty;

            dt.Clear();

            // �r�p�k���ʂ��A�c�s�`�`�s�`�a�k�d�֊i�[
            if (rdbCartonId.Checked)
            {
                if (cartnId.Length < 6)
                {
                    MessageBox.Show("Please select at least 6 characters like LM1601", "Notice",
                        MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    return;
                }
                sql = "select cartonid, boxid1, boxid2, packdate, suser, invoice, invctnno, pcshipdate FROM carton_id " +
                    " WHERE cartonid like '" + cartnId + "%'" + " order by cartonid";
            }
            else if(rdbPackDate.Checked)
            {
                sql = "select cartonid, boxid1, boxid2, packdate, suser, invoice, invctnno, pcshipdate FROM carton_id " +
                    "WHERE packdate>='" + packDate.Date + "' AND packdate<'" + packDate.Date.AddDays(1) + "' order by cartonid";
            }
            else if (rdbBoxId.Checked)
            {
                if (boxId == String.Empty)
                {
                    MessageBox.Show("Please select full 12 characters.", "Notice",
                        MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    return;
                }
                sql = "select cartonid, boxid1, boxid2, packdate, suser, invoice, invctnno, pcshipdate FROM carton_id " +
                    " WHERE boxid1='" + boxId + "' OR boxid2='" + boxId + "' order by cartonid";
            }
            else if (rdbInvoice.Checked)
            {
                if (invoice == String.Empty)
                {
                    MessageBox.Show("Please select some characters.", "Notice",
                        MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    return;
                }
                sql = "select cartonid, boxid1, boxid2, packdate, suser, invoice, invctnno, pcshipdate FROM carton_id " +
                    " WHERE invoice like '%" + invoice + "%' order by cartonid";
            }
            else if (rdbSerno.Checked)
            {
                if (serialNo == String.Empty)
                {
                    MessageBox.Show("Please select full digit of a serial no.", "Notice",
                        MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    return;
                }
                sql = "select boxid FROM product_serial_rt WHERE serialno='" + serialNo + "'";
                boxId = tf.sqlExecuteScalarString(sql);
                if (boxId == String.Empty) return;
                txtBoxId.Text = boxId;
                sql = "select cartonid, boxid1, boxid2, packdate, suser, invoice, invctnno, pcshipdate FROM carton_id " +
                    " WHERE boxid1='" + boxId + "' OR boxid2='" + boxId + "' order by cartonid";
            }
            else if (dtpShipDate.Checked)
            {
                sql = "select cartonid, boxid1, boxid2, packdate, suser, invoice, invctnno, pcshipdate FROM carton_id " +
                    "WHERE pcshipdate>='" + shipDate.Date + "' AND pcshipdate<'" + shipDate.Date.AddDays(1) + "' order by cartonid";
            }

            System.Diagnostics.Debug.Print(sql);
            tf.sqlDataAdapterFillDatatable(sql, ref dt);

            // �f�[�^�O���b�g�r���[�ւc�s�`�`�s�`�a�k�d���i�[
            dgv.DataSource = dt;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // �O���b�g�r���[�E�[�Ƀ{�^����ǉ��i����̂݁j
            if (load) addButtonsToDataGridView(dgv);

            //�s�w�b�_�[�ɍs�ԍ���\������
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
            //�s�w�b�_�[�̕����������߂���
            dgv.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            // ��ԉ��̍s��\������
            if (dgv.Rows.Count != 0)
                dgv.FirstDisplayedScrollingRowIndex = dgv.Rows.Count - 1;
         }
        
        // �T�u�T�u�v���V�[�W���F�O���b�g�r���[�E�[�Ƀ{�^����ǉ�
        private void addButtonsToDataGridView(DataGridView dgv)
        {
            // �X�V�E�u���E�o�ד��ҏW�̌����́A�S�Ẵ��[�U�[�ɗ^����
            updateId = new DataGridViewButtonColumn();
            updateId.HeaderText = "Invoice";
            updateId.Text = "Update";
            updateId.UseColumnTextForButtonValue = true;
            updateId.Width = 80;
            dgv.Columns.Add(updateId);

            replaceId = new DataGridViewButtonColumn();
            replaceId.HeaderText = "BoxId";
            replaceId.Text = "Replace";
            replaceId.UseColumnTextForButtonValue = true;
            replaceId.Width = 80;
            dgv.Columns.Add(replaceId);

            editShip = new DataGridViewButtonColumn();
            editShip.HeaderText = "Ship";
            editShip.Text = "Ship";
            editShip.UseColumnTextForButtonValue = true;
            editShip.Width = 80;
            dgv.Columns.Add(editShip);
        }
                
        // �{�^���P�̓t�H�[���X���Q�o�^���[�h�ŊJ���i�f���Q�[�g����j�A�{�^���Q�͏o�ד��̕ҏW
        private void dgvBoxId_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int currentRow = int.Parse(e.RowIndex.ToString());

            if (dgvCartonId.Columns[e.ColumnIndex] == updateId && currentRow >= 0)
            {
                //����frmModule ���J����Ă���ꍇ�́A��������
                TfGeneral.closeOpenForm("frmCartonContent");

                string cartonId = dgvCartonId["cartonid", currentRow].Value.ToString();
                string boxId1 = dgvCartonId["boxid1", currentRow].Value.ToString();
                string boxId2 = dgvCartonId["boxid2", currentRow].Value.ToString();
                DateTime packDate = DateTime.Parse(dgvCartonId["packdate", currentRow].Value.ToString());
                string user = dgvCartonId["suser", currentRow].Value.ToString();
                string invoice = dgvCartonId["invoice", currentRow].Value.ToString();
                string cartonNo = dgvCartonId["invctnno", currentRow].Value.ToString();

                frmCartonContent f9 = new frmCartonContent();
                //�q�C�x���g���L���b�`���āA�f�[�^�O���b�h���X�V����
                f9.RefreshEvent += delegate(object sndr, EventArgs excp)
                {
                    updateDataGridView(ref dgvCartonId, dtCarton, false);
                    this.Focus();
                };
                f9.Show();
                f9.updateControls(cartonId, boxId1, boxId2, packDate, user, invoice, cartonNo, "Step2", "Update");
            }

            if (dgvCartonId.Columns[e.ColumnIndex] == replaceId && currentRow >= 0)
            {
                //����frmModule ���J����Ă���ꍇ�́A��������
                TfGeneral.closeOpenForm("frmCartonContent");

                string cartonId = dgvCartonId["cartonid", currentRow].Value.ToString();
                string boxId1 = dgvCartonId["boxid1", currentRow].Value.ToString();
                string boxId2 = dgvCartonId["boxid2", currentRow].Value.ToString();
                DateTime packDate = DateTime.Parse(dgvCartonId["packdate", currentRow].Value.ToString());
                string user = dgvCartonId["suser", currentRow].Value.ToString();
                string invoice = dgvCartonId["invoice", currentRow].Value.ToString();
                string cartonNo = dgvCartonId["invctnno", currentRow].Value.ToString();

                frmCartonContent f9 = new frmCartonContent();
                //�q�C�x���g���L���b�`���āA�f�[�^�O���b�h���X�V����
                f9.RefreshEvent += delegate(object sndr, EventArgs excp)
                {
                    updateDataGridView(ref dgvCartonId, dtCarton, false);
                    this.Focus();
                };
                f9.Show();
                f9.updateControls(cartonId, boxId1, boxId2, packDate, user, invoice, cartonNo, "Step1", "Replace");
            }

            if (dgvCartonId.Columns[e.ColumnIndex] == editShip && currentRow >= 0)
            {
                string cartonId = dgvCartonId["cartonid", currentRow].Value.ToString();
                DateTime shipdate = dtpShipDate.Value;

                DialogResult result = MessageBox.Show("Do you want to update the shipping date of as follows:" + System.Environment.NewLine +
                    cartonId + ": " + shipdate, "Notice",MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    string sql = "update carton_id SET pcshipdate ='" + shipdate + "' " +
                        "WHERE cartonid = '" + cartonId + "'";
                    System.Diagnostics.Debug.Print(sql);
                    TfSQL tf = new TfSQL();
                    int res = tf.sqlExecuteNonQueryInt(sql, false);
                    updateDataGridView(ref dgvCartonId, dtCarton, false);
                }
            }
        }

        // �����{�^�������A���ۂ̓O���b�g�r���[�̍X�V�����邾��
        private void btnSearchBoxId_Click(object sender, EventArgs e)
        {
            updateDataGridView(ref dgvCartonId, dtCarton, false);
        }

        // �t�H�[���X������o�^���[�h�ŊJ���A�f���Q�[�g����
        private void btnAddCartonId_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text;

            bool bl = TfGeneral.checkOpenFormExists("frmCartonContent");
            if (bl)
            {
                MessageBox.Show("Please close the current Carton Content form.", "Notice",
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            }
            else
            {
                frmCartonContent f9 = new frmCartonContent();
                //�q�C�x���g���L���b�`���āA�f�[�^�O���b�h���X�V����
                f9.RefreshEvent += delegate(object sndr, EventArgs excp)
                {
                    updateDataGridView(ref dgvCartonId, dtCarton, false);
                    this.Focus();
                };

                f9.Show();
                f9.updateControls(String.Empty, String.Empty, String.Empty, DateTime.Now, user, String.Empty, String.Empty, "Step1", "Add");
            }
        }
        
        // �o�ד����ꊇ�o�^����
        private void btnEditShipping_Click(object sender, EventArgs e)
        {
            string idFrom = txtCtnIdFrom.Text;
            string idTo = txtCtnIdTo.Text;
            DateTime shipdate = dtpShipDate.Value;

            if (idFrom == String.Empty || idTo == String.Empty)
            {
                MessageBox.Show("Both box-id-from and box-id-to, plus ship date have to be selected.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }
            else
            {
                DialogResult result1 = MessageBox.Show("Have you slected carton-id-from, carton-id-to, and shipdate correctly?", "Notice",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
             
                if (result1 == DialogResult.Yes)
                {
                    DialogResult result2 = MessageBox.Show("Are you really sure to update the ship date?", "Notice",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result2 == DialogResult.Yes)
                    {
                        string sql = "update carton_id SET pcshipdate = '" + shipdate + "' " +
                            "WHERE cartonid BETWEEN '" + idFrom + "' AND '" + idTo + "'";
                            TfSQL tf = new TfSQL();
                            int res = tf.sqlExecuteNonQueryInt(sql, false);
                            MessageBox.Show(res + " records were updated", "Notice",
                            MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                            rdbShipDate.Checked = true;
                            updateDataGridView(ref dgvCartonId, dtCarton, false);
                    }
                }
            }
        }

        //frmCarton�����ہA��\���ɂȂ��Ă���e�t�H�[��frmLogin�����
        private void frmCarton_FormClosed(object sender, FormClosedEventArgs e)
        {
            //�e�t�H�[��frmLogin�����悤�A�f���Q�[�g�C�x���g�𔭐�������
            this.RefreshEvent(this, new EventArgs());
        }

        // ����{�^����V���[�g�J�b�g�ł̏I���������Ȃ�
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x112;
            const long SC_CLOSE = 0xF060L;
            if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE) { return; }
            base.WndProc(ref m);
        }

        // �t�H�[���R���J����Ă��Ȃ����Ƃ��m�F���Ă���A����
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string formName = "frmCartonContent";
            bool bl = false;
            foreach (Form buff in Application.OpenForms)
            {
                if (buff.Name == formName) { bl = true; }
            }
            if (bl) 
            {
                MessageBox.Show("You need to close Form Carton Content first.", "Notice",
                  MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                return;
            }
            Close();
        }

        // �T�u�T�u�v���V�[�W���F�c�`�s�d�s�h�l�d�o�h�b�j�d�q�̕��ȉ���������
        private void dtpRounddownHour(DateTimePicker dtp)
        {
            DateTime dt = dtp.Value;
            int hour = dt.Hour;
            int minute = dt.Minute;
            int second = dt.Second;
            int millisecond = dt.Millisecond;
            dtp.Value = dt.AddHours(-hour).AddMinutes(-minute).AddSeconds(-second).AddMilliseconds(-millisecond);
        }

        // �f�[�^���G�N�Z���փG�N�X�|�[�g
        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)dgvCartonId.DataSource;
            ExcelClass xl = new ExcelClass();
            xl.ExportToExcel(dt);
            //xl.ExportToCsv(dt, System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\ipqcdb.csv");
        }

        // ���W�I�{�^���u�J�[�g���h�c�v�ύX���̏���
        private void rdbCartonId_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCartonId.Checked)
            {
                txtCtnIdTo.Text = String.Empty;
                txtBoxId.Text = String.Empty;
                txtInvoice.Text = String.Empty;
                txtProductSerial.Text = String.Empty;
            }
        }
        // ���W�I�{�^���u������v�ύX���̏���
        private void rdbPackDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPackDate.Checked)
            {
                txtCtnIdFrom.Text = String.Empty;
                txtCtnIdTo.Text = String.Empty;
                txtBoxId.Text = String.Empty;
                txtInvoice.Text = String.Empty;
                txtProductSerial.Text = String.Empty;
            }
        }
        // ���W�I�{�^���u�{�b�N�X�h�c�v�ύX���̏���
        private void rdbBoxId_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbBoxId.Checked)
            {
                txtCtnIdFrom.Text = String.Empty;
                txtCtnIdTo.Text = String.Empty;
                txtInvoice.Text = String.Empty;
                txtProductSerial.Text = String.Empty;
            }
        }
        // ���W�I�{�^���u�C���{�C�X�ԍ��v�ύX���̏���
        private void rdbInvoice_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbInvoice.Checked)
            {
                txtCtnIdFrom.Text = String.Empty;
                txtCtnIdTo.Text = String.Empty;
                txtBoxId.Text = String.Empty;
                txtProductSerial.Text = String.Empty;
            }
        }
        // ���W�I�{�^���u�o�ד��v�ύX���̏���
        private void rdbShipDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbShipDate.Checked)
            {
                txtCtnIdFrom.Text = String.Empty;
                txtCtnIdTo.Text = String.Empty;
                txtBoxId.Text = String.Empty;
                txtInvoice.Text = String.Empty;
                txtProductSerial.Text = String.Empty;
            }
        }
        // ���W�I�{�^���u���i�V���A���v�ύX���̏���
        private void rdbSerno_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSerno.Checked)
            {
                txtCtnIdFrom.Text = String.Empty;
                txtCtnIdTo.Text = String.Empty;
                txtBoxId.Text = String.Empty;
                txtInvoice.Text = String.Empty;
            }
        }
    }
}