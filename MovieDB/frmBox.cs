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
        //親フォームfrmLoginへ、イベント発生を連絡（デレゲート）
        public delegate void RefreshEventHandler(object sender, EventArgs e);
        public event RefreshEventHandler RefreshEvent;

        //データグリッドビュー用ボタン
        DataGridViewButtonColumn openBoxId;
        DataGridViewButtonColumn editShipDate;

        //その他非ローカル変数

        // コンストラクタ
        public frmBox()
        {
            InitializeComponent();
        }

        // ロード時の処理
        private void frmBox_Load(object sender, EventArgs e)
        {
            //フォームの場所を指定
            this.Left = 50;
            this.Top = 10;
            updateDataGridViews(ref dgvBoxId, true);

            // ＤＡＴＥＴＩＭＥＰＩＣＫＥＲの分以下を下げる
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

        // サブプロシージャ：データグリットビューの更新。親フォームで呼び出し、親フォームの情報を引き継ぐ
        public void updateControls(string user)
        {
            txtUser.Text = user;
        }

        // サブプロシージャ：データテーブルの定義
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


        // サブプロシージャ：データグリットビューの更新
        public void updateDataGridViews(ref DataGridView dgv, bool load)
        {
            string boxId = txtBoxIdFrom.Text;
            DateTime printDate = dtpPrintDate.Value;
            DateTime shipDate = dtpShipDate.Value;
            string serialNo = txtProductSerial.Text;
            string sql = String.Empty;

            // ＳＱＬ結果を、ＤＴＡＡＴＡＢＬＥへ格納
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

            // データグリットビューへＤＴＡＡＴＡＢＬＥを格納
            dgv.DataSource = dt1;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // グリットビュー右端にボタンを追加（初回のみ）
            if (load) addButtonsToDataGridView(dgv);

            //行ヘッダーに行番号を表示する
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
            //行ヘッダーの幅を自動調節する
            dgv.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            // 一番下の行を表示する
            if (dgv.Rows.Count != 0)
                dgv.FirstDisplayedScrollingRowIndex = dgv.Rows.Count - 1;

            // パネルにバーコードを表示
            pnlBarcode.Refresh();
         }
        
        // サブサブプロシージャ：グリットビュー右端にボタンを追加
        private void addButtonsToDataGridView(DataGridView dgv)
        {
            // 開くボタンは全てのユーザー
            openBoxId = new DataGridViewButtonColumn();
            openBoxId.HeaderText = "Open";
            openBoxId.Text = "Open";
            openBoxId.UseColumnTextForButtonValue = true;
            openBoxId.Width = 80;
            dgv.Columns.Add(openBoxId);

            // 出荷日編集は、特定のユーザー
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
                
        // ボタン１はフォーム３を閲覧モードで開く（デレゲートなし）、ボタン２は出荷日の編集
        private void dgvBoxId_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int currentRow = int.Parse(e.RowIndex.ToString());

            if (dgvBoxId.Columns[e.ColumnIndex] == openBoxId && currentRow >= 0)
            {
                //既にfrmModule が開かれている場合は、それ閉じるように促す
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
                //子イベントをキャッチして、データグリッドを更新する
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

        // 検索ボタン押下、実際はグリットビューの更新をするだけ
        private void btnSearchBoxId_Click(object sender, EventArgs e)
        {
            updateDataGridViews(ref dgvBoxId, false);
        }

        // フォーム３を編集モードで開く、デレゲートあり
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
                //子イベントをキャッチして、データグリッドを更新する
                f3.RefreshEvent += delegate(object sndr, EventArgs excp) 
                {
                    updateDataGridViews(ref dgvBoxId, false);
                    this.Focus(); 
                };

                f3.updateControls(String.Empty, DateTime.Now, String.Empty, String.Empty, String.Empty, user, true, false);
                f3.Show();            
            }
        }

        // 返品の登録：フォーム３を編集モードで開く、デレゲートあり
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
                //子イベントをキャッチして、データグリッドを更新する
                f3.RefreshEvent += delegate(object sndr, EventArgs excp)
                {
                    updateDataGridViews(ref dgvBoxId, false);
                    this.Focus();
                };

                f3.updateControls(String.Empty, DateTime.Now, String.Empty, String.Empty, String.Empty, user, true, true);
                f3.Show();
            }
        }

        // 出荷日を一括登録する
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

        // サブプロシージャ：装飾用のバーコード表示パネルの更新、実際の出力とは関係のないライブラリを使用している
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

        //frmBoxを閉じる際、非表示になっている親フォームfrmLoginを閉じる
        private void frmBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            //親フォームfrmLoginを閉じるよう、デレゲートイベントを発生させる
            this.RefreshEvent(this, new EventArgs());
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

        // フォーム３が開かれていないことを確認してから、閉じる
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

        // ラジオボタン「ボックスＩＤ」変更時の処理（テキストボックス編集による検索条件の変更）
        private void rdbBoxId_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbBoxId.Checked) { txtProductSerial.Text = String.Empty; }
        }
        // ラジオボタン「プリント日付」変更時の処理（テキストボックス編集による検索条件の変更）
        private void rdbPrintDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPrintDate.Checked)
            {
                txtBoxIdFrom.Text = String.Empty;
                txtProductSerial.Text = String.Empty;
            }
        }
        // ラジオボタン「製品シリアル」変更時の処理（テキストボックス編集による検索条件の変更）
        private void rdbProductSerial_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbProductSerial.Checked)
            {
                txtBoxIdFrom.Text = String.Empty;
            }
        }
        // ラジオボタン「出荷日」変更時の処理（テキストボックス編集による検索条件の変更）
        private void rdbShipDate_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rdbShipDate.Checked)
            {
                txtBoxIdFrom.Text = String.Empty;
                txtBoxIdTo.Text = String.Empty;
                txtProductSerial.Text = String.Empty;
            }
        }

        // サブサブプロシージャ：ＤＡＴＥＴＩＭＥＰＩＣＫＥＲの分以下を下げる
        private void dtpRounddownHour(DateTimePicker dtp)
        {
            DateTime dt = dtp.Value;
            int hour = dt.Hour;
            int minute = dt.Minute;
            int second = dt.Second;
            int millisecond = dt.Millisecond;
            dtp.Value = dt.AddHours(-hour).AddMinutes(-minute).AddSeconds(-second).AddMilliseconds(-millisecond);
        }

        // データをエクセルへエクスポート
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