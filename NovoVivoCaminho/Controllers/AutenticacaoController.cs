using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NovoVivoCaminho.Models;
using NovoVivoCaminho.ViewModels;
using NovoVivoCaminho.Utils;
using System.Security.Claims;

namespace NovoVivoCaminho.Controllers
{
    public class AutenticacaoController : Controller
    {
        private NVCEntities db = new NVCEntities();
        // GET: Autenticacao
        public ActionResult Cadastrar()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Cadastrar(CadastroUsuarioViewModel viewmodel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(viewmodel);
        //    }

        //    if (db.Usuarios.Count(u => u.Login == viewmodel.Login) > 0)
        //    {
        //        ModelState.AddModelError("Login", "Esse login já está em uso");
        //        return View(viewmodel);
        //    }

        //    Usuario novoUsuario = new Usuario
        //    {
        //        Nome = viewmodel.Nome,
        //        Login = viewmodel.Login,
        //        Senha = Hash.GerarHash(viewmodel.Senha)
        //    };

        //    db.Usuarios.Add(novoUsuario);
        //    db.SaveChanges();

        //    TempData["Mensagem"] = "Cadastro realizado com sucesso. Efetue o login.";

        //    return RedirectToAction("Login");
        //}

        public ActionResult Login(string ReturnUrl)
        {
            var viewmodel = new LoginViewModel
            {
                UrlRetorno = ReturnUrl
            };

            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }

            var usuario = db.Usuarios.FirstOrDefault(u => u.Login == viewmodel.Login);

            if (usuario == null)
            {
                ModelState.AddModelError("Login", "Login incorreto");
                return View(viewmodel);
            }

            if (usuario.Senha != Hash.GerarHash(viewmodel.Senha))
            {
                ModelState.AddModelError("Senha", "Senha incorreta");
                return View(viewmodel);
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim("Login", usuario.Login),
            }, "ApplicationCookie");

            Request.GetOwinContext().Authentication.SignIn(identity);

            if (!string.IsNullOrWhiteSpace(viewmodel.UrlRetorno) || Url.IsLocalUrl(viewmodel.UrlRetorno))
                return Redirect(viewmodel.UrlRetorno);
            else
                return RedirectToAction("Index", "Painel");
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }
    }
}