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

namespace BoxIdDb
{
    public partial class frmShaftOverlay : Form
    {
        //�e�t�H�[��frmModule�փC�x���g������A���i�f���Q�[�g�j
        //public delegate void RefreshEventHandler(object sender, EventArgs e);
        //public event RefreshEventHandler RefreshEvent;

        //���̑��񃍁[�J���ϐ�
        NpgsqlConnection connection;
        NpgsqlCommand command;
        NpgsqlDataAdapter adapter;
        NpgsqlCommandBuilder cmdbuilder;
        DataSet ds;
        DataTable dt;
        string conStringBoxidDb = @"Server=192.168.193.4;Port=5432;User Id=pqm;Password=dbuser;Database=boxiddb; CommandTimeout=100; Timeout=100;";

        // �R���X�g���N�^
        public frmShaftOverlay()
        {
            InitializeComponent();
        }

        // ���[�h���̏���
        private void frmShaftOverlay_Load(object sender, EventArgs e)
        {
            //�t�H�[���̏ꏊ���w��
            this.Left = 450;
            this.Top = 100;
            defineAndReadTable();
        }

        // �T�u�v���V�[�W���F�e�[�u�����`���A�c�a���f�[�^��ǂݍ���
        private void defineAndReadTable()
        {
            // �c�a���f�[�^��ǂݍ��݁A�c�s�`�`�s�`�a�k�d�֊i�[
            string sql = "select model, shaft, over_lay from mdl_sht_ovl order by model";
            connection = new NpgsqlConnection(conStringBoxidDb);
            connection.Open();
            command = new NpgsqlCommand(sql, connection);
            adapter = new NpgsqlDataAdapter(command);
            cmdbuilder = new NpgsqlCommandBuilder(adapter);
            adapter.InsertCommand = cmdbuilder.GetInsertCommand();
            adapter.UpdateCommand = cmdbuilder.GetUpdateCommand();
            adapter.DeleteCommand = cmdbuilder.GetDeleteCommand();
            ds = new DataSet();
            adapter.Fill(ds,"buff");
            dt = ds.Tables["buff"];
            
            // �f�[�^�O���b�g�r���[�ւc�s�`�`�s�`�a�k�d���i�[
            dgvShaftOverlay.DataSource = dt;
            dgvShaftOverlay.ReadOnly = true;
            btnSave.Enabled = false;
            dgvShaftOverlay.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvShaftOverlay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        // �V�K���R�[�h�̒ǉ�
        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgvShaftOverlay.ReadOnly = false;
            dgvShaftOverlay.AllowUserToAddRows = true;
            btnSave.Enabled = true;
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
        }

        // �������R�[�h�̍폜
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Do you want to delete this row ?", "Delete", MessageBoxButtons.YesNo);
            if (dlg == DialogResult.No) return;

            try
            {
                dgvShaftOverlay.Rows.RemoveAt(dgvShaftOverlay.SelectedRows[0].Index);
                adapter.Update(dt);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // �ۑ�
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                adapter.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally 
            {
                dgvShaftOverlay.ReadOnly = true;
                dgvShaftOverlay.AllowUserToAddRows = false;
                btnSave.Enabled = false;
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
            }
        }
    }
}