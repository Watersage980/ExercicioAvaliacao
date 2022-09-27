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
    public partial class PesquisaTelefones : Form
    {
        public PesquisaTelefones()
        {
            InitializeComponent();
        }

        private void cmbPesquisarNome_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /*void Mostrar()
        {
            try
            {
                using (MySqlConnection cnx = new MySqlConnection())
                {
                    cnx.ConnectionString = "server = localhost; database = controle; uid = root; pwd =; port = 3306";
                    cnx.Open();
                    string sql = "select idTelefone,nome,cpf,operadora,DDD,numero from contato inner join telefone where contato.idContato = telefone.fkContato and fkContato = '" + txtID.Text + "'";
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
        }*/
    }
}
