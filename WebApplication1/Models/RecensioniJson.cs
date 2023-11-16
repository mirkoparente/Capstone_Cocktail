using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class RecensioniJson
    {
        public int IdRecensioni { get; set; }

        public int? IdProdotti { get; set; }

        [StringLength(128)]
        public string Id { get; set; }


        [Range(1, 5, ErrorMessage = "La valutazione deve essere compresa tra 1 e 5.")]
        public int? Valutazione { get; set; }

        public string Commento { get; set; }

        public DateTime? DataCommento { get; set; }
    }
}