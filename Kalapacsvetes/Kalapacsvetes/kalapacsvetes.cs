using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kalapacsvetes
{
    public class Sportolo
    {
        public int Helyezes { get; set; }
        public double Eredmeny { get; set; }
        public string Nev { get; set; }
        public string Orszagkod { get; set; }
        public string Helyszin { get; set; }
        public DateTime Datum { get; set; }

        public Sportolo(int helyezes, double eredmeny, string nev, string orszagkod, string helyszin, DateTime datum)
        {
            Helyezes = helyezes;
            Eredmeny = eredmeny;
            Nev = nev;
            Orszagkod = orszagkod;
            Helyszin = helyszin;
            Datum = datum;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string fajlNev = "./kalapacsvetes.txt";
            List<Sportolo> sportolok = new List<Sportolo>();

            try
            {
                var sorok = File.ReadAllLines(fajlNev);

                if (sorok.Length > 1)
                {
                    for (int i = 1; i < sorok.Length; i++) 
                    {
                        var mezok = sorok[i].Split(';');
                        if (mezok.Length == 6 && 
                            int.TryParse(mezok[0], out int helyezes) && 
                            double.TryParse(mezok[1], out double eredmeny) && 
                            DateTime.TryParse(mezok[5], out DateTime datum))
                        {
                            sportolok.Add(new Sportolo(
                                helyezes,
                                eredmeny,
                                mezok[2],
                                mezok[3],
                                mezok[4],
                                datum
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt a fájl beolvasása során: {ex.Message}");
            }

            Console.WriteLine($"A fájlban található dobások száma: {sportolok.Count}");

            var magyarSportolok = sportolok.Where(s => s.Orszagkod == "HUN").ToList();
            if ( magyarSportolok.Any())
            {
                double atlag = magyarSportolok.Average(s => s.Eredmeny);
                Console.WriteLine($"A magyar sportolók dobásainak átlaga: {atlag} m");
            }
            else 
            {
                Console.WriteLine("Nem található magyar sportoló.");
            }

            Console.Write("Adjon meg egy évszámot: ");
            if (int.TryParse(Console.ReadLine(), out int evszam))
            {
                var EviDobasok = sportolok.Where(s => s.Datum.Year == evszam).ToList();
                if(EviDobasok.Any())
                {
                    Console.WriteLine($"Az {evszam} év-ben: {EviDobasok.Count} dobás került be a legjobbak közé");
                    Console.WriteLine("Sportolók:");
                    foreach (var sportolo in EviDobasok)
                    {
                        Console.WriteLine($"{sportolo.Nev} ({sportolo.Eredmeny} m)");
                    }  
                }
                else
                {
                    Console.WriteLine($"{evszam} évben nem került be egyetlen dobás se a legjobbak közé");
                }
            }
            else {
                Console.WriteLine("Érvénytelen szám");
            }
            var orszagStatisztika = sportolok
                .GroupBy(s => s.Orszagkod)
                .Select(g => new { Orszag = g.Key, DobasokSzama = g.Count() })
                .OrderByDescending(o => o.DobasokSzama);

            Console.WriteLine("Országonkénti statisztika:");
            foreach (var stat in orszagStatisztika)
            {
                Console.WriteLine($"{stat.Orszag}: {stat.DobasokSzama} dobás");
            }
            string magyarFajlNev = "magyarok.txt";
            try
            {
                var magyarEredmenyek = magyarSportolok
                    .Select(s => $"{s.Helyezes};{s.Eredmeny};{s.Nev};{s.Orszagkod};{s.Helyszin};{s.Datum:yyyy.MM.dd}");

                File.WriteAllLines(magyarFajlNev, magyarEredmenyek);
                Console.WriteLine($"A magyar sportolók eredményei kiírva a következő fájlba: {magyarFajlNev}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt a fájl kiírása során: {ex.Message}");
            }
            Console.ReadLine();
        }
    }
}
