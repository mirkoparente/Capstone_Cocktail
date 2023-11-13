using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    

    public class RecensioniController : Controller
    {
        private ContextDbModel db = new ContextDbModel();

        // prendo le prime 4 recensioni
        public ActionResult RecensioniProdotto(int id)
        {
            var recensioni = db.Recensioni.Include(m=>m.AspNetUsers).Where(r => r.IdProdotti == id).OrderByDescending(m=>m.DataCommento).Take(4).ToList();
            return PartialView("RecensioniProdotto",recensioni);
        }

       

       //creo la recensione dell'utente
        public ActionResult CreaRecensione(int id,int valutazione,string commento)
        {
                Recensioni recensioni=new Recensioni();
            if (ModelState.IsValid)
            {

                string user = User.Identity.GetUserId();
                recensioni.DataCommento = DateTime.Now;
                recensioni.IdProdotti = id;
                recensioni.Id = user;
                recensioni.Valutazione= valutazione;
                recensioni.Commento= commento;
                db.Recensioni.Add(recensioni);
                db.SaveChanges();

            }

            return Json(recensioni,JsonRequestBehavior.AllowGet);

        }

        // GET: Recensioni/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recensioni recensioni = db.Recensioni.Find(id);
            if (recensioni == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", recensioni.Id);
            ViewBag.IdProdotti = new SelectList(db.Prodotti, "IdProdotti", "NomeProdotto", recensioni.IdProdotti);
            return View(recensioni);
        }

        // POST: Recensioni/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRecensioni,IdProdotti,Id,Valutazione,Commento,DataCommento")] Recensioni recensioni)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recensioni).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", recensioni.Id);
            ViewBag.IdProdotti = new SelectList(db.Prodotti, "IdProdotti", "NomeProdotto", recensioni.IdProdotti);
            return View(recensioni);
        }

        // GET: Recensioni/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recensioni recensioni = db.Recensioni.Find(id);
            if (recensioni == null)
            {
                return HttpNotFound();
            }
            return View(recensioni);
        }

        // POST: Recensioni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recensioni recensioni = db.Recensioni.Find(id);
            db.Recensioni.Remove(recensioni);
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
