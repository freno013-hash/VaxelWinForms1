// ============================================================
// Namn: Fredrik Beck-Norén
// E-post: fredrikbecknoren@gmail.com
// Kurs: L0002B – Inlämningsuppgift 1 (Windows Forms)
// Datum: 28/10-2025
//
// Kort beskrivning:
// Startar Windows Forms-programmet och öppnar huvudfönstret.
// ============================================================

using System;
using System.Windows.Forms;

namespace VaxelApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Starta mitt formulär
            Application.Run(new MainForm());
        }
    }
}
