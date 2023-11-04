using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UtentiController : Controller
    {
        private ContextDbModel db = new ContextDbModel();

        // GET: Utenti
        public ActionResult ListaUtenti()
        {
            var utenti = db.AspNetUsers.Where(m=>m.AspNetRoles.Any(i=>i.Name=="User")) ;
            return View(utenti.ToList());
        }
    }
}