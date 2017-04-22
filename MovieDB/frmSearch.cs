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
using Excel = Microsoft.Office.Interop.Excel; 

namespace BoxIdDb
{
    public partial class frmSearch : Form
    {
        //親フォームfrmLoginへ、イベント発生を連絡（デレゲート）
        public delegate void RefreshEventHandler(object sender, EventArgs e);
        public event RefreshEventHandler RefreshEvent;

        // コンストラクタ
        public frmSearch()
        {
            InitializeComponent();
        }

        // ロード時の処理
        private void frmModule_Load(object sender, EventArgs e)
        {
            //フォームの場所を指定
            this.Left = 250;
            this.Top = 30;

            // ＤＡＴＥＴＩＭＥＰＩＣＫＥＲ日付の調整
            dtpSet0daysBefore(dtpPackDateFrom);
            dtpRoundUpHour(dtpPackDateTo);
            dtpSet0daysBefore(dtpPrintDateFrom);
            dtpRoundUpHour(dtpPrintDateTo);
        }

        // フォームの条件をＡＮＤで結合し、ＳＱＬ問い合わせ結果をデータグリッドビューに反映
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string invoiceFrom = txtInvoiceFrom.Text;
            string invoiceTo = txtInvoiceTo.Text;
            string cartonFrom = txtCartonIdFrom.Text;
            string cartonTo = txtCartonIdTo.Text;
            DateTime packFrom = dtpPackDateFrom.Value;
            DateTime packTo = dtpPackDateTo.Value;
            string boxFrom = txtBoxIdFrom.Text;
            string boxTo = txtBoxIdTo.Text;
            DateTime printFrom = dtpPrintDateFrom.Value;
            DateTime printTo = dtpPrintDateTo.Value;
            string serialFrom = txtProductSerialFrom.Text;
            string serialTo = txtProductSerialTo.Text;
            string returnState = cmbReturn.Text;
            bool b_invoice = chkInvoice.Checked;
            bool b_carton = chkCartonId.Checked;
            bool b_pack = chkPackDate.Checked;
            bool b_box = chkBoxId.Checked;
            bool b_print = chkPrintDate.Checked;
            bool b_serial = chkProductSerial.Checked;
            bool b_return = chkReturn.Checked;

            string sql1 = "select invoice, invctnno, cartonid, packdate, boxid, printdate, serialno, model, datecd, line, " +
                "lot, eeee, stationid, judge, testtime, return, shaft, over_lay FROM v_invoice_serial WHERE ";

            bool[] cr = { invoiceFrom == String.Empty ? false : true,
                          invoiceTo   == String.Empty ? false : true,
                          cartonFrom  == String.Empty ? false : true,
                          cartonTo    == String.Empty ? false : true,
                                                                true,
                                                                true,
                          boxFrom     == String.Empty ? false : true,
                          boxTo       == String.Empty ? false : true,
                                                                true,
                                                                true,
                          serialFrom  == String.Empty ? false : true,
                          serialTo    == String.Empty ? false : true,
                          returnState == String.Empty ? false : true };

            bool[] ck = { b_invoice,
                          b_invoice,
                          b_carton,
                          b_carton,
                          b_pack,
                          b_pack,
                          b_box,
                          b_box,
                          b_print,
                          b_print,
                          b_serial,
                          b_serial,
                          b_return };

            string sql2 = (!(cr[0] && ck[0]) ? String.Empty   : "invoice >= '" + invoiceFrom + "' AND ") +
                          (!(cr[1] && ck[1]) ? String.Empty   : "invoice <= '" + invoiceTo + "' AND ") +
                          (!(cr[2] && ck[2]) ? String.Empty   : "cartonid >= '" + cartonFrom + "' AND ") +
                          (!(cr[3] && ck[3]) ? String.Empty   : "cartonid <= '" + cartonTo + "' AND ") +
                          (!(cr[4] && ck[4]) ? String.Empty   : "packdate >= '" + packFrom.ToString() + "' AND ") +
                          (!(cr[5] && ck[5]) ? String.Empty   : "packdate <= '" + packTo.ToString() + "' AND ") +
                          (!(cr[6] && ck[6]) ? String.Empty   : "boxid >= '" + boxFrom + "' AND ") +
                          (!(cr[7] && ck[7]) ? String.Empty   : "boxid <= '" + boxTo + "' AND ") +
                          (!(cr[8] && ck[8]) ? String.Empty   : "printdate >= '" + printFrom.ToString() + "' AND ") +
                          (!(cr[9] && ck[9]) ? String.Empty   : "printdate <= '" + printTo.ToString() + "' AND ") +
                          (!(cr[10] && ck[10]) ? String.Empty : "serialno >= '" + serialFrom + "' AND ") +
                          (!(cr[11] && ck[11]) ? String.Empty : "serialno <= '" + serialTo + "' AND ") +
                          (!(cr[12] && ck[12]) ? String.Empty : "return = '" + returnState + "' AND ");

            bool b_all = (cr[0] && ck[0]) || (cr[1] && ck[1]) || (cr[2] && ck[2]) || (cr[3] && ck[3]) || (cr[4] && ck[4]) ||
                         (cr[5] && ck[5]) || (cr[6] && ck[6]) || (cr[7] && ck[7]) || (cr[8] && ck[8]) || (cr[9] && ck[9]) ||
                         (cr[10] && ck[10]) || (cr[11] && ck[11]) || (cr[12] && ck[12]) ;

            System.Diagnostics.Debug.Print(b_all.ToString());

            System.Diagnostics.Debug.Print(cr[0].ToString() + " " + ck[0].ToString() + " " + cr[1].ToString() + " " + ck[1].ToString() + " " + cr[2].ToString() + " " + ck[2].ToString() + " " + cr[3].ToString() + " " + ck[3].ToString() + " " + cr[4].ToString() + " " + ck[4].ToString() + System.Environment.NewLine +
                         cr[5].ToString() + " " + ck[5].ToString() + " " + cr[6].ToString() + " " + ck[6].ToString() + " " + cr[7].ToString() + " " + ck[7].ToString() + " " + cr[8].ToString() + " " + ck[8].ToString() + " " + cr[9].ToString() + " " + ck[9].ToString() + System.Environment.NewLine +
                         cr[10].ToString() + " " + ck[10].ToString() + " " + cr[11].ToString() + " " + ck[11].ToString() + " " + cr[12].ToString() + " " + ck[12].ToString());

            if (!b_all)
            {
                MessageBox.Show("Please select at least one check box and fill the criteria.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                return;
            }

            string sql3 = sql1 + VBS.Left(sql2, sql2.Length - 5);
            System.Diagnostics.Debug.Print(sql3);

            btnSearch.Enabled = false;
            btnExport.Enabled = false;

            DataTable dataTable = new DataTable();
            TfSQL tf = new TfSQL();
            tf.sqlDataAdapterFillDatatable(sql3, ref dataTable);
            updateDataGridViews(dataTable, ref dgvProductSerial);

            btnSearch.Enabled = true;
            btnExport.Enabled = true;
        }

        // サブプロシージャ：データグリットビューの更新
        private void updateDataGridViews(DataTable dt, ref DataGridView dgv)
        {
            // データグリットビューへＳＱＬ問い合わせ結果を格納
            dgv.DataSource = dt;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            //行ヘッダーに行番号を表示する
            for (int i = 0; i < dgv.Rows.Count; i++)
                dgv.Rows[i].HeaderCell.Value = (i + 1).ToString();

            //行ヘッダーの幅を自動調節する
            dgv.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);        

            // 一番下の行を表示する
            if (dgv.Rows.Count >= 1)
                dgv.FirstDisplayedScrollingRowIndex = dgv.Rows.Count - 1;
        }

        // サブサブプロシージャ：ＤＡＴＥＴＩＭＥＰＩＣＫＥＲを当日の日付にする
        private void dtpSet0daysBefore(DateTimePicker dtp)
        {
            DateTime dt = dtp.Value.Date.AddDays(0);
            dtp.Value = dt;
        }

        // サブサブプロシージャ：ＤＡＴＥＴＩＭＥＰＩＣＫＥＲの分以下を切り上げる
        private void dtpRoundUpHour(DateTimePicker dtp)
        {
            DateTime dt = dtp.Value;
            int hour = dt.Hour;
            int minute = dt.Minute;
            int second = dt.Second;
            int millisecond = dt.Millisecond;
            dtp.Value = dt.AddHours(1).AddMinutes(-minute).AddSeconds(-second).AddMilliseconds(-millisecond);
        }

        // ＣＳＶ出力
        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)dgvProductSerial.DataSource;
            ExcelClass xl = new ExcelClass();
            //xl.ExportToExcel(dt);
            xl.ExportToCsv(dt, System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\boxid.csv");
        }

        private void frmSearch_FormClosed(object sender, FormClosedEventArgs e)
        {
            //親フォームfrmLoginを閉じるよう、デレゲートイベントを発生させる
            this.RefreshEvent(this, new EventArgs());
        }
    }
}