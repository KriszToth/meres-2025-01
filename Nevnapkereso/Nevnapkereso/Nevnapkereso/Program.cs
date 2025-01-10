namespace Nevnapkereso
{
    internal static class Program
    {
        public class Fuggohid
        {
            public required string Helyezes { get; set; }
            public string Nev { get; set; }
            public string Hely { get; set; }
            public string Orszag { get; set; }
            public double Hossz { get; set; }
            public int AtadasEve { get; set; }
        }
        static void Main()
        {
            string fajlNev = "fuggohidak.csv";
            List<Fuggohid> hidak = new List<Fuggohid>();

            using (var reader = new StreamReader(fajlNev))
            {
                string sor;
                while ((sor = reader.ReadLine()) != null)
                {
                    var mezok = sor.Split('\t');
                    if (mezok.Length == 6 &&
                        double.TryParse(mezok[4], out double hossz) &&
                        int.TryParse(mezok[5], out int atadasEve))
                    {
                        hidak.Add(new Fuggohid
                        {
                            Helyezes = mezok[0],
                            Nev = mezok[1],
                            Hely = mezok[2],
                            Orszag = mezok[3],
                            Hossz = hossz,
                            AtadasEve = atadasEve
                        });
                    }
                }
            }

        }
    }
}