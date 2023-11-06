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
        public List<SelectListItem> Valutazione
        {
            get
            {
                List<SelectListItem> list = new List<SelectListItem>();
                SelectListItem item = new SelectListItem { Text = "Seleziona", Value = "0" };
                SelectListItem item2 = new SelectListItem { Text = "1", Value = "1" };
                SelectListItem item3 = new SelectListItem { Text = "2", Value = "2" };
                SelectListItem item4 = new SelectListItem { Text = "3", Value = "3" };
                SelectListItem item5 = new SelectListItem { Text = "4", Value = "4" };
                SelectListItem item6 = new SelectListItem { Text = "5", Value = "5" };
                list.Add(item);
                list.Add(item2);
                list.Add(item3);
                list.Add(item4);
                list.Add(item5);
                list.Add(item6);
                return list;
            }
        }


        private ContextDbModel db = new ContextDbModel();

        // GET: Recensioni
        public ActionResult RecensioniProdotto(int id)
        {
            var recensioni = db.Recensioni.Include(m=>m.AspNetUsers).Where(r => r.IdProdotti == id).ToList();
            return PartialView("RecensioniProdotto",recensioni);
        }

        // GET: Recensioni/Details/5
        public ActionResult Details(int? id)
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

        // GET: Recensioni/Create
        public ActionResult _CreaRecensione()
        {

            ViewBag.valutazione = Valutazione;
            return PartialView("_CreaRecensione");

        }

        // POST: Recensioni/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult _CreaRecensione([Bind(Include = "IdRecensioni,IdProdotti,Id,Valutazione,Commento,DataCommento")] Recensioni recensioni, int id,int valutazione)
        {
            if (ModelState.IsValid)
            {
                ViewBag.valutazione = Valutazione;

                string user = User.Identity.GetUserId();
                recensioni.DataCommento = DateTime.Now;
                recensioni.IdProdotti = id;
                recensioni.Id = user;
                recensioni.Valutazione= valutazione;
                db.Recensioni.Add(recensioni);
                db.SaveChanges();

            }

            return PartialView("_CreaRecensione",recensioni);

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
