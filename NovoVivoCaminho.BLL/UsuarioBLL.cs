using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovoVivoCaminho.Models;
using NovoVivoCaminho.DAL;
using System.Windows.Forms;

namespace NovoVivoCaminho.BLL
{
    public class UsuarioBLL
    {
        public void Incluir(UsuarioInfo usuario)
        {
            if (string.IsNullOrEmpty(usuario.Login))
                MessageBox.Show("O nome do usuario deve ser informado");
            if (string.IsNullOrEmpty(usuario.Senha))
                MessageBox.Show("A senha do usuario deve ser informado");

            UsuarioDAL dal = new UsuarioDAL();
            dal.Incluir(usuario);
        }

        public bool Acessar(UsuarioInfo usuario)
        {
            if (string.IsNullOrEmpty(usuario.Login))
            {
                MessageBox.Show("O nome do usuario deve ser informado");
                return false;
            }
            if (string.IsNullOrEmpty(usuario.Senha))
            {
                MessageBox.Show("A senha do usuario deve ser informado");
                return false;
            }

            UsuarioDAL dal = new UsuarioDAL();

            return dal.Acessar(usuario);
        }
    }
}
