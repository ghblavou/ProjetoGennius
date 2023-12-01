using MySql.Data.MySqlClient;
using projGennius.Dados;
using projGennius.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projGennius.Dados
{
    public class AcUsuario
    {
        Conexao con = new Conexao();

        public void testarUsuario(Usuario user)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbCliente where Email = @Email and senha = @senha", con.MyConectarBD());

            cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = user.Email;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = user.senha;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    user.Email = Convert.ToString(leitor["Email"]);
                    user.senha = Convert.ToString(leitor["senha"]);
                    user.tipo = Convert.ToString(leitor["tipo"]);
                }
            }

            else
            {
                user.Email = null;
                user.senha = null;
                user.tipo = null;
            }

            con.MyDesconectarBD();
        }
    }
}