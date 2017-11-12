using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NovoVivoCaminho.Models;
using NovoVivoCaminho.BLL;

namespace NovoVivoCaminho
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void acessarButton_Click(object sender, EventArgs e)
        {
            UsuarioInfo usuario = new UsuarioInfo();

            usuario.Login = usuarioTextBox.Text;
            usuario.Senha = senhaTextBox.Text;

            UsuarioBLL bll = new UsuarioBLL();
            if(bll.Acessar(usuario))
            {
                this.Hide();
                ControleForm controle = new ControleForm();
                controle.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario e Senha INVALIDO...");
            }
        }

        private void loginForm_Load(object sender, EventArgs e)
        {
            usuarioTextBox.Text = "edimilson";
            senhaTextBox.Text = "edimilson";
        }
    }
}
