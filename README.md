# DatumBestimmen
````csharp
using System;
using System.Globalization;

namespace DatumBestimmen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Trage Datum ein:");
                string datum = Console.ReadLine();

                bool gueltig = IstGueltigesDatum(datum);
                if (gueltig)
                {
                    Console.WriteLine("Das Datum ist gültig.");
                    int[] a = ErmittleTagMonatJahr(datum);
                    Console.WriteLine($"Tag {a[0]}");
                    Console.WriteLine($"Monat {a[1]}");
                    Console.WriteLine($"Jahr {a[2]}");

                    Wochentag w = ErmittleWochentag(a[0], a[1], a[2]);
                    Console.WriteLine($"{w}");
                    break;
                }
                else
                {
                    Console.WriteLine("Ungültiges Datum. Nochmal versuchen? J/N");
                    char eingabe = Console.ReadKey().KeyChar;
                    Console.WriteLine(); // Für eine neue Zeile nach der Eingabe
                    if (char.ToUpper(eingabe) == 'J')
                    {
                        continue;
                    }
                    else if (char.ToUpper(eingabe) == 'N')
                    {
                        Environment.Exit(0); // Anwendung beenden
                    }
                    else
                    {
                        Console.WriteLine("unbekannte Eingabe.");
                        Environment.Exit(0);
                    }
                }
            }
        }

        public static bool IstGueltigesDatum(string datum)
        {
            DateTime dDate;
            if (DateTime.TryParseExact(datum, "dd.MM.yyyy", null, DateTimeStyles.None, out dDate))
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

            if (DateTime.TryParseExact(datum, "dd.MM.yyyy", null, DateTimeStyles.None, out dDate))
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
            if (monat < 3)
            {
                monat += 12;
                jahr -= 1;
            }

            int k = jahr % 100; // letzten zwei Ziffern des Jahres
            int j = jahr / 100; // ersten zwei Ziffern des Jahres

            // Zeller-Algorithmus
            int h = (tag + (13 * (monat + 1)) / 5 + k + (k / 4) + (j / 4) + 5 * j) % 7;

            // Anpassung für das korrekte Enum-Mapping
            return (Wochentag)((h + 6) % 7);
        }

        public enum Wochentag
        {
            Sonntag = 0,
            Montag = 1,
            Dienstag = 2,
            Mittwoch = 3,
            Donnerstag = 4,
            Freitag = 5,
            Samstag = 6
        }
    }
}