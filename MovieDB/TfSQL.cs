using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using Npgsql;

namespace BoxIdDb
{
    public class TfSQL
    {
        NpgsqlConnection connection;
        static string conStringBoxidDb = @"Server=192.168.193.4;Port=5432;User Id=pqm;Password=dbuser;Database=boxiddb; CommandTimeout=100; Timeout=100;";
        static string conStringTesterDb = @"Server=192.168.193.5;Port=5432;User Id=pqm;Password=dbuser;Database=pqmdb; CommandTimeout=100; Timeout=100;";

        public void getComboBoxData(string sql, ref ComboBox cmb)
        {
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
            NpgsqlCommand command;
            DataSet ds = new DataSet();
            try
            {
                connection = new NpgsqlConnection(conStringBoxidDb);
                connection.Open();
                command = new NpgsqlCommand(sql, connection);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    cmb.Items.Add(row[0].ToString());
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                connection.Close();
            }
        }

        public void getComboBoxDataViaCsv(string sql, ref ComboBox cmb)
        {
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
            NpgsqlCommand command;
            DataSet ds = new DataSet();
            try
            {
                connection = new NpgsqlConnection(conStringBoxidDb);
                connection.Open();
                command = new NpgsqlCommand(sql, connection);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                connection.Close();

                string text = ds.Tables[0].Rows[0][0].ToString();
                if (text == String.Empty) return;

                cmb.Items.Clear();
                string[] words = text.Split(',');
                foreach (string s in words)
                {
                    string t = s.Trim();
                    cmb.Items.Add(t); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                connection.Close();
            }
        }
        public void getAutoCompleteData(string sql, ref TextBox txt)
        {
            txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
            NpgsqlCommand command;
            DataSet ds = new DataSet();
            try
            {
                connection = new NpgsqlConnection(conStringBoxidDb);
                connection.Open();
                command = new NpgsqlCommand(sql, connection);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                connection.Close();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DataCollection.Add(row[0].ToString());
                }
                txt.AutoCompleteCustomSource = DataCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                connection.Close();
            }
        }

        // 本プロジェクトでの用途：当月テーブルと翌月テーブルから、ＰＡＳＳの一番古い日付を取得
        // データが存在しない場合は、ＤＡＴＥＴＩＭＥの最大値を返す
        public DateTime sqlExecuteScalarDate(string sql)
        {
            DateTime response;
            try
            {
                connection = new NpgsqlConnection(conStringTesterDb);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.CommandText = sql;
                response = (DateTime)command.ExecuteScalar();
                connection.Close();
                return response;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                System.Diagnostics.Debug.Print(ex.Message);
                connection.Close();
                return DateTime.MaxValue;
            }
        }

        public double sqlExecuteScalarDouble(string sql)
        {
            double response;
            try
            {
                connection = new NpgsqlConnection(conStringBoxidDb);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                response = Convert.ToDouble(command.ExecuteScalar());
                connection.Close();
                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL executeschalar moethod failed." + "\r\n" + ex.Message
                                , "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                connection.Close();
                return 100;
            }
        }

        public string sqlExecuteScalarString(string sql)
        {
            string response;
            try
            {
                connection = new NpgsqlConnection(conStringBoxidDb);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                response = Convert.ToString(command.ExecuteScalar());
                connection.Close();
                return response;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                System.Diagnostics.Debug.Print(ex.Message);
                connection.Close();
                return String.Empty;
            }
        }

        public bool sqlExecuteScalarBool(string sql)
        {
            bool response;
            try
            {
                connection = new NpgsqlConnection(conStringBoxidDb);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                response = (bool)command.ExecuteScalar();
                connection.Close();
                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL executeschalar moethod failed." + "\r\n" + ex.Message
                                , "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                connection.Close();
                return false;
            }
        }

        public long sqlExecuteScalarLong(string sql)
        {
            long response;
            try
            {
                connection = new NpgsqlConnection(conStringBoxidDb);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                response = (long)command.ExecuteScalar();
                connection.Close();
                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL executeschalar moethod failed." + "\r\n" + ex.Message
                                , "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                connection.Close();
                return 0;
            }
        }

        public bool sqlExecuteNonQuery(string sql, bool result_message_show)
        {
            try
            {
                connection = new NpgsqlConnection(conStringBoxidDb);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                int response = command.ExecuteNonQuery();
                if (response >= 1)
                {
                    if (result_message_show) { MessageBox.Show("Successful!", "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Information); }                    
                    connection.Close();
                    return true;
                }
                else
                {
                    MessageBox.Show("Not successful!", "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    connection.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Not successful!" + "\r\n" + ex.Message
                                , "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                connection.Close();
                return false;
            }
        }

        public int sqlExecuteNonQueryInt(string sql, bool result_message_show)
        {
            try
            {
                connection = new NpgsqlConnection(conStringBoxidDb);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                int response = command.ExecuteNonQuery();
                if (response >= 1)
                {
                    if (result_message_show) { MessageBox.Show("Successful!", "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    connection.Close();
                    return response;
                }
                else
                {
                    MessageBox.Show("Not successful!", "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    connection.Close();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Not successful!" + "\r\n" + ex.Message
                                , "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                connection.Close();
                return 0;
            }
        }

        // ＦＩＬＬ ＦＲＯＭ ＢＯＸＩＤ ＤＢ
        public void sqlDataAdapterFillDatatable(string sql, ref DataTable dt)
        {
            NpgsqlConnection connection = new NpgsqlConnection(conStringBoxidDb);
            NpgsqlCommand command = new NpgsqlCommand();

            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
            {
                command.CommandText = sql;
                command.Connection = connection;
                adapter.SelectCommand = command;
                adapter.Fill(dt);
             }
        }

        // ＦＩＬＬ ＦＲＯＭ ＰＱＭ ＴＥＳＴＥＲ ＤＢ
        public void sqlDataAdapterFillDatatableFromTesterDb(string sql, ref DataTable dt)
        {
            NpgsqlConnection connection = new NpgsqlConnection(conStringTesterDb);
            NpgsqlCommand command = new NpgsqlCommand();

            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
            {
                command.CommandText = sql;
                command.Connection = connection;
                adapter.SelectCommand = command;
                adapter.Fill(dt);
            }
        }

        // ボックスＩＤ・シリアルの一括登録
        public bool sqlMultipleInsertOverall(DataTable dt)
        {
            int res1;
            bool res2 = false;
            connection = new NpgsqlConnection(conStringBoxidDb);
            connection.Open();
            NpgsqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

            try
            {
                string sql = "INSERT INTO product_serial_rt(boxid, serialno, model, datecd, line, lot, eeee, stationid, judge, testtime, return) " +
                    "VALUES (:boxid, :serialno, :model, :datecd, :line, :lot, :eeee, :stationid, :judge, :testtime, :return)";
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);

                command.Parameters.Add(new NpgsqlParameter("boxid", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("serialno", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("model", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("datecd", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("line", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("lot", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("eeee", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("stationid", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("judge", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("testtime", NpgsqlTypes.NpgsqlDbType.TimestampTZ));
                command.Parameters.Add(new NpgsqlParameter("return", NpgsqlTypes.NpgsqlDbType.Varchar));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    command.Parameters[0].Value = dt.Rows[i]["boxid"].ToString();
                    command.Parameters[1].Value = dt.Rows[i]["serialno"].ToString();
                    command.Parameters[2].Value = dt.Rows[i]["model"].ToString();
                    command.Parameters[3].Value = dt.Rows[i]["datecd"].ToString();
                    command.Parameters[4].Value = dt.Rows[i]["line"].ToString();
                    command.Parameters[5].Value = dt.Rows[i]["lot"].ToString();
                    command.Parameters[6].Value = dt.Rows[i]["eeee"].ToString();
                    command.Parameters[7].Value = dt.Rows[i]["stationid"].ToString();                    
                    command.Parameters[8].Value = dt.Rows[i]["judge"].ToString();
                    command.Parameters[9].Value = DateTime.Parse(dt.Rows[i]["testtime"].ToString());
                    command.Parameters[10].Value = dt.Rows[i]["return"].ToString();

                    res1 = command.ExecuteNonQuery();
                    if (res1 == -1) res2 = true;
                }
                
                if (!res2)
                {
                    transaction.Commit();
                    connection.Close();
                    return true;
                }
                else
                {
                    transaction.Rollback();
                    MessageBox.Show("Not successful!", "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    connection.Close();
                    transaction.Rollback();
                    return false;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show("Not successful!" + "\r\n" + ex.Message
                                , "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                connection.Close();
                return false;
            }
        }

        // ボックスＩＤ・シリアルの一括登録
        public int sqlDeleteBoxid(string boxid)
        {
            int res1 = 0;
            int res2 = 0;
            string sql1 = "delete from box_id_rt where boxid = '" + boxid + "'";
            string sql2 = "delete from product_serial_rt where boxid = '" + boxid + "'";

            System.Diagnostics.Debug.Print(sql1);
            System.Diagnostics.Debug.Print(sql2);

            connection = new NpgsqlConnection(conStringBoxidDb);
            connection.Open();
            NpgsqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

            try
            {
                NpgsqlCommand command1 = new NpgsqlCommand(sql1, connection);
                NpgsqlCommand command2 = new NpgsqlCommand(sql2, connection);
                res1 = command1.ExecuteNonQuery();
                res2 = command2.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();
                return res2;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show("Not successful!" + "\r\n" + ex.Message
                                , "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                connection.Close();
                return -1;
            }
        }
    }
}
