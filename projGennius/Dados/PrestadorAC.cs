using MySql.Data.MySqlClient;
using projGennius.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using projGennius.Dados;


namespace projGennius.Dados
{
    public class PrestadorAC
    {
        Conexao con = new Conexao();

        //Cadastrar cliente

        public void inserirPres(clPrestador cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbUsuario(tipo, nome, cpf, telefone, login, Senha, gitPres, sta) " +
                "values(@tipo, @nome, @cpf, @telefone, @login, @Senha, @gitPres, @sta)", con.MyConectarBD());

            cmd.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = cm.tipo;
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cm.nome;
            cmd.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = cm.cpf;
            cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cm.telefone;
            cmd.Parameters.Add("@login", MySqlDbType.VarChar).Value = cm.login;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = cm.senha;
            cmd.Parameters.Add("@gitPres", MySqlDbType.VarChar).Value = cm.gitPres;
            cmd.Parameters.Add("@sta", MySqlDbType.VarChar).Value = "Ativo";

            cmd.ExecuteNonQuery();

            con.MyDesconectarBD();





        }


        public void testarLogin(clPrestador user)
        {
            MySqlCommand cmd = new MySqlCommand("select login from tbUsuario where login = @login", con.MyConectarBD());

            cmd.Parameters.Add("@login", MySqlDbType.VarChar).Value = user.login;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                user.login = null;
            }

            con.MyDesconectarBD();

        }

        public List<clPublicacao> PublisPres(int id) //Publicações aceitas do prestador
        {
            List<clPublicacao> publis = new List<clPublicacao>();

            MySqlCommand cmd = new MySqlCommand("call selecionar_post_pres(@codPres)", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@codPres", id);

            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                publis.Add(
                    new clPublicacao
                    {
                        catPubli = Convert.ToString(dr["catPubli"]),
                        descricao = Convert.ToString(dr["descricao"]),
                        empresaCLi = Convert.ToString(dr["empresaCLi"]),
                        situacaoPubli = Convert.ToString(dr["situacaoPubli"]),
                        codPubli = Convert.ToString(dr["codPubli"]),
                        codUsu = Convert.ToString(dr["codUsu"]),
                        codPres = Convert.ToString(dr["codPres"])
                    });
            }

            return publis;
        }

    }
}
