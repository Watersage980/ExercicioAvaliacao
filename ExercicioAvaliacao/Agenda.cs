using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExercicioAvaliacao
{
    public partial class Agenda : Form
    {
        public Agenda()
        {
            InitializeComponent();
            mostrar();
            btnDeletar.Visible = false;
            btnAlterar.Visible = false;
        }
        string continua = "yes";
        string date;

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dtpData.Format = DateTimePickerFormat.Custom;
            dtpData.CustomFormat = "ddddddd, dd MMM yyyy";
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            verificarvazio();
            if (continua == "yes" && btnInserir.Text == "INSERIR")
            {
                pegaData();
                try
                {
                    using (MySqlConnection cnn = new MySqlConnection())
                    {
                        cnn.ConnectionString = "server=localhost;database=controle;uid=root;pwd=;port=3306";
                        cnn.Open();
                        MessageBox.Show("Inserido com sucesso!");
                        string sql = "insert into agenda (titulo, hora, data, descricao) values ('" + txtTitulo.Text + "', '" + cmbHora.Text.ToString() + "', '" + date + "', '" + rtbDescricao.Text + "')";
                        MySqlCommand cmd = new MySqlCommand(sql, cnn);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                limpar();
            }
            else if (btnInserir.Text == "NOVO")
            {
                limpar();
            }
            mostrar();
        }
        void mostrar()
        {
            try
            {                
                using (MySqlConnection cnn = new MySqlConnection())
                {
                    cnn.ConnectionString = "server=localhost;database=controle;uid=root;pwd=;port=3306";
                    cnn.Open();
                    string sql = "Select * from agenda";
                    DataTable table = new DataTable();
                    MySqlDataAdapter adpter = new MySqlDataAdapter(sql, cnn);
                    adpter.Fill(table);
                    dgwAgenda.DataSource = table;

                    dgwAgenda.AutoGenerateColumns = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void verificarvazio()
        {            
            if (txtPesquisar.Text != "")
            {
                continua = "no";
            }
            else if (txtTitulo.Text == "" || cmbHora.Text == "" || dtpData.Text == "" || rtbDescricao.Text=="")
            {
                continua = "no";
                MessageBox.Show("Insira todos os dados parça");
            }
            else
            {
                continua = "yes";
            }
        }
        void limpar()
        {           
            txtIdAgenda.Clear();
            txtTitulo.Clear();
            txtPesquisar.Clear();
            rtbDescricao.Clear();
            cmbHora.Text = "";
            btnInserir.Text = "INSERIR";
            btnDeletar.Visible = false;
            btnAlterar.Visible = false;
        }

        private void dgwAgenda_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgwAgenda.CurrentRow.Index != -1)
            {
                txtIdAgenda.Text = dgwAgenda.CurrentRow.Cells[0].Value.ToString();
                txtTitulo.Text = dgwAgenda.CurrentRow.Cells[1].Value.ToString();
                cmbHora.Text = dgwAgenda.CurrentRow.Cells[2].Value.ToString();
                dtpData.Text = dgwAgenda.CurrentRow.Cells[3].Value.ToString();
                rtbDescricao.Text = dgwAgenda.CurrentRow.Cells[4].Value.ToString();
                btnInserir.Text = "NOVO";
                btnDeletar.Visible = true;
                btnAlterar.Visible = true;
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Deseja realmente atualizar", "Confirmação", MessageBoxButtons.YesNo))
            {
                try
                {
                    using (MySqlConnection cnn = new MySqlConnection())
                    {
                        cnn.ConnectionString = "server=localhost;database=controle;uid=root;pwd=;port=3306";
                        cnn.Open();
                        string sql = "Update agenda set titulo='" + txtTitulo.Text + "', hora='" + cmbHora.Text + "', data='" + dtpData.Text + "', descricao='" + rtbDescricao.Text + "' where idContatos='" + txtIdAgenda.Text + "'";
                        MySqlCommand cmd = new MySqlCommand(sql, cnn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Atualizado com sucesso!");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            mostrar();
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Deseja realmente excluir", "Confirmação", MessageBoxButtons.YesNo))
            {
                try
                {
                    using (MySqlConnection cnn = new MySqlConnection())
                    {
                        cnn.ConnectionString = "server=localhost;database=controle;uid=root;pwd=;port=3306";
                        cnn.Open();
                        string sql = "Delete from agenda where idAgenda = '" + txtIdAgenda.Text + "'";
                        MySqlCommand cmd = new MySqlCommand(sql, cnn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(" Deletado com sucesso! ");

                    }
                    limpar();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            mostrar();
        }
        void pegaData()
        {
            DateTime data = dtpData.Value;
            string dataCurta = data.ToShortDateString();
            string[] vetData = dataCurta.Split('/');
            string dataNova = vetData[2] + "-" + vetData[1] + "-" + vetData[0];
            date = dataNova;
        }

        private void txtPesquisar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection())
                {
                    cnn.ConnectionString = "server=localhost;database=controle;uid=root;pwd=;port=3306";
                    cnn.Open();
                    string sql;
                    sql = "Select * from Agenda where titulo Like'" + txtPesquisar.Text + "%'";                    
                    MySqlCommand cmd = new MySqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    DataTable table = new DataTable();
                    MySqlDataAdapter adpter = new MySqlDataAdapter(sql, cnn);
                    adpter.Fill(table);
                    dgwAgenda.DataSource = table;

                    dgwAgenda.AutoGenerateColumns = false;


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (txtPesquisar != null)
            {
                btnInserir.Text = "NOVO";
            }
        }
    }   
}