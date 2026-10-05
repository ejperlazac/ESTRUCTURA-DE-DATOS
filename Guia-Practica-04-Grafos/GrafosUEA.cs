using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

class Grafo
{
    private Dictionary<string, List<string>> adyacencia =
        new Dictionary<string, List<string>>();

    public void CargarDesdeArchivo(string ruta)
    {
        string[] lineas = File.ReadAllLines(ruta);

        foreach (string linea in lineas)
        {
            if (string.IsNullOrWhiteSpace(linea))
                continue;

            string[] partes = linea.Split(',');

            if (partes.Length != 2)
                continue;

            string origen = partes[0].Trim();
            string destino = partes[1].Trim();

            AgregarArista(origen, destino);
        }
    }

    private void AgregarArista(string origen, string destino)
    {
        if (!adyacencia.ContainsKey(origen))
            adyacencia[origen] = new List<string>();

        if (!adyacencia.ContainsKey(destino))
            adyacencia[destino] = new List<string>();

        if (!adyacencia[origen].Contains(destino))
            adyacencia[origen].Add(destino);

        if (!adyacencia[destino].Contains(origen))
            adyacencia[destino].Add(origen);
    }

    public void MostrarGrafo()
    {
        Console.WriteLine("\nLISTA DE ADYACENCIA");

        foreach (var vertice in adyacencia)
        {
            Console.Write(vertice.Key + " -> ");
            Console.WriteLine(string.Join(", ", vertice.Value));
        }
    }

    public void MostrarReporte()
    {
        int aristas = 0;

        foreach (var vertice in adyacencia)
            aristas += vertice.Value.Count;

        aristas /= 2;

        Console.WriteLine("\nREPORTE DEL GRAFO");
        Console.WriteLine("Número de vértices: " + adyacencia.Count);
        Console.WriteLine("Número de aristas: " + aristas);
    }

    public void BFS(string inicio)
    {
        if (!adyacencia.ContainsKey(inicio))
            return;

        HashSet<string> visitados = new HashSet<string>();
        Queue<string> cola = new Queue<string>();

        visitados.Add(inicio);
        cola.Enqueue(inicio);

        Console.Write("\nRecorrido BFS: ");

        while (cola.Count > 0)
        {
            string actual = cola.Dequeue();
            Console.Write(actual + " ");

            foreach (string vecino in adyacencia[actual])
            {
                if (!visitados.Contains(vecino))
                {
                    visitados.Add(vecino);
                    cola.Enqueue(vecino);
                }
            }
        }

        Console.WriteLine();
    }

    public void DFS(string inicio)
    {
        HashSet<string> visitados = new HashSet<string>();

        Console.Write("Recorrido DFS: ");
        DFSRecursivo(inicio, visitados);
        Console.WriteLine();
    }

    private void DFSRecursivo(string actual, HashSet<string> visitados)
    {
        if (!adyacencia.ContainsKey(actual) || visitados.Contains(actual))
            return;

        visitados.Add(actual);
        Console.Write(actual + " ");

        foreach (string vecino in adyacencia[actual])
        {
            DFSRecursivo(vecino, visitados);
        }
    }

    public void BuscarCamino(string origen, string destino)
    {
        if (!adyacencia.ContainsKey(origen) ||
            !adyacencia.ContainsKey(destino))
        {
            Console.WriteLine("No existen los vértices indicados.");
            return;
        }

        Queue<string> cola = new Queue<string>();
        HashSet<string> visitados = new HashSet<string>();
        Dictionary<string, string> anterior =
            new Dictionary<string, string>();

        cola.Enqueue(origen);
        visitados.Add(origen);

        bool encontrado = false;

        while (cola.Count > 0)
        {
            string actual = cola.Dequeue();

            if (actual == destino)
            {
                encontrado = true;
                break;
            }

            foreach (string vecino in adyacencia[actual])
            {
                if (!visitados.Contains(vecino))
                {
                    visitados.Add(vecino);
                    anterior[vecino] = actual;
                    cola.Enqueue(vecino);
                }
            }
        }

        if (!encontrado)
        {
            Console.WriteLine("No existe un camino entre los vértices.");
            return;
        }

        List<string> camino = new List<string>();
        string nodo = destino;

        while (nodo != origen)
        {
            camino.Add(nodo);
            nodo = anterior[nodo];
        }

        camino.Add(origen);
        camino.Reverse();

        Console.WriteLine(
            "Camino encontrado: " + string.Join(" -> ", camino)
        );
    }
}

class Program
{
    static void Main()
    {
        Stopwatch tiempoTotal = Stopwatch.StartNew();

        Console.WriteLine("==========================================");
        Console.WriteLine("   REPRESENTACIÓN Y RECORRIDO DE GRAFOS");
        Console.WriteLine("==========================================");

        Console.WriteLine("\nEJEMPLO 1 - RED DE RUTAS ENTRE CIUDADES");

        Stopwatch tiempo1 = Stopwatch.StartNew();

        Grafo grafo1 = new Grafo();
        grafo1.CargarDesdeArchivo("/uploads/grafo1.txt");
        grafo1.MostrarGrafo();
        grafo1.MostrarReporte();
        grafo1.BFS("Quito");
        grafo1.DFS("Quito");
        grafo1.BuscarCamino("Quito", "Loja");

        tiempo1.Stop();

        Console.WriteLine(
            "Tiempo de ejecución del Grafo 1: " +
            tiempo1.Elapsed.TotalMilliseconds.ToString("F4") +
            " ms"
        );

        Console.WriteLine("\n==========================================");

        Console.WriteLine("\nEJEMPLO 2 - RED DE CONEXIONES INFORMÁTICAS");

        Stopwatch tiempo2 = Stopwatch.StartNew();

        Grafo grafo2 = new Grafo();
        grafo2.CargarDesdeArchivo("/uploads/grafo2.txt");
        grafo2.MostrarGrafo();
        grafo2.MostrarReporte();
        grafo2.BFS("ServidorA");
        grafo2.DFS("ServidorA");
        grafo2.BuscarCamino("ServidorA", "ServidorF");

        tiempo2.Stop();

        Console.WriteLine(
            "Tiempo de ejecución del Grafo 2: " +
            tiempo2.Elapsed.TotalMilliseconds.ToString("F4") +
            " ms"
        );

        tiempoTotal.Stop();

        Console.WriteLine("\n==========================================");
        Console.WriteLine(
            "Tiempo total de ejecución: " +
            tiempoTotal.Elapsed.TotalMilliseconds.ToString("F4") +
            " ms"
        );
        Console.WriteLine("==========================================");
    }
}