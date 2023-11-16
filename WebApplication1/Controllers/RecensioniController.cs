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
          // prendo tutte le recensioni
        public ActionResult RecensioniProdottoTot(int id)
        {
            var recensioni = db.Recensioni.Include(m=>m.AspNetUsers).Where(r => r.IdProdotti == id).OrderByDescending(m=>m.DataCommento).ToList();
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

      
        public ActionResult edito(int id)
        {
            RecensioniJson r=new RecensioniJson();
            Recensioni recensioni=db.Recensioni.Find(id);

            r.Commento = recensioni.Commento;
            r.Valutazione=recensioni.Valutazione;
            r.IdProdotti = recensioni.IdProdotti;
            r.IdRecensioni = recensioni.IdRecensioni;
            return Json(r, JsonRequestBehavior.AllowGet);
        }
     
        public ActionResult Edit( int id, string commento, int idRece, int valutazione, Recensioni recensioni)
        {
            if (ModelState.IsValid)
            {
                string user = User.Identity.GetUserId();
                recensioni.DataCommento = DateTime.Now;
                recensioni.IdRecensioni = idRece;
                recensioni.IdProdotti = id;
                recensioni.Id = user;
                recensioni.Valutazione =valutazione;
                recensioni.Commento = commento;
                db.Entry(recensioni).State = EntityState.Modified;
                db.SaveChanges();
            }
           
            return Json(recensioni,JsonRequestBehavior.AllowGet);
        }



        //Recensioni/Delete/5
        public ActionResult Delete(int id)
        {
            Recensioni recensioni = db.Recensioni.Find(id);
            int? idprod = recensioni.IdProdotti;
            db.Recensioni.Remove(recensioni);
            db.SaveChanges();
            return RedirectToAction("DettagliProdotto","Prodotti",new {id=idprod});
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
