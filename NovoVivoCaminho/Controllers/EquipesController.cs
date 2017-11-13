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
    public class EquipesController : Controller
    {
        private NVCEntities db = new NVCEntities();

        // GET: Equipes
        public ActionResult Index()
        {
            var equipes = db.Equipes.Include(e => e.Igrejas);
            return View(equipes.ToList());
        }

        // GET: Equipes/Details/5
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
        public ActionResult Create([Bind(Include = "ID,Nome,IDIgreja")] Equipes equipes)
        {
            if (ModelState.IsValid)
            {
                db.Equipes.Add(equipes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDIgreja = new SelectList(db.Igrejas, "ID", "Nome", equipes.IDIgreja);
            return View(equipes);
        }

        // GET: Equipes/Edit/5
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
        public ActionResult Edit([Bind(Include = "ID,Nome,IDIgreja")] Equipes equipes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDIgreja = new SelectList(db.Igrejas, "ID", "Nome", equipes.IDIgreja);
            return View(equipes);
        }

        // GET: Equipes/Delete/5
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
