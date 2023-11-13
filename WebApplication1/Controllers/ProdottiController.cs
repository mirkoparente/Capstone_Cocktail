using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProdottiController : Controller
    {
        private ContextDbModel db = new ContextDbModel();

        // lista cocktail user
        public ActionResult ListaCocktail()
        {
            var prodotti = db.Prodotti.Include(p => p.Categoria).Where(p => p.Categoria.DescrizioneCategoria == "Cocktail");
            return View(prodotti.ToList());
        }
        //Lista accessori user
        public ActionResult ListaAccessori()
        {
            var prodotti = db.Prodotti.Include(p => p.Categoria).Where(p => p.Categoria.DescrizioneCategoria == "Accessori Mixology");
            return View(prodotti.ToList());
        }

        //Lista cocktail admin
        public ActionResult ListaCocktailAdmin()
        {
            var prodotti = db.Prodotti.Include(p => p.Categoria).Where(p => p.Categoria.DescrizioneCategoria == "Cocktail");
            return View(prodotti.ToList());
        }

        //Lista accessori admin
        public ActionResult ListaAccessoriAdmin()
        {
            var prodotti = db.Prodotti.Include(p => p.Categoria).Where(p => p.Categoria.DescrizioneCategoria == "Accessori Mixology");
            return View(prodotti.ToList());
        }

        // Dettagli prodotto user
        public ActionResult DettagliProdotto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            double? media = db.Recensioni
         .Where(r => r.IdProdotti == id)
         .Average(r => r.Valutazione);
            prodotti.MediaValutazione = media;

            int? tot= db.Recensioni.Where(r => r.IdProdotti == id)
         .Count();
            prodotti.TotRecensioni = tot;

            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }


        // GET: Prodottis/Create
        public ActionResult AddProdotto()
        {
            ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "DescrizioneCategoria");
            return View();
        }

       //creo un prodotto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProdotto([Bind(Include = "IdProdotti,NomeProdotto,Descrizione,DescrizioneBreve,Prezzo,PrezzoScontato,Disponibile,Quantita,FotoCopertina,Foto1,Foto2,Gusto,Abbinamenti,IdCategoria,Ingredienti")] Prodotti prodotti, HttpPostedFileBase FotoCopertina, HttpPostedFileBase Foto1, HttpPostedFileBase Foto2)
        {
            if (ModelState.IsValid)
            {
                if (FotoCopertina != null)
                {
                    string source = Path.Combine(Server.MapPath("~/Content/img"), FotoCopertina.FileName);
                    FotoCopertina.SaveAs(source);
                    prodotti.FotoCopertina = FotoCopertina.FileName;
                }
                if (Foto1 != null)
                {
                    string source = Path.Combine(Server.MapPath("~/Content/img"), Foto1.FileName);
                    Foto1.SaveAs(source);
                    prodotti.Foto1 = Foto1.FileName;
                }
                if (Foto2 != null)
                {
                    string source = Path.Combine(Server.MapPath("~/Content/img"), Foto2.FileName);
                    Foto2.SaveAs(source);
                    prodotti.Foto2 = Foto2.FileName;
                }
                if (prodotti.Quantita > 0)
                {
                    prodotti.Disponibile = true;
                }


                db.Prodotti.Add(prodotti);
                db.SaveChanges();
                if (User.IsInRole("Admin"))
                {

                if (prodotti.IdCategoria == 1)
                {
                    return RedirectToAction("ListaCocktailAdmin");

                }
                else
                {
                    return RedirectToAction("ListaAccessoriAdmin");

                }
                }
              
                
            }

            ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "DescrizioneCategoria", prodotti.IdCategoria);
            return View(prodotti);
        }

        // view modifica prodotto
        public ActionResult EditProdotto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "DescrizioneCategoria", prodotti.IdCategoria);
            return View(prodotti);
        }

       //modifico il prodotto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProdotto([Bind(Include = "IdProdotti,NomeProdotto,Descrizione,DescrizioneBreve,Prezzo,PrezzoScontato,Disponibile,Quantita,FotoCopertina,Foto1,Foto2,Gusto,Abbinamenti,IdCategoria,Ingredienti")] Prodotti prodotti, HttpPostedFileBase FotoCopertina, HttpPostedFileBase Foto1, HttpPostedFileBase Foto2)
        {
            if (ModelState.IsValid)
            {
                if (FotoCopertina != null)
                {
                    string source = Path.Combine(Server.MapPath("~/Content/img"), FotoCopertina.FileName);
                    FotoCopertina.SaveAs(source);
                    prodotti.FotoCopertina = FotoCopertina.FileName;
                }
                if (Foto1 != null)
                {
                    string source = Path.Combine(Server.MapPath("~/Content/img"), Foto1.FileName);
                    Foto1.SaveAs(source);
                    prodotti.Foto1 = Foto1.FileName;
                }
                if (Foto2 != null)
                {
                    string source = Path.Combine(Server.MapPath("~/Content/img"), Foto2.FileName);
                    Foto2.SaveAs(source);
                    prodotti.Foto2 = Foto2.FileName;
                }
                if (prodotti.Quantita != 0)
                {
                    prodotti.Disponibile = true;
                }
                else
                {
                    prodotti.Disponibile = false;

                }
                db.Entry(prodotti).State = EntityState.Modified;
                db.SaveChanges();
                if (prodotti.IdCategoria == 1)
                {
                    return RedirectToAction("ListaCocktailAdmin");

                }
                else
                {
                    return RedirectToAction("ListaAccessoriAdmin");

                }
            }
            ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "DescrizioneCategoria", prodotti.IdCategoria);
            return View(prodotti);
        }

        // GET: Prodottis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // POST: Prodottis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prodotti prodotti = db.Prodotti.Find(id);
            db.Prodotti.Remove(prodotti);
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