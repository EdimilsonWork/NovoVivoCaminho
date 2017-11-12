using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovoVivoCaminho.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace NovoVivoCaminho.DAL
{
    public class UsuarioDAL
    {
        private string conexao = ConfigurationManager.AppSettings["Conection"];
        public void Incluir(UsuarioInfo usuario)
        {
            try
            {
                string sql = "INSERT INTO Usuarios(Usuario, Senha) VALUES (@Usuario, @Senha)";

                if (!Existe(usuario))
                {
                    SqlConnection cn = new SqlConnection(conexao);
                    SqlCommand cmd = new SqlCommand(sql);

                    cmd.Parameters.AddWithValue("@Usuario", usuario.Login);
                    cmd.Parameters.AddWithValue("Senha", usuario.Senha);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
                else
                {
                    MessageBox.Show("O usuario informado já existe!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir um novo USUÁRIO, verifique suas conexões.");
            }

        }

        public bool Existe(UsuarioInfo usuario)
        {
            string sql = "SELECT TOP 1 ID FROM Usuarios WHERE Usuario = @Usuario";

            SqlConnection cn = new SqlConnection(conexao);
            SqlCommand cmd = new SqlCommand(sql);

            cmd.Parameters.AddWithValue("@Usuario", usuario.Login);

            cn.Open();
            cmd.ExecuteReader();
            cn.Close();

            return true;
        }

        public bool Acessar(UsuarioInfo usuario)
        {
            try
            {
                string sql = "SELECT TOP 1 ID FROM Usuarios WHERE Login = @Login AND Senha = @Senha";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Connection = new SqlConnection(conexao);

                cmd.Parameters.AddWithValue("@Login", usuario.Login);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);

                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.HasRows)
                    return true;
                else
                    return false;

                cmd.Connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("");
            }
        }
    }
}
