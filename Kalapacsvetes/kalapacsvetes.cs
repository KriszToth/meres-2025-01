
using System;

namespace Kalapacsvetes
{
    public class Sportolo {
        public string Helyezes { get; set; }
        public string Eredmeny { get; set; }
        public string Sportolo { get; set; }
        public string Országkod { get; set; }
        public string Helyszin { get; set; }
        public string Datum { get; set; }
    }

    public Sportolo(string Helyezes, string Eredmeny, string Sportolo, string Országkod, string Helyszin, string Datum)
        {
            Helyezes = Helyezes;
            Eredmeny = Eredmeny;
            Sportolo = Sportolo;
            Országkod = Országkod;
            Helyszin = Helyszin;
            Datum = Datum;
        }

    
}