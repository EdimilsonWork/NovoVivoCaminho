using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NovoVivoCaminho.ViewModels;
using System.Security.Claims;
using NovoVivoCaminho.Models;
using NovoVivoCaminho.Utils;

namespace NovoVivoCaminho.Controllers
{
    public class PerfilController : Controller
    {
        private NVCEntities db = new NVCEntities();

        // GET: Perfil
        [Authorize]
        public ActionResult AlterarSenha()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenhaViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            string login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;

            Usuarios usuario = db.Usuarios.FirstOrDefault(u => u.Login == login);

            if (Hash.GerarHash(viewmodel.SenhaAtual) != usuario.Senha)
            {
                ModelState.AddModelError("Senha", "Senha incorreta");
                return View();
            }

            usuario.Senha = Hash.GerarHash(viewmodel.SenhaAtual);
            db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            TempData["Mensagem"] = "Senha alterada com sucesso!";

            return RedirectToAction("Index", "Painel");
        }
    }
}