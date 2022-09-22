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
    public partial class ControleContas : Form
    {
        public ControleContas()
        {
            InitializeComponent();
            mostrar1();
            mostrar2();
        }

        private void btnContasPagar_Click(object sender, EventArgs e)
        {
            ContasPagar contasPagar =   new ContasPagar();  
            //contasPagar.MdiParent = this;
            contasPagar.Show(); 
        }

        private void btnContasReceber_Click(object sender, EventArgs e)
        {
            ContasReceber contasreceber = new ContasReceber();
            //contasreceber.MdiParent = this;
            contasreceber.Show();   
        }
        void mostrar1()
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection())
                {
                    cnn.ConnectionString = "server=localhost;database=controle;uid=root;pwd=;port=3306;convert zero datetime = true";
                    cnn.Open();
                    string sql = "Select * from contas where tipo = 1";
                    DataTable table = new DataTable();
                    MySqlDataAdapter adpter = new MySqlDataAdapter(sql, cnn);
                    adpter.Fill(table);
                    dgwContasPagar.DataSource = table;

                    dgwContasPagar.AutoGenerateColumns = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void mostrar2()
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
        }
}
