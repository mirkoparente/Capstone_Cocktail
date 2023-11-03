using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ContextDbModel db = new ContextDbModel();

        public ActionResult Index()
        {
            int prod = 4;
            var prodotti = db.Prodotti.Where(p => p.Categoria.DescrizioneCategoria == "Cocktail").Take(prod).ToList();
            return View(prodotti);
        }

       
    }
}