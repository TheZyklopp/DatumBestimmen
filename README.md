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
            // Schritt 1: Monatsnummer ermitteln
            MonatGregorianisch monatGregorianisch = (MonatGregorianisch)monat;

            // Schritt 2: Monatswert für die Berechnung (MonatJulianisch)
            MonatJulianisch monatJulianisch = ErmittleJulianischenMonat(monatGregorianisch);

            // Anpassung für Januar und Februar (Algorithmusvorgabe)
            if (monatJulianisch == MonatJulianisch.Januar || monatJulianisch == MonatJulianisch.Februar)
            {
                jahr -= 1;
            }

            // Schritt 3: Wochentag berechnen
            int m = (int)monatJulianisch;
            int k = jahr % 100; //24 letzten zahlen
            int j = jahr / 100; //20 ersten zahlen

            // Algorithmus von Georg Glaeser
            int h = (tag + ((13 * m - 1) / 5) + k + (k / 4) + (j / 4) - (2 * j)) % 7;

            // h kann negativ sein, deshalb die Anpassung für das korrekte Enum-Mapping
            if (h < 0)
            {
                h += 7;
            }

            return (Wochentag)h;
        }

        public static MonatJulianisch ErmittleJulianischenMonat(MonatGregorianisch monat)
        {
            // Mapping der gregorianischen Monate auf die julianischen
            // tauscht also die Monatsangaben von z.b. april 4 zu 2
            switch (monat)
            {
                case MonatGregorianisch.Januar: return MonatJulianisch.Januar;
                case MonatGregorianisch.Februar: return MonatJulianisch.Februar;
                case MonatGregorianisch.März: return MonatJulianisch.März;
                case MonatGregorianisch.April: return MonatJulianisch.April;
                case MonatGregorianisch.Mai: return MonatJulianisch.Mai;
                case MonatGregorianisch.Juni: return MonatJulianisch.Juni;
                case MonatGregorianisch.Juli: return MonatJulianisch.Juli;
                case MonatGregorianisch.August: return MonatJulianisch.August;
                case MonatGregorianisch.September: return MonatJulianisch.September;
                case MonatGregorianisch.Oktober: return MonatJulianisch.Oktober;
                case MonatGregorianisch.November: return MonatJulianisch.November;
                case MonatGregorianisch.Dezember: return MonatJulianisch.Dezember;
                default: throw new ArgumentOutOfRangeException(nameof(monat), "Ungültiger Monat.");
            }
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

        public enum MonatGregorianisch
        {
            Januar = 1,
            Februar = 2,
            März = 3,
            April = 4,
            Mai = 5,
            Juni = 6,
            Juli = 7,
            August = 8,
            September = 9,
            Oktober = 10,
            November = 11,
            Dezember = 12
        }

        public enum MonatJulianisch
        {
            Januar = 11,
            Februar = 12,
            März = 1,
            April = 2,
            Mai = 3,
            Juni = 4,
            Juli = 5,
            August = 6,
            September = 7,
            Oktober = 8,
            November = 9,
            Dezember = 10
        }
    }
}