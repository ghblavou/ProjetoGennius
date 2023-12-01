using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using projGennius.Dados;
using projGennius.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projGennius.Controllers
{
    public class UsuarioAdminController : Controller
    {
        Conexao con = new Conexao();
        PublicacaoAC clP = new PublicacaoAC();
        AdmAC admAC = new AdmAC();
        ModelLogin mdLogin = new ModelLogin();
        produtoAC prodAC = new produtoAC();

        public ActionResult Index()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado3"] == null)
            {
                return RedirectToAction("semAcesso", "Home");

            }
            else
            {
                ViewBag.Nome = Session["usuarioLogado"];
                return View(clP.selecionarPubliPendente());
            }


        }

        public ActionResult PubliAprovada()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado3"] == null)
            {
                return RedirectToAction("semAcesso", "Home");

            }
            else
            {
                ViewBag.Nome = Session["usuarioLogado"];
                return View(clP.selecionarPubliAprovada());
            }

        }

        public ActionResult UsuariosCadastrados()
        {

            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado3"] == null)
            {
                return RedirectToAction("semAcesso", "Home");

            }
            else
            {
                return View(admAC.Users());
            }

        }

        public ActionResult DevCadastrado()
        {

            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado3"] == null)
            {
                return RedirectToAction("semAcesso", "Home");

            }
            else
            {
                return View(admAC.DevCad());
            }

        }

        public ActionResult AdminsCadastrados()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado3"] == null)
            {
                return RedirectToAction("semAcesso", "Home");

            }
            else
            {
                string id = Session["codUsuario"].ToString();

                MySqlCommand cmd = new MySqlCommand("select * from tbUsuario where codUsu = @codUsu", con.MyConectarBD());

                cmd.Parameters.AddWithValue("@codUsu", id);

                MySqlDataReader leitor;

                leitor = cmd.ExecuteReader();

                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        ViewBag.sta = Convert.ToString(leitor["sta"]);
                    }
                }
                con.MyDesconectarBD();

                if(ViewBag.sta == "Desativado")
                {
                    return RedirectToAction("semAcesso", "Home");

                }
                else
                {
                    return View(admAC.AdminCad());

                }

            }
        }




        public ActionResult PerfilUsu(string id)
        {

            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado3"] == null)
            {
                return RedirectToAction("semAcesso", "Home");

            }
            else
            {
                MySqlCommand cmd = new MySqlCommand("select * from tbUsuario where codUsu = @codUsu", con.MyConectarBD());

                cmd.Parameters.AddWithValue("@codUsu", id);

                MySqlDataReader leitor;

                leitor = cmd.ExecuteReader();

                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        ViewBag.codUsu = Convert.ToString(leitor["codUsu"]);
                        ViewBag.tipo = Convert.ToString(leitor["tipo"]);
                    }
                }
                con.MyDesconectarBD();


                TempData["idUsu"] = ViewBag.codUsu;

                if (ViewBag.tipo == "1")
                {
                    return RedirectToAction("PerfilComum", "UsuarioAdmin");

                }
                else if (ViewBag.tipo == "2")
                {
                    return RedirectToAction("PerfilPres", "UsuarioAdmin");

                }
                else
                {
                    return RedirectToAction("PerfilAdmin", "UsuarioAdmin");
                }
            }

        }

        public ActionResult PerfilComum()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado3"] == null)
            {
                return RedirectToAction("semAcesso", "Home");

            }
            else
            {
                ViewBag.Link = "https://wa.me/55";
                if (TempData["idUsu"] == null)
                {
                    return RedirectToAction("UsuariosCadastrados", "UsuarioAdmin");
                }
                ViewBag.cod = TempData["idUsu"].ToString();
                return View(admAC.SelectUserComum(ViewBag.cod));
            }
        }

        public ActionResult PerfilPres()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado3"] == null)
            {
                return RedirectToAction("semAcesso", "Home");

            }
            else
            {
                ViewBag.Link = "https://wa.me/55";
                if (TempData["idUsu"] == null)
                {
                    return RedirectToAction("DevCadastrado", "UsuarioAdmin");
                }
                ViewBag.cod = TempData["idUsu"].ToString();
                return View(admAC.SelectUserPres(ViewBag.cod));
            }

        }

        public ActionResult PerfilAdmin()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado3"] == null)
            {
                return RedirectToAction("semAcesso", "Home");

            }
            else
            {
                if (TempData["idUsu"] == null)
                {
                    return RedirectToAction("AdminsCadastrados", "UsuarioAdmin");
                }
                ViewBag.cod = TempData["idUsu"].ToString();
                return View(admAC.SelectUserAdmin(ViewBag.cod));
            }
        }




        public ActionResult RecusarPubli(string id)
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado3"] == null)
            {
                return RedirectToAction("semAcesso", "Home");

            }
            else
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE tbPubli SET situacaoPubli = 'Pendente' WHERE codPubli = @codPubli;", con.MyConectarBD());


                cmd.Parameters.Add("@codPubli", MySqlDbType.Int64).Value = id;

                cmd.ExecuteNonQuery();
                con.MyDesconectarBD();

                return RedirectToAction("PubliAprovada", "UsuarioAdmin");
            }
        }

        public ActionResult AprovarPubli(string id)
        {

            MySqlCommand cmd = new MySqlCommand("UPDATE tbPubli SET situacaoPubli = 'Aprovado' WHERE codPubli = @codPubli;", con.MyConectarBD());


            cmd.Parameters.Add("@codPubli", MySqlDbType.Int64).Value = id;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            return RedirectToAction("Index", "UsuarioAdmin");
        }

        public ActionResult EditarPubli()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado3"] == null)
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
                clP.editarPubli(id, descricao, catPubli);
                ModelState.Clear();

                publicacao = new clPublicacao();

                ViewBag.Message = "Publicação alterada com sucesso!";

                return View(publicacao);

            }
            return View(publicacao);
        }

        public ActionResult ExcluirPubli(int id)
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["tipoLogado3"] == null)
            {
                return RedirectToAction("semAcesso", "Home");

            }
            else
            {
                ViewBag.codPubli = id;
                return View();
            }
        }

        [HttpPost]
        public ActionResult ConfirmarExcluir(int id)
        {
           clP.ExcluirPubli(id);
           return RedirectToAction("Index", "UsuarioAdmin");
        }



        public ActionResult DesativarPerfil(int codUsu)
        {
            admAC.desativar(codUsu);
            MySqlCommand cmd = new MySqlCommand("select * from tbUsuario where codUsu = @codUsu", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@codUsu", codUsu);
            MySqlDataReader leitor;
            leitor = cmd.ExecuteReader();
            if (leitor.HasRows)
            {
                while (leitor.Read())
                {

                    ViewBag.tipo = Convert.ToString(leitor["tipo"]);
                }
            }
            con.MyDesconectarBD();

            if(ViewBag.tipo == "1")
            {
                return RedirectToAction("UsuariosCadastrados", "UsuarioAdmin");

            }
            else if(ViewBag.tipo == "2")
            {
                return RedirectToAction("DevCadastrado", "UsuarioAdmin");

            }
            else
            {
                return RedirectToAction("AdminsCadastrados", "UsuarioAdmin");

            }
        }
        public ActionResult AtivarPerfil(int codUsu)
        {
            admAC.ativar(codUsu);
            MySqlCommand cmd = new MySqlCommand("select * from tbUsuario where codUsu = @codUsu", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@codUsu", codUsu);
            MySqlDataReader leitor;
            leitor = cmd.ExecuteReader();
            if (leitor.HasRows)
            {
                while (leitor.Read())
                {

                    ViewBag.tipo = Convert.ToString(leitor["tipo"]);
                }
            }
            con.MyDesconectarBD();


            if (ViewBag.tipo == "1")
            {

                return RedirectToAction("UsuariosCadastrados", "UsuarioAdmin");

            }
            else if (ViewBag.tipo == "2")
            {
                return RedirectToAction("DevCadastrado", "UsuarioAdmin");

            }
            else
            {
                return RedirectToAction("AdminsCadastrados", "UsuarioAdmin");

            }
        }

        public ActionResult VendaSite()
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
        public ActionResult VendaSite(clProduto clProd, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                string arquivo = Path.GetFileName(file.FileName);
                string file2 = "/Imagens/" + arquivo;
                string _path = Path.Combine(Server.MapPath("~/Imagens"), arquivo);
                file.SaveAs(_path);

                clProd.imagemProd = file2;
                prodAC.VendaSite(clProd);
                ModelState.Clear();
                clProd = new clProduto();
                ViewBag.msg = "Cadastro realizado";

                return View(clProd);
            }
            return View(clProd);
        }

    }
}
