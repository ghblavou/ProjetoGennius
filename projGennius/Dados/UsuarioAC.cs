using MySql.Data.MySqlClient;
using projGennius.Dados;
using projGennius.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Services.Description;
using System.Web.Mvc;

namespace projGennius.Dados
{
    public class UsuarioAC
    {
        Conexao con = new Conexao();

        //Cadastrar cliente

        public void inserirCliente(clUsuario cm)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO tbUsuario(tipo, nome, cpf, telefone, login, empresaCli, Senha, sta) " +
        "VALUES(@tipo, @nome, @cpf, @telefone, @login, @empresaCli, @Senha, @sta)", con.MyConectarBD());


            cmd.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = cm.tipo;
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cm.nome;
            cmd.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = cm.cpf;
            cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cm.telefone;       
            cmd.Parameters.Add("@login", MySqlDbType.VarChar).Value = cm.login;
            cmd.Parameters.Add("@empresaCli", MySqlDbType.VarChar).Value = cm.empresaCli;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = cm.senha;
            cmd.Parameters.Add("@sta", MySqlDbType.VarChar).Value = "Ativo";

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }


        public void testarLogin(clUsuario user)
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

        public List<clUsuario> buscarCli()
        {
            List<clUsuario> clList = new List<clUsuario>();
            MySqlCommand cmd = new MySqlCommand("Select * from tbCliente", con.MyConectarBD());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                clList.Add(
                    new clUsuario
                    {
                        codUsu = Convert.ToString(dr["codCli"]),
                        nome = Convert.ToString(dr["nome"]),
                        telefone = Convert.ToString(dr["telefone"]),
                        login = Convert.ToString(dr["login"])
                    }
                    );
            }
            return clList;
        }

        //atualizar cliente
        public void atualizarCliente(clUsuario cm)
        {
            MySqlCommand cmd = new MySqlCommand("update tbCliente set nome=@nome, telefone=@telefone," +
                "login=@login where codCli=@codCli", con.MyConectarBD());

            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cm.nome;
            cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cm.telefone;
            cmd.Parameters.Add("@login", MySqlDbType.VarChar).Value = cm.login;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public void deleteCli(int cod)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbCliente where codCli=@codCli", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@codCli", cod);
            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
        
        public void excluirPubli(int cod)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbPubli where codPubli=@codPubli", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@codPubli", cod);
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
    }
}