using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NovoVivoCaminho.Models;
using System.Security.Claims;
using NovoVivoCaminho.Utils;

namespace NovoVivoCaminho.Controllers
{
    public class UsuariosController : Controller
    {
        private NVCEntities db = new NVCEntities();

        // GET: Usuarios
        [Authorize]
        public ActionResult Index()
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            string login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;

            //TODO: Criar atributo "ADMINISTRADOR".
            if (login == "edimilson")
            {
                var usuarios = db.Usuarios.Include(u => u.Igrejas);
                return View(usuarios.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Painel");
            }


        }

        [Authorize]
        public ActionResult Acesso()
        {
            return View();
        }

        // GET: Usuarios/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IDIgreja = new SelectList(db.Igrejas.OrderBy(x => x.Nome), "ID", "Nome");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,IDIgreja,Login,Senha,Nome,DataCriacao,DataAtualizacao,Ativo")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                ClaimsIdentity identity = User.Identity as ClaimsIdentity;
                string login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;

                usuarios.DataCriacao = DateTime.Now;
                usuarios.Senha = Hash.GerarHash(usuarios.Senha);

                db.Usuarios.Add(usuarios);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDIgreja = new SelectList(db.Igrejas, "ID", "Nome", usuarios.IDIgreja);
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,IDIgreja,Login,Senha,Nome,DataCriacao,DataAtualizacao,Ativo")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                Usuarios user = db.Usuarios.FirstOrDefault(u => u.ID == usuarios.ID);

                if (user.Login != usuarios.Login)
                    user.Login = usuarios.Login;

                if (usuarios.Nome.ToUpper() != user.Nome)
                    user.Nome = usuarios.Nome.ToUpper();

                if (usuarios.Senha != user.Senha)
                    user.Senha = Hash.GerarHash(usuarios.Senha);

                user.DataAtualizacao = DateTime.Now;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
