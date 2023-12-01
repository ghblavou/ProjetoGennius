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
    public class acPrestador 
    {
        Conexao con = new Conexao();

            //Cadastrar cliente

            public void inserirPres(Prestador cm)
            {
                MySqlCommand cmd = new MySqlCommand("insert into tbCliente(tipo, nome, cpf, telefone, Email, Senha, gitPres, Login) " +
                    "values(@tipo, @nome, @cpf, @telefone, @Email, @Senha, @gitPres, @Login)", con.MyConectarBD());

                cmd.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = cm.tipo;
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cm.nome;
                cmd.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = cm.cpf;
                cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cm.telefone;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = cm.Email;
                cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = cm.senha;
                cmd.Parameters.Add("@gitPres", MySqlDbType.VarChar).Value = cm.gitPres;

                cmd.ExecuteNonQuery();
                con.MyDesconectarBD();
            }

    }
}
