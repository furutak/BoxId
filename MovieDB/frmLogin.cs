using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb; 
using System.Security.Permissions;

namespace BoxIdDb
{
    public partial class frmLogin : Form
    {
        // コンストラクタ
        public frmLogin()
        {
            InitializeComponent();
        }

        // ロード時の処理（コンボボックスに、オートコンプリート機能の追加）
        private void frmLogin_Load(object sender, EventArgs e)
        {
            string sql = "select DISTINCT suser FROM s_user ORDER BY suser";
            TfSQL tf = new TfSQL();
            tf.getComboBoxData(sql, ref cmbUserName);
        }

        // ＯＱＣユーザーログイン
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string sql = null;
            string user = null;
            string pass = null;
            bool login = false;

            user = cmbUserName.Text;

            if (user != null) 
            {
                TfSQL tf = new TfSQL();

                sql = "select pass FROM s_user WHERE suser='" + user + "'";
                pass = tf.sqlExecuteScalarString(sql);

                sql = "select loginstatus FROM s_user WHERE suser='" + user + "'";
                login = tf.sqlExecuteScalarBool(sql); 

                if (pass == txtPassword.Text)
                {
                    if (login) 
                    { 
                        DialogResult reply = MessageBox.Show("This user account is currently used by other user," + System.Environment.NewLine +
                            "or the log out last time had a problem." + System.Environment.NewLine + "Do you log in with this account ?", 
                            "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (reply == DialogResult.No)
                        {
                            return;
                        }
                    }

                    // ログイン状態をＴＲＵＥへ変更
                    sql = "UPDATE s_user SET loginstatus=true WHERE suser='" + user + "'";
                    bool res = tf.sqlExecuteNonQuery(sql, false);

                    // 子フォームfrmBoxを表示し、デレゲートイベントを追加： 
                    frmBox f1 = new frmBox();
                    f1.RefreshEvent += delegate(object sndr, EventArgs excp)
                    {
                        // frmBoxを閉じる際、ログイン状態をＦＡＬＳＥへ変更し、当フォームfrmLoginも閉じる
                        sql = "UPDATE s_user SET loginstatus=false WHERE suser='" + user + "'";
                        res = tf.sqlExecuteNonQuery(sql, false);
                        this.Close();
                    };
                    f1.updateControls(user);
                    f1.Show();
                    this.Visible = false;
                }
                else if(pass != txtPassword.Text)
                {
                    MessageBox.Show("Password does not match", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // 検索メニュースタート
        private void btnOpenSerchMenu_Click(object sender, EventArgs e)
        {
            // 子フォームfrmSearchを表示し、デレゲートイベントを追加： 
            frmSearch f6 = new frmSearch();
            f6.RefreshEvent += delegate(object sndr, EventArgs excp)
            {
                this.Close();
            };
            f6.Show();
            this.Visible = false;
        }

        // ＰＣユーザーログイン
        private void btnPcLogIn_Click(object sender, EventArgs e)
        {
            string sql = null;
            string user = null;
            string pass = null;
            bool login = false;

            user = cmbUserName.Text;

            if (user != null)
            {
                TfSQL tf = new TfSQL();

                sql = "select pass FROM s_user WHERE suser='" + user + "'";
                pass = tf.sqlExecuteScalarString(sql);

                sql = "select loginstatus FROM s_user WHERE suser='" + user + "'";
                login = tf.sqlExecuteScalarBool(sql);

                if (pass == txtPassword.Text)
                {
                    if (login)
                    {
                        DialogResult reply = MessageBox.Show("This user account is currently used by other user," + System.Environment.NewLine +
                            "or the log out last time had a problem." + System.Environment.NewLine + "Do you log in with this account ?",
                            "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (reply == DialogResult.No)
                        {
                            return;
                        }
                    }

                    // ログイン状態をＴＲＵＥへ変更
                    sql = "UPDATE s_user SET loginstatus=true WHERE suser='" + user + "'";
                    bool res = tf.sqlExecuteNonQuery(sql, false);

                    // 子フォームfrmBoxを表示し、デレゲートイベントを追加： 
                    frmCarton f8 = new frmCarton();
                    f8.RefreshEvent += delegate(object sndr, EventArgs excp)
                    {
                        // frmCartonを閉じる際、ログイン状態をＦＡＬＳＥへ変更し、当フォームfrmLoginも閉じる
                        sql = "UPDATE s_user SET loginstatus=false WHERE suser='" + user + "'";
                        res = tf.sqlExecuteNonQuery(sql, false);
                        this.Close();
                    };
                    f8.updateControls(user);
                    f8.Show();
                    this.Visible = false;
                }
                else if (pass != txtPassword.Text)
                {
                    MessageBox.Show("Password does not match", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}



