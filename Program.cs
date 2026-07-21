using System;
using NavegadorWeb.Models;
using NavegadorWeb.Utils;

namespace NavegadorWeb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Navegador navegador = new Navegador();

            bool salir = false;

            while (!salir)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===============================================");
                Console.WriteLine("   SIMULADOR DEL BOTÓN RETROCEDER DEL NAVEGADOR");
                Console.WriteLine("===============================================");
                Console.ResetColor();

                Console.WriteLine("1. Visitar una página");
                Console.WriteLine("2. Retroceder");
                Console.WriteLine("3. Mostrar página actual");
                Console.WriteLine("4. Mostrar historial");
                Console.WriteLine("5. Mostrar cantidad de páginas");
                Console.WriteLine("6. Consultar próxima página");
                Console.WriteLine("7. Medir tiempo de ejecución");
                Console.WriteLine("8. Salir");

                Console.Write("\nSeleccione una opción: ");

                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":

                        Console.Write("\nIngrese la dirección de la página: ");

                        string pagina = Console.ReadLine() ?? "";

                        navegador.VisitarPagina(pagina);

                        break;

                    case "2":

                        navegador.Retroceder();

                        break;

                    case "3":

                        navegador.MostrarPaginaActual();

                        break;

                    case "4":

                        navegador.MostrarHistorial();

                        break;

                    case "5":

                        navegador.MostrarCantidad();

                        break;

                    case "6":

                        navegador.MostrarProxima();

                        break;

                    case "7":

                        Cronometro.MedirTiempo(() =>
                        {
                            navegador.ObtenerCantidad();
                        });

                        break;

                    case "8":

                        salir = true;

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nPrograma finalizado correctamente.");
                        Console.ResetColor();

                        break;

                    default:

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOpción inválida.");
                        Console.ResetColor();

                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
    }
}
