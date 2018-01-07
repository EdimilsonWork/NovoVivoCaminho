using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NovoVivoCaminho.Models;

namespace NovoVivoCaminho.Controllers
{
    public class UsuariosController : Controller
    {
        private NVCEntities db = new NVCEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            if (Session["usuarioLogadoIDIgreja"] != null)
            {
                int idIgreja = int.Parse(Session["usuarioLogadoIDIgreja"].ToString());

                var usuarios = db.Usuarios.Where(x => x.IDIgreja.Equals(idIgreja)).Include(u => u.Igrejas);
                return View(usuarios.ToList());
            }
            else
                return RedirectToAction("Index", "Usuarios");
        }

        public ActionResult Acesso()
        {
            return View();
        }

        // GET: Usuarios/Details/5
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
        public ActionResult Create([Bind(Include = "ID,IDIgreja,Login,Senha,Nome,DataCriacao,DataAtualizacao,Ativo")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                if (Session["usuarioLogadoIDIgreja"] != null)
                {
                    int idIgreja = int.Parse(Session["usuarioLogadoIDIgreja"].ToString());
                    usuarios.DataCriacao = DateTime.Now;
                    usuarios.IDIgreja = idIgreja;

                    db.Usuarios.Add(usuarios);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.IDIgreja = new SelectList(db.Igrejas, "ID", "Nome", usuarios.IDIgreja);
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
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
        public ActionResult Edit([Bind(Include = "ID,IDIgreja,Login,Senha,Nome,DataCriacao,DataAtualizacao,Ativo")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                usuarios.DataAtualizacao = DateTime.Now;

                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDIgreja = new SelectList(db.Igrejas, "ID", "Nome", usuarios.IDIgreja);
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
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
