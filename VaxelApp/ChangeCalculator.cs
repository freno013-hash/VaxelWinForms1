// ============================================================
// Namn: Fredrik Beck-Norén
// E-post: fredrikbecknoren@gmail.com
// Kurs: L0002B – Inlämningsuppgift 1 (Windows Forms)
// Datum: 28/10-2025
//
// Kort beskrivning:
// Själva växellogiken. Jag håller mig till heltal i kronor.
// 1) Räkna ut hur mycket växel (betalt, pris).
// 2) Gå igenom valörer från störst till minst.
// 3) Lägg bara till rader där antal > 0 (inga nollrader).
// ============================================================

using System;
using System.Collections.Generic;

namespace VaxelApp
{
    public static class ChangeCalculator
    {
        // Jag använder en enkel lista med (etikett, värde).
        // Etiketten används i utskriften.
        private static readonly List<(string label, int värde)> Valörer = new()
        {
            ("femhundralapp", 500),
            ("tvåhundralapp", 200),
            ("hundralapp",    100),
            ("femtiolapp",      50),
            ("tjuga",           20),
            ("tiokrona",        10),
            ("femkrona",         5),
            ("enkrona",          1)
        };

        // Liten uppslagsbok för plural (räcker för uppgiften)
        private static readonly Dictionary<string, string> Plural = new()
        {
            ["femhundralapp"] = "femhundralappar",
            ["tvåhundralapp"] = "tvåhundralappar",
            ["hundralapp"]    = "hundralappar",
            ["femtiolapp"]    = "femtiolappar",
            ["tjuga"]         = "tjugor",
            ["tiokrona"]      = "tiokronor",
            ["femkrona"]      = "femkronor",
            ["enkrona"]       = "enkronor"
        };

        /// Beräknar växel och returnerar total växel samt en lista med utskriftsrader.
        /// Kastar ArgumentException om betalt < pris eller om något oväntat.
      
        public static (int totalVäxel, List<string> rader) BeräknaVäxel(int pris, int betalt)
        {
            if (pris < 0 || betalt < 0)
                throw new ArgumentException("Pris och betalt måste vara 0 eller större.");
            if (betalt < pris)
                throw new ArgumentException($"Kunden har betalat för lite ({betalt - pris} kr).");

            int rest = betalt - pris; // hur mycket växel vi ska dela upp
            var resultat = new List<string>();

            // Gå igenom alla valörer från störst till minst
            foreach (var (etikett, värde) in Valörer)
            {
                // Hur många av den här valören går det in?
                int antal = rest / värde;

                // Om 0, hoppa över (vi skriver inte ut nollor)
                if (antal <= 0)
                    continue;

                // Välj singular/plural på ett enkelt sätt
                string ord = antal == 1 ? etikett : Plural[etikett];

                // Spara raden som text, t.ex. "2 hundralappar"
                resultat.Add($"{antal} {ord}");

                // Minska återstående växel
                rest -= antal * värde;
            }

            // Skulle rest bli något annat än 0 här är något konstigt,
            // men med dessa heltalsvalörer ska det alltid bli 0.
            return (betalt - pris, resultat);
        }
    }
}
