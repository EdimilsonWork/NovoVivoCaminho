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
    public class IgrejasController : Controller
    {
        private NVCEntities db = new NVCEntities();

        // GET: Igrejas
        public ActionResult Index()
        {
            return View(db.Igrejas.ToList());
        }

        // GET: Igrejas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Igrejas igrejas = db.Igrejas.Find(id);
            if (igrejas == null)
            {
                return HttpNotFound();
            }
            return View(igrejas);
        }

        // GET: Igrejas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Igrejas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Ativo,Equipe")] Igrejas igrejas)
        {
            if (ModelState.IsValid)
            {
                db.Igrejas.Add(igrejas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(igrejas);
        }

        // GET: Igrejas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Igrejas igrejas = db.Igrejas.Find(id);
            if (igrejas == null)
            {
                return HttpNotFound();
            }
            return View(igrejas);
        }

        // POST: Igrejas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Ativo,Equipe")] Igrejas igrejas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(igrejas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(igrejas);
        }

        // GET: Igrejas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Igrejas igrejas = db.Igrejas.Find(id);
            if (igrejas == null)
            {
                return HttpNotFound();
            }
            return View(igrejas);
        }

        // POST: Igrejas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Igrejas igrejas = db.Igrejas.Find(id);
            db.Igrejas.Remove(igrejas);
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
