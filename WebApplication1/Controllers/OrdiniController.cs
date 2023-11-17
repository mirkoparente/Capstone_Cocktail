using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class OrdiniController : Controller
    {
        private ContextDbModel db = new ContextDbModel();

        // lista ordini
        [Authorize(Roles ="Admin")]
        public ActionResult ListaOrdini()
        {
            var ordini = db.Ordini.Include(o => o.AspNetUsers);
            return View(ordini.ToList());
        }

        public ActionResult DettagliOrdine(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordini prodotti = db.Ordini.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }


        //aggiungo il prodotto al carrello in una sessione
        public ActionResult AggiungiAlCarrello(int id)
        {
            List<Carrello> carrello;
            Prodotti p = db.Prodotti.Find(id);
            Carrello prdCar;
            int quantita = 1;
            bool IsSession = false;
            bool newProduct = true;

            if (Session["Carrello"] == null)
            {
                carrello = new List<Carrello>();

            }
            else
            {
                carrello = Session["Carrello"] as List<Carrello>;
                IsSession = true;
            }

            if (IsSession)
            {
                foreach (var i in carrello)
                {
                    if (i.IdProdotti == id)
                    {
                        i.QuantitaAcquistata += quantita;
                        newProduct = false;

                        break;
                    }
                }
            }

            if (newProduct)
            {
                prdCar = new Carrello(
                   p.IdProdotti,
                   p.NomeProdotto,
                   p.Descrizione,
                   p.Quantita,
                   p.Prezzo,
                   p.IdCategoria,
                   quantita,
                   p.FotoCopertina);

                carrello.Add(prdCar);
            }

            Session["Carrello"] = carrello;





            return RedirectToAction("Carrello", "Ordini");
        }


        //creo il carrello
        public ActionResult Carrello()
        {
            List<Carrello> carrello;
            decimal Totale = 0;
            if (Session["Carrello"] != null)
            {
                carrello = Session["Carrello"] as List<Carrello>;
                foreach (var e in carrello)
                {
                    Totale += (e.QuantitaAcquistata * e.Prezzo);

                }
                ViewBag.Totale = Totale;
            }
            else
            {
                carrello = new List<Carrello>();
                ViewBag.Totale = Totale;
            }

            ViewBag.Carrello = carrello;

            return View();
        }


        //json per modificare la quantità nel carrello e aggiornare il totale
        public ActionResult editPrdCarello(int id, int quantita)
        {

            List<Carrello> carrello = Session["Carrello"] as List<Carrello>;
            decimal Totale = 0;
            if (quantita == 0)
            {
                Carrello prdToRemove = carrello.FirstOrDefault(e => e.IdProdotti == id);
                carrello.Remove(prdToRemove);
            }

            foreach (var i in carrello)
            {
                if (id == i.IdProdotti)
                {
                    i.QuantitaAcquistata = quantita;
                }


                Totale += (i.QuantitaAcquistata * i.Prezzo);
            }
            Session["Carrello"] = carrello;
            ViewBag.Totale = Totale;
            if (carrello.Count == 0)
            {
                Session.Clear();
            }
            return Json(carrello, JsonRequestBehavior.AllowGet);


        }


        //rimuovere il prodotto nel carrello
        public ActionResult deletePrdCarello(int id)
        {

            List<Carrello> carrello = Session["Carrello"] as List<Carrello>;

            Carrello prdToRemove = carrello.FirstOrDefault(e => e.IdProdotti == id);
            carrello.Remove(prdToRemove);
            decimal Totale = 0;
            foreach (var i in carrello)
            {
                Totale += (i.Prezzo * i.QuantitaAcquistata);

            }
            Session["Carrello"] = carrello;
            ViewBag.Totale = Totale;
            if (carrello.Count == 0)
            {
                Session.Clear();
            }
            return Json(carrello, JsonRequestBehavior.AllowGet);

        }



        //checkout, creo l'ordine e il dettaglio ordine  da salvare nel db 
        public ActionResult Checkout()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<Prodotti> p = db.Prodotti.ToList();

                List<AspNetUsers> ut = db.AspNetUsers.ToList();


                Ordini ordine = new Ordini();
                ordine.DataOrdine = DateTime.Now;
                foreach (var i in ut)
                {
                    string user = User.Identity.Name;
                    if (i.UserName == user)
                    {

                        ordine.Id = i.Id;
                    }
                }

                decimal Totale = 0;

                if (Session["Carrello"] != null)
                {

                    List<Carrello> carrello = Session["Carrello"] as List<Carrello>;
                    foreach (var e in carrello)
                    {

                        Totale += (e.QuantitaAcquistata * e.Prezzo);

                        foreach (var a in p)
                        {
                            if (a.IdProdotti == e.IdProdotti)
                            {
                                a.Quantita -= e.QuantitaAcquistata;

                            }
                            if (a.Quantita == 0)
                            {
                                a.Disponibile = false;
                            }

                        }



                    }
                    ordine.TotaleOrdine = Totale;

                    db.Ordini.Add(ordine);
                    db.SaveChanges();

                    foreach (var i in carrello)
                    {
                        DettagliOrdine prd = new DettagliOrdine();
                        prd.IdProdotti = i.IdProdotti;
                        prd.IdOrdini = ordine.IdOrdini;
                        prd.Quantita = i.QuantitaAcquistata;

                        prd.Totale = (i.QuantitaAcquistata * i.Prezzo);
                        db.DettagliOrdine.Add(prd);
                        db.SaveChanges();
                    }
                }
                db.Dispose();
                Session.Clear();
            }
            else
            {
                RedirectToAction("Login", "User");
            }

            var respone = new
            {
                response = true,

            };

            return Json(respone, JsonRequestBehavior.AllowGet);
        }





        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //async totale giornaliero
        public JsonResult Lista(DateTime data)
        {
            DateTime D = data.Date;

            List<Ordini> PrdInData = db.Ordini
                .Where(m => DbFunctions.TruncateTime(m.DataOrdine) == D)
                .ToList();

            List<OrdiniToJson> or = new List<OrdiniToJson>();

            foreach (var i in PrdInData)
            {
                OrdiniToJson o = new OrdiniToJson();
                o.IdOrdini = i.IdOrdini;
                o.DataOrdine = i.DataOrdine.ToString();
                o.TotaleOrdine = i.TotaleOrdine;

                o.DettagliOrdine = new List<DettagliOrdineToJson>();

                foreach (var j in i.DettagliOrdine)
                {
                    DettagliOrdineToJson dettaglio = new DettagliOrdineToJson
                    {
                        Quantita = j.Quantita,
                        Nome = j.Prodotti.NomeProdotto
                    };
                    o.DettagliOrdine.Add(dettaglio);
                }

                or.Add(o);
            }

            return Json(or, JsonRequestBehavior.AllowGet);
        }


        //totale ordini
        public ActionResult TotOrdini()
        {
            int TotOrd = db.Ordini.Count();

            return Json(TotOrd,JsonRequestBehavior.AllowGet);
        }
    }
}