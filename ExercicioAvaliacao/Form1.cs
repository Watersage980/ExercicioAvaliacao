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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text == "Admin" && txtSenha.Text == "admin")
            {
                Principal abrir = new Principal();
                abrir.Show();
            }
            else
            {
                MessageBox.Show("Insira os dados corretos");
            }                       
        }
    }
}
