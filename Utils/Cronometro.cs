using System;
using System.Diagnostics;

namespace NavegadorWeb.Utils
{
    public static class Cronometro
    {
        public static void MedirTiempo(Action accion)
        {
            Stopwatch cronometro = new Stopwatch();

            cronometro.Start();

            accion();

            cronometro.Stop();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n========== TIEMPO DE EJECUCIÓN ==========");
            Console.WriteLine($"Ticks: {cronometro.ElapsedTicks}");
            Console.WriteLine($"Milisegundos: {cronometro.Elapsed.TotalMilliseconds:F6}");
            Console.WriteLine("=========================================");
            Console.ResetColor();
        }
    }
}