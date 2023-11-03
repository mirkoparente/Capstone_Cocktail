namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Recensioni")]
    public partial class Recensioni
    {
        [Key]
        public int IdRecensioni { get; set; }

        public int? IdProdotti { get; set; }

        [StringLength(128)]
        public string Id { get; set; }

        public int? Valutazione { get; set; }

        public string Commento { get; set; }

        public DateTime? DataCommento { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual Prodotti Prodotti { get; set; }
    }
}
