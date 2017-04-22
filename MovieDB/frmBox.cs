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
    public partial class frmBox : Form
    {
        //�e�t�H�[��frmLogin�ցA�C�x���g������A���i�f���Q�[�g�j
        public delegate void RefreshEventHandler(object sender, EventArgs e);
        public event RefreshEventHandler RefreshEvent;

        //�f�[�^�O���b�h�r���[�p�{�^��
        DataGridViewButtonColumn openBoxId;
        DataGridViewButtonColumn editShipDate;

        //���̑��񃍁[�J���ϐ�

        // �R���X�g���N�^
        public frmBox()
        {
            InitializeComponent();
        }

        // ���[�h���̏���
        private void frmBox_Load(object sender, EventArgs e)
        {
            //�t�H�[���̏ꏊ���w��
            this.Left = 50;
            this.Top = 10;
            updateDataGridViews(ref dgvBoxId, true);

            // �c�`�s�d�s�h�l�d�o�h�b�j�d�q�̕��ȉ���������
            dtpRounddownHour(dtpShipDate);

            if (txtUser.Text == "User_9")
            {
                btnAddReturn.Enabled = true;
                txtBoxIdTo.Enabled = true;
            }
            else
            {
                btnAddReturn.Enabled = false;
                txtBoxIdTo.Enabled = false;
            }
        }

        // �T�u�v���V�[�W���F�f�[�^�O���b�g�r���[�̍X�V�B�e�t�H�[���ŌĂяo���A�e�t�H�[���̏��������p��
        public void updateControls(string user)
        {
            txtUser.Text = user;
        }

        // �T�u�v���V�[�W���F�f�[�^�e�[�u���̒�`
        private void defineAndReadDatatable(ref DataTable dt)
        {
            dt.Columns.Add("boxid", Type.GetType("System.String"));
            dt.Columns.Add("shaft", Type.GetType("System.String"));
            dt.Columns.Add("over_lay", Type.GetType("System.String"));
            dt.Columns.Add("suser", Type.GetType("System.String"));
            dt.Columns.Add("printdate", Type.GetType("System.DateTime"));
            dt.Columns.Add("shipdate", Type.GetType("System.DateTime"));
            dt.Columns.Add("return", Type.GetType("System.String"));
        }


        // �T�u�v���V�[�W���F�f�[�^�O���b�g�r���[�̍X�V
        public void updateDataGridViews(ref DataGridView dgv, bool load)
        {
            string boxId = txtBoxIdFrom.Text;
            DateTime printDate = dtpPrintDate.Value;
            DateTime shipDate = dtpShipDate.Value;
            string serialNo = txtProductSerial.Text;
            string sql = String.Empty;

            // �r�p�k���ʂ��A�c�s�`�`�s�`�a�k�d�֊i�[
            TfSQL tf = new TfSQL();
            if (rdbBoxId.Checked)
            {
                if (boxId.Length < 6)
                {
                    MessageBox.Show("Please select at least 6 characters like LM1601", "BoxId DB",
                        MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    return;
                }

                sql = "select boxid, shaft, over_lay, suser, printdate, shipdate, return FROM v_box_id_rt" +
                    (boxId == String.Empty ? String.Empty : " WHERE boxid like '" + boxId + "%'") +
                    " order by boxid";
            }
            else if(rdbPrintDate.Checked)
            {
                sql = "select boxid, shaft, over_lay, suser, printdate, shipdate, return FROM v_box_id_rt WHERE printdate " +
                    "BETWEEN '" + printDate.Date + "' AND '" + printDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59) + "'" +
                    " order by boxid";
            }
            else if (rdbProductSerial.Checked)
            {
                sql = "select boxid FROM product_serial_rt WHERE serialno='" + serialNo + "'";
                boxId = tf.sqlExecuteScalarString(sql);
                txtBoxIdFrom.Text = boxId;
                if (boxId == String.Empty)
                {
                    sql = "select boxid, shaft, over_lay, suser, printdate, shipdate, return FROM v_box_id_rt WHERE printdate " +
                        "BETWEEN '" + printDate.Date + "' AND '" + printDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59) + "'" +
                        " order by boxid";
                }
                else
                {
                    sql = "select boxid, shaft, over_lay, suser, printdate, shipdate, return FROM v_box_id_rt" + 
                        " WHERE boxid='" + boxId + "'";
                }
            }
            else if (dtpShipDate.Checked)
            {
                sql = "select boxid, shaft, over_lay, suser, printdate, shipdate, return FROM v_box_id_rt WHERE shipdate " +
                    "BETWEEN '" + shipDate.Date + "' AND '" + shipDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59) + "'" +
                    " order by boxid";
            }

            DataTable dt1 = new DataTable();
            tf.sqlDataAdapterFillDatatable(sql, ref dt1);

            // �f�[�^�O���b�g�r���[�ւc�s�`�`�s�`�a�k�d���i�[
            dgv.DataSource = dt1;
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

            // �p�l���Ƀo�[�R�[�h��\��
            pnlBarcode.Refresh();
         }
        
        // �T�u�T�u�v���V�[�W���F�O���b�g�r���[�E�[�Ƀ{�^����ǉ�
        private void addButtonsToDataGridView(DataGridView dgv)
        {
            // �J���{�^���͑S�Ẵ��[�U�[
            openBoxId = new DataGridViewButtonColumn();
            openBoxId.HeaderText = "Open";
            openBoxId.Text = "Open";
            openBoxId.UseColumnTextForButtonValue = true;
            openBoxId.Width = 80;
            dgv.Columns.Add(openBoxId);

            // �o�ד��ҏW�́A����̃��[�U�[
            if (txtUser.Text == "User_9")
            {
                editShipDate = new DataGridViewButtonColumn();
                editShipDate.HeaderText = "Ship";
                editShipDate.Text = "Ship";
                editShipDate.UseColumnTextForButtonValue = true;
                editShipDate.Width = 80;
                dgv.Columns.Add(editShipDate);
            }
        }
                
        // �{�^���P�̓t�H�[���R���{�����[�h�ŊJ���i�f���Q�[�g�Ȃ��j�A�{�^���Q�͏o�ד��̕ҏW
        private void dgvBoxId_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int currentRow = int.Parse(e.RowIndex.ToString());

            if (dgvBoxId.Columns[e.ColumnIndex] == openBoxId && currentRow >= 0)
            {
                //����frmModule ���J����Ă���ꍇ�́A�������悤�ɑ���
                bool inUse = TfGeneral.checkOpenFormExists("frmModule");
                if (inUse)
                {
                    MessageBox.Show("Please close the other already open window.", "Notice",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    return;
                }
           
                string boxId = dgvBoxId["boxid", currentRow].Value.ToString();
                DateTime printDate = DateTime.Parse(dgvBoxId["printdate", currentRow].Value.ToString());
                string serialNo = txtProductSerial.Text;
                string shaft = dgvBoxId["shaft", currentRow].Value.ToString();
                string overlay = dgvBoxId["over_lay", currentRow].Value.ToString();
                string user = txtUser.Text;

                frmModule f3 = new frmModule();
                //�q�C�x���g���L���b�`���āA�f�[�^�O���b�h���X�V����
                f3.RefreshEvent += delegate (object sndr, EventArgs excp)
                {
                    updateDataGridViews(ref dgvBoxId, false);
                    this.Focus();
                };
                f3.updateControls(boxId, printDate, serialNo, shaft, overlay, user, false, false);
                f3.Show();
            }

            if (dgvBoxId.Columns[e.ColumnIndex] == editShipDate && currentRow >= 0)
            {
                string boxId = dgvBoxId["boxid", currentRow].Value.ToString();
                DateTime shipdate = dtpShipDate.Value;

                DialogResult result1 = MessageBox.Show("Do you want to update the shipping date of as follows:" + System.Environment.NewLine +
                    boxId + ": " + shipdate, "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result1 == DialogResult.Yes)
                {
                    string sql = "update box_id_rt SET shipdate ='" + shipdate + "' " +
                        "WHERE boxid= '" + boxId + "'";
                    System.Diagnostics.Debug.Print(sql);
                    TfSQL tf = new TfSQL();
                    int res = tf.sqlExecuteNonQueryInt(sql, false);
                    updateDataGridViews(ref dgvBoxId, false);
                }
            }
        }

        // �����{�^�������A���ۂ̓O���b�g�r���[�̍X�V�����邾��
        private void btnSearchBoxId_Click(object sender, EventArgs e)
        {
            updateDataGridViews(ref dgvBoxId, false);
        }

        // �t�H�[���R��ҏW���[�h�ŊJ���A�f���Q�[�g����
        private void btnAddBoxId_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text;

            bool bl = TfGeneral.checkOpenFormExists("frmModule");
            if (bl) 
            {
                MessageBox.Show("Please close brows-mode form or finish the current edit form.", "BoxId DB",
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            }
            else
            {
                frmModule f3 = new frmModule();
                //�q�C�x���g���L���b�`���āA�f�[�^�O���b�h���X�V����
                f3.RefreshEvent += delegate(object sndr, EventArgs excp) 
                {
                    updateDataGridViews(ref dgvBoxId, false);
                    this.Focus(); 
                };

                f3.updateControls(String.Empty, DateTime.Now, String.Empty, String.Empty, String.Empty, user, true, false);
                f3.Show();            
            }
        }

        // �ԕi�̓o�^�F�t�H�[���R��ҏW���[�h�ŊJ���A�f���Q�[�g����
        private void btnAddReturn_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text;

            bool bl = TfGeneral.checkOpenFormExists("frmModule");
            if (bl)
            {
                MessageBox.Show("Please close brows-mode form or finish the current edit form.", "BoxId DB",
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            }
            else
            {
                frmModule f3 = new frmModule();
                //�q�C�x���g���L���b�`���āA�f�[�^�O���b�h���X�V����
                f3.RefreshEvent += delegate(object sndr, EventArgs excp)
                {
                    updateDataGridViews(ref dgvBoxId, false);
                    this.Focus();
                };

                f3.updateControls(String.Empty, DateTime.Now, String.Empty, String.Empty, String.Empty, user, true, true);
                f3.Show();
            }
        }

        // �o�ד����ꊇ�o�^����
        private void btnEditShipping_Click(object sender, EventArgs e)
        {
            string idFrom = txtBoxIdFrom.Text;
            string idTo = txtBoxIdTo.Text;
            DateTime shipdate = dtpShipDate.Value;

            if (idFrom == String.Empty || idTo == String.Empty)
            {
                MessageBox.Show("Both box-id-from and box-id-to, plus ship date have to be selected.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }
            else
            {
                DialogResult result1 = MessageBox.Show("Have you slected box-id-from, box-id-to, and shipdate correctly?", "Notice",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result1 == DialogResult.Yes)
                {
                    DialogResult result2 = MessageBox.Show("Are you really sure to update the ship date?", "Notice",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result2 == DialogResult.Yes)
                    {
                        string sql = "update box_id_rt SET shipdate = '" + shipdate + "' " +
                            "WHERE boxid BETWEEN '" + idFrom + "' AND '" + idTo + "'";
                        TfSQL tf = new TfSQL();
                        int res = tf.sqlExecuteNonQueryInt(sql, false);
                        MessageBox.Show(res + " records were updated", "Notice",
                        MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                        rdbShipDate.Checked = true;
                        updateDataGridViews(ref dgvBoxId, false);
                    }
                }
            }
        }

        // �T�u�v���V�[�W���F�����p�̃o�[�R�[�h�\���p�l���̍X�V�A���ۂ̏o�͂Ƃ͊֌W�̂Ȃ����C�u�������g�p���Ă���
        private void pnlBarcode_Paint(object sender, PaintEventArgs e)
        {
            DotNetBarcode barCode = new DotNetBarcode();
            string barcodeNumber;
            Single x1;
            Single y1;
            Single x2;
            Single y2;
            x1 = 0;
            y1 = 0;
            x2 = pnlBarcode.Size.Width;
            y2 = pnlBarcode.Size.Height;
            barcodeNumber = txtBoxIdFrom.Text;
            barCode.Type = DotNetBarcode.Types.Jan13;

            if (barcodeNumber != String.Empty)
                barCode.WriteBar(barcodeNumber, x1, y1, x2, y2, e.Graphics);
        }

        //frmBox�����ہA��\���ɂȂ��Ă���e�t�H�[��frmLogin�����
        private void frmBox_FormClosed(object sender, FormClosedEventArgs e)
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
            string formName = "frmModule";
            bool bl = false;
            foreach (Form buff in Application.OpenForms)
            {
                if (buff.Name == formName) { bl = true; }
            }
            if (bl) 
            {
                MessageBox.Show("You need to close Form Product Serial first.", "BoxId DB",
                  MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                return;
            }
            Close();
        }

        // ���W�I�{�^���u�{�b�N�X�h�c�v�ύX���̏����i�e�L�X�g�{�b�N�X�ҏW�ɂ�錟�������̕ύX�j
        private void rdbBoxId_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbBoxId.Checked) { txtProductSerial.Text = String.Empty; }
        }
        // ���W�I�{�^���u�v�����g���t�v�ύX���̏����i�e�L�X�g�{�b�N�X�ҏW�ɂ�錟�������̕ύX�j
        private void rdbPrintDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPrintDate.Checked)
            {
                txtBoxIdFrom.Text = String.Empty;
                txtProductSerial.Text = String.Empty;
            }
        }
        // ���W�I�{�^���u���i�V���A���v�ύX���̏����i�e�L�X�g�{�b�N�X�ҏW�ɂ�錟�������̕ύX�j
        private void rdbProductSerial_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbProductSerial.Checked)
            {
                txtBoxIdFrom.Text = String.Empty;
            }
        }
        // ���W�I�{�^���u�o�ד��v�ύX���̏����i�e�L�X�g�{�b�N�X�ҏW�ɂ�錟�������̕ύX�j
        private void rdbShipDate_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rdbShipDate.Checked)
            {
                txtBoxIdFrom.Text = String.Empty;
                txtBoxIdTo.Text = String.Empty;
                txtProductSerial.Text = String.Empty;
            }
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
            dt = (DataTable)dgvBoxId.DataSource;
            ExcelClass xl = new ExcelClass();
            xl.ExportToExcel(dt);
            //xl.ExportToCsv(dt, System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\ipqcdb.csv");
        }
    }
}