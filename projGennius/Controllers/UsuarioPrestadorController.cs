using MySql.Data.MySqlClient;
using projGennius.Dados;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projGennius.Controllers;
using projGennius.Dados;
using projGennius.Models;

namespace projGennius.Controllers
{

    public class UsuarioPrestadorController : Controller
    {
        Conexao con = new Conexao();
        PublicacaoAC clP = new PublicacaoAC();
        PrestadorAC acPres = new PrestadorAC();
        AdmAC admAC = new AdmAC();


        // GET: UsuarioPrestador
        public ActionResult Index()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado1"] != null)
            {
                return RedirectToAction("semAcesso", "Home");

            }
            else
            {
                ViewBag.Nome = Session["usuarioLogado"];
                return View(clP.selecionarPubliAprovada());
            }
        }

        public ActionResult PublisAceitas()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado1"] != null)
            {
                return RedirectToAction("semAcesso", "Home");

            }
            else
            {
                int idUsuarioAtual = int.Parse(Session["codUsuario"].ToString());
                return View(acPres.PublisPres(idUsuarioAtual));

            }
        }

        public ActionResult AceitarPubli(int id)
        {
            MySqlCommand cmd = new MySqlCommand("update tbPubli set situacaoPubli = 'Aceito', codPres = @codPres where codPubli = @codPubli;", con.MyConectarBD());


            cmd.Parameters.Add("@codPubli", MySqlDbType.Int64).Value = id;
            cmd.Parameters.Add("@codPres", MySqlDbType.Int64).Value = Session["codUsuario"];

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            ViewBag.Message = "Serviço aceito com sucesso!";
            return RedirectToAction("PublisAceitas", "UsuarioPrestador");
        }

        public ActionResult RecusarPubli(int id)
        {
            MySqlCommand cmd = new MySqlCommand("update tbPubli set situacaoPubli = 'Aprovado', codPres = null where codPubli = @codPubli;", con.MyConectarBD());


            cmd.Parameters.Add("@codPubli", MySqlDbType.Int64).Value = id;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
            return RedirectToAction("PublisAceitas", "UsuarioPrestador");
        }




        public ActionResult PerfilUsuAceito(string codPubli)
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado1"] != null)
            {
                return RedirectToAction("semAcesso", "Home");
            }
            else
            {

                MySqlCommand cmd = new MySqlCommand("select * from tbPubli where codPubli = @codPubli;", con.MyConectarBD());
                cmd.Parameters.AddWithValue("@codPubli", codPubli);

                MySqlDataReader leitor;

                leitor = cmd.ExecuteReader();


                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        ViewBag.codUsu = Convert.ToString(leitor["codUsu"]);
                        ViewBag.codPres = Convert.ToString(leitor["codPres"]);
                    }
                }
                con.MyDesconectarBD();
                ViewBag.Link = "https://wa.me/55";
                return View(admAC.SelectUserComum(ViewBag.codUsu));

            }

        }

        public ActionResult MeuPerfil()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado1"] != null)
            {
                return RedirectToAction("semAcesso", "Home");
            }
            else
            {
                ViewBag.Link = "https://wa.me/55";
                ViewBag.cod = Session["codUsuario"].ToString();
                return View(admAC.SelectUserPres(ViewBag.cod));
            }
        }



        public ActionResult NegociarProjeto()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado1"] != null)
            {
                return RedirectToAction("semAcesso", "Home");

            }
            else
            {
                ViewBag.Nome = Session["usuarioLogado"];
                return View();
            }
        }

        [HttpPost]
        public ActionResult NegociarProjeto(clPublicacao publicacao, string id, string descricao, string catPubli)
        {
            if (ModelState.IsValid)
            {
                clP.editarPubli(id, descricao, catPubli);
                ModelState.Clear();

                publicacao = new clPublicacao();

                ViewBag.Message = "Publicação alterada com sucesso!";

                return View(publicacao);

            }
            return View(publicacao);
        }

    }
}