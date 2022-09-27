﻿using MySql.Data.MySqlClient;
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
    public partial class Contatos : Form
    {
        public Contatos()
        {
            InitializeComponent();
            Mostrar();
            MostrarTelefone();
            btnAlterar.Visible = false;
            btnDeletar.Visible = false;
        }
        string continua = "yes";
        string DataNova;

        private void btnInserir_Click(object sender, EventArgs e)
        {
            Data();
            verificaVazio();

            if (btnInserir.Text == "INSERIR" && continua == "yes")
            {
                if (MessageBox.Show("Deseja realmente inserir?", "INSERIR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        try
                        {
                            addEndereco();
                            using (MySqlConnection cnx = new MySqlConnection())
                            {
                                cnx.ConnectionString = "server = localhost; database = controle; uid = root; pwd =; port = 3306;Convert Zero datetime = true";
                                cnx.Open();
                                string sql;
                                if (rbMasculino.Checked)
                                {
                                    sql = "insert into contato (nome,cpf,dataNascimento,email,sexo,numeroCasa,complemento,fkEndereco) values ('" + txtNome.Text + "','" + txtCPF.Text + "','" + DataNova + "','" + txtEmail.Text + "','" + rbMasculino.Text + "','" + txtNumero.Text + "','" + txtComplemento.Text + "',( select idEndereco from endereco where  CEP = " + txtCEP.Text + " limit 1))";

                                }
                                else
                                {
                                    sql = "insert into contato (nome,cpf,dataNascimento,email,sexo,numeroCasa,complemento,fkEndereco) values ('" + txtNome.Text + "','" + txtCPF.Text + "','" + DataNova + "','" + txtEmail.Text + "','" + rbFeminino.Text + "','" + txtNumero.Text + "','" + txtComplemento.Text + "',( select idEndereco from endereco where  CEP =  " + txtCEP.Text + " limit 1))";

                                }
                                MessageBox.Show("Dados inseridos com sucesso!!!");
                                MySqlCommand cmd = new MySqlCommand(sql, cnx);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            Mostrar();
            Limpar();
        }

        private void btnCadastrarTelefone_Click(object sender, EventArgs e)
        {
            Telefones telefone = new Telefones();
            telefone.Show();
        }
        void Data()
        {
            DateTime data = dtpDataNascimento.Value;
            string dataCurta = data.ToShortDateString();
            string[] vetData = dataCurta.Split('/');
            DataNova = $"{vetData[2]}-{vetData[1]}-{vetData[0]}";
        }
        void addEndereco()
        {
            try
            {
                using (MySqlConnection cnx = new MySqlConnection())
                {
                    cnx.ConnectionString = "server = localhost; database = controle; uid = root; pwd =; port = 3306; Convert Zero Datetime = true";
                    cnx.Open();
                    string sql = "insert into endereco (logradouro,cidade,bairro,UF,cep) values ('" + txtLogradouro.Text + "','" + txtCidade.Text + "','" + txtBairro.Text + "','" + cmbEstado.Text + "','" + txtCEP.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, cnx);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        void Mostrar()
        {
            try
            {
                using (MySqlConnection cnx = new MySqlConnection())
                {
                    cnx.ConnectionString = "server = localhost; database = controle; uid = root; pwd =; port = 3306;Convert Zero DateTime = true";
                    cnx.Open();
                    string sql = "select idContato,nome,cpf,dataNascimento,email,sexo,cep,logradouro,numeroCasa,complemento,bairro,cidade,uf from endereco inner join contato on endereco.idEndereco = contato.fkEndereco";
                    DataTable table = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(sql, cnx);
                    adapter.Fill(table);
                    dgwContatos.DataSource = table;
                    dgwContatos.AutoGenerateColumns = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void MostrarTelefone()
        {
            try
            {
                using (MySqlConnection cnx = new MySqlConnection())
                {
                    cnx.ConnectionString = "server = localhost; database = controle; uid = root; pwd =; port = 3306;Convert Zero DateTime = true";
                    cnx.Open();
                    string sql = "select DDD,numero,operadora from telefone inner join contato where contato.idContato = telefone.fkContato and idContato = '" + txtIdContato.Text + "'";
                    DataTable table = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(sql, cnx);
                    adapter.Fill(table);
                    dgwTelefones.DataSource = table;
                    dgwTelefones.AutoGenerateColumns = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void Limpar()
        {
            txtNome.Clear();
            txtEmail.Clear();
            txtCPF.Clear();
            txtCEP.Clear();
            txtLogradouro.Clear();
            txtCidade.Clear();
            txtBairro.Clear();
            cmbEstado.Text = null;
            rbMasculino.Checked = false;
            rbFeminino.Checked = false;
            txtNumero.Clear();
            txtIdContato.Clear();
            txtComplemento.Clear();

            btnInserir.Text = "INSERIR";
            btnDeletar.Visible = false;
            btnAlterar.Visible = false;
        }

        void verificaVazio()
        {
            if (txtNome.Text == "" || txtEmail.Text == "" || txtCPF.Text == "" || txtCEP.Text == "" || txtLogradouro.Text == "" || txtCidade.Text == "" || cmbEstado.Text == "" || txtBairro.Text == "" || txtNumero.Text == "")
            {
                continua = "no";
                MessageBox.Show("Preencha todos os campos");
            }
            else
            {
                continua = "yes";
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            Data();
            if (MessageBox.Show("Deseja realmente Alterar?", "ALTERAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection cnx = new MySqlConnection())
                    {
                        cnx.ConnectionString = "server = localhost; database = controle; uid = root; pwd =; port = 3306;Convert Zero DateTime = true";
                        cnx.Open();
                        string sql = "insert into endereco (logradouro,cidade,bairro,UF,cep) values ('" + txtLogradouro.Text + "','" + txtCidade.Text + "','" + txtBairro.Text + "','" + cmbEstado.Text + "','" + txtCEP.Text + "')";
                        txtLogradouro.Enabled = false;
                        txtBairro.Enabled = false;
                        txtCidade.Enabled = false;
                        cmbEstado.Enabled = false;
                        string sql3 = "update contato set nome = '" + txtNome.Text + "',email = '" + txtEmail.Text + "', CPF = '" + txtCPF.Text + "', dataNascimento = '" + DataNova + "', numeroCasa = '" + txtNumero.Text + "', complemento = '" + txtComplemento.Text + "', fkEndereco = ( select idEndereco from endereco where  CEP = '" + txtCEP.Text + "' limit 1) where idContato = '" + txtIdContato.Text + "'";
                        MySqlCommand cmd = new MySqlCommand(sql, cnx);
                        cmd.ExecuteNonQuery();
                        MySqlCommand cmd3 = new MySqlCommand(sql3, cnx);
                        cmd3.ExecuteNonQuery();
                        MessageBox.Show("Atualizado com sucesso");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            Mostrar();
        }

        private void dgwContatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgwContatos.CurrentRow.Index != -1)
            {
                txtIdContato.Text = dgwContatos.CurrentRow.Cells[0].Value.ToString();
                txtNome.Text = dgwContatos.CurrentRow.Cells[1].Value.ToString();
                txtEmail.Text = dgwContatos.CurrentRow.Cells[4].Value.ToString();
                txtCPF.Text = dgwContatos.CurrentRow.Cells[2].Value.ToString();
                dtpDataNascimento.Value = Convert.ToDateTime(dgwContatos.CurrentRow.Cells[3].Value.ToString());
                txtCEP.Text = dgwContatos.CurrentRow.Cells[6].Value.ToString();
                txtLogradouro.Text = dgwContatos.CurrentRow.Cells[7].Value.ToString();
                txtNumero.Text = dgwContatos.CurrentRow.Cells[8].Value.ToString();
                txtComplemento.Text = dgwContatos.CurrentRow.Cells[9].Value.ToString();
                txtBairro.Text = dgwContatos.CurrentRow.Cells[10].Value.ToString();
                txtCidade.Text = dgwContatos.CurrentRow.Cells[11].Value.ToString();
                cmbEstado.Text = dgwContatos.CurrentRow.Cells[12].Value.ToString();
                Fk_s.Fk = int.Parse(dgwContatos.CurrentRow.Cells[0].Value.ToString());
                btnInserir.Text = "NOVO";
                btnDeletar.Visible = true;
                btnAlterar.Visible = true;
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente deletar?", "Deletar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection cnn = new MySqlConnection())
                    {
                        cnn.ConnectionString = "server = localhost; database = controle; uid = root; pwd =; port = 3306;Convert Zero DateTime = true";
                        cnn.Open();
                        string sql = "delete from contato where idContato = '" + txtIdContato.Text + "'"; 
                        MySqlCommand cmd = new MySqlCommand(sql, cnn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Deletado com sucesso!");

                    }
                    Limpar();
                    Mostrar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtPesquisar_TextChanged(object sender, EventArgs e)
        {
            try
            {

                using (MySqlConnection cnx = new MySqlConnection())
                {
                    cnx.ConnectionString = "server = localhost; database = controle; uid = root; pwd =; port = 3306; Convert Zero DateTime = true";
                    cnx.Open();
                    string sql = "select idContato,nome,cpf,dataNascimento,email,sexo,cep,logradouro,numeroCasa,complemento,bairro,cidade,uf from endereco inner join contato on endereco.idEndereco = contato.fkEndereco where nome like '" + txtPesquisar.Text + "%'";
                    DataTable table = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(sql, cnx);
                    adapter.Fill(table);
                    dgwContatos.DataSource = table;
                    dgwContatos.AutoGenerateColumns = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
