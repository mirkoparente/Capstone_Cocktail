using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class OrdiniToJson
    {
        public int IdOrdini { get; set; }


        public string DataOrdine { get; set; }


        public decimal? TotaleOrdine { get; set; }


        public List<DettagliOrdineToJson> DettagliOrdine { get; internal set; }
    }
}