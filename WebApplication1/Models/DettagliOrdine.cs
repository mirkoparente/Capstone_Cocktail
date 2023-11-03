namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DettagliOrdine")]
    public partial class DettagliOrdine
    {
        [Key]
        public int IdDettagliOrdine { get; set; }

        public int IdOrdini { get; set; }

        public int IdProdotti { get; set; }

        public int? Quantita { get; set; }

        [Column(TypeName = "money")]
        public decimal? Totale { get; set; }

        public virtual Ordini Ordini { get; set; }

        public virtual Prodotti Prodotti { get; set; }
    }
}
