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

        //carico i 4 prodotti per bestseller
        public ActionResult Index()
        {
            
            var prodotti = db.Prodotti.Where(p => p.Categoria.DescrizioneCategoria == "Cocktail").Take(4).ToList();
            return View(prodotti);
        }

       
    }
}