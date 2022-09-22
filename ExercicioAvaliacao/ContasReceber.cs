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
    public partial class ContasReceber : Form
    {
        public ContasReceber()
        {
            InitializeComponent();
            mostrar();
            btnAlterar.Visible = false;
            btnDeletar.Visible = false;
        }
        string continua = "yes";
        string date;
        int pago = 0;

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
                        string sql = "insert into contas (nome, valor, dataVencimento, descricao, pago_recebido, tipo) values ('" + txtNome.Text + "', '" + txtValor.Text + "', '" + date + "', '" + txtDescricao.Text + "', '" + pago + "', '" + 0 + "')";
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
                    cnn.ConnectionString = "server=localhost;database=controle;uid=root;pwd=;port=3306;convert zero datetime = true";
                    cnn.Open();
                    string sql = "Select * from contas where tipo = 0";
                    DataTable table = new DataTable();
                    MySqlDataAdapter adpter = new MySqlDataAdapter(sql, cnn);
                    adpter.Fill(table);
                    dgwContasReceber.DataSource = table;

                    dgwContasReceber.AutoGenerateColumns = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void verificarvazio()
        {
            if (txtNome.Text == "" || txtValor.Text == "" || dtpDataVencimento.Text == "" || txtDescricao.Text == "")
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
            txtIdContasReceber.Clear();
            txtNome.Clear();
            txtValor.Clear();
            txtDescricao.Clear();
            btnInserir.Text = "INSERIR";
            btnDeletar.Visible = false;
            btnAlterar.Visible = false;
        }
        void pegaData()
        {
            DateTime data = dtpDataVencimento.Value;
            string dataCurta = data.ToShortDateString();
            string[] vetData = dataCurta.Split('/');
            string dataNova = vetData[2] + "-" + vetData[1] + "-" + vetData[0];
            date = dataNova;
        }

        private void dgwContasPagar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgwContasReceber.CurrentRow.Index != -1)
            {
                txtIdContasReceber.Text = dgwContasReceber.CurrentRow.Cells[0].Value.ToString();
                txtNome.Text = dgwContasReceber.CurrentRow.Cells[1].Value.ToString();
                txtValor.Text = dgwContasReceber.CurrentRow.Cells[2].Value.ToString();
                dtpDataVencimento.Text = dgwContasReceber.CurrentRow.Cells[3].Value.ToString();
                txtDescricao.Text = dgwContasReceber.CurrentRow.Cells[4].Value.ToString();
                btnInserir.Text = "NOVO";
                btnDeletar.Visible = true;
                btnAlterar.Visible = true;
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Deseja realmente atualizar", "Confirmação", MessageBoxButtons.YesNo))
            {
                if (cbRecebido.Checked == false)
                {
                    try
                    {
                        using (MySqlConnection cnn = new MySqlConnection())
                        {
                            cnn.ConnectionString = "server=localhost;database=controle;uid=root;pwd=;port=3306";
                            cnn.Open();
                            string sql = "Update agenda set nome='" + txtNome.Text + "', valor='" + txtValor.Text + "', data='" + dtpDataVencimento.Text + "', descricao='" + txtDescricao.Text + "' where idContatos='" + txtIdContasReceber.Text + "'";
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
                else
                {
                    try
                    {
                        using (MySqlConnection cnn = new MySqlConnection())
                        {
                            cnn.ConnectionString = "server=localhost;database=controle;uid=root;pwd=;port=3306";
                            cnn.Open();
                            string sql = "Update agenda set nome='" + txtNome.Text + "', valor='" + txtValor.Text + "', data='" + dtpDataVencimento.Text + "', descricao='" + txtDescricao.Text + "', dataConclusao= NOW() where idContas ='" + txtIdContasReceber.Text + "'";
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
            }
            mostrar();
        }

        private void cbRecebido_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRecebido.Checked == true)
            {
                pago = 1;
            }
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
                        string sql = "Delete from contas where idContas = '" + txtIdContasReceber.Text + "'";
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

        private void dtpDataVencimento_ValueChanged(object sender, EventArgs e)
        {
            dtpDataVencimento.Format = DateTimePickerFormat.Custom;
            dtpDataVencimento.CustomFormat = "ddddddd, dd MMMMMMMMM yyyy";
        }

        private void dgwContasReceber_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgwContasReceber.CurrentRow.Index != -1)
            {
                txtIdContasReceber.Text = dgwContasReceber.CurrentRow.Cells[0].Value.ToString();
                txtNome.Text = dgwContasReceber.CurrentRow.Cells[1].Value.ToString();
                txtValor.Text = dgwContasReceber.CurrentRow.Cells[3].Value.ToString();
                dtpDataVencimento.Value = Convert.ToDateTime(dgwContasReceber.CurrentRow.Cells[4].Value.ToString());
                txtDescricao.Text = dgwContasReceber.CurrentRow.Cells[2].Value.ToString();
                btnInserir.Text = "NOVO";
                btnDeletar.Visible = true;
                btnAlterar.Visible = true;
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisa pesquisar = new Pesquisa();
            //pesquisar.MdiParent = this;
            pesquisar.Show();
        }
    }
}
