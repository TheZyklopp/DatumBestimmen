using System;

namespace DatumBestimmen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Trage Datum ein. DD.MM.YYYY");
                string datum = Console.ReadLine();
                bool gueltig = IstGueltigesDatum(datum);
                if (!gueltig)
                {
                    Console.WriteLine("Ungültiges Datum. Nochmal versuchen? J/N");
                    char eingabe = Console.ReadKey().KeyChar;
                    Console.WriteLine(); // Für eine neue Zeile nach der Eingabe
                    if (char.ToUpper(eingabe) == 'J')
                    {
                        continue; // Schleife von vorne beginnen
                    }
                    else if (char.ToUpper(eingabe) == 'N')
                    {
                        Environment.Exit(0); // Anwendung beenden
                    }
                    else
                    {
                        Console.WriteLine("Ungültige Eingabe. Anwendung wird beendet.");
                        Environment.Exit(0);
                    }
                }

                Console.WriteLine(gueltig);

                int[] ermittle = ErmittleTagMonatJahr(datum);
                if (ermittle != null)
                {
                    Console.WriteLine($"{ermittle[0]}.{ermittle[1]}.{ermittle[2]}");
                    Wochentag wochentag = ErmittleWochentag(ermittle[0], ermittle[1], ermittle[2]);
                    Console.WriteLine($"Der Wochentag ist: {wochentag}");
                }
                else
                {
                    Console.WriteLine("Fehler bei der Ermittlung des Datums.");
                }

                break; // Schleife beenden, wenn ein gültiges Datum eingegeben wurde
            }
        }

        // Prüft ob passendes Datum eingegeben wurde.
        public static bool IstGueltigesDatum(string datum)
        {
            DateTime dDate;

            if (DateTime.TryParse(datum, out dDate))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int[] ErmittleTagMonatJahr(string datum)
        {
            int[] a = new int[3];
            DateTime dDate;

            if (DateTime.TryParse(datum, out dDate))
            {
                a[0] = dDate.Day;
                a[1] = dDate.Month;
                a[2] = dDate.Year;
                return a;
            }
            else
            {
                return null;
            }
        }

        public static Wochentag ErmittleWochentag(int tag, int monat, int jahr)
        {
            DateTime dDate = new DateTime(jahr, monat, tag);
            return (Wochentag)dDate.DayOfWeek;
        }
    }

    public enum Wochentag
    {
        Sonntag,
        Montag,
        Dienstag,
        Mittwoch,
        Donnerstag,
        Freitag,
        Samstag
    }
}
