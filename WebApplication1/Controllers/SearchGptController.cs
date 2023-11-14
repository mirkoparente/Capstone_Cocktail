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
using System.Configuration;
using Microsoft.AspNet.Identity;

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

            APIAuthentication authentication = new APIAuthentication(ConfigurationManager.AppSettings["ApiKey"]);
            OpenAIAPI api = new OpenAIAPI(authentication);  


            string LogUser = User.Identity.GetUserId();
            string userg=db.AspNetUsers.Where(m=>m.Id==LogUser).Select(g=>g.GustoFavorito).FirstOrDefault();

            prompt.ToLower();

            if(prompt =="che cocktail mi consigli?"|| prompt=="consigliami un cocktail"||prompt=="che cocktail posso bere?"||prompt=="cosa posso bere?")
            {
                    prompt +=" "+ userg;
            }
            

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
            //se scrive vorrei un cocktail con la tequila prendo solo tequila
            string[] promp=prompt.Split(' ');
            string pr=promp.LastOrDefault();


            //passo l'ultima parola per cercare il prodotto in base al gusto o ingredienti
            var prodotto = await db.Prodotti
       .Where(m => m.Gusto.Contains(pr)|| m.Ingredienti.Contains(pr))
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