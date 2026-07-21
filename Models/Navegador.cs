using System;
using System.Collections.Generic;

namespace NavegadorWeb.Models
{
    public class Navegador
    {
        private readonly Stack<string> historial;
        private string? paginaActual;

        public Navegador()
        {
            historial = new Stack<string>();
            paginaActual = null;
        }

        public void VisitarPagina(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Debe ingresar una dirección válida.");
                Console.ResetColor();
                return;
            }

            if (paginaActual != null)
            {
                historial.Push(paginaActual);
            }

            paginaActual = url;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nAhora estás en: {paginaActual}");
            Console.ResetColor();
        }

        public void Retroceder()
        {
            if (historial.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nNo existen páginas anteriores.");
                Console.ResetColor();
                return;
            }

            paginaActual = historial.Pop();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nSe regresó a: {paginaActual}");
            Console.ResetColor();
        }

        public void MostrarPaginaActual()
        {
            if (paginaActual == null)
            {
                Console.WriteLine("\nNo existe una página abierta.");
            }
            else
            {
                Console.WriteLine($"\nPágina actual: {paginaActual}");
            }
        }

        public void MostrarHistorial()
        {
            Console.WriteLine("\n===== HISTORIAL =====");

            if (historial.Count == 0)
            {
                Console.WriteLine("No existen páginas almacenadas.");
            }
            else
            {
                foreach (string pagina in historial)
                {
                    Console.WriteLine(pagina);
                }
            }

            Console.WriteLine("=====================");
        }

        public void MostrarCantidad()
        {
            Console.WriteLine($"\nCantidad de páginas almacenadas: {historial.Count}");
        }

        public void MostrarProxima()
        {
            if (historial.Count == 0)
            {
                Console.WriteLine("\nNo existe una página para retroceder.");
            }
            else
            {
                Console.WriteLine($"\nPróxima página: {historial.Peek()}");
            }
        }

        public int ObtenerCantidad()
        {
            return historial.Count;
        }
    }
}