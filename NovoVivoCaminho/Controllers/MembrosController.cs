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
    public class MembrosController : Controller
    {
        private NVCEntities db = new NVCEntities();
        private Comum comum = new Comum();

        // GET: Membros
        [Authorize]
        public ActionResult Index()
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            string login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;

            var membros = db.Membros.Where(m => m.IDIgreja == db.Usuarios.Where(u => u.Login == login).FirstOrDefault().IDIgreja).Include(m => m.Equipes).Include(m => m.Igrejas).OrderBy(x => x.Nome);
            return View(membros.ToList());

        }

        // GET: Membros/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membros membros = db.Membros.Find(id);
            if (membros == null)
            {
                return HttpNotFound();
            }
            return View(membros);
        }

        // GET: Membros/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.TiposDeEnderecos = comum.TiposDeEnderecos;
            ViewBag.EstadoCivil = comum.EstadoCivil;

            ViewBag.IDEquipe = new SelectList(db.Equipes.OrderBy(x => x.Nome), "ID", "Nome");
            ViewBag.IDIgreja = new SelectList(db.Igrejas.OrderBy(x => x.Nome), "ID", "Nome");
            return View();
        }

        // POST: Membros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,IDIgreja,IDEquipe,Nome,Tipo,Endereco,Numero,Complemento,Bairro,Cidade,UF,CEP,DDD,Fone,EstadoCivil,Batizado,DataDeNascimento,MembroDesde")] Membros membros)
        {
            if (ModelState.IsValid)
            {
                db.Membros.Add(membros);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDEquipe = new SelectList(db.Equipes, "ID", "Nome", membros.IDEquipe);
            ViewBag.IDIgreja = new SelectList(db.Igrejas, "ID", "Nome", membros.IDIgreja);
            return View(membros);
        }

        // GET: Membros/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membros membros = db.Membros.Find(id);
            if (membros == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDEquipe = new SelectList(db.Equipes, "ID", "Nome", membros.IDEquipe);
            ViewBag.IDIgreja = new SelectList(db.Igrejas, "ID", "Nome", membros.IDIgreja);
            return View(membros);
        }

        // POST: Membros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,IDIgreja,IDEquipe,Nome,Tipo,Endereco,Numero,Complemento,Bairro,Cidade,UF,CEP,DDD,Fone,EstadoCivil,Batizado,DataDeNascimento,MembroDesde,Ativo")] Membros membros)
        {

            if (Session["usuarioLogadoIDIgreja"] != null)
            {
                int idIgreja = int.Parse(Session["usuarioLogadoIDIgreja"].ToString());

                if (ModelState.IsValid)
                {
                    membros.IDIgreja = idIgreja;

                    db.Entry(membros).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IDEquipe = new SelectList(db.Equipes, "ID", "Nome", membros.IDEquipe);
                ViewBag.IDIgreja = new SelectList(db.Igrejas, "ID", "Nome", membros.IDIgreja);
                return View(membros);
            }
            else
                return RedirectToAction("Index", "Membros");
        }

        // GET: Membros/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membros membros = db.Membros.Find(id);
            if (membros == null)
            {
                return HttpNotFound();
            }
            return View(membros);
        }

        // POST: Membros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Membros membros = db.Membros.Find(id);
            db.Membros.Remove(membros);
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
