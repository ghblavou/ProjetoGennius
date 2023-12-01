using MySql.Data.MySqlClient;
using projGennius.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projGennius.Dados
{
    public class PublicacaoAC
    {
        DateTime data = DateTime.Now;
        Conexao con = new Conexao();
        public void registrarPubliPendente(clPublicacao cm)
        {

            MySqlCommand cmd = new MySqlCommand("INSERT INTO tbPubli(catPubli, descricao, codUsu, situacaoPubli, dataPubli) VALUES (@catPubli, @descricao, @codUsu, @situacaoPubli, @dataPubli)", con.MyConectarBD());

            cmd.Parameters.Add("@catPubli", MySqlDbType.VarChar).Value = cm.catPubli;
            cmd.Parameters.Add("@descricao", MySqlDbType.Text).Value = cm.descricao;
            cmd.Parameters.Add("@codUsu", MySqlDbType.VarChar).Value = cm.codUsu;
            cmd.Parameters.Add("@situacaoPubli", MySqlDbType.VarChar).Value = "Pendente";
            cmd.Parameters.Add("@dataPubli", MySqlDbType.VarChar).Value = data;


            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

        }

        public List<clPublicacao> selecionarPubliAprovada()
        {
            List<clPublicacao> publis = new List<clPublicacao>();

            MySqlCommand cmd = new MySqlCommand("call selecionar_post_aprovado( );", con.MyConectarBD());
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
                        codPubli = Convert.ToString(dr["codPubli"]),
                        empresaCLi = Convert.ToString(dr["empresaCLi"]),
                        descricao = Convert.ToString(dr["descricao"]),
                        situacaoPubli = Convert.ToString(dr["situacaoPubli"])
                    });
            }
            return publis;
            
        }

        public List<clPublicacao> selecionarPubliPendente()
        {
            List<clPublicacao> publis = new List<clPublicacao>();

            MySqlCommand cmd = new MySqlCommand("call selecionar_post_pendente( );", con.MyConectarBD());
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
                        codPubli = Convert.ToString(dr["codPubli"]),
                        empresaCLi = Convert.ToString(dr["empresaCLi"]),
                        descricao = Convert.ToString(dr["descricao"]),
                        situacaoPubli = Convert.ToString(dr["situacaoPubli"])
                    });
            }
            return publis;
        }

        public List<clPublicacao> selecionarPubli(string id)
        {
            List<clPublicacao> publis = new List<clPublicacao>();

            MySqlCommand cmd = new MySqlCommand("select * from tbPubli where codPubli = @codPubli;", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@codPubli", id);

            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                publis.Add(
                    new clPublicacao
                    {
                        codPubli = Convert.ToString(dr["codPubli"]),
                        catPubli = Convert.ToString(dr["catPubli"]),
                        descricao = Convert.ToString(dr["descricao"]),
                        situacaoPubli = Convert.ToString(dr["situacaoPubli"]),
                        codUsu = Convert.ToString(dr["codUsu"]),
                        codPres = Convert.ToString(dr["codPres"])
                    });
            }
            return publis;
        }


        public List<clPublicacao> PublisUsuario(int idUsuario)
        {
            List<clPublicacao> publis = new List<clPublicacao>();

            MySqlCommand cmd = new MySqlCommand("call selecionar_post_usu(@codUsu)", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@codUsu", idUsuario);

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
                        codUsu = Convert.ToString(dr["codUsu"])
                    });
            }

            return publis;
        }

        public void editarPubli(string id, string descricao, string catPubli)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE tbPubli SET descricao = @descricao, catPubli = @catPubli WHERE codPubli = @codPubli;", con.MyConectarBD());
            cmd.Parameters.Add("@codPubli", MySqlDbType.Int32).Value = Convert.ToInt32(id);
            cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = Convert.ToString(descricao);
            cmd.Parameters.Add("@catPubli", MySqlDbType.VarChar).Value = Convert.ToString(catPubli);

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }


        public void editarPubliCliente(string id, string descricao, string catPubli)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE tbPubli SET situacaoPubli = 'Pendente', descricao = @descricao, catPubli = @catPubli WHERE codPubli = @codPubli;", con.MyConectarBD());
            cmd.Parameters.Add("@codPubli", MySqlDbType.Int32).Value = Convert.ToInt32(id);
            cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = Convert.ToString(descricao);
            cmd.Parameters.Add("@catPubli", MySqlDbType.VarChar).Value = Convert.ToString(catPubli);

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public void ExcluirPubli(int id)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM tbPubli WHERE codPubli = @codPubli;", con.MyConectarBD());
            cmd.Parameters.Add("@codPubli", MySqlDbType.Int32).Value = Convert.ToInt32(id);
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

   
    }
}
