// ============================================================
// Namn: Fredrik Beck-Norén
// E-post: fredrikbecknoren@gmail.com
// Kurs: L0002B – Inlämningsuppgift 1 (Windows Forms)
// Datum: 28/10-2025
//
// Kort beskrivning:
// En enkel form med två inmatningsfält (pris och betalt),
// en knapp för beräkning, en knapp för att avsluta, och en lista
// som visar hur växeln ska lämnas i valörer.
// ============================================================

using System;
using System.Drawing;
using System.Windows.Forms;

namespace VaxelApp
{
    public class MainForm : Form
    {
        // Etiketter och fält
        private readonly Label lblPris = new() { Text = "Ange pris (kr):" };
        private readonly TextBox txtPris = new();
        private readonly Label lblBetalt = new() { Text = "Betalt (kr):" };
        private readonly TextBox txtBetalt = new();

        // Knappar
        private readonly Button btnBerakna = new() { Text = "Beräkna" };
        private readonly Button btnAvsluta = new() { Text = "Avsluta" };

        // Resultatvisning
        private readonly Label lblResult = new() { Text = "Växel tillbaka:", Font = new Font("Segoe UI", 10, FontStyle.Bold) };
        private readonly ListBox lstResultat = new();

        public MainForm()
        {
            Text = "Växelräknare (Windows Forms)";
            // Lagom storlek för en enkel uppgift
            ClientSize = new Size(420, 360);
            StartPosition = FormStartPosition.CenterScreen;

            lblPris.SetBounds(20, 20, 140, 28);
            txtPris.SetBounds(180, 20, 200, 28);

            lblBetalt.SetBounds(20, 60, 140, 28);
            txtBetalt.SetBounds(180, 60, 200, 28);

            btnBerakna.SetBounds(180, 100, 95, 32);
            btnAvsluta.SetBounds(285, 100, 95, 32);

            lblResult.SetBounds(20, 150, 200, 28);
            lstResultat.SetBounds(20, 180, 360, 150);

            Controls.AddRange(new Control[]
            {
                lblPris, txtPris, lblBetalt, txtBetalt, btnBerakna, btnAvsluta, lblResult, lstResultat
            });

            // Koppla knapparna
            btnBerakna.Click += OnBerakna;
            btnAvsluta.Click += (_, __) => Close(); // Uppgiftens "Avsluta"-knapp

            // Små tips i fälten (heltal i kronor)
            txtPris.PlaceholderText = "t.ex. 152";
            txtBetalt.PlaceholderText = "t.ex. 500";
        }

        private void OnBerakna(object? sender, EventArgs e)
        {
            lstResultat.Items.Clear();

            // Validera att det är heltal (uppgiften kör med kronor, inte ören)
            if (!int.TryParse(txtPris.Text.Trim(), out int pris) ||
                !int.TryParse(txtBetalt.Text.Trim(), out int betalt))
            {
                MessageBox.Show("Skriv heltal i båda fälten (kronor).", "Felaktig inmatning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Använder min separata klass för logiken (enkelt att testa)
                var (total, rader) = ChangeCalculator.BeräknaVäxel(pris, betalt);

                if (total == 0)
                {
                    lstResultat.Items.Add("Ingen växel.");
                    return;
                }

                lstResultat.Items.Add($"Totalt: {total} kr");

                // Visa bara de rader som faktiskt används (inga nollor)
                foreach (var rad in rader)
                    lstResultat.Items.Add(rad);
            }
            catch (ArgumentException ex)
            {
                // Fångar t.ex. “betalat för lite”
                MessageBox.Show(ex.Message, "Obs", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
