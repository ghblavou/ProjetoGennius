using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using projGennius.Dados;
using projGennius.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Numerics;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace projGennius.Controllers
{
    public class UsuarioComumController : Controller
    {
        // GET:     
        acoesLogin acLg = new acoesLogin();
        PublicacaoAC pbAC = new PublicacaoAC();
        Conexao con = new Conexao();
        AdmAC admAC = new AdmAC();
        produtoAC produtoAC = new produtoAC();

        public ActionResult Index()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado2"] != null)
            {
                return RedirectToAction("semAcesso", "Home");

            }
            else
            {
                ViewBag.Nome = Session["usuarioLogado"];
                return View(pbAC.selecionarPubliAprovada());
            }
        }

        // GET: Publicacao
        public ActionResult CriarPubli()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado2"] != null)
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
        public ActionResult CriarPubli(clPublicacao clP, string Categoria)// 1 = Ecommerce ; 2 = Sistema
        {
            if (ModelState.IsValid)
            {
                clP.codUsu = Session["codUsuario"].ToString();
                pbAC.registrarPubliPendente(clP);
                ModelState.Clear();

                clP = new clPublicacao();


                ViewBag.Message = "Publicação encaminhada para analise. Aguarde a publicação ser aprovada!";

                return View(clP);
            }
            return View(clP);
        }


        //acessar perfil
        public ActionResult PerfilPresAceito(string codPubli)
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado2"] != null)
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
                string codPres = ViewBag.codPres;
                return View(admAC.SelectUserPres(codPres));
            }
        }



        public ActionResult MeuPerfil()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado2"] != null)
            {
                return RedirectToAction("semAcesso", "Home");
            }
            else
            {
                ViewBag.Link = "https://wa.me/55";
                ViewBag.cod = Session["codUsuario"].ToString();
                return View(admAC.SelectUserComum(ViewBag.cod));
            }
        }



        public ActionResult MinhasPublicacoes()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado2"] != null)
            {
                return RedirectToAction("semAcesso", "Home");
            }
            else
            {

                int idUsuarioAtual = int.Parse(Session["codUsuario"].ToString());
                return View(pbAC.PublisUsuario(idUsuarioAtual));
            }
        }

        public ActionResult EditarPubli()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado2"] != null)
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
        public ActionResult EditarPubli(clPublicacao publicacao, string id, string descricao, string catPubli)
        {
            if (ModelState.IsValid)
            {
                pbAC.editarPubliCliente(id, descricao, catPubli);
                ModelState.Clear();

                publicacao = new clPublicacao();

                ViewBag.Message = "Publicação alterada com sucesso!";

                return View(publicacao);

            }
            return View(publicacao);
        }

        public ActionResult ExcluirPubli(int id)
        {
            ViewBag.codPubli = id;
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmarExcluir(int id)
        {
            pbAC.ExcluirPubli(id);
            return RedirectToAction("MinhasPublicacoes", "UsuarioComum");
        }




        public ActionResult Produto()
        {
            return View(produtoAC.selecionarProduto());
        }





        public ActionResult Pagamento(int id)
        {
            TempData["codProd"] = id;

            return View();
        }

        [HttpPost]
        public ActionResult Pagamento(clPagamento clPag, string prod)
        {
            if (ModelState.IsValid)
            {
                string codProd = TempData["codProd"].ToString();
                clPag.codProd = Convert.ToInt32(codProd);

                produtoAC.Pagamento(clPag);
                ModelState.Clear();
                clPag = new clPagamento();
                ViewBag.Message = "Pagamento efetuado com sucesso!";
                return View(clPag);
            }
            return View(clPag);
        }



    }
}
