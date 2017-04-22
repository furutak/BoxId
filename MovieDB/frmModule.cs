using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Linq;


namespace BoxIdDb
{
    public partial class frmModule : Form
    {
        //�e�t�H�[��frmBox�փC�x���g������A���i�f���Q�[�g�j
        public delegate void RefreshEventHandler(object sender, EventArgs e);
        public event RefreshEventHandler RefreshEvent;

        // �v�����g�p�e�L�X�g�t�@�C���̕ۑ��p�t�H���_���A��{�ݒ�t�@�C���Őݒ肷��
        string appconfig = System.Environment.CurrentDirectory + "\\info.ini";
        string directory = @"C:\Users\takusuke.fujii\Desktop\Auto Print\\";

        //���̑��A�񃍁[�J���ϐ�
        bool formEditMode;
        bool formReturnMode;
        bool formAddMode;
        string user;
        string m_eeee;
        string m_model;
        string m_model_long;
        int okCount;
        bool inputBoxModeOriginal;
        string testerTableThisMonth;
        string testerTableLastMonth;
        DataTable dtOverall;
        DataTable dtOqc;
        //DataTable dtTester;
        int limit1 = 9999;
        public int limit2 = 0;
        int limitHw1Large;
        int limitHw1Small;
        int limitHw2Large;
        int limitHw2Small, limitHw3Small, limitHw3Large;
        int updateRowIndex;
        bool sound;
        string fltHw1Line = "lot not like '%CA%' and lot not like '%REL%'";
        string fltHw1Oqc = "lot like '%CA%' and lot not like '%REL%'";
        string fltHw2Line = "lot like '%DEVELOPMENT4' or lot like '%TAP-CAL'";
        string fltHw2Oqc = "lot like '%DEVELOPMENT6' or lot like '%TAP-CAL-OQC' or lot like '%TAP-OQC'";
        string fltHw3Line = "lot like '%DEVELOPMENT4'";
        string fltHw3Oqc = "lot like '%DEVELOPMENT5'";

        // �R���X�g���N�^
        public frmModule()
        {
            InitializeComponent();
        }

        // ���[�h���̏���
        private void frmModule_Load(object sender, EventArgs e)
        {
            // �ҏW���[�h�p���[�U�[����ێ�����
            user = txtUser.Text;

            // ���[�U�[�X���ݒ肷��k�h�l�h�s���A�e�L�X�g�{�b�N�X�֕\��
            txtLimit.Text = limit2.ToString();

            // �v�����g�p�t�@�C���̕ۑ���t�H���_�A���̑��ݒ���A�ǂݍ���
            directory = readIni("TARGET DIRECTORY", "DIR", appconfig);
            fltHw1Line = readIni("TESTER FILTER", "HW1INLINE", appconfig);
            fltHw1Oqc = readIni("TESTER FILTER", "HW1OQC", appconfig);
            fltHw2Line = readIni("TESTER FILTER", "HW2INLINE", appconfig);
            fltHw2Oqc = readIni("TESTER FILTER", "HW2OQC", appconfig);
            fltHw3Line = readIni("TESTER FILTER", "HW3INLINE", appconfig);
            fltHw3Oqc = readIni("TESTER FILTER", "HW3OQC", appconfig);
            int.TryParse(readIni("BOX CAPACITY", "HW1 LARGE", appconfig),out limitHw1Large);
            int.TryParse(readIni("BOX CAPACITY", "HW1 SMALL", appconfig), out limitHw1Small);
            int.TryParse(readIni("BOX CAPACITY", "HW2 LARGE", appconfig), out limitHw2Large);
            int.TryParse(readIni("BOX CAPACITY", "HW2 SMALL", appconfig), out limitHw2Small);
            int.TryParse(readIni("BOX CAPACITY", "HW3 LARGE", appconfig), out limitHw3Large);
            int.TryParse(readIni("BOX CAPACITY", "HW3 SMALL", appconfig), out limitHw3Small);

            // ���t�H�[���̕\���ꏊ���w��
            this.Left = 250;
            this.Top = 20;

            // �e�폈���p�̃e�[�u���𐶐�
            dtOverall = new DataTable();
            defineAndReadDtOverall(ref dtOverall);
            dtOqc = new DataTable();
            definedtOqc(ref dtOqc);
            //dtTester = new DataTable();
            //defineAndReaddtTester(ref dtTester);

            // �k�h�l�h�s�̐������Œ����K�v����
            if (!formEditMode)
            {
                // �f�[�^�e�[�u���̐擪�s�̃V���A������A�k�h�l�h�s�𔻒f����
                if (dtOverall.Rows.Count >= 0)
                {
                    string m = dtOverall.Rows[0]["model"].ToString();
                    if (m == "LM" || m == "LN") limit1 = 850;
                    else if (m == "SM" || m == "SN") limit1 = 1560;
                    else if (m == "LT") limit1 = 1560;
                    else if (m == "ST") limit1 = 1530;
                    else if (m == "L3") limit1 = 1500;
                    else if (m == "S3") limit1 = 1500;
                    else limit1 = 9999;
                }
            }

            // �O���b�g�r���[�̍X�V
            updateDataGridViews(dtOverall, dtOqc, ref dgvInline, ref dgvOqc);

            // �V���A���p�e�L�X�g�{�b�N�X�̐������Œ����K�v����
            if (!formEditMode)
            {
                txtProductSerial.Enabled = false;
            }
        }

        // �ݒ�e�L�X�g�t�@�C���̓ǂݍ���
        private string readIni(string s, string k, string cfs)
        {
            StringBuilder retVal = new StringBuilder(255);
            string section = s;
            string key = k;
            string def = String.Empty;
            int size = 255;
            //get the value from the key in section
            int strref = GetPrivateProfileString(section, key, def, retVal, size, cfs);
            return retVal.ToString();
        }
        // Windows API ���C���|�[�g
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filepath);

        // �T�u�v���V�[�W���F�e�t�H�[���ŌĂяo���A�e�t�H�[���̏����A�e�L�X�g�{�b�N�X�֊i�[���Ĉ����p��
        public void updateControls(string boxId, DateTime printDate, string serialNo, string shaft, string overlay, string user, bool editMode, bool returnMode)
        {
            txtBoxId.Text = boxId;
            dtpPrintDate.Value = printDate;
            txtProductSerial.Text = serialNo;
            cmbShaft.Text = shaft;
            cmbOverlay.Text = overlay;
            txtUser.Text = user;

            txtProductSerial.Enabled = editMode;
            cmbShaft.Enabled = editMode;
            cmbOverlay.Enabled = editMode;
            btnPrint.Visible = !editMode;
            btnRegisterBoxId.Enabled = !editMode;
            btnDeleteSelection.Visible = editMode;
            formEditMode = editMode;
            formReturnMode = returnMode;

            this.Text = editMode ? "Product Serial - Edit Mode" : "Product Serial - Browse Mode";
            if (editMode && user == "User_9")
            {
                btnChangeLimit.Visible = true;
                txtLimit.Visible = true;
            }
            if (!editMode && user == "User_9")
            {
                btnAddSerial.Visible = true;
                btnCancelBoxid.Visible = true;
                btnDeleteSerial.Visible = true;
            }
        }

        // �T�u�v���V�[�W���F�c�a����̂c�s�n�u�d�q�`�k�k�ւ̓ǂݍ���
        private void defineAndReadDtOverall(ref DataTable dt)
        {
            string boxId = txtBoxId.Text;

            dt.Columns.Add("serialno", Type.GetType("System.String"));
            dt.Columns.Add("model", Type.GetType("System.String"));
            dt.Columns.Add("datecd", Type.GetType("System.String"));
            dt.Columns.Add("line", Type.GetType("System.String"));
            dt.Columns.Add("lot", Type.GetType("System.String"));
            dt.Columns.Add("eeee", Type.GetType("System.String"));
            dt.Columns.Add("stationid", Type.GetType("System.String"));
            dt.Columns.Add("judge", Type.GetType("System.String"));
            dt.Columns.Add("testtime", Type.GetType("System.DateTime"));
            dt.Columns.Add("return", Type.GetType("System.String"));

            if (!formEditMode)
            {
                string sql = "select serialno, model, datecd, line, lot, eeee, stationid, judge, testtime, return " +
                    "FROM product_serial_rt WHERE boxid='" + boxId + "'";
                TfSQL tf = new TfSQL();
                System.Diagnostics.Debug.Print(sql);
                tf.sqlDataAdapterFillDatatable(sql, ref dt);
            }
        }

        // �T�u�v���V�[�W���F�c�s�n�p�b�̒�`
        private void definedtOqc(ref DataTable dt)
        {
            dt.Columns.Add("serialno", Type.GetType("System.String"));
            dt.Columns.Add("model", Type.GetType("System.String"));
            dt.Columns.Add("datecd", Type.GetType("System.String"));
            dt.Columns.Add("line", Type.GetType("System.String"));
            dt.Columns.Add("lot", Type.GetType("System.String"));
            dt.Columns.Add("eeee", Type.GetType("System.String"));
            dt.Columns.Add("stationid", Type.GetType("System.String"));
            dt.Columns.Add("judge", Type.GetType("System.String"));
            dt.Columns.Add("testtime", Type.GetType("System.DateTime"));
            dt.Columns.Add("return", Type.GetType("System.String"));
        }

        // �T�u�v���V�[�W���F�c�s�s�d�r�s�d�q�̒�`
        private void defineAndReaddtTester(ref DataTable dt)
        {
            dt.Columns.Add("tester", Type.GetType("System.String"));
            dt.Columns.Add("place", Type.GetType("System.String"));

            string sql = "select tester, substr(lower(place), 1, 3) as place FROM tester_id";
            TfSQL tf = new TfSQL();
            System.Diagnostics.Debug.Print(sql);
            tf.sqlDataAdapterFillDatatable(sql, ref dt);
        }

        // �T�u�v���V�[�W���F�f�[�^�O���b�g�r���[�̍X�V
        private void updateDataGridViews(DataTable dt1, DataTable dt2, ref DataGridView dgv1, ref DataGridView dgv2)
        {
            // ���͗p�{�b�N�X�̗L���E������ێ����A����̏ꍇ���ꎞ�I�ɖ����ɂ���
            inputBoxModeOriginal = txtProductSerial.Enabled;
            txtProductSerial.Enabled = false;

            // �f�[�^�O���b�g�r���[�ւc�s�`�`�s�`�a�k�d���i�[
            updateDataGridViewsSub(dt1, dt2, ref dgv1, ref dgv2);

            // �e�X�g���ʂ��e�`�h�k�܂��̓��R�[�h�Ȃ��̃V���A�����}�[�L���O���� 
            colorViewForFailAndBlank(ref dgv1);
            // colorViewForFailAndBlank(ref dgv2);

            // �d�����R�[�h�A����тP�Z���Q�d���͂��}�[�L���O����
            colorViewForDuplicateSerial(ref dgv1);
            // colorViewForDuplicateSerial(ref dgv2);

            // �Q�ȏ�̃R���t�B�O�����݂���ꍇ�Ɍx�����A�f�[�^�O���b�g�r���[���}�[�N����
            colorMixedEcode(dt1, ref dgv1);
            colorMixedConfig(dt1, ref dgv1);

            //�s�w�b�_�[�ɍs�ԍ���\������i�C�����C���j
            for (int i = 0; i < dgv1.Rows.Count; i++)
                dgv1.Rows[i].HeaderCell.Value = (i + 1).ToString();

            //�s�w�b�_�[�ɍs�ԍ���\������i�r�h�j
            for (int j = 0; j < dgv2.Rows.Count; j++)
                dgv2.Rows[j].HeaderCell.Value = (j + 1).ToString();

            //�s�w�b�_�[�̕����������߂���i�C�����C���j
            dgv1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            //�s�w�b�_�[�̕����������߂���i�r�h�j
            dgv2.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            // ��ԉ��̍s��\������i�C�����C���j
            if (dgv1.Rows.Count >= 1)
                dgv1.FirstDisplayedScrollingRowIndex = dgv1.Rows.Count - 1;

            // ��ԉ��̍s��\������i�r�h�j
            if (dgv2.Rows.Count >= 1)
                dgv2.FirstDisplayedScrollingRowIndex = dgv2.Rows.Count - 1;

            // ���͗p�{�b�N�X�̗L���E���������֖߂�
            txtProductSerial.Enabled = inputBoxModeOriginal;

            // ���݂̈ꎞ�o�^������ϐ��֕ێ�����
            okCount = getOkCount(dt1);
            txtOkCount.Text = okCount.ToString() + "/" + limit1.ToString();

            // �X�L�������������ɂk�h�l�h�s�ɒB���Ă���ꍇ�́A���͗p�{�b�N�X�𖳌��ɂ���
            if (okCount == limit1)
            {
                txtProductSerial.Enabled = false;
            }
            else
            {
                txtProductSerial.Enabled = true;
            }

            // �O���b�g���R�[�h�����ƁAokCount������v���Ă���ꍇ�ɁA�o�^�{�^����L���ɂ���
            if (okCount == limit1 && dgv1.Rows.Count == limit1)
            {
                btnRegisterBoxId.Enabled = true;
            }
            else
            {
                btnRegisterBoxId.Enabled = false;
            }
        }

        // �T�u�v���V�[�W���F�V���A���ԍ��d���Ȃ��̂o�`�r�r�����擾����
        private int getOkCount(DataTable dt)
        {
            if (dt.Rows.Count <= 0) return 0;
            DataTable distinct = dt.DefaultView.ToTable(true, new string[] { "serialno", "judge" });
            DataRow[] dr = distinct.Select("judge = 'PASS'");
            int dist = dr.Length;
            return dist;
        }

        // �T�u�v���V�[�W���F���C���f�[�^�O���b�g�r���[�փf�[�^�e�[�u�����i�[�A����яW�v�O���b�h�r���[�̍쐬
        private void updateDataGridViewsSub(DataTable dt1, DataTable dt2, ref DataGridView dgv1, ref DataGridView dgv2)
        {
            dgv1.DataSource = dt1;
            dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgv2.DataSource = dt2;
            dgv2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            //string[] criteriaLine = { "1", "2", "3", "4", "5", "Total" };
            string[] criteriaLine = getLineArray(dt1);
            makeDatatableSummary(dt1, ref dgvLine, criteriaLine, "line");

            string[] criteriaConfig = { "LM", "LN", "SM", "SN", "LT", "ST", "Total" };
            makeDatatableSummary(dt1, ref dgvConfig, criteriaConfig, "model");

            string[] criteriaPassFail = { "PASS", "FAIL", "PLS NG", "No Data" };
            makeDatatableSummary(dt1, ref dgvPassFail, criteriaPassFail, "judge");

            string[] criteriaDateCode = getLotArray(dt1);
            makeDatatableSummary(dt1, ref dgvDateCode, criteriaDateCode, "lot");
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
                dr[criteria[i]] = count;
                if (criteria[i] == "Total") dr[criteria[i]] = total;
                if (criteria[i] == "No Data") dr[criteria[i]] = dgvInline.Rows.Count - total;
            }
            dt1.Rows.Add(dr);

            dgv.Columns.Clear();
            dgv.DataSource = dt1;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        // �T�u�T�u�v���V�[�W���Flot�̕�����z����擾
        private string[] getLineArray(DataTable dt0)
        {
            DataTable dt1 = dt0.Copy();
            DataView dv = dt1.DefaultView;
            dv.Sort = "line";
            DataTable dt2 = dv.ToTable(true, "line");
            string[] array = new string[dt2.Rows.Count + 1];
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                array[i] = dt2.Rows[i]["line"].ToString();
            }
            array[dt2.Rows.Count] = "Total";
            return array;
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

        // �T�u�v���V�[�W���F�e�X�g���ʂ��e�`�h�k�܂��̓��R�[�h�Ȃ��̃V���A�����}�[�L���O����
        private void colorViewForFailAndBlank(ref DataGridView dgv)
        {
            int row = dgv.Rows.Count;
            for (int i = 0; i < row; ++i)
            {
                // �e�X�g���ʂ̃}�[�L���O
                if (dgv["judge", i].Value.ToString() == "FAIL" || dgv["judge", i].Value.ToString() == "PLS NG" || dgv["judge", i].Value.ToString() == String.Empty)
                {
                    dgv["stationid", i].Style.BackColor = Color.Red;
                    dgv["judge", i].Style.BackColor = Color.Red;
                    dgv["testtime", i].Style.BackColor = Color.Red;

                    if (dgv.Name == "dgvOqc") tabControl1.SelectedIndex = 1;
                    else tabControl1.SelectedIndex = 0;

                    soundAlarm();
                }
                else
                {
                    dgv["stationid", i].Style.BackColor = Color.FromKnownColor(KnownColor.Window);
                    dgv["judge", i].Style.BackColor = Color.FromKnownColor(KnownColor.Window);
                    dgv["testtime", i].Style.BackColor = Color.FromKnownColor(KnownColor.Window);

                    tabControl1.SelectedIndex = 0;
                }
            }
        }

        // �T�u�v���V�[�W���F�d�����R�[�h�A�܂��͂P�Z���Q�d���͂��}�[�L���O����
        private void colorViewForDuplicateSerial(ref DataGridView dgv)
        {
            DataTable dt = ((DataTable)dgv.DataSource).Copy();
            if (dt.Rows.Count <= 0) return;

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                string serial = dgv["serialno", i].Value.ToString();
                DataRow[] dr = dt.Select("serialno = '" + serial + "'");
                if (dr.Length >= 2 || dgv["serialno", i].Value.ToString().Length > 24)
                {
                    if (dgv.Name == "dgvOqc") tabControl1.SelectedIndex = 1;
                    else tabControl1.SelectedIndex = 0;

                    dgv["serialno", i].Style.BackColor = Color.Red;
                    soundAlarm();
                }
                else
                {
                    dgv["serialno", i].Style.BackColor = Color.FromKnownColor(KnownColor.Window);
                    tabControl1.SelectedIndex = 0;
                }
            }
        }

        // �T�u�v���V�[�W���F�Q�ȏ�̃��f�������݂���ꍇ�Ɍx�����A�f�[�^�O���b�g�r���[���}�[�N����
        private void colorMixedConfig(DataTable dt, ref DataGridView dgv)
        {
            if (dt.Rows.Count <= 0) return;

            DataTable distinct = dt.DefaultView.ToTable(true, new string[] { "model" });

            if (distinct.Rows.Count == 1)
                m_model = distinct.Rows[0]["model"].ToString();

            if (distinct.Rows.Count >= 2)
            {
                string A = distinct.Rows[0]["model"].ToString();
                string B = distinct.Rows[1]["model"].ToString();
                int a = distinct.Select("model = '" + A + "'").Length;
                int b = distinct.Select("model = '" + B + "'").Length;

                // �����̑����R���t�B�O���A���̔��̃��C�����f���Ƃ���
                m_model = a > b ? A : B;

                // �����̏��Ȃ��ق��̃��C�����f���������擾���A�Z���Ԓn����肵�ă}�[�N����
                string C = a < b ? A : B;
                int c = -1;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["model"].ToString() == C) { c = i; }
                }

                if (c != -1)
                {
                    dgv["model", c].Style.BackColor = Color.Red;
                    soundAlarm();
                }
                else
                {
                    dgv.Columns["model"].DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Window);
                }
            }
        }
        private void colorMixedEcode(DataTable dt, ref DataGridView dgv)
        {
                //Alarm e code
                DataTable distinct1 = dt.DefaultView.ToTable(true, new string[] { "eeee" });
                
            if (distinct1.Rows.Count == 1)
                m_eeee = distinct1.Rows[0]["eeee"].ToString();

            if (distinct1.Rows.Count >= 2)
            {
                string A1 = distinct1.Rows[0]["eeee"].ToString();
                string B1 = distinct1.Rows[1]["eeee"].ToString();
                int a1 = distinct1.Select("eeee = '" + A1 + "'").Length;
                int b1 = distinct1.Select("eeee = '" + B1 + "'").Length;

                // �����̑����R���t�B�O���A���̔��̃��C�����f���Ƃ���
                m_eeee = a1 > b1 ? A1 : B1;

                // �����̏��Ȃ��ق��̃��C�����f���������擾���A�Z���Ԓn����肵�ă}�[�N����
                string C1 = a1 < b1 ? A1 : B1;
                int c1 = -1;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["eeee"].ToString() == C1) { c1 = i; }
                }

                if (c1 != -1)
                {
                    dgv["eeee", c1].Style.BackColor = Color.Red;
                    soundAlarm();
                }
                else
                {
                    dgv.Columns["eeee"].DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Window);
                }
            }
        }

        // �V���A�����X�L�������ꂽ���̏���
        private void txtProductSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // ���͗p�e�L�X�g�{�b�N�X��ҏW�s�ɂ��āA�������̃X�L�������u���b�N����
                txtProductSerial.Enabled = false;

                string serial = txtProductSerial.Text;
                string serialShort = VBS.Left(txtProductSerial.Text, 17);

                if (serial != String.Empty)
                {
                    // �V���A������A�Q�Ƃ��ׂ��o�p�l�e�[�u��������肵�ăO���[�o���ϐ��Ɋi�[����Ƌ��ɁA
                    // �w�C���[�h�P�܂��̓w�C���[�h�Q����肷��
                    string filterKey = decideReferenceTable(serialShort);

                    // �e�X�^�[�f�[�^�̓����e�[�u���E�O���e�[�u������A�e�X�g���ʓ����擾����
                    string sql1 = "(SELECT serno, lot, tjudge, inspectdate" +
                        " FROM " + testerTableThisMonth +
                        " WHERE serno = '" + serialShort + "'" +
                        " ORDER BY tjudge ASC,inspectdate ASC)" +
                        " UNION ALL" +
                        " (SELECT serno, lot, tjudge, inspectdate" +
                        " FROM " + testerTableLastMonth +
                        " WHERE serno = '" + serialShort + "'" +
                        " ORDER BY tjudge ASC,inspectdate ASC)";
                    System.Diagnostics.Debug.Print(System.Environment.NewLine + sql1);
                    DataTable dt1 = new DataTable();
                    TfSQL tf = new TfSQL();
                    tf.sqlDataAdapterFillDatatableFromTesterDb(sql1, ref dt1);

                    // �g�v�Q�̏ꍇ�̂݁A���׃e�[�u���i�����E�O���j����A����̌������ځi�U���j�̃e�X�g���ʓ����擾����
                    bool pulseNG = false;
                    if (filterKey == "HW2")
                    {
                        string sql2 = "select serno, lot, inspectdate, inspect, judge from " + testerTableThisMonth + "data " +
                            "where inspect in ('McPM_Min','MiPM_Min','MiP34MMi','McP76MMi') and judge = '1' and serno = '" + serialShort + "' " +
                            "union all " +
                            "select serno, lot, inspectdate, inspect, judge from " + testerTableLastMonth + "data " +
                            "where inspect in ('McPM_Min','MiPM_Min','MiP34MMi','McP76MMi') and judge = '1' and serno = '" + serialShort + "'";
                        System.Diagnostics.Debug.Print(System.Environment.NewLine + sql2);
                        DataTable dt2 = new DataTable();
                        tf.sqlDataAdapterFillDatatableFromTesterDb(sql2, ref dt2);
                        pulseNG = dt2.Rows.Count >= 1 ? true : false;
                    }
                    
                    // �w�b�_�[�e�[�u���f�[�^���A�h�m�k�h�m�d�E�n�p�b�ɕ����Ĕ��肷��
                    string filterLine = string.Empty;
                    string filterOqc = string.Empty;
                    if (filterKey == "HW1") { filterLine = fltHw1Line; filterOqc = fltHw1Oqc; }
                    else if (filterKey == "HW2") { filterLine = fltHw2Line; filterOqc = fltHw2Oqc; }
                    else if (filterKey == "HW3") { filterLine = fltHw3Line; filterOqc = fltHw3Oqc; }
                    else { filterLine = fltHw1Line; filterOqc = fltHw1Oqc; } //�G���[�΍�

                    //�@ �C�����C���݂̂̃f�[�^�����
                    DataView dv1 = new DataView(dt1);
                    dv1.RowFilter = filterLine;
                    dv1.Sort = "tjudge, inspectdate";
                    System.Diagnostics.Debug.Print(System.Environment.NewLine + "In-Line:");
                    printDataView(dv1);
                    DataTable dt3 = dv1.ToTable();

                    //�A �r�h�݂̂̃f�[�^�����
                    DataView dv2 = new DataView(dt1);
                    dv2.RowFilter = filterOqc;
                    dv2.Sort = "tjudge, inspectdate";
                    System.Diagnostics.Debug.Print(System.Environment.NewLine + "OQC:");
                    printDataView(dv2);
                    DataTable dt4 = dv2.ToTable();

                    // ���f������肵�A�ꎞ�e�[�u���֓o�^����
                    string model = String.Empty;
                    if (VBS.Mid(serial, 14, 2) == "FR") model = "LM";
                    else if (VBS.Mid(serial, 14, 2) == "FQ") model = "LN";
                    else if (VBS.Mid(serial, 14, 2) == "FM") model = "SM";
                    else if (VBS.Mid(serial, 14, 2) == "FL") model = "SN";
                    else if (VBS.Mid(serial, 14, 2) == "RM") model = "L3";
                    else if (VBS.Mid(serial, 14, 2) == "RL") model = "S3";
                    else if (VBS.Mid(serial, 14, 2) == "XH" || VBS.Mid(serial, 14, 2) == "23" || VBS.Mid(serial, 14, 2) == "H1") model = "LT";
                    else if (VBS.Mid(serial, 14, 2) == "XD" || VBS.Mid(serial, 14, 2) == "1Y" || VBS.Mid(serial, 14, 2) == "H0") model = "ST";
                    else model = "Error";


                    //�@�C�����C��
                    // �e�X�^�[�f�[�^�ɊY�����Ȃ��ꍇ�ł��A���[�U�[�ɔF�������邽�߂ɕ\������
                    DataRow dr = dtOverall.NewRow();
                    dr["serialno"] = serial;
                    dr["model"] = model;
                    dr["datecd"] = VBS.Mid(serial, 4, 4).Length < 4 ? "Error" : VBS.Mid(serial, 4, 4);
                    dr["line"] = VBS.Mid(serial, 8, 1).Length < 1 ? "Eror" : VBS.Mid(serial, 8, 1);
                    dr["lot"] = VBS.Mid(serial, 4, 5).Length < 5 ? "Error" : VBS.Mid(serial, 4, 5);
                    dr["eeee"] = VBS.Mid(serial, 12, 4).Length < 4 ? "Error" : VBS.Mid(serial, 12, 4);
                    dr["return"] = formReturnMode ? "R" : "N";

                    // �e�X�^�[�f�[�^�ɊY��������ꍇ�A���R�\������
                    if (dt3.Rows.Count != 0)
                    {
                        // �e�X�^�[�f�[�^�̔��蕶����ϊ�
                        string linepass = String.Empty;
                        string buff = dt3.Rows[0]["tjudge"].ToString();
                        if (buff == "0") linepass = "PASS";
                        else if (buff == "1") linepass = "FAIL";
                        else linepass = "ERROR";
                        // �o�t�k�r�d����̒ǋL
                        if (pulseNG) linepass = "PLS NG";

                        dr["stationid"] = dt3.Rows[0]["lot"].ToString();
                        dr["judge"] = linepass;
                        dr["testtime"] = (DateTime)dt3.Rows[0]["inspectdate"];
                    }

                    // ��������̃e�[�u���Ƀ��R�[�h��ǉ�
                    dtOverall.Rows.Add(dr);


                    // �n�p�b���R�[�h��ʃ^�u�ɕ\������
                    // �A�r�h
                    // �e�X�^�[�f�[�^�ɊY�����Ȃ��ꍇ�ł��A���[�U�[�ɔF�������邽�߂ɕ\������
                    DataRow dr_si = dtOqc.NewRow();
                    dr_si["serialno"] = serial;
                    dr_si["model"] = model;
                    dr_si["datecd"] = VBS.Mid(serial, 4, 4).Length < 4 ? "Error" : VBS.Mid(serial, 4, 4);
                    dr_si["line"] = VBS.Mid(serial, 8, 1).Length < 1 ? "Error" : VBS.Mid(serial, 8, 1);
                    dr_si["lot"] = VBS.Mid(serial, 4, 5).Length < 5 ? "Error" : VBS.Mid(serial, 4, 5);
                    dr_si["eeee"] = VBS.Mid(serial, 12, 4).Length < 4 ? "Error" : VBS.Mid(serial, 12, 4);
                    dr_si["return"] = formReturnMode ? "R" : "N";

                    // �e�X�^�[�f�[�^�ɊY��������ꍇ�A���R�\������
                    if (dt4.Rows.Count != 0)
                    {
                        // �e�X�^�[�f�[�^�̔��蕶����ϊ�
                        string linepass = String.Empty;
                        string buff = dt4.Rows[0]["tjudge"].ToString();
                        if (buff == "0") linepass = "PASS";
                        else if (buff == "1") linepass = "FAIL";
                        else linepass = "ERROR";

                        // �o�t�k�r�d����̒ǋL
                        if (pulseNG) linepass = "PLS NG";

                        dr_si["stationid"] = dt4.Rows[0]["lot"].ToString();
                        dr_si["judge"] = linepass;
                        dr_si["testtime"] = (DateTime)dt4.Rows[0]["inspectdate"];
                    }

                    // ��������̃e�[�u���Ƀ��R�[�h��ǉ�
                    dtOqc.Rows.Add(dr_si);


                    // �f�[�^�e�[�u���̐擪�s�̃V���A������A�k�h�l�h�s�𔻒f����
                    if (dtOverall.Rows.Count == 1)
                    {
                        string m = dtOverall.Rows[0]["model"].ToString();
                        if (m == "LM" || m == "LN") limit1 = limitHw1Large;
                        else if (m == "SM" || m == "SN") limit1 = limitHw1Small;
                        else if (m == "LT") limit1 = limitHw2Large;
                        else if (m == "ST") limit1 = limitHw2Small;
                        else if (m == "L3") limit1 = limitHw3Large;
                        else if (m == "S3") limit1 = limitHw3Small;
                        else limit1 = 9999;

                        // �t�r�d�q�X���k�h�l�h�s��ݒ肵���ꍇ�́A����ɏ]��
                        if (limit2 != 0) limit1 = limit2;
                    }

                    // �f�[�^�O���b�g�r���[�̍X�V
                    updateDataGridViews(dtOverall, dtOqc, ref dgvInline, ref dgvOqc);
                }
                // ���͗p�e�L�X�g�{�b�N�X��ҏW�\�֖߂��A�A�����ăX�L�����ł���悤�A�e�L�X�g��I����Ԃɂ���
                if (okCount >= limit1 && !formAddMode)
                {
                    txtProductSerial.Enabled = false;
                }
                else
                {
                    txtProductSerial.Enabled = true;
                    txtProductSerial.Focus();
                    txtProductSerial.SelectAll();
                }
            }
        }

        // �T�u�v���V�[�W���F�V���A������A�Q�Ƃ��ׂ��o�p�l�e�[�u��������肷��
        private string decideReferenceTable(string serno)
        {
            string tablekey = string.Empty;
            string filterkey = string.Empty;
            if (VBS.Mid(serno, 14, 2) == "FR" || VBS.Mid(serno, 14, 2) == "FQ")
            { tablekey = "ld4g"; filterkey = "HW1"; }
            else if (VBS.Mid(serno, 14, 2) == "FM" || VBS.Mid(serno, 14, 2) == "FL")
            { tablekey = "ld4f"; filterkey = "HW1"; }
            else if (VBS.Mid(serno, 14, 2) == "XH" || VBS.Mid(serno, 14, 2) == "23" || VBS.Mid(serno, 14, 2) == "H1")
            { tablekey = "x584"; filterkey = "HW2"; }
            else if (VBS.Mid(serno, 14, 2) == "XD" || VBS.Mid(serno, 14, 2) == "1Y" || VBS.Mid(serno, 14, 2) == "H0")
            { tablekey = "x583"; filterkey = "HW2"; }
            else if (VBS.Mid(serno, 14, 2) == "RM")
            { tablekey = "x999"; filterkey = "HW3"; }
            else if (VBS.Mid(serno, 14, 2) == "RL")
            { tablekey = "x998"; filterkey = "HW3"; }
            else
            { tablekey = "ld4f"; filterkey = "HW1"; }// �G���[�΍�

            testerTableThisMonth = tablekey + DateTime.Today.ToString("yyyyMM");
            testerTableLastMonth = tablekey + ((VBS.Right(DateTime.Today.ToString("yyyyMM"), 2) != "01") ?
                (long.Parse(DateTime.Today.ToString("yyyyMM")) - 1).ToString() : (long.Parse(DateTime.Today.ToString("yyyy")) - 1).ToString() + "12");

            return filterkey;
        }

        // �r���[���[�h�ōĈ�����s��
        private void btnPrint_Click(object sender, EventArgs e)
        {
            string boxId = txtBoxId.Text;
            m_model = dtOverall.Rows[0]["model"].ToString();
            m_model_long = getMainModelLong(m_model);
            string shaft = cmbShaft.Text;
            string overlay = cmbOverlay.Text;
            string shipKind = dtOverall.Rows[0]["return"].ToString();
            printBarcode(directory, boxId, m_model_long, shaft, overlay, dgvDateCode, ref dgvDateCode2, ref txtBoxIdPrint, shipKind);
        }

        // �e��m�F��A�{�b�N�X�h�c�̔��s�A�V���A���̓o�^�A�o�[�R�[�h���x���̃v�����g���s��
        private void btnRegisterBoxId_Click(object sender, EventArgs e)
        {
            if (cmbShaft.Text == String.Empty | cmbOverlay.Text == String.Empty && VBS.Right(m_model, 1) == "T")
            {
                MessageBox.Show("Please select shaft or overlay.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                return;
            }

            btnRegisterBoxId.Enabled = false;
            btnDeleteSelection.Enabled = false;
            btnCancel.Enabled = false;

            string boxId = txtBoxId.Text;

            //�ꎞ�e�[�u���̃V���A���S�Ăɂ��āA�{�ԃe�[�u���ɓo�^���Ȃ����A�`�F�b�N
            string checkResult = checkDataTableWithRealTable(dtOverall);

            if (checkResult != String.Empty)
            {
                MessageBox.Show("The following serials are already registered with box id:" + Environment.NewLine +
                    checkResult + Environment.NewLine + "Please check and delete.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                btnRegisterBoxId.Enabled = true;
                btnDeleteSelection.Enabled = true;
                btnCancel.Enabled = true;
                return;
            }

            //�{�b�N�X�h�c�̐V�K�̔�
            string boxIdNew = getNewBoxId(cmbShaft.Text, cmbOverlay.Text, txtUser.Text);

            //�悸�́ADataTble�Ƀ{�b�N�X�h�c��o�^
            DataTable dt = dtOverall.Copy();
            dt.Columns.Add("boxid", Type.GetType("System.String"));
            for (int i = 0; i < dt.Rows.Count; i++)
                dt.Rows[i]["boxid"] = boxIdNew;

            //DataTable����{�ԃe�[�u���ֈꊇ�o�^
            TfSQL tf = new TfSQL();
            bool res1 = tf.sqlMultipleInsertOverall(dt);

            if (res1)
            {
                // �o�[�R�[�h���󎚁i�O�̂��߃��C�����f��������x�擾������j
                m_model = dtOverall.Rows[0]["model"].ToString();
                m_model_long = getMainModelLong(m_model);
                string shipKind = dtOverall.Rows[0]["return"].ToString();
                printBarcode(directory, boxIdNew, m_model_long, cmbShaft.Text, cmbOverlay.Text, dgvDateCode, ref dgvDateCode2, ref txtBoxIdPrint, shipKind);

                //�f�[�^�e�[�u���̃��R�[�h����
                dtOverall.Clear();
                dtOqc.Clear();
                dt = null;

                txtBoxId.Text = boxIdNew;
                dtpPrintDate.Value = DateTime.ParseExact(VBS.Mid(boxIdNew, 3, 6), "yyMMdd", CultureInfo.InvariantCulture);

                //�e�t�H�[��frmBox�̃f�[�^�O���b�g�r���[���X�V���邽�߁A�f���Q�[�g�C�x���g�𔭐�������
                this.RefreshEvent(this, new EventArgs());

                this.Focus();
                MessageBox.Show("The box id " + boxIdNew + " and " + Environment.NewLine +
                    "its product serials were registered.", "Process Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBoxId.Text = String.Empty;
                txtProductSerial.Text = String.Empty;
                updateDataGridViews(dtOverall, dtOqc, ref dgvInline, ref dgvOqc);
                btnRegisterBoxId.Enabled = false;
                btnDeleteSelection.Enabled = false;
                btnCancel.Enabled = true;
            }
            else
            {
                //��U�o�^�����a�n�w�h�c����������
                string sql = "delete from box_id_rt WHERE boxid= '" + boxIdNew + "'";
                int res = tf.sqlExecuteNonQueryInt(sql, false);

                MessageBox.Show("Box id and product serials were not registered.", "Process Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnRegisterBoxId.Enabled = true;
                btnDeleteSelection.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        // �T�u�v���V�[�W���F�f�[�^�e�[�u���̃V���A���S�Ăɂ��āA�{�e�[�u���ɓo�^���Ȃ����ꊇ�m�F
        private string checkDataTableWithRealTable(DataTable dt1)
        {
            string result = String.Empty;
            if (formReturnMode) return result;

            string sql = "select serialno, boxid FROM product_serial_rt " +
                 "WHERE testtime BETWEEN '" + System.DateTime.Today.AddDays(-5) + "' AND '" + System.DateTime.Today.AddDays(1) + "' AND " +
                 "return != 'R'";

            DataTable dt2 = new DataTable();
            TfSQL tf = new TfSQL();
            tf.sqlDataAdapterFillDatatable(sql, ref dt2);

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string serial = dt1.Rows[i]["serialno"].ToString();
                DataRow[] dr = dt2.Select("serialno = '" + serial + "'");
                if (dr.Length >= 1)
                {
                    string boxid = dr[0]["boxId"].ToString();
                    result += (i + 1 + ": " + serial + " / " + boxid + Environment.NewLine);
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

        // �T�u�v���V�[�W���F���ꂩ�甭�s����{�b�N�X�h�c�̍̔�
        private string getNewBoxId(string shaft, string overlay, string user)
        {
            m_model = dtOverall.Rows[0]["model"].ToString();
            string sql = "select MAX(boxid) FROM box_id_rt where boxid like '" + m_model + "%'";
            System.Diagnostics.Debug.Print(sql);
            TfSQL tf = new TfSQL();
            string boxIdOld = tf.sqlExecuteScalarString(sql);

            DateTime dateOld = new DateTime(0);
            long numberOld = 0;
            string boxIdNew;

            if (boxIdOld != string.Empty)
            {
                dateOld = DateTime.ParseExact(VBS.Mid(boxIdOld, 3, 6), "yyMMdd", CultureInfo.InvariantCulture);
                numberOld = long.Parse(VBS.Right(boxIdOld, 4));
            }

            if (dateOld != DateTime.Today)
            {
                boxIdNew = m_model + DateTime.Today.ToString("yyMMdd") + "0001";
            }
            else
            {
                boxIdNew = m_model + DateTime.Today.ToString("yyMMdd") + (numberOld + 1).ToString("0000");
            }

            sql = "INSERT INTO box_id_rt(" +
                "boxid," +
                "shaft," +
                "over_lay," +
                "suser," +
                "printdate) " +
                "VALUES(" +
                "'" + boxIdNew + "'," +
                "'" + shaft + "'," +
                "'" + overlay + "'," +
                "'" + user + "'," +
                "'" + DateTime.Now.ToString() + "')";
            System.Diagnostics.Debug.Print(sql);
            tf.sqlExecuteNonQuery(sql, false);
            return boxIdNew;
        }

        // �T�u�v���V�[�W���F���C�����f���̒Z�������񂩂�A������������擾����
        private string getMainModelLong(string m_short)
        {
            string m_long = String.Empty;
            if (m_short == "LM") m_long = "LD4G-003L";
            else if (m_short == "LN") m_long = "LD4G-001";
            else if (m_short == "SM") m_long = "LD4F-003L";
            else if (m_short == "SN") m_long = "LD4F-001";
            else if (m_short == "LT") m_long = "LD4J-001";
            else if (m_short == "ST") m_long = "LD4H-001";
            else m_long = "Error";
            return m_long;
        }

        // �T�u�v���V�[�W���F�o�[�R�[�h���v�����g����i�{�o�[�W�����́A�a�n�w�h�c���̃e�L�X�g�t�@�C���𐶐�����j
        private void printBarcode(string dir, string id, string m_model_long, string shaft, string overlay, DataGridView dgv1, ref DataGridView dgv2, ref TextBox txt, string shipKind)
        {
            TfPrint tf = new TfPrint();
            tf.createBoxidFiles(dir, id, m_model_long, shaft, overlay, dgv1, ref dgv2, ref txt, shipKind);
        }

        // �ꎞ�e�[�u���̑I�����ꂽ�������R�[�h���A�ꊇ����������
        private void btnDeleteSelection_Click(object sender, EventArgs e)
        {
            DataGridView dgv = new DataGridView();

            if (tabControl1.SelectedTab == tabControl1.TabPages["tabInline"])
                dgv = dgvInline;
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabOqc"])
                dgv = dgvOqc;

            // �Z���̑I��͈͂��Q��ȏ�̏ꍇ�́A���b�Z�[�W�̕\���݂̂Ńv���V�[�W���𔲂���
            if (dgv.Columns.GetColumnCount(DataGridViewElementStates.Selected) >= 2)
            {
                MessageBox.Show("Please select range with only one columns.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                return;
            }

            DialogResult result = MessageBox.Show("Do you really want to delete the selected rows?",
                "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewCell cell in dgv.SelectedCells)
                {
                    int i = cell.RowIndex;
                    dtOverall.Rows[i].Delete();
                    dtOqc.Rows[i].Delete();
                }
                dtOverall.AcceptChanges();
                dtOqc.AcceptChanges();
                updateDataGridViews(dtOverall, dtOqc, ref dgvInline, ref dgvOqc);
                txtProductSerial.Focus();
            }
        }

        // �P���x��������̃V���A������ύX����i�Ǘ��������[�U�[�̂݁j
        private void btnChangeLimit_Click(object sender, EventArgs e)
        {
            // �t�H�[���S�i�P���x��������V���A�����ύX�j���A�f���Q�[�g�C�x���g��t�����ĊJ��
            bool bl = TfGeneral.checkOpenFormExists("frmCapacity");
            if (bl)
            {
                MessageBox.Show("Please close or complete another form.", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }
            else
            {
                frmCapacity f4 = new frmCapacity();
                //�q�C�x���g���L���b�`���āA�f�[�^�O���b�h���X�V����
                f4.RefreshEvent += delegate (object sndr, EventArgs excp)
                {
                    int l = f4.getLimit();
                    if (l != 0)
                    {
                        limit2 = f4.getLimit();
                        txtLimit.Text = limit2.ToString();
                        limit1 = limit2;
                    }
                    updateDataGridViews(dtOverall, dtOqc, ref dgvInline, ref dgvOqc);
                    this.Focus();
                };

                f4.updateControls(limit2.ToString());
                f4.Show();
            }
        }

        // �o�^�ς݂̃{�b�N�X�h�c�ցA���W���[����ǉ��i�Ǘ����[�U�[�̂݁j
        private void btnAddSerial_Click(object sender, EventArgs e)
        {
            // �ǉ����[�h�łȂ��ꍇ�́A�ǉ����[�h�̕\���֐؂�ւ���
            if (!formAddMode)
            {
                formAddMode = true;
                btnAddSerial.Text = "Register";
                btnRegisterBoxId.Enabled = false;
                btnExport.Enabled = false;
                btnCancelBoxid.Enabled = false;
                btnDeleteSerial.Enabled = false;
                btnPrint.Enabled = false;
                txtProductSerial.Enabled = true;
                if (dtOverall.Rows.Count >= 0)
                {
                    formReturnMode = (dtOverall.Rows[0]["return"].ToString() == "R" ? true : false);
                }
            }
            // ���ɒǉ����[�h�̏ꍇ�́A�c�a�ւ̓o�^���s��
            else
            {
                // �c�d�k�d�s�d �r�p�k���𔭍s���A�f�[�^�x�[�X����폜����
                string boxId = txtBoxId.Text;
                string sql = "delete from product_serial_rt where boxid = '" + boxId + "'";
                System.Diagnostics.Debug.Print(sql);
                TfSQL tf = new TfSQL();
                bool res1 = tf.sqlExecuteNonQuery(sql, false);

                // DataTble�Ƀ{�b�N�X�h�c��ǉ����A�{�ԃe�[�u���ֈꊇ�o�^
                DataTable dt = dtOverall.Copy();
                dt.Columns.Add("boxid", Type.GetType("System.String"));
                for (int i = 0; i < dt.Rows.Count; i++)
                    dt.Rows[i]["boxid"] = boxId;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string buff = string.Empty;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        buff += dt.Rows[i][j].ToString() + "  ";
                        System.Diagnostics.Debug.Print(buff);
                    }
                }

                bool res2 = tf.sqlMultipleInsertOverall(dt);

                if (!res1 || !res2)
                {
                    MessageBox.Show("Error happened in the register process.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    MessageBox.Show("Register completed.", "Notice",
                        MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                }

                // �ǉ����[�h���I�����A�{�����[�h�̕\���֐؂�ւ���
                formAddMode = false;
                btnAddSerial.Text = "Add Product";
                btnRegisterBoxId.Enabled = true;
                btnExport.Enabled = true;
                btnCancelBoxid.Enabled = true;
                btnDeleteSerial.Enabled = true; 
                btnPrint.Enabled = true;
                txtProductSerial.Enabled = false;
                txtProductSerial.Text = string.Empty;
            }
        }

        // �o�^�ς݂̃{�b�N�X�h�c�́A���W���[�����폜�i�Ǘ����[�U�[�̂݁j
        private void btnDeleteSerial_Click(object sender, EventArgs e)
        {
            // �Z���̑I��͈͂��Q��ȏ�̏ꍇ�́A���b�Z�[�W�̕\���݂̂Ńv���V�[�W���𔲂���
            if (dgvInline.Columns.GetColumnCount(DataGridViewElementStates.Selected) >= 2)
            {
                MessageBox.Show("Please select range with only one columns.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                return;
            }

            DialogResult result = MessageBox.Show("Do you really want to delete the selected rows?",
                "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                // �c�d�k�d�s�d �r�p�k���𔭍s���A�f�[�^�x�[�X����폜����
                string boxId = txtBoxId.Text;
                string whereSer = string.Empty;
                foreach (DataGridViewCell cell in dgvInline.SelectedCells)
                {
                    whereSer += "'" + cell.Value.ToString() + "', ";
                }
                string sql = "delete from product_serial_rt where boxid = '" + boxId + "' and  serialno in (" + VBS.Left(whereSer, whereSer.Length - 2) + ")";
                System.Diagnostics.Debug.Print(sql);
                TfSQL tf = new TfSQL();
                int res = tf.sqlExecuteNonQueryInt(sql, false);

                if (res >= 1)
                {
                    // �f�[�^�O���b�h�r���[����폜����
                    foreach (DataGridViewCell cell in dgvInline.SelectedCells)
                    {
                        int i = cell.RowIndex;
                        dtOverall.Rows[i].Delete();
                    }
                    dtOverall.AcceptChanges();
                    updateDataGridViews(dtOverall, dtOqc, ref dgvInline, ref dgvOqc);
                    MessageBox.Show(res.ToString() + " module(s) deleted.", "Notice",
                        MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    MessageBox.Show("Delete failed.", "Notice",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                }
            }
        }
        
        // �o�^�ς݂̃{�b�N�X�h�c����ъY�����W���[���̍폜�i�Ǘ����[�U�[�̂݁j
        private void btnCancelBoxid_Click(object sender, EventArgs e)
        {
            // �{���ɍ폜���Ă悢���A�Q�d�Ŋm�F����B
            DialogResult result1 = MessageBox.Show("Do you really delete this box id's all the serial data?",
                "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result1 == DialogResult.Yes)
            {
                DialogResult result2 = MessageBox.Show("Are you really sure? Please select NO if you are not sure.",
                    "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result2 == DialogResult.Yes)
                {
                    string boxid = txtBoxId.Text;
                    TfSQL tf = new TfSQL();
                    int res = tf.sqlDeleteBoxid(boxid);

                    dtOverall.Clear();
                    dtOqc.Clear();
                    // �f�[�^�O���b�g�r���[�̍X�V
                    updateDataGridViews(dtOverall, dtOqc, ref dgvInline, ref dgvOqc);

                    //�e�t�H�[��frmBox�̃f�[�^�O���b�g�r���[���X�V���邽�߁A�f���Q�[�g�C�x���g�𔭐�������
                    this.RefreshEvent(this, new EventArgs());
                    this.Focus();

                    if (res != -1)
                    {
                        MessageBox.Show("Boxid " + boxid + " and its " + res + " products were registered.", "Process Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnPrint.Enabled = false;
                        btnCancelBoxid.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("An Error has happened in the process and no data has been deleted.", "Process Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    btnAddSerial.Enabled = false;
                    btnExport.Enabled = false;
                }
            }
        }

        // �T�u�T�u�v���V�[�W���F�c�a����̂c�s�ւ̓ǂݍ���
        private void readDatatable(ref DataTable dt)
        {
            string boxId = txtBoxId.Text;
            string sql = "select serialno, model, datecd, line, lot, eeee, stationid, judge, testtime, return " +
                "FROM product_serial_rt WHERE boxid='" + boxId + "'";
            TfSQL tf = new TfSQL();
            tf.sqlDataAdapterFillDatatable(sql, ref dt);
        }

        // �L�����Z�����ɁA�f�[�^�e�[�u���̃��R�[�h�̕ێ����ł��Ȃ��|�A�x������
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // frmCapacity �i�a�n�w������V���A�����j����Ă��Ȃ��ꍇ�́A��ɕ���悤�ʒm����
            string formName = "frmCapacity";
            bool bl = false;
            foreach (Form buff in Application.OpenForms)
            {
                if (buff.Name == formName) { bl = true; }
            }
            if (bl)
            {
                MessageBox.Show("You need to close another form before canceling.", "Notice",
                  MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                return;
            }

            // �f�[�^�e�[�u���̃��R�[�h�������Ȃ��ꍇ�A�܂��͕ҏW���[�h�̏ꍇ�́A���̂܂ܕ���                        
            if (dtOverall.Rows.Count == 0 || !formEditMode)
            {
                Application.OpenForms["frmBox"].Focus();
                Close();
                return;
            }

            // �f�[�^�e�[�u���̃��R�[�h����������ꍇ�A�ꎞ�I�ɕێ�����Ă��郌�R�[�h�����ł���|�A�x������
            DialogResult result = MessageBox.Show("The current serial data has not been saved." + System.Environment.NewLine +
                "Do you rally cancel?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                dtOverall.Clear();
                dtOqc.Clear();
                updateDataGridViews(dtOverall, dtOqc, ref dgvInline, ref dgvOqc);
                MessageBox.Show("The temporary serial numbers are deleted.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                Application.OpenForms["frmBox"].Focus();
                Close();
            }
            else
            {
                return;
            }
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

        // �f�[�^���G�N�Z���փG�N�X�|�[�g
        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1 = (DataTable)dgvInline.DataSource;
            dt2 = (DataTable)dgvOqc.DataSource;
            ExcelClass xl = new ExcelClass();
            xl.ExportToExcel2(dt1, dt2);
            //xl.ExportToCsv(dt, System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\ipqcdb.csv");
        }

        // �T�u�v���V�[�W���F�f�[�^�e�[�u���̒��g���`�F�b�N����A�{�A�v���P�[�V�����ɑ΂��āA���ڂ͊֌W�Ȃ�
        private void printDataTable(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    System.Diagnostics.Debug.Print(dt.Rows[i][j].ToString());
                }
            }
        }

        // �T�u�v���V�[�W���F�f�[�^�r���[�̒��g���`�F�b�N����A�{�A�v���P�[�V�����ɑ΂��āA���ڂ͊֌W�Ȃ�
        private void printDataView(DataView dv)
        {
            foreach (DataRowView drv in dv)
            {
                System.Diagnostics.Debug.Print(drv["lot"].ToString() + " " +
                    drv["tjudge"].ToString() + " " + drv["inspectdate"].ToString());
            }
        }

        // �R���{�{�b�N�X�֌����i�[����
        private void cmbShaft_Enter(object sender, EventArgs e)
        {
            if (m_model == null) return;

            string sql = "select distinct shaft from mdl_sht_ovl where model='" + m_model + "'";
            System.Diagnostics.Debug.Print(sql);
            TfSQL tf = new TfSQL();
            tf.getComboBoxDataViaCsv(sql, ref cmbShaft);
        }

        // �R���{�{�b�N�X�֌����i�[����
        private void cmbOverlay_Enter(object sender, EventArgs e)
        {
            if (m_model == null) return;

            string sql = "select distinct over_lay from mdl_sht_ovl where model='" + m_model + "'";
            System.Diagnostics.Debug.Print(sql);
            TfSQL tf = new TfSQL();
            tf.getComboBoxDataViaCsv(sql, ref cmbOverlay);
        }

        // �d�m�s�d�q�j�d�x�����ŁA�o�^�p�}�X�^���Ăяo��
        private void cmbShaft_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || user != "User_9") return;
            if (TfGeneral.checkOpenFormExists("frmShaftOverlay")) return;

            frmShaftOverlay f7 = new frmShaftOverlay();
            f7.Show();
        }

        // �d�m�s�d�q�j�d�x�����ŁA�o�^�p�}�X�^���Ăяo��
        private void cmbOverlay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || user != "User_9") return;
            if (TfGeneral.checkOpenFormExists("frmShaftOverlay")) return;

            frmShaftOverlay f7 = new frmShaftOverlay();
            f7.Show();
        }

    }
}