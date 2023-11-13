using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using OpenAI_API.Completions;
using WebApplication1.Models;
using System.Data.Entity;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Controllers
{

    public class SearchGptController : Controller
    {
        private ContextDbModel db = new ContextDbModel();

        // view SearchGpt
        public ActionResult Search()
        {
            return View();
        }

        
        //chiamata all api di chat gpt
        public async Task<JsonResult> SearchGptAsync(string prompt)
        {
            string OutPutResult = "";

            APIAuthentication authentication = new APIAuthentication("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");
            OpenAIAPI api = new OpenAIAPI(authentication);  


                CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = prompt;
            completionRequest.Model = OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 100;
            completionRequest.Temperature= 0.7;
            
            
            


            var comp = await api.Completions.CreateCompletionAsync(completionRequest);
            foreach (var compl in comp.Completions)
            {
                OutPutResult += compl.Text;
            }


            //divido la stringa per ottenere l'ultma parola da passare come ricerca
            //se l'utente scrive "vorrei un cocktail amaro" prendo solo amaro come criterio
            string[] promp=prompt.Split(' ');
            string pr=promp.LastOrDefault();


            //passo l'ultima parola per cercare il prodotto in base al gusto
            var prodotto = await db.Prodotti
       .Where(m => m.Gusto.Contains(pr))
       .ToListAsync();

            //creo una lista prodotti
            List<ProdottiGpt>pro= new List<ProdottiGpt>();
            foreach(var i in  prodotto)
            {
            ProdottiGpt p= new ProdottiGpt();
                p.NomeProdotto = i.NomeProdotto;
                p.IdProdotti = i.IdProdotti;
                p.FotoCopertina=i.FotoCopertina;
                p.Prezzo = i.Prezzo;
                pro.Add(p);
            }

            var result = new
            {
                res = OutPutResult,
                prodotti = pro
            };

            return Json(result,JsonRequestBehavior.AllowGet);

        }

    

    }
}