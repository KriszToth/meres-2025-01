﻿using System;
using System.Collections.Generic;
using System.IO;

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
        }
    }
}
