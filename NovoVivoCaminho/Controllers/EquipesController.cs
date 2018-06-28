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

namespace NovoVivoCaminho.Controllers
{
    public class EquipesController : Controller
    {
        private NVCEntities db = new NVCEntities();

        // GET: Equipes
        [Authorize]
        public ActionResult Index()
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            string login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;

            var equipes = db.Equipes.Where(e => e.IDIgreja == db.Usuarios.Where(u => u.Login == login).FirstOrDefault().IDIgreja).Include(e => e.Igrejas).OrderBy(x => x.Nome);

            return View(equipes.ToList());
        }

        // GET: Equipes/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipes equipes = db.Equipes.Find(id);
            if (equipes == null)
            {
                return HttpNotFound();
            }
            return View(equipes);
        }

        // GET: Equipes/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IDIgreja = new SelectList(db.Igrejas, "ID", "Nome");
            return View();
        }

        // POST: Equipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Nome,IDIgreja,Ativo")] Equipes equipes)
        {
            if (ModelState.IsValid)
            {
                ClaimsIdentity identity = User.Identity as ClaimsIdentity;
                string login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;

                equipes.IDIgreja = db.Usuarios.FirstOrDefault(u => u.Login == login).IDIgreja;

                db.Equipes.Add(equipes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");

        }

        // GET: Equipes/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipes equipes = db.Equipes.Find(id);
            if (equipes == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDIgreja = new SelectList(db.Igrejas, "ID", "Nome", equipes.IDIgreja);
            return View(equipes);
        }

        // POST: Equipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Nome,IDIgreja,Ativo")] Equipes equipes)
        {

            if (ModelState.IsValid)
            {
                ClaimsIdentity identity = User.Identity as ClaimsIdentity;
                string login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;

                equipes.IDIgreja = db.Usuarios.FirstOrDefault(u => u.Login == login).IDIgreja;
                db.Entry(equipes).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.IDIgreja = new SelectList(db.Igrejas, "ID", "Nome", equipes.IDIgreja);
            return View(equipes);
        }

        // GET: Equipes/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipes equipes = db.Equipes.Find(id);
            if (equipes == null)
            {
                return HttpNotFound();
            }
            return View(equipes);
        }

        // POST: Equipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipes equipes = db.Equipes.Find(id);
            db.Equipes.Remove(equipes);
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
