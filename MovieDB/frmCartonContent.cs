using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Security.Permissions;
using System.Collections;
using Npgsql;
using System.Runtime.InteropServices;

namespace BoxIdDb
{
    public partial class frmCartonContent : Form
    {
        //�e�t�H�[��frmBox�փC�x���g������A���i�f���Q�[�g�j
        public delegate void RefreshEventHandler(object sender, EventArgs e);
        public event RefreshEventHandler RefreshEvent;

        //���̑��A�񃍁[�J���ϐ�
        DataTable dtBoxId;
        DataTable dtInvoice;
        DataTable dtModel;
        DataTable dtShaft;
        DataTable dtOverlay;
        DataTable dtLot;
        DataTable dtReturn;
        string stepForm;
        bool sound;
        bool firstBoxReplace = true;
        bool firstInvReplace = true;

        // �R���X�g���N�^
        public frmCartonContent()
        {
            InitializeComponent();
        }

        // ���[�h���̏���
        private void frmModule_Load(object sender, EventArgs e)
        {
            // ���t�H�[���̕\���ꏊ���w��
            this.Left = 300;
            this.Top = 20;
            
            // �e�폈���p�̃e�[�u���𐶐�
            dtBoxId = new DataTable();
            dtInvoice = new DataTable();
            dtModel = new DataTable();
            dtShaft = new DataTable();
            dtOverlay = new DataTable();
            dtLot = new DataTable();
            dtReturn = new DataTable();
            defineDatatables(ref dtBoxId, ref dtInvoice, ref dtModel, ref dtReturn);
        }

        // �T�u�v���V�[�W���F�e�t�H�[���ŌĂяo���A�e�t�H�[���̏����A�e�L�X�g�{�b�N�X�֊i�[���Ĉ����p��
        public void updateControls(string cartonId, string boxId1, string boxId2, 
            DateTime packDate, string user, string invoice, string cartonNo, string step, string command)
        {
            txtCartonId.Text = cartonId;
            dtBoxId.Rows[0]["boxid1"] = boxId1;
            dtBoxId.Rows[0]["boxid2"] = boxId2;
            dtpPackDate.Value = packDate;
            txtUser.Text = user;
            dtInvoice.Rows[0]["invoice"] = invoice;
            dtInvoice.Rows[0]["invctnno"] = cartonNo;
            stepForm = step;
            btnRegisterCartonId.Text = (command == "Replace" ? "Replace Carton" : "Register Carton");

            // �O���b�g�r���[�̍X�V
            updateDataGridViews(dtBoxId, ref dgvBoxId, dtInvoice, ref dgvInvoice);

            // ����u�����[�h�Ăяo�����́A�u���{�^���𖳌��ɂ���
            if (command == "Replace" && firstBoxReplace)
            {
                btnRegisterCartonId.Enabled = false;
                firstBoxReplace = false;
            }
            if (command == "Update" && firstInvReplace)
            {
                btnRegisterInvoice.Enabled = false;
                firstInvReplace = false;
            }
        }

        // �T�u�v���V�[�W���F�c�`�s�`�s�`�a�k�d�̒�`
        private void defineDatatables(ref DataTable dt1, ref DataTable dt2, ref DataTable dt3, ref DataTable dt4)
        {
            dt1.Columns.Add("boxid1", Type.GetType("System.String"));
            dt1.Columns.Add("boxid2", Type.GetType("System.String"));
            DataRow dr1 = dt1.NewRow();
            dt1.Rows.Add(dr1);

            dt2.Columns.Add("invoice", Type.GetType("System.String"));
            dt2.Columns.Add("invctnno", Type.GetType("System.String"));
            DataRow dr2 = dt2.NewRow();
            dt2.Rows.Add(dr2);

            dt3.Columns.Add("model", Type.GetType("System.String"));
            DataRow dr3 = dt3.NewRow();
            dt3.Rows.Add(dr3);

            dt4.Columns.Add("return", Type.GetType("System.String"));
            DataRow dr4 = dt4.NewRow();
            dt4.Rows.Add(dr4);
        }
        
        // �T�u�v���V�[�W���F�f�[�^�O���b�g�r���[�̍X�V
        private void updateDataGridViews(DataTable dt1, ref DataGridView dgv1, DataTable dt2, ref DataGridView dgv2)
        {
            //���C���f�[�^�O���b�g�r���[�փf�[�^�e�[�u�����i�[
            dgv1.DataSource = dt1;
            dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv2.DataSource = dt2;
            dgv2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // �W�v�O���b�h�r���[�̍쐬
            updateDataGridViewsSub(dt1);

            // �o�^�{�^���L���E�����̐؂�ւ��i�ÓI�R���g���[���X�V�j
            if (stepForm == "Step1")
            {
                btnPrint.Visible = false;
                txtBoxId.Enabled = true;
                btnRegisterCartonId.Enabled = true;
                btnClearBoxId.Enabled = true;
                txtInvoice.Enabled = false;
                btnRegisterInvoice.Enabled = false;
                btnClearInvoice.Enabled = false;
            }
            else if (stepForm == "Step2")
            {
                btnPrint.Visible = true;
                txtBoxId.Enabled = false;
                btnRegisterCartonId.Enabled = false;
                btnClearBoxId.Enabled = false;
                txtInvoice.Enabled = true;
                btnRegisterInvoice.Enabled = true;
                btnClearInvoice.Enabled = true;                
            }

            // �o�^�{�^���L���E�����̐؂�ւ��i���I�R���g���[���X�V�j
            if (stepForm == "Step1")
            {
                bool b1 = (dt1.Rows.Count >= 1 && dt1.Rows[0]["boxid1"].ToString() != String.Empty &&
                    dt1.Rows[0]["boxid2"].ToString() != String.Empty);
                btnRegisterCartonId.Enabled = b1;
                txtBoxId.Enabled = !b1;
            }
            else if (stepForm == "Step2")
            {
                bool b2 = (dt2.Rows.Count >= 1 && dt2.Rows[0]["invoice"].ToString() != String.Empty &&
                    dt2.Rows[0]["invctnno"].ToString() != String.Empty);
                btnRegisterInvoice.Enabled = b2;
                txtInvoice.Enabled = !b2;
            }
            
            // �C���{�C�X�̓o�^�́A�J�[�g���h�c�̓o�^��ɉ\
            if (txtCartonId.Text == String.Empty)
            {
                txtInvoice.Enabled = false;
                btnClearInvoice.Enabled = false;
            }
        }

        // �T�u�T�u�v���V�[�W���F�W�v�O���b�h�r���[�̍쐬
        private void updateDataGridViewsSub(DataTable dt1)
        {
            string boxid1 = dt1.Rows[0]["boxid1"].ToString();
            string boxid2 = dt1.Rows[0]["boxid2"].ToString();
            if (boxid1 == String.Empty) return;

            string model1 = VBS.Left(boxid1, 2);
            string model2 = VBS.Left(boxid2, 2);
            dtModel.Clear();
            if (model1 != String.Empty)
            {
                DataRow dr_m = dtModel.NewRow();
                bool error = (model1 != String.Empty && model2 != String.Empty && model1 != model2);
                if (error)
                {
                    dr_m["model"] = "Error";
                }
                else
                {
                    string m_long;
                    if (model1 == "LM") m_long = "LD4G-003L";
                    else if (model1 == "LN") m_long = "LD4G-001";
                    else if (model1 == "SM") m_long = "LD4F-003L";
                    else if (model1 == "SN") m_long = "LD4F-001";
                    else if (model1 == "LT") m_long = "LD4J-001";
                    else if (model1 == "ST") m_long = "LD4H-001";
                    else m_long = "Error";

                    dr_m["model"] = m_long;
                }

                dtModel.Rows.Add(dr_m);
                dgvModel.DataSource = dtModel;
                dgvModel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                colorDataGridViews(ref dgvModel, error);
            }

            string sql;
            TfSQL tf = new TfSQL();
           
            sql = "select shaft from (" +
                "(select a.shaft from box_id_rt as a where a.boxid = '" + boxid1 + "' limit 1) union " +
                "(select b.shaft from box_id_rt as b where b.boxid = '" + boxid2 + "' limit 1)) as c";
            dtShaft.Clear();
            System.Diagnostics.Debug.Print(sql);
            tf.sqlDataAdapterFillDatatable(sql, ref dtShaft);
            dgvShaft.DataSource = dtShaft;
            dgvShaft.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            colorDataGridViews(ref dgvShaft, dtShaft.Rows.Count >= 2);

            sql = "select over_lay from (" +
                "(select a.over_lay from box_id_rt as a where a.boxid = '" + boxid1 + "' limit 1) union " +
                "(select b.over_lay from box_id_rt as b where b.boxid = '" + boxid2 + "' limit 1)) as c";
            dtOverlay.Clear();
            System.Diagnostics.Debug.Print(sql);
            tf.sqlDataAdapterFillDatatable(sql, ref dtOverlay);
            dgvOverlay.DataSource = dtOverlay;
            dgvOverlay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            colorDataGridViews(ref dgvOverlay, dtOverlay.Rows.Count >= 2);

            sql = "select c.lot, sum(c.count) as count from (" + 
                "select a.lot, count(a.lot) as count from product_serial_rt as a where a.boxid = '" + boxid1 + "' group by a.lot union all " +
                "select b.lot, count(b.lot) as count from product_serial_rt as b where b.boxid = '" + boxid2 + "' group by b.lot) as c group by c.lot";
            System.Diagnostics.Debug.Print(sql);
            dtLot.Clear();
            System.Diagnostics.Debug.Print(sql);
            tf.sqlDataAdapterFillDatatable(sql, ref dtLot);
            DataRow dr_l = dtLot.NewRow();
            dr_l["lot"] = "sum";
            dr_l["count"] = dtLot.Compute("Sum(count)", null);
            dtLot.Rows.Add(dr_l);
            dgvLot.DataSource = dtLot;
            dgvLot.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            sql = "select return from (" +
                "(select a.return from product_serial_rt as a where a.boxid = '" + boxid1 + "' limit 1) union " +
                "(select b.return from product_serial_rt as b where b.boxid = '" + boxid2 + "'  limit 1)) as c";
            dtReturn.Clear();
            System.Diagnostics.Debug.Print(sql);
            tf.sqlDataAdapterFillDatatable(sql, ref dtReturn);
            dgvReturn.DataSource = dtReturn;
            dgvReturn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            colorDataGridViews(ref dgvReturn, dtReturn.Rows.Count >= 2);
        }

        // �T�u�T�u�v���V�[�W���F���f���A�V���t�g�A�n�u�d�q�k�`�x���d���̏ꍇ�Ɍx������
        private void colorDataGridViews(ref DataGridView dgv, bool error)
        {
            if (error)
            {
                dgv[0, 0].Style.BackColor = Color.Red;
                soundAlarm();
            }
            else
            {
                dgv[0, 0].Style.BackColor = Color.FromKnownColor(KnownColor.Window);
            }
        }

        // �T�u�T�u�v���V�[�W���F�W�v�p�̃f�[�^�e�[�u�����A�f�[�^�O���b�h�r���[�Ɋi�[
        public void makeDatatableSummary(DataTable dt0, ref DataGridView dgv, string[] criteria, string header)
        {
            DataTable dt1 = new DataTable();
            DataRow dr = dt1.NewRow();
            Int32 count;
            Int32 total = 0;
            string condition;

            for (int i = 0; i < criteria.Length; i++)
            {
                dt1.Columns.Add(criteria[i], typeof(Int32));
                condition = header + " = '" + criteria[i] + "'";
                count = dt0.Select(condition).Length;
                total += count;
                dr[criteria[i]] = (i != criteria.Length - 1  ? count : total);
                if (criteria[i] == "Total" && header == "judge")
                {
                    dr[criteria[i]] = dgvBoxId.Rows.Count;
                    dr[criteria[i - 1]] = dgvBoxId.Rows.Count - total;
                }
            }
            dt1.Rows.Add(dr);

            dgv.Columns.Clear();
            dgv.DataSource = dt1;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        // �T�u�T�u�v���V�[�W���Flot�̕�����z����擾
        private string[] getLotArray(DataTable dt0)
        {
            DataTable dt1 = dt0.Copy();
            DataView dv = dt1.DefaultView;
            dv.Sort = "lot";
            DataTable dt2 = dv.ToTable(true, "lot");
            string[] array = new string[dt2.Rows.Count + 1];
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                array[i] = dt2.Rows[i]["lot"].ToString();
            }
            array[dt2.Rows.Count] = "Total";
            return array;
        }

        // �a�n�w�h�c���X�L�������ꂽ���̏���
        private void txtBoxId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            string boxId = txtBoxId.Text;
            if(boxId == String.Empty) return;

            // �X�L�������ꂽ�l���a�n�w�h�c�}�X�^�ɓo�^�ς��ۂ��A�m�F����i�q�d�r�͖��Ȃ��ꍇ�s�q�t�d�j
            string sql = "SELECT boxid FROM box_id_rt WHERE boxid = '" + boxId + "'";
            
            TfSQL tf = new TfSQL();
            bool res = (tf.sqlExecuteScalarString(sql) != String.Empty);

            // �O���b�g�r���[�̂Q�̃Z���̂ǂ��瑤�ɕ\�����邩���f���A�K�؂ȃZ���ɕ\������
            bool b1 = (dtBoxId.Rows[0]["boxid1"].ToString() == String.Empty);
            bool b2 = (dtBoxId.Rows[0]["boxid2"].ToString() == String.Empty);

            if(b1) dtBoxId.Rows[0]["boxid1"] = boxId;
            else if (b2)@dtBoxId.Rows[0]["boxid2"] = boxId;

            // �Q�̃Z���̏d���𔻒f����
            bool duplicate = false;
            if (b2) duplicate = (dtBoxId.Rows[0]["boxid1"].ToString() == dtBoxId.Rows[0]["boxid2"].ToString());

            // �f�[�^�O���b�g�r���[�̍X�V
            updateDataGridViews(dtBoxId, ref dgvBoxId, dtInvoice, ref dgvInvoice);

            // �o�^�Ȃ��a�n�w�h�c����яd���a�n�w�h�c�����}�[�L���O���A�x�����b�Z�[�W��\������
            if (b1) colorViewForBoxIdError(ref dgvBoxId, 0, !res);
            else if (b2) colorViewForBoxIdError(ref dgvBoxId, 1, (!res || duplicate));

            if (!res) MessageBox.Show("The box id does not exist.", "Warning",
                 MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            // �A�����ăX�L�����ł���悤�A�e�L�X�g��I����Ԃɂ���
            txtBoxId.Focus();
            txtBoxId.SelectAll();    
        }

        // �T�u�v���V�[�W���F�o�^�Ȃ��a�n�w�h�c����яd���a�n�w�h�c�����}�[�L���O����
        private void colorViewForBoxIdError(ref DataGridView dgv, int column, bool color)
        {
            if(color)
            {
                dgv[column, 0].Style.BackColor = Color.Red;
                soundAlarm();
            }
            else
            {
                dgv[column, 0].Style.BackColor = Color.FromKnownColor(KnownColor.Window);
            }
        }

        // �C���{�C�X�ԍ����X�L�������ꂽ���̏���
        private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            string buff = txtInvoice.Text;
            if (buff == String.Empty) return;

            int split = buff.IndexOf(" ") + 1;
            int len = buff.Length;
            string invoice = VBS.Left(buff, split - 1);
            string carton = VBS.Mid(buff, split + 1, len - split);

            dtInvoice.Rows[0]["invoice"] = invoice;
            dtInvoice.Rows[0]["invctnno"] = carton;

            // �f�[�^�O���b�g�r���[�̍X�V
            updateDataGridViews(dtBoxId, ref dgvBoxId, dtInvoice, ref dgvInvoice);
        }

        // �r���[���[�h�ōĈ�����s��
        private void btnPrint_Click(object sender, EventArgs e)
        {
            printBigLabel();
        }

        // �T�u�v���V�[�W���F�����������x�����������
        private void printBigLabel()
        {
            // �e�X�g�p
            //string sql;
            //TfSQL tfsql = new TfSQL();
            //sql = "select c.lot, sum(c.count) as count from (" +
            //    "select a.lot, count(a.lot) as count from product_serial_rt as a where a.boxid = '" + "LM1512120001" + "' group by a.lot union all " +
            //    "select b.lot, count(b.lot) as count from product_serial_rt as b where b.boxid = '" + "LM1512120002" + "' group by b.lot) as c group by c.lot";
            //System.Diagnostics.Debug.Print(sql);
            //dtLot.Clear();
            //tfsql.sqlDataAdapterFillDatatable(sql, ref dtLot);
            //DataRow dr_l = dtLot.NewRow();
            //dr_l["lot"] = "sum";
            //dr_l["count"] = dtLot.Compute("Sum(count)", null);
            //dtLot.Rows.Add(dr_l);
            //dgvLot.DataSource = dtLot;
            //dgvLot.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            //string apn = "810-00631";
            //string qpn = "LY0103113B7";
            //string vpn = "Model";
            //string desc = "HAYWARD LRG NAT PSA";
            //string qty = "750";
            //string carton = "LN5557";
            //string stage = "FOR MP";
            //string shaft = "Mizukichi";
            //string overlay = "Oliver Ray";

            //if (cmbStage.Text == String.Empty)
            //{
            //    MessageBox.Show("Please select stage", "Note",
            //        MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            //    return;
            //}

            string[] vpnAry = { "LD4F-003L", "LD4F-001", "LD4G-003L", "LD4G-001", "LD4J-001", "LD4H-001" };
            string[] apnAry = { "810-00628", "810-00627", "810-00632", "810-00631", "610-00173", "610-00174" };
            string[] qpnAry = { "LY0103113B6", "LY0103113B5", "LY0103113B8", "LY0103113B7", "LY0103113X7", "LY0103113X8" };
            string[] descAry = { "HAYWARD,SML,MAT,PSA", "HAYWARD,SML,NAT,PSA", "HAYWARD,LRG,MAT,PSA", "HAYWARD,LRG,NAT,PSA", "ASSY,ALERT,SM,NIDEC,X583", "ASSY,ALERT,LG,NIDEC,X584" };

            string apn = "Error";
            string qpn = "Error";
            string vpn = "Error";
            string desc = "Error";
            string qty = dtLot.Rows[dtLot.Rows.Count - 1]["count"].ToString();
            string carton = txtCartonId.Text;
            string stage = cmbStage.Text;
            string shaft = dtShaft.Rows[0]["shaft"].ToString();
            string overlay = dtOverlay.Rows[0]["over_lay"].ToString();
            string rtn = dtReturn.Rows[0]["return"].ToString();

            for (int i = 0; i < vpnAry.Length; i++)
            {
                if (vpnAry[i] == dtModel.Rows[0]["model"].ToString())
                {
                    apn = apnAry[i];
                    qpn = qpnAry[i];
                    vpn = vpnAry[i];
                    desc = descAry[i];
                }
            }

            TfPrint tf = new TfPrint();
            if (cmbStage.Text == String.Empty)
            {
                tf.printBigBarcode(apn, qpn, vpn, desc, qty, carton, "", shaft, overlay, rtn, dtLot);
            }
            else
                tf.printBigBarcode(apn, qpn, vpn, desc, qty, carton, stage, shaft, overlay, rtn, dtLot);
        }

        // �{�b�N�X�h�c�̓o�^�A�J�[�g���h�c�̔��s�A�o�[�R�[�h���x���̃v�����g���s��
        private void btnRegisterBoxId_Click(object sender, EventArgs e)
        {
            // �ꎞ�e�[�u���̂a�n�w�h�c�Ăɂ��āA�c�a�e�[�u���Ɋ��ɓo�^���Ȃ����m�F���A�d����h�~����
            string checkResult = checkDuplicateBoxidWithHistory(dtBoxId);

            if (checkResult != String.Empty)
            {
                MessageBox.Show("The following box id are already registered with carton id:" + Environment.NewLine +
                    checkResult + Environment.NewLine + "Please check and delete.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                btnRegisterCartonId.Enabled = true;
                btnCancel.Enabled = true;
                return;
            }

            // �J�[�g���h�c�̐V�K�̔ԁA���̑��t�B�[���h�̓o�^
            string cartonIdNew = getNewCartonIdAndRegister(dtBoxId, btnRegisterCartonId.Text);
            txtCartonId.Text = cartonIdNew;
            dtpPackDate.Value = DateTime.Now;

            //�e�t�H�[��frmBox�̃f�[�^�O���b�g�r���[���X�V���邽�߁A�f���Q�[�g�C�x���g�𔭐�������
            this.RefreshEvent(this, new EventArgs());

            MessageBox.Show("The carton id was registered.", "Process Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Focus();

            // �f�[�^�O���b�g�r���[�̍X�V
            stepForm = "Step2";
            updateDataGridViews(dtBoxId, ref dgvBoxId, dtInvoice, ref dgvInvoice);

            // �o�[�R�[�h����
            printBigLabel();
        }

        // �C���{�C�X�ԍ��̓o�^���s��
        private void btnRegisterInvoice_Click(object sender, EventArgs e)
        {
            string cartonIdCurrent = txtCartonId.Text;
            string invoice = dtInvoice.Rows[0]["invoice"].ToString();
            string invctnno = dtInvoice.Rows[0]["invctnno"].ToString();
            string user = txtUser.Text;

            string sql = "UPDATE carton_id SET " +
                "invoice='" + invoice + "'," +
                "invctnno='" + invctnno + "'," +
                "suser='" + user + "' " +
                "WHERE cartonid='" + cartonIdCurrent + "'";
            System.Diagnostics.Debug.Print(sql);
            TfSQL tf = new TfSQL();
            tf.sqlExecuteNonQuery(sql, false);

            //�e�t�H�[��frmBox�̃f�[�^�O���b�g�r���[���X�V���邽�߁A�f���Q�[�g�C�x���g�𔭐�������
            this.RefreshEvent(this, new EventArgs());

            MessageBox.Show("The invoice information was registered.", "Process Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Focus();
        }

        // �T�u�v���V�[�W���F�f�[�^�e�[�u���̃V���A���S�Ăɂ��āA�{�e�[�u���ɓo�^���Ȃ����ꊇ�m�F
        private string checkDuplicateBoxidWithHistory(DataTable dt1)
        {
            string result = String.Empty;

            string sql = "select a.cartonid as cartonid, a.boxid1 as boxid from carton_id as a " +
                 "WHERE a.packdate BETWEEN '" + System.DateTime.Today.AddDays(-30) + "' AND '" + System.DateTime.Today.AddDays(1) + "' union " +
                 "select b.cartonid as cartonid, b.boxid2 as boxid from carton_id as b " +
                 "WHERE b.packdate BETWEEN '" + System.DateTime.Today.AddDays(-30) + "' AND '" + System.DateTime.Today.AddDays(1) + "'";
            
            DataTable dt2 = new DataTable();
            TfSQL tf = new TfSQL();
            tf.sqlDataAdapterFillDatatable(sql, ref dt2);

            for (int i = 0; i < dt1.Columns.Count; i++)
            {
                string boxId = dt1.Rows[0][i].ToString();
                DataRow[] dr = dt2.Select("boxid = '" + boxId + "'");
                if (dr.Length >= 1)
                {
                    string boxid = dr[0]["boxid"].ToString();
                    string cartonid = dr[0]["cartonid"].ToString();
                    result += (i + 1 + ": " + boxid + " / " + cartonid + Environment.NewLine);
                }
            }

            if (result == String.Empty)
            {
                return String.Empty;
            }
            else
            {
                return result;
            }
        }

        // �T�u�v���V�[�W���F���ꂩ�甭�s����J�[�g���h�c�̍̔ԂƁA���̑��t�B�[���h�̓o�^
        private string getNewCartonIdAndRegister(DataTable dt,  string sqltype)
        {
            string cartonIdCurrent = txtCartonId.Text;
            string user = txtUser.Text;
            string boxid1 = dt.Rows[0]["boxid1"].ToString();
            string boxid2 = dt.Rows[0]["boxid2"].ToString();
            string model = VBS.Left(boxid1, 2);
            string sql = String.Empty;
            TfSQL tf = new TfSQL();

            if (sqltype == "Register Carton")
            {
                sql = "select MAX(cartonid) FROM carton_id where cartonid like '" + model + "%'";
                string cartonIdOld = tf.sqlExecuteScalarString(sql);

                String monthOld = String.Empty;
                String monthToday = DateTime.Today.ToString("yyMM");
                long numberOld = 0;
                string cartonIdNew;

                if (cartonIdOld != String.Empty)
                {
                    monthOld = VBS.Mid(cartonIdOld, 3, 4);
                    numberOld = long.Parse(VBS.Right(cartonIdOld, 5));
                }

                if (monthOld != monthToday)
                {
                    cartonIdNew = model + monthToday + "00001";
                }
                else
                {
                    cartonIdNew = model + monthToday + (numberOld + 1).ToString("00000");
                }

                sql = "INSERT INTO carton_id(" +
                    "cartonid," +
                    "boxid1," +
                    "boxid2," +
                    "suser," +
                    "packdate) " +
                    "VALUES(" +
                    "'" + cartonIdNew + "'," +
                    "'" + boxid1 + "'," +
                    "'" + boxid2 + "'," +
                    "'" + user + "'," +
                    "'" + DateTime.Now.ToString() + "')";

                tf.sqlExecuteNonQuery(sql, false);
                return cartonIdNew;
            }

            else if (sqltype == "Replace Carton")
            {
                sql = "UPDATE carton_id SET " +
                    "boxid1='" + boxid1 + "'," +
                    "boxid2='" + boxid2 + "'," +
                    "suser='" + user + "'," +
                    "packdate='" + DateTime.Now.ToString() + "' " +
                    "WHERE cartonid='" + cartonIdCurrent + "'";

                System.Diagnostics.Debug.Print(sql);
                tf.sqlExecuteNonQuery(sql, false);
                return cartonIdCurrent;
            }
            else
            {
                return cartonIdCurrent;
            }
        }

        // ����{�^����V���[�g�J�b�g�ł̏I���������Ȃ�
        //[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        //protected override void WndProc(ref Message m)
        //{
        //    const int WM_SYSCOMMAND = 0x112;
        //    const long SC_CLOSE = 0xF060L;
        //    if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE) { return; }
        //    base.WndProc(ref m);
        //}

        // �Q�̃{�b�N�X�h�c���N���A
        private void btnClearBoxId_Click(object sender, EventArgs e)
        {
            dtBoxId.Rows[0]["boxid1"] = String.Empty;
            dtBoxId.Rows[0]["boxid2"] = String.Empty;
            updateDataGridViews(dtBoxId, ref dgvBoxId, dtInvoice, ref dgvInvoice);
            dgvBoxId[0, 0].Style.BackColor = Color.FromKnownColor(KnownColor.Window);
            dgvBoxId[1, 0].Style.BackColor = Color.FromKnownColor(KnownColor.Window);
            dgvModel.DataSource = "";
            dgvShaft.DataSource = "";
            dgvOverlay.DataSource = "";
            dgvLot.DataSource = "";
        }

        // �C���{�C�X�ԍ����N���A
        private void btnClearInvoice_Click(object sender, EventArgs e)
        {
            dtInvoice.Rows[0]["invoice"] = String.Empty;
            dtInvoice.Rows[0]["invctnno"] = String.Empty;
            updateDataGridViews(dtBoxId, ref dgvBoxId, dtInvoice, ref dgvInvoice);
        }

        //MP3�t�@�C���i����͌x�����j���Đ�����
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern int mciSendString(String command,
           StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

        private string aliasName = "MediaFile";

        private void soundAlarm()
        {
            string currentDir = System.Environment.CurrentDirectory;
            string fileName = currentDir + @"\warning.mp3";
            string cmd;

            if (sound)
            {
                cmd = "stop " + aliasName;
                mciSendString(cmd, null, 0, IntPtr.Zero);
                cmd = "close " + aliasName;
                mciSendString(cmd, null, 0, IntPtr.Zero);
                sound = false;
            }

            cmd = "open \"" + fileName + "\" type mpegvideo alias " + aliasName;
            if (mciSendString(cmd, null, 0, IntPtr.Zero) != 0) return;
            cmd = "play " + aliasName;
            mciSendString(cmd, null, 0, IntPtr.Zero);
            sound = true;
        }

        // ���̃t�H�[���ƊO�ς����낦�邽�߁A�L�����Z���{�^����ݒu����
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}