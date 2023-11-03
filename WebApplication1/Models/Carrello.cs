using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Carrello
    {


        public int IdProdotti { get; set; }


        public string NomeProdotto { get; set; }

        public string Descrizione { get; set; }

        public int QuantitaProdotto { get; set; }

        public decimal Prezzo { get; set; }


        public int IdCategoria { get; set; }


        public int QuantitaAcquistata { get; set; }

        public string FotoCopertina { get; set; }

        public Carrello() { }
        public Carrello(int idProdotti, string nomeProdotto, string descrizione, int quantitaProdotto, decimal prezzo, int idCategoria, int quantitaAcquistata, string fotoCopertina)
        {
            IdProdotti = idProdotti;
            NomeProdotto = nomeProdotto;
            Descrizione = descrizione;
            QuantitaProdotto = quantitaProdotto;
            Prezzo = prezzo;
            IdCategoria = idCategoria;
            QuantitaAcquistata = quantitaAcquistata;
            FotoCopertina = fotoCopertina;
        }

    }
}