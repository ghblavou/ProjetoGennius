using MySql.Data.MySqlClient;
using projGennius.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace projGennius.Dados
{
    public class produtoAC
    {
        clProduto clProduto = new clProduto();
        Conexao con = new Conexao();

        public void VendaSite(clProduto clProd)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO tbProduto(codProd, codUsu, catProd, descricaoProd, imagemProd) VALUES (@codProd, @codUsu, @catProd, @descricaoProd, @imagemProd)", con.MyConectarBD());

            cmd.Parameters.Add("@codProd", MySqlDbType.VarChar).Value = clProd.codProd;
            cmd.Parameters.Add("@codUsu", MySqlDbType.VarChar).Value = clProd.codUsu;
            cmd.Parameters.Add("@catProd", MySqlDbType.VarChar).Value = clProd.catProd;
            cmd.Parameters.Add("@descricaoProd", MySqlDbType.Text).Value = clProd.descricaoProd;
            cmd.Parameters.Add("@imagemProd", MySqlDbType.VarChar).Value = clProd.imagemProd;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }


        public List<clProduto> selecionarProduto()
        {
            List<clProduto> produto = new List<clProduto>();

            MySqlCommand cmd = new MySqlCommand("select * from tbProduto", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                produto.Add(
                    new clProduto
                    {
                        codUsu = Convert.ToString(dr["codUsu"]),
                        codProd = Convert.ToString(dr["codProd"]),
                        catProd = Convert.ToString(dr["catProd"]),
                        descricaoProd = Convert.ToString(dr["descricaoProd"]),
                        imagemProd = Convert.ToString(dr["imagemProd"]),
                    });
            }
            con.MyDesconectarBD();
            return produto;
        }

        public void Pagamento(clPagamento clPag)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbPag (codpag, NomeCartao, numeroCartao, anoCartao, mesCartao, cvvCartao, codProd, codUsu) " +
                "values (@codpag, @NomeCartao, @numeroCartao, @anoCartao, @mesCartao, @cvvCartao, @codProd, @codProd)", con.MyConectarBD());

            cmd.Parameters.Add("@codpag", MySqlDbType.Int32).Value = clPag.codpag;
            cmd.Parameters.Add("@NomeCartao", MySqlDbType.VarChar).Value = clPag.NomeCartao;
            cmd.Parameters.Add("@numeroCartao", MySqlDbType.VarChar).Value = clPag.numeroCartao;
            cmd.Parameters.Add("@validadeCartao", MySqlDbType.VarChar).Value = clPag.validadeCartao;
            cmd.Parameters.Add("@anoCartao", MySqlDbType.VarChar).Value = clPag.anoCartao;
            cmd.Parameters.Add("@mesCartao", MySqlDbType.VarChar).Value = clPag.mesCartao;
            cmd.Parameters.Add("@cvvCartao", MySqlDbType.VarChar).Value = clPag.cvvCartao;
            cmd.Parameters.Add("@codProd", MySqlDbType.Int32).Value = clPag.codProd;
            cmd.Parameters.Add("@codUsu", MySqlDbType.Int32).Value = clPag.codUsu;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
    }
}