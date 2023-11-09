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

        // GET: SearchGpt
        public ActionResult Search()
        {
            return View();
        }

        
        public async Task<JsonResult> SearchGptAsync(string prompt)
        {
            string OutPutResult = "";
            APIAuthentication authentication = new APIAuthentication("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");
            // APIAuthentication object used to create an instance of the OpenAIAPI class
            OpenAIAPI api = new OpenAIAPI(authentication);

            //if(prompt =="Vorrei un cocktail amaro"|| prompt == "vorrei un cocktail amaro")
            //{
            //    prompt = "Amaro";
            //}else if(prompt== "Vorrei un cocktail dolce")
            //{
            //    prompt = "Dolce";

            //}
            //else if (prompt == "Vorrei un cocktail fruttato")
            //{
            //    prompt = "Fruttato";

            //}
           
            


                CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = prompt;
            completionRequest.Model = OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens= 50;
            completionRequest.Temperature= 0;
            
            
            


            var comp = await api.Completions.CreateCompletionAsync(completionRequest);
            foreach (var compl in comp.Completions)
            {
                OutPutResult += compl.Text;
            }



            string[] promp=prompt.Split(' ');
            string pr=promp.LastOrDefault();


            var prodotto = await db.Prodotti
       .Where(m => m.Gusto.Contains(pr))
       .ToListAsync();

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