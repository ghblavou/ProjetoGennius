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

namespace projGennius.Dados
{
    public class acCliente
    {
        Conexao con = new Conexao();

        //Cadastrar cliente

        public void inserirCliente(Cliente cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbCliente(tipo, nome, cpf, telefone, Email, empresaCli, Senha) " +
                "values(@tipo, @nome, @cpf, @telefone, @Email, @empresaCli, @Senha)", con.MyConectarBD());

            cmd.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = cm.tipo;
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cm.nome;
            cmd.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = cm.cpf;
            cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cm.telefone;       
            cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = cm.Email;
            cmd.Parameters.Add("@empresaCli", MySqlDbType.VarChar).Value = cm.empresaCli;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = cm.senha;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public List<Cliente> buscarCli()
        {
            List<Cliente> clList = new List<Cliente>();
            MySqlCommand cmd = new MySqlCommand("Select * from tbCliente", con.MyConectarBD());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                clList.Add(
                    new Cliente
                    {
                        codCli = Convert.ToInt32(dr["codCli"]),
                        nome = Convert.ToString(dr["nome"]),
                        telefone = Convert.ToString(dr["telefone"]),
                        Email = Convert.ToString(dr["email"])
                    }
                    );
            }
            return clList;
        }

        //atualizar cliente
        public void atualizarCliente(Cliente cm)
        {
            MySqlCommand cmd = new MySqlCommand("update tbCliente set nome=@nome, telefone=@telefone," +
                "email=@email where codCli=@codCli", con.MyConectarBD());

            cmd.Parameters.Add("@codCli", MySqlDbType.VarChar).Value = cm.codCli;
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cm.nome;
            cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cm.telefone;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = cm.Email;

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
    }
}