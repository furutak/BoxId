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
        //親フォームfrmLoginへ、イベント発生を連絡（デレゲート）
        public delegate void RefreshEventHandler(object sender, EventArgs e);
        public event RefreshEventHandler RefreshEvent;

        //データグリッドビュー用ボタン
        DataGridViewButtonColumn updateId;
        DataGridViewButtonColumn replaceId;
        DataGridViewButtonColumn editShip;

        //その他非ローカル変数
        DataTable dtCarton;

        // コンストラクタ
        public frmCarton()
        {
            InitializeComponent();
        }

        // ロード時の処理
        private void frmCarton_Load(object sender, EventArgs e)
        {
            //フォームの場所を指定
            this.Left = 50;
            this.Top = 10;

            dtCarton = new DataTable();
            defineDatatable(ref dtCarton);
            updateDataGridView(ref dgvCartonId, dtCarton, true);

            // ＤＡＴＥＴＩＭＥＰＩＣＫＥＲの分以下を下げる
            dtpRounddownHour(dtpShipDate);

            // 出荷日の一括登録は、ユーザー９のみが可能
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

        // サブプロシージャ：データグリットビューの更新。親フォームで呼び出し、親フォームの情報を引き継ぐ
        public void updateControls(string user)
        {
            txtUser.Text = user;
        }

        // サブプロシージャ：データテーブルの定義
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

        // サブプロシージャ：データグリットビューの更新
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

            // ＳＱＬ結果を、ＤＴＡＡＴＡＢＬＥへ格納
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

            // データグリットビューへＤＴＡＡＴＡＢＬＥを格納
            dgv.DataSource = dt;
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
         }
        
        // サブサブプロシージャ：グリットビュー右端にボタンを追加
        private void addButtonsToDataGridView(DataGridView dgv)
        {
            // 更新・置換・出荷日編集の権限は、全てのユーザーに与える
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
                
        // ボタン１はフォーム９を第２登録モードで開く（デレゲートあり）、ボタン２は出荷日の編集
        private void dgvBoxId_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int currentRow = int.Parse(e.RowIndex.ToString());

            if (dgvCartonId.Columns[e.ColumnIndex] == updateId && currentRow >= 0)
            {
                //既にfrmModule が開かれている場合は、それを閉じる
                TfGeneral.closeOpenForm("frmCartonContent");

                string cartonId = dgvCartonId["cartonid", currentRow].Value.ToString();
                string boxId1 = dgvCartonId["boxid1", currentRow].Value.ToString();
                string boxId2 = dgvCartonId["boxid2", currentRow].Value.ToString();
                DateTime packDate = DateTime.Parse(dgvCartonId["packdate", currentRow].Value.ToString());
                string user = dgvCartonId["suser", currentRow].Value.ToString();
                string invoice = dgvCartonId["invoice", currentRow].Value.ToString();
                string cartonNo = dgvCartonId["invctnno", currentRow].Value.ToString();

                frmCartonContent f9 = new frmCartonContent();
                //子イベントをキャッチして、データグリッドを更新する
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
                //既にfrmModule が開かれている場合は、それを閉じる
                TfGeneral.closeOpenForm("frmCartonContent");

                string cartonId = dgvCartonId["cartonid", currentRow].Value.ToString();
                string boxId1 = dgvCartonId["boxid1", currentRow].Value.ToString();
                string boxId2 = dgvCartonId["boxid2", currentRow].Value.ToString();
                DateTime packDate = DateTime.Parse(dgvCartonId["packdate", currentRow].Value.ToString());
                string user = dgvCartonId["suser", currentRow].Value.ToString();
                string invoice = dgvCartonId["invoice", currentRow].Value.ToString();
                string cartonNo = dgvCartonId["invctnno", currentRow].Value.ToString();

                frmCartonContent f9 = new frmCartonContent();
                //子イベントをキャッチして、データグリッドを更新する
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

        // 検索ボタン押下、実際はグリットビューの更新をするだけ
        private void btnSearchBoxId_Click(object sender, EventArgs e)
        {
            updateDataGridView(ref dgvCartonId, dtCarton, false);
        }

        // フォーム９を初回登録モードで開く、デレゲートあり
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
                //子イベントをキャッチして、データグリッドを更新する
                f9.RefreshEvent += delegate(object sndr, EventArgs excp)
                {
                    updateDataGridView(ref dgvCartonId, dtCarton, false);
                    this.Focus();
                };

                f9.Show();
                f9.updateControls(String.Empty, String.Empty, String.Empty, DateTime.Now, user, String.Empty, String.Empty, "Step1", "Add");
            }
        }
        
        // 出荷日を一括登録する
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

        //frmCartonを閉じる際、非表示になっている親フォームfrmLoginを閉じる
        private void frmCarton_FormClosed(object sender, FormClosedEventArgs e)
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
            dt = (DataTable)dgvCartonId.DataSource;
            ExcelClass xl = new ExcelClass();
            xl.ExportToExcel(dt);
            //xl.ExportToCsv(dt, System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\ipqcdb.csv");
        }

        // ラジオボタン「カートンＩＤ」変更時の処理
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
        // ラジオボタン「梱包日」変更時の処理
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
        // ラジオボタン「ボックスＩＤ」変更時の処理
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
        // ラジオボタン「インボイス番号」変更時の処理
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
        // ラジオボタン「出荷日」変更時の処理
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
        // ラジオボタン「製品シリアル」変更時の処理
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