using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UtentiController : Controller
    {
        private ContextDbModel db = new ContextDbModel();

        //view Utenti
        //prendo la lista degli utenti che non sono admin
        public ActionResult ListaUtenti()
        {
            var utenti = db.AspNetUsers.Where(m=>m.AspNetRoles.Any(i=>i.Name=="User")) ;
            return View(utenti.ToList());
        }


        //controllo se l'utente esiste ed è loggato per passarlo alla view
        public ActionResult ModificaUtente()
        {
            string userUse = User.Identity.Name;
            AspNetUsers user = db.AspNetUsers.FirstOrDefault(e => e.UserName == userUse);
            
            return View(user);



        }


        //modifica profilo utente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificaUtente([Bind(Exclude = "IdUtenti")] AspNetUsers user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Manage");

            }
            return View(user);

        }

        public ActionResult Delete(string id)
        {
            AspNetUsers utenti = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(utenti);
            db.SaveChanges();
            FormsAuthentication.SignOut();
            Session.Clear();

            return RedirectToAction("Index", "Home");
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