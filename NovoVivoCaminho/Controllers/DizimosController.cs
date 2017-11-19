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
    public class DizimosController : Controller
    {
        private NVCEntities db = new NVCEntities();

        // GET: Dizimos
        public ActionResult Index()
        {
            var dizimos = db.Dizimos.Include(d => d.Membros).OrderByDescending(x => x.Data);
            return View(dizimos.ToList());
        }

        // GET: Dizimos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dizimos dizimos = db.Dizimos.Find(id);
            if (dizimos == null)
            {
                return HttpNotFound();
            }
            return View(dizimos);
        }

        // GET: Dizimos/Create
        public ActionResult Create()
        {
            ViewBag.IDMembro = new SelectList(db.Membros.OrderBy(x => x.Nome), "ID", "Nome");
            return View();
        }

        // POST: Dizimos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDMembro,Valor,Data")] Dizimos dizimos)
        {
            if (ModelState.IsValid)
            {
                if (dizimos.Data == null)
                    dizimos.Data = DateTime.Now;

                db.Dizimos.Add(dizimos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDMembro = new SelectList(db.Membros, "ID", "Nome", dizimos.IDMembro);
            return View(dizimos);
        }

        // GET: Dizimos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dizimos dizimos = db.Dizimos.Find(id);
            if (dizimos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDMembro = new SelectList(db.Membros, "ID", "Nome", dizimos.IDMembro);
            return View(dizimos);
        }

        // POST: Dizimos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDMembro,Valor,Data")] Dizimos dizimos)
        {
            if (ModelState.IsValid)
            {
                if (dizimos.Data == null)
                    dizimos.Data = DateTime.Now;

                db.Entry(dizimos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDMembro = new SelectList(db.Membros, "ID", "Nome", dizimos.IDMembro);
            return View(dizimos);
        }

        // GET: Dizimos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dizimos dizimos = db.Dizimos.Find(id);
            if (dizimos == null)
            {
                return HttpNotFound();
            }
            return View(dizimos);
        }

        // POST: Dizimos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dizimos dizimos = db.Dizimos.Find(id);
            db.Dizimos.Remove(dizimos);
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
