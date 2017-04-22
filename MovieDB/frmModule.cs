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
        //親フォームfrmBoxへイベント発生を連絡（デレゲート）
        public delegate void RefreshEventHandler(object sender, EventArgs e);
        public event RefreshEventHandler RefreshEvent;

        // プリント用テキストファイルの保存用フォルダを、基本設定ファイルで設定する
        string appconfig = System.Environment.CurrentDirectory + "\\info.ini";
        string directory = @"C:\Users\takusuke.fujii\Desktop\Auto Print\\";

        //その他、非ローカル変数
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

        // コンストラクタ
        public frmModule()
        {
            InitializeComponent();
        }

        // ロード時の処理
        private void frmModule_Load(object sender, EventArgs e)
        {
            // 編集モード用ユーザー名を保持する
            user = txtUser.Text;

            // ユーザー９が設定するＬＩＭＩＴを、テキストボックスへ表示
            txtLimit.Text = limit2.ToString();

            // プリント用ファイルの保存先フォルダ、その他設定を、読み込む
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

            // 当フォームの表示場所を指定
            this.Left = 250;
            this.Top = 20;

            // 各種処理用のテーブルを生成
            dtOverall = new DataTable();
            defineAndReadDtOverall(ref dtOverall);
            dtOqc = new DataTable();
            definedtOqc(ref dtOqc);
            //dtTester = new DataTable();
            //defineAndReaddtTester(ref dtTester);

            // ＬＩＭＩＴの制御を後で直す必要あり
            if (!formEditMode)
            {
                // データテーブルの先頭行のシリアルから、ＬＩＭＩＴを判断する
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

            // グリットビューの更新
            updateDataGridViews(dtOverall, dtOqc, ref dgvInline, ref dgvOqc);

            // シリアル用テキストボックスの制御を後で直す必要あり
            if (!formEditMode)
            {
                txtProductSerial.Enabled = false;
            }
        }

        // 設定テキストファイルの読み込み
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
        // Windows API をインポート
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filepath);

        // サブプロシージャ：親フォームで呼び出し、親フォームの情報を、テキストボックスへ格納して引き継ぐ
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

        // サブプロシージャ：ＤＢからのＤＴＯＶＥＲＡＬＬへの読み込み
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

        // サブプロシージャ：ＤＴＯＱＣの定義
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

        // サブプロシージャ：ＤＴＴＥＳＴＥＲの定義
        private void defineAndReaddtTester(ref DataTable dt)
        {
            dt.Columns.Add("tester", Type.GetType("System.String"));
            dt.Columns.Add("place", Type.GetType("System.String"));

            string sql = "select tester, substr(lower(place), 1, 3) as place FROM tester_id";
            TfSQL tf = new TfSQL();
            System.Diagnostics.Debug.Print(sql);
            tf.sqlDataAdapterFillDatatable(sql, ref dt);
        }

        // サブプロシージャ：データグリットビューの更新
        private void updateDataGridViews(DataTable dt1, DataTable dt2, ref DataGridView dgv1, ref DataGridView dgv2)
        {
            // 入力用ボックスの有効・無効を保持し、何れの場合も一時的に無効にする
            inputBoxModeOriginal = txtProductSerial.Enabled;
            txtProductSerial.Enabled = false;

            // データグリットビューへＤＴＡＡＴＡＢＬＥを格納
            updateDataGridViewsSub(dt1, dt2, ref dgv1, ref dgv2);

            // テスト結果がＦＡＩＬまたはレコードなしのシリアルをマーキングする 
            colorViewForFailAndBlank(ref dgv1);
            // colorViewForFailAndBlank(ref dgv2);

            // 重複レコード、および１セル２重入力をマーキングする
            colorViewForDuplicateSerial(ref dgv1);
            // colorViewForDuplicateSerial(ref dgv2);

            // ２つ以上のコンフィグが混在する場合に警告し、データグリットビューをマークする
            colorMixedEcode(dt1, ref dgv1);
            colorMixedConfig(dt1, ref dgv1);

            //行ヘッダーに行番号を表示する（インライン）
            for (int i = 0; i < dgv1.Rows.Count; i++)
                dgv1.Rows[i].HeaderCell.Value = (i + 1).ToString();

            //行ヘッダーに行番号を表示する（ＳＩ）
            for (int j = 0; j < dgv2.Rows.Count; j++)
                dgv2.Rows[j].HeaderCell.Value = (j + 1).ToString();

            //行ヘッダーの幅を自動調節する（インライン）
            dgv1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            //行ヘッダーの幅を自動調節する（ＳＩ）
            dgv2.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            // 一番下の行を表示する（インライン）
            if (dgv1.Rows.Count >= 1)
                dgv1.FirstDisplayedScrollingRowIndex = dgv1.Rows.Count - 1;

            // 一番下の行を表示する（ＳＩ）
            if (dgv2.Rows.Count >= 1)
                dgv2.FirstDisplayedScrollingRowIndex = dgv2.Rows.Count - 1;

            // 入力用ボックスの有効・無効を元へ戻す
            txtProductSerial.Enabled = inputBoxModeOriginal;

            // 現在の一時登録件数を変数へ保持する
            okCount = getOkCount(dt1);
            txtOkCount.Text = okCount.ToString() + "/" + limit1.ToString();

            // スキャン件数が既にＬＩＭＩＴに達している場合は、入力用ボックスを無効にする
            if (okCount == limit1)
            {
                txtProductSerial.Enabled = false;
            }
            else
            {
                txtProductSerial.Enabled = true;
            }

            // グリットレコード件数と、okCount数が一致している場合に、登録ボタンを有効にする
            if (okCount == limit1 && dgv1.Rows.Count == limit1)
            {
                btnRegisterBoxId.Enabled = true;
            }
            else
            {
                btnRegisterBoxId.Enabled = false;
            }
        }

        // サブプロシージャ：シリアル番号重複なしのＰＡＳＳ個数を取得する
        private int getOkCount(DataTable dt)
        {
            if (dt.Rows.Count <= 0) return 0;
            DataTable distinct = dt.DefaultView.ToTable(true, new string[] { "serialno", "judge" });
            DataRow[] dr = distinct.Select("judge = 'PASS'");
            int dist = dr.Length;
            return dist;
        }

        // サブプロシージャ：メインデータグリットビューへデータテーブルを格納、および集計グリッドビューの作成
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

        // サブサブプロシージャ：集計用のデータテーブルを、データグリッドビューに格納
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

        // サブサブプロシージャ：lotの文字列配列を取得
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

        // サブサブプロシージャ：lotの文字列配列を取得
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

        // サブプロシージャ：テスト結果がＦＡＩＬまたはレコードなしのシリアルをマーキングする
        private void colorViewForFailAndBlank(ref DataGridView dgv)
        {
            int row = dgv.Rows.Count;
            for (int i = 0; i < row; ++i)
            {
                // テスト結果のマーキング
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

        // サブプロシージャ：重複レコード、または１セル２重入力をマーキングする
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

        // サブプロシージャ：２つ以上のモデルが混在する場合に警告し、データグリットビューをマークする
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

                // 件数の多いコンフィグを、この箱のメインモデルとする
                m_model = a > b ? A : B;

                // 件数の少ないほうのメインモデル文字を取得し、セル番地を特定してマークする
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

                // 件数の多いコンフィグを、この箱のメインモデルとする
                m_eeee = a1 > b1 ? A1 : B1;

                // 件数の少ないほうのメインモデル文字を取得し、セル番地を特定してマークする
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

        // シリアルがスキャンされた時の処理
        private void txtProductSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // 入力用テキストボックスを編集不可にして、処理中のスキャンをブロックする
                txtProductSerial.Enabled = false;

                string serial = txtProductSerial.Text;
                string serialShort = VBS.Left(txtProductSerial.Text, 17);

                if (serial != String.Empty)
                {
                    // シリアルから、参照すべきＰＱＭテーブル名を特定してグローバル変数に格納すると共に、
                    // ヘイワード１またはヘイワード２を特定する
                    string filterKey = decideReferenceTable(serialShort);

                    // テスターデータの当月テーブル・前月テーブルから、テスト結果等を取得する
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

                    // ＨＷ２の場合のみ、明細テーブル（当月・前月）から、特定の検査項目（振動）のテスト結果等を取得する
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
                    
                    // ヘッダーテーブルデータを、ＩＮＬＩＮＥ・ＯＱＣに分けて判定する
                    string filterLine = string.Empty;
                    string filterOqc = string.Empty;
                    if (filterKey == "HW1") { filterLine = fltHw1Line; filterOqc = fltHw1Oqc; }
                    else if (filterKey == "HW2") { filterLine = fltHw2Line; filterOqc = fltHw2Oqc; }
                    else if (filterKey == "HW3") { filterLine = fltHw3Line; filterOqc = fltHw3Oqc; }
                    else { filterLine = fltHw1Line; filterOqc = fltHw1Oqc; } //エラー対策

                    //① インラインのみのデータを取る
                    DataView dv1 = new DataView(dt1);
                    dv1.RowFilter = filterLine;
                    dv1.Sort = "tjudge, inspectdate";
                    System.Diagnostics.Debug.Print(System.Environment.NewLine + "In-Line:");
                    printDataView(dv1);
                    DataTable dt3 = dv1.ToTable();

                    //② ＳＩのみのデータを取る
                    DataView dv2 = new DataView(dt1);
                    dv2.RowFilter = filterOqc;
                    dv2.Sort = "tjudge, inspectdate";
                    System.Diagnostics.Debug.Print(System.Environment.NewLine + "OQC:");
                    printDataView(dv2);
                    DataTable dt4 = dv2.ToTable();

                    // モデルを特定し、一時テーブルへ登録する
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


                    //①インライン
                    // テスターデータに該当がない場合でも、ユーザーに認識させるために表示する
                    DataRow dr = dtOverall.NewRow();
                    dr["serialno"] = serial;
                    dr["model"] = model;
                    dr["datecd"] = VBS.Mid(serial, 4, 4).Length < 4 ? "Error" : VBS.Mid(serial, 4, 4);
                    dr["line"] = VBS.Mid(serial, 8, 1).Length < 1 ? "Eror" : VBS.Mid(serial, 8, 1);
                    dr["lot"] = VBS.Mid(serial, 4, 5).Length < 5 ? "Error" : VBS.Mid(serial, 4, 5);
                    dr["eeee"] = VBS.Mid(serial, 12, 4).Length < 4 ? "Error" : VBS.Mid(serial, 12, 4);
                    dr["return"] = formReturnMode ? "R" : "N";

                    // テスターデータに該当がある場合、当然表示する
                    if (dt3.Rows.Count != 0)
                    {
                        // テスターデータの判定文字列変換
                        string linepass = String.Empty;
                        string buff = dt3.Rows[0]["tjudge"].ToString();
                        if (buff == "0") linepass = "PASS";
                        else if (buff == "1") linepass = "FAIL";
                        else linepass = "ERROR";
                        // ＰＵＬＳＥ判定の追記
                        if (pulseNG) linepass = "PLS NG";

                        dr["stationid"] = dt3.Rows[0]["lot"].ToString();
                        dr["judge"] = linepass;
                        dr["testtime"] = (DateTime)dt3.Rows[0]["inspectdate"];
                    }

                    // メモリ上のテーブルにレコードを追加
                    dtOverall.Rows.Add(dr);


                    // ＯＱＣレコードを別タブに表示する
                    // ②ＳＩ
                    // テスターデータに該当がない場合でも、ユーザーに認識させるために表示する
                    DataRow dr_si = dtOqc.NewRow();
                    dr_si["serialno"] = serial;
                    dr_si["model"] = model;
                    dr_si["datecd"] = VBS.Mid(serial, 4, 4).Length < 4 ? "Error" : VBS.Mid(serial, 4, 4);
                    dr_si["line"] = VBS.Mid(serial, 8, 1).Length < 1 ? "Error" : VBS.Mid(serial, 8, 1);
                    dr_si["lot"] = VBS.Mid(serial, 4, 5).Length < 5 ? "Error" : VBS.Mid(serial, 4, 5);
                    dr_si["eeee"] = VBS.Mid(serial, 12, 4).Length < 4 ? "Error" : VBS.Mid(serial, 12, 4);
                    dr_si["return"] = formReturnMode ? "R" : "N";

                    // テスターデータに該当がある場合、当然表示する
                    if (dt4.Rows.Count != 0)
                    {
                        // テスターデータの判定文字列変換
                        string linepass = String.Empty;
                        string buff = dt4.Rows[0]["tjudge"].ToString();
                        if (buff == "0") linepass = "PASS";
                        else if (buff == "1") linepass = "FAIL";
                        else linepass = "ERROR";

                        // ＰＵＬＳＥ判定の追記
                        if (pulseNG) linepass = "PLS NG";

                        dr_si["stationid"] = dt4.Rows[0]["lot"].ToString();
                        dr_si["judge"] = linepass;
                        dr_si["testtime"] = (DateTime)dt4.Rows[0]["inspectdate"];
                    }

                    // メモリ上のテーブルにレコードを追加
                    dtOqc.Rows.Add(dr_si);


                    // データテーブルの先頭行のシリアルから、ＬＩＭＩＴを判断する
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

                        // ＵＳＥＲ９がＬＩＭＩＴを設定した場合は、それに従う
                        if (limit2 != 0) limit1 = limit2;
                    }

                    // データグリットビューの更新
                    updateDataGridViews(dtOverall, dtOqc, ref dgvInline, ref dgvOqc);
                }
                // 入力用テキストボックスを編集可能へ戻し、連続してスキャンできるよう、テキストを選択状態にする
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

        // サブプロシージャ：シリアルから、参照すべきＰＱＭテーブル名を特定する
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
            { tablekey = "ld4f"; filterkey = "HW1"; }// エラー対策

            testerTableThisMonth = tablekey + DateTime.Today.ToString("yyyyMM");
            testerTableLastMonth = tablekey + ((VBS.Right(DateTime.Today.ToString("yyyyMM"), 2) != "01") ?
                (long.Parse(DateTime.Today.ToString("yyyyMM")) - 1).ToString() : (long.Parse(DateTime.Today.ToString("yyyy")) - 1).ToString() + "12");

            return filterkey;
        }

        // ビューモードで再印刷を行う
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

        // 各種確認後、ボックスＩＤの発行、シリアルの登録、バーコードラベルのプリントを行う
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

            //一時テーブルのシリアル全てについて、本番テーブルに登録がないか、チェック
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

            //ボックスＩＤの新規採番
            string boxIdNew = getNewBoxId(cmbShaft.Text, cmbOverlay.Text, txtUser.Text);

            //先ずは、DataTbleにボックスＩＤを登録
            DataTable dt = dtOverall.Copy();
            dt.Columns.Add("boxid", Type.GetType("System.String"));
            for (int i = 0; i < dt.Rows.Count; i++)
                dt.Rows[i]["boxid"] = boxIdNew;

            //DataTableから本番テーブルへ一括登録
            TfSQL tf = new TfSQL();
            bool res1 = tf.sqlMultipleInsertOverall(dt);

            if (res1)
            {
                // バーコードを印字（念のためメインモデルを今一度取得した後）
                m_model = dtOverall.Rows[0]["model"].ToString();
                m_model_long = getMainModelLong(m_model);
                string shipKind = dtOverall.Rows[0]["return"].ToString();
                printBarcode(directory, boxIdNew, m_model_long, cmbShaft.Text, cmbOverlay.Text, dgvDateCode, ref dgvDateCode2, ref txtBoxIdPrint, shipKind);

                //データテーブルのレコード消去
                dtOverall.Clear();
                dtOqc.Clear();
                dt = null;

                txtBoxId.Text = boxIdNew;
                dtpPrintDate.Value = DateTime.ParseExact(VBS.Mid(boxIdNew, 3, 6), "yyMMdd", CultureInfo.InvariantCulture);

                //親フォームfrmBoxのデータグリットビューを更新するため、デレゲートイベントを発生させる
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
                //一旦登録したＢＯＸＩＤを消去する
                string sql = "delete from box_id_rt WHERE boxid= '" + boxIdNew + "'";
                int res = tf.sqlExecuteNonQueryInt(sql, false);

                MessageBox.Show("Box id and product serials were not registered.", "Process Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnRegisterBoxId.Enabled = true;
                btnDeleteSelection.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        // サブプロシージャ：データテーブルのシリアル全てについて、本テーブルに登録がないか一括確認
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

        // サブプロシージャ：これから発行するボックスＩＤの採番
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

        // サブプロシージャ：メインモデルの短い文字列から、長い文字列を取得する
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

        // サブプロシージャ：バーコードをプリントする（本バージョンは、ＢＯＸＩＤ名のテキストファイルを生成する）
        private void printBarcode(string dir, string id, string m_model_long, string shaft, string overlay, DataGridView dgv1, ref DataGridView dgv2, ref TextBox txt, string shipKind)
        {
            TfPrint tf = new TfPrint();
            tf.createBoxidFiles(dir, id, m_model_long, shaft, overlay, dgv1, ref dgv2, ref txt, shipKind);
        }

        // 一時テーブルの選択された複数レコードを、一括消去させる
        private void btnDeleteSelection_Click(object sender, EventArgs e)
        {
            DataGridView dgv = new DataGridView();

            if (tabControl1.SelectedTab == tabControl1.TabPages["tabInline"])
                dgv = dgvInline;
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabOqc"])
                dgv = dgvOqc;

            // セルの選択範囲が２列以上の場合は、メッセージの表示のみでプロシージャを抜ける
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

        // １ラベルあたりのシリアル数を変更する（管理権限ユーザーのみ）
        private void btnChangeLimit_Click(object sender, EventArgs e)
        {
            // フォーム４（１ラベルあたりシリアル数変更）を、デレゲートイベントを付加して開く
            bool bl = TfGeneral.checkOpenFormExists("frmCapacity");
            if (bl)
            {
                MessageBox.Show("Please close or complete another form.", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }
            else
            {
                frmCapacity f4 = new frmCapacity();
                //子イベントをキャッチして、データグリッドを更新する
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

        // 登録済みのボックスＩＤへ、モジュールを追加（管理ユーザーのみ）
        private void btnAddSerial_Click(object sender, EventArgs e)
        {
            // 追加モードでない場合は、追加モードの表示へ切り替える
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
            // 既に追加モードの場合は、ＤＢへの登録を行う
            else
            {
                // ＤＥＬＥＴＥ ＳＱＬ文を発行し、データベースから削除する
                string boxId = txtBoxId.Text;
                string sql = "delete from product_serial_rt where boxid = '" + boxId + "'";
                System.Diagnostics.Debug.Print(sql);
                TfSQL tf = new TfSQL();
                bool res1 = tf.sqlExecuteNonQuery(sql, false);

                // DataTbleにボックスＩＤを追加し、本番テーブルへ一括登録
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

                // 追加モードを終了し、閲覧モードの表示へ切り替える
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

        // 登録済みのボックスＩＤの、モジュールを削除（管理ユーザーのみ）
        private void btnDeleteSerial_Click(object sender, EventArgs e)
        {
            // セルの選択範囲が２列以上の場合は、メッセージの表示のみでプロシージャを抜ける
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
                // ＤＥＬＥＴＥ ＳＱＬ文を発行し、データベースから削除する
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
                    // データグリッドビューから削除する
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
        
        // 登録済みのボックスＩＤおよび該当モジュールの削除（管理ユーザーのみ）
        private void btnCancelBoxid_Click(object sender, EventArgs e)
        {
            // 本当に削除してよいか、２重で確認する。
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
                    // データグリットビューの更新
                    updateDataGridViews(dtOverall, dtOqc, ref dgvInline, ref dgvOqc);

                    //親フォームfrmBoxのデータグリットビューを更新するため、デレゲートイベントを発生させる
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

        // サブサブプロシージャ：ＤＢからのＤＴへの読み込み
        private void readDatatable(ref DataTable dt)
        {
            string boxId = txtBoxId.Text;
            string sql = "select serialno, model, datecd, line, lot, eeee, stationid, judge, testtime, return " +
                "FROM product_serial_rt WHERE boxid='" + boxId + "'";
            TfSQL tf = new TfSQL();
            tf.sqlDataAdapterFillDatatable(sql, ref dt);
        }

        // キャンセル時に、データテーブルのレコードの保持ができない旨、警告する
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // frmCapacity （ＢＯＸあたりシリアル個数）を閉じていない場合は、先に閉じるよう通知する
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

            // データテーブルのレコード件数がない場合、または編集モードの場合は、そのまま閉じる                        
            if (dtOverall.Rows.Count == 0 || !formEditMode)
            {
                Application.OpenForms["frmBox"].Focus();
                Close();
                return;
            }

            // データテーブルのレコード件数がある場合、一時的に保持されているレコードが消滅する旨、警告する
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

        // 閉じるボタンやショートカットでの終了を許さない
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x112;
            const long SC_CLOSE = 0xF060L;
            if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE) { return; }
            base.WndProc(ref m);
        }

        //MP3ファイル（今回は警告音）を再生する
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

        // データをエクセルへエクスポート
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

        // サブプロシージャ：データテーブルの中身をチェックする、本アプリケーションに対して、直接は関係ない
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

        // サブプロシージャ：データビューの中身をチェックする、本アプリケーションに対して、直接は関係ない
        private void printDataView(DataView dv)
        {
            foreach (DataRowView drv in dv)
            {
                System.Diagnostics.Debug.Print(drv["lot"].ToString() + " " +
                    drv["tjudge"].ToString() + " " + drv["inspectdate"].ToString());
            }
        }

        // コンボボックスへ候補を格納する
        private void cmbShaft_Enter(object sender, EventArgs e)
        {
            if (m_model == null) return;

            string sql = "select distinct shaft from mdl_sht_ovl where model='" + m_model + "'";
            System.Diagnostics.Debug.Print(sql);
            TfSQL tf = new TfSQL();
            tf.getComboBoxDataViaCsv(sql, ref cmbShaft);
        }

        // コンボボックスへ候補を格納する
        private void cmbOverlay_Enter(object sender, EventArgs e)
        {
            if (m_model == null) return;

            string sql = "select distinct over_lay from mdl_sht_ovl where model='" + m_model + "'";
            System.Diagnostics.Debug.Print(sql);
            TfSQL tf = new TfSQL();
            tf.getComboBoxDataViaCsv(sql, ref cmbOverlay);
        }

        // ＥＮＴＥＲＫＥＹ押下で、登録用マスタを呼び出す
        private void cmbShaft_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || user != "User_9") return;
            if (TfGeneral.checkOpenFormExists("frmShaftOverlay")) return;

            frmShaftOverlay f7 = new frmShaftOverlay();
            f7.Show();
        }

        // ＥＮＴＥＲＫＥＹ押下で、登録用マスタを呼び出す
        private void cmbOverlay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || user != "User_9") return;
            if (TfGeneral.checkOpenFormExists("frmShaftOverlay")) return;

            frmShaftOverlay f7 = new frmShaftOverlay();
            f7.Show();
        }

    }
}