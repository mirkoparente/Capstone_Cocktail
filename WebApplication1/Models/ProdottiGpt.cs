using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ProdottiGpt
    {
        public int IdProdotti { get; set; }

        [Required]
        public string NomeProdotto { get; set; }


        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:f3}", ApplyFormatInEditMode = true)]
        public decimal Prezzo { get; set; }
        public string FotoCopertina { get; set; }
        public bool Disponibile { get; set; }


    }
}