using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using projGennius.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projGennius.Dados
{
    public class AdmAC
    {
        Conexao con = new Conexao();
        public List<clUsuario> Users()
        {
            List<clUsuario> user = new List<clUsuario>();

            MySqlCommand cmd = new MySqlCommand("CALL selecionar_cliente()", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                user.Add(
                    new clUsuario
                    {
                        codUsu = Convert.ToString(dr["codUsu"]),
                        nome = Convert.ToString(dr["nome"]),
                        tipo = Convert.ToString(dr["tipo"]),
                        login = Convert.ToString(dr["login"]),
                        telefone = Convert.ToString(dr["telefone"]),
                        sta = Convert.ToString(dr["sta"]),
                        empresaCli = Convert.ToString(dr["empresaCli"])
                    });
            }

            con.MyDesconectarBD();
            return user;
        }



        public List<clPrestador> DevCad()
        {
            List<clPrestador> Dev = new List<clPrestador>();

            MySqlCommand cmd = new MySqlCommand("call selecionar_DevCad();", con.MyConectarBD());
            con.MyConectarBD();
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Dev.Add(
                    new clPrestador
                    {
                        codUsu = Convert.ToString(dr["codUsu"]),
                        nome = Convert.ToString(dr["nome"]),
                        sta = Convert.ToString(dr["sta"]),
                        login = Convert.ToString(dr["login"]),
                        telefone = Convert.ToString(dr["telefone"]),
                        gitPres = Convert.ToString(dr["gitPres"])
                    });
            }
            con.MyDesconectarBD();
            return Dev;
        }

        public List<ModelLogin> AdminCad()
        {
            List<ModelLogin> Dev = new List<ModelLogin>();

            MySqlCommand cmd = new MySqlCommand("call selecionar_AdminCad();", con.MyConectarBD());
            con.MyConectarBD();
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Dev.Add(
                    new clPrestador
                    {
                        codUsu = Convert.ToString(dr["codUsu"]),
                        nome = Convert.ToString(dr["nome"]),
                        sta = Convert.ToString(dr["sta"]),
                        login = Convert.ToString(dr["login"]),
                        telefone = Convert.ToString(dr["telefone"]),
                        gitPres = Convert.ToString(dr["gitPres"])
                    });
            }
            con.MyDesconectarBD();
            return Dev;
        }





        public List<clUsuario> SelectUserComum(string id)
        {
            List<clUsuario> user = new List<clUsuario>();

            MySqlCommand cmd = new MySqlCommand("select * from tbUsuario where codUsu = @codUsu", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@codUsu", id);

            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                user.Add(
                    new clUsuario
                    {
                        codUsu = Convert.ToString(dr["codUsu"]),
                        nome = Convert.ToString(dr["nome"]),
                        cpf = Convert.ToString(dr["cpf"]),
                        senha = Convert.ToString(dr["senha"]),
                        sta = Convert.ToString(dr["sta"]),
                        tipo = Convert.ToString(dr["tipo"]),
                        empresaCli = Convert.ToString(dr["empresaCli"]),
                        login = Convert.ToString(dr["login"]),
                        telefone = Convert.ToString(dr["telefone"])
                    });
            }

            con.MyDesconectarBD();
            return user;
        }

        public List<clPrestador> SelectUserPres(string id)
        {
            List<clPrestador> user = new List<clPrestador>();

            MySqlCommand cmd = new MySqlCommand("select * from tbUsuario where codUsu = @codUsu", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@codUsu", id);


            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                user.Add(
                    new clPrestador
                    {
                        codUsu = Convert.ToString(dr["codUsu"]),
                        nome = Convert.ToString(dr["nome"]),
                        cpf = Convert.ToString(dr["cpf"]),
                        senha = Convert.ToString(dr["senha"]),
                        tipo = Convert.ToString(dr["tipo"]),
                        sta = Convert.ToString(dr["sta"]),
                        gitPres = Convert.ToString(dr["gitPres"]),
                        login = Convert.ToString(dr["login"]),
                        telefone = Convert.ToString(dr["telefone"])
                    });
            }

            con.MyDesconectarBD();
            return user;
        }

        public List<ModelLogin> SelectUserAdmin(string id)
        {

            List<ModelLogin> user = new List<ModelLogin>();

            MySqlCommand cmd = new MySqlCommand("select * from tbUsuario where codUsu = @codUsu", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@codUsu", id);

            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                user.Add(
                    new ModelLogin
                    {
                        codUsu = Convert.ToString(dr["codUsu"]),
                        nome = Convert.ToString(dr["nome"]),
                        sta = Convert.ToString(dr["sta"]),
                        senha = Convert.ToString(dr["senha"]),
                        tipo = Convert.ToString(dr["tipo"]),
                        login = Convert.ToString(dr["login"])
                    });
            }

            con.MyDesconectarBD();
            return user;
        }



        public void desativar(int codUsu)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE tbUsuario SET sta = 2 WHERE codUsu = @codUsu;", con.MyConectarBD());

            cmd.Parameters.Add("@codUsu", MySqlDbType.Int32).Value = codUsu;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public void ativar(int codUsu)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE tbUsuario SET sta = 1 WHERE codUsu = @codUsu;", con.MyConectarBD());

            cmd.Parameters.Add("@codUsu", MySqlDbType.Int32).Value = codUsu;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
    }
}

