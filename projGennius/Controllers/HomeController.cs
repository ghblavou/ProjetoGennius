using projGennius.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using projGennius.Dados;
using MySqlX.XDevAPI;


namespace projGennius.Controllers
{
    public class HomeController : Controller
    {


        acoesLogin acLg = new acoesLogin();
        UsuarioAC acUsuarioAcoes = new UsuarioAC();
        clUsuario modUsuario = new clUsuario();
        PrestadorAC acPrestadorAcoes = new PrestadorAC();
        clPrestador modPres = new clPrestador();
        PublicacaoAC pbAC = new PublicacaoAC();



        public ActionResult Index()
        {
            Session["nomeUsu"] = null;
            Session["usuarioLogado"] = null;
            Session["senhaLogado"] = null;
            Session["tipoLogado1"] = null;
            Session["tipoLogado2"] = null;
            Session["tipoLogado3"] = null;
            return View();
        }

        public ActionResult IndexComum()
        {
            Session["nomeUsu"] = null;
            Session["usuarioLogado"] = null;
            Session["senhaLogado"] = null;
            Session["tipoLogado1"] = null;
            Session["tipoLogado2"] = null;
            Session["tipoLogado3"] = null;
            ViewBag.homeUsu = "Usuário";
            return View(pbAC.selecionarPubliAprovada());
        }

        public ActionResult IndexPrestador()
        {
            Session["nomeUsu"] = null;
            Session["usuarioLogado"] = null;
            Session["senhaLogado"] = null;
            Session["tipoLogado1"] = null;
            Session["tipoLogado2"] = null;
            Session["tipoLogado3"] = null;
            ViewBag.homePres = "Dev";
            return View(pbAC.selecionarPubliAprovada());
        }



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ModelLogin verLogin)
        {

            acLg.testarUsuario(verLogin);
            if (verLogin.login != null && verLogin.senha != null)
            {
                if(verLogin.sta != "Ativo")
                {
                    ViewBag.msgDesativado = "Usuário desativado.";
                    return View();

                }
                Session["nomeUsu"] = verLogin.nome.ToString();
                Session["usuarioLogado"] = verLogin.login.ToString();
                Session["SenhaLogado"] = verLogin.senha.ToString();
                Session["codUsuario"] = verLogin.codUsu.ToString();

                if (verLogin.tipo == "1")
                {
                    Session["tipoLogado1"] = verLogin.tipo.ToString();
                    return RedirectToAction("Index", "UsuarioComum");
                }
                if (verLogin.tipo == "2")
                {
                    Session["tipoLogado2"] = verLogin.tipo.ToString();
                    return RedirectToAction("Index", "UsuarioPrestador");
                }

                else
                {
                    Session["tipoLogado3"] = verLogin.tipo.ToString();
                    return RedirectToAction("Index", "UsuarioAdmin");
                }
                //return RedirectToAction("about","Home"
            }
            else
            {
                ViewBag.msgLogar = "Usuario não encontrado. Verifique o nome do usuario e a senha";
                return View();
            }
        }




        public ActionResult tipoUsu()
        {

            return View();
        }
        [HttpPost]
        public ActionResult tipoUsu(string TipoUsuario)
        {
            TempData["tipo"] = TipoUsuario;

            // Definir automaticamente o tipo com base na escolha do usuário
            int tipo = TipoUsuario == "Prestador" ? 2 : 1;

            TempData["tipo"] = tipo.ToString();

            if (TipoUsuario == "Prestador")
            {
                return RedirectToAction("cadastroPres", "Home");
            }
            else if (TipoUsuario == "Usuario")
            {
                return RedirectToAction("cadastroUsu", "Home");
            }

            return View();
        }


        public ActionResult cadastroUsu()
        {
            ViewBag.tipo = TempData["tipo"];

            if (ViewBag.tipo == "1")
            {
                return View();
            }

            return RedirectToAction("tipoUsu", "Home");
        }

        //Cadastrar Usuario
        [HttpPost]
        public ActionResult cadastroUsu(clUsuario cliente)
        {
            acUsuarioAcoes.testarLogin(cliente);
            if (string.IsNullOrEmpty(cliente.login)) // Condição para invalidar o modelo
            {
                ModelState.AddModelError("Login", "O login já existe.");
            }

            if (ModelState.IsValid)
            {
                cliente.tipo = "1";


                acUsuarioAcoes.inserirCliente(cliente);

                ModelState.Clear();

                cliente = new clUsuario();

                ViewBag.Message = "Cadastro realizado com sucesso.";

                return View(cliente);

            }
            return View(cliente);

        }




        public ActionResult cadastroPres()
        {
            ViewBag.tipo = TempData["tipo"];

            if (ViewBag.tipo == "2")
            {
                return View();
            }

            return RedirectToAction("tipoUsu", "Home");
        }
        //Cadastrar prestador
        [HttpPost]
        public ActionResult cadastroPres(clPrestador prestador)
        {
            acPrestadorAcoes.testarLogin(prestador);
            if (string.IsNullOrEmpty(prestador.login)) // Condição para invalidar o modelo
            {
                ModelState.AddModelError("Login", "O login já existe.");
            }

            if (ModelState.IsValid)
            {
                prestador.tipo = "2";

                acPrestadorAcoes.inserirPres(prestador);

                ModelState.Clear();
                prestador = new clPrestador();

                ViewBag.Message = "Cadastro realizado com sucesso.";

                return View(prestador);
            }

            return View(prestador);
        }







        public ActionResult Logout()
        {
            //toda vez que você acessa a página inicial você desloga de tudo
            return RedirectToAction("Index", "Home");
        }

        public ActionResult semAcesso()
        {
            Session["nomeUsuario"] = null;
            Session["usuarioLogado"] = null;
            Session["senhaLogado"] = null;
            Session["tipoLogado1"] = null;
            Session["tipoLogado2"] = null;
            Session["tipoLogado3"] = null;
            Response.Write("<script>alert('Sem acesso')</script>");
            ViewBag.message = "Você não tem acesso a essa página";
            return View();
        }

    }
}