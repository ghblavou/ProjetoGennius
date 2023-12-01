using MySql.Data.MySqlClient;
using projGennius.Dados;
using projGennius.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projGennius.Dados
{
    public class acoesLogin
    {
        Conexao con = new Conexao();

        public void testarUsuario(ModelLogin user)
        {
            MySqlCommand cmd = new MySqlCommand("select *, SUBSTRING_INDEX(nome, ' ', 1) as 'priNome' from tbUsuario where login = @login and senha = @senha;", con.MyConectarBD());

            cmd.Parameters.Add("@login", MySqlDbType.VarChar).Value = user.login;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = user.senha;



            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    user.codUsu = Convert.ToString(leitor["codUsu"]);
                    user.nome = Convert.ToString(leitor["priNome"]);
                    user.sta = Convert.ToString(leitor["sta"]);
                    user.login = Convert.ToString(leitor["login"]);
                    user.senha = Convert.ToString(leitor["senha"]);
                    user.tipo = Convert.ToString(leitor["tipo"]);
                }
            }

            else
            {
                user.login = null;
                user.senha = null;
                user.tipo = null;
                user.codUsu = null;
            }

            con.MyDesconectarBD();
        }

        public void pegarNome(ModelLogin user)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT  as 'Nome' FROM tbUsuario;", con.MyConectarBD());


        }


    }
}