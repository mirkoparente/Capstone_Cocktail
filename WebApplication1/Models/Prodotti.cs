namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prodotti")]
    public partial class Prodotti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prodotti()
        {
            DettagliOrdine = new HashSet<DettagliOrdine>();
            Recensioni = new HashSet<Recensioni>();
        }

        [Key]
        public int IdProdotti { get; set; }

        [Required]
        public string NomeProdotto { get; set; }

        [Required]
        public string Descrizione { get; set; }

        [Required]
        public string DescrizioneBreve { get; set; }

        [Column(TypeName = "money")]
        public decimal Prezzo { get; set; }

        [Column(TypeName = "money")]
        public decimal? PrezzoScontato { get; set; }

        public bool Disponibile { get; set; }

        public int Quantita { get; set; }

        public string FotoCopertina { get; set; }

        public string Foto1 { get; set; }

        public string Foto2 { get; set; }

        [StringLength(50)]
        public string Gusto { get; set; }

        public string Abbinamenti { get; set; }

        public string Ingredienti { get; set; }

        public int IdCategoria { get; set; }

        public virtual Categoria Categoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DettagliOrdine> DettagliOrdine { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recensioni> Recensioni { get; set; }
    }
}
