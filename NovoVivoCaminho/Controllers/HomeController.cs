﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NovoVivoCaminho.Models;

namespace NovoVivoCaminho.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["usuarioLogadoID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                using(NVCEntities nvc = new NVCEntities())
                {
                    Usuarios u = nvc.Usuarios.Where(x => x.Login.Equals(usuario.Login) && x.Senha.Equals(usuario.Senha)).FirstOrDefault();

                    if (u != null)
                    {
                        Igrejas igreja = nvc.Igrejas.Where(x => x.ID.Equals(u.IDIgreja)).FirstOrDefault();

                        if (u.Ativo.Equals(false) || igreja.Ativo.Equals(false))
                        {
                            return View(usuario);
                        }
                        else
                        {
                            Session["usuarioLogadoID"] = u.ID.ToString();
                            Session["usuarioLogadoIDIgreja"] = u.IDIgreja.ToString();
                            Session["nomeUsuarioLogado"] = u.Nome.ToString();
                            return RedirectToAction("Index");
                        }
                    }
                }
            }

            return View(usuario);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}