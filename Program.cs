using System;

public struct Contacto
{
    public string Telefono;
    public string Correo;
    public string Direccion;

    public Contacto(string telefono, string correo, string direccion)
    {
        Telefono = telefono;
        Correo = correo;
        Direccion = direccion;
    }
}

public record DatosPaciente(string Cedula, string Nombre, int Edad);

public class Paciente
{
    public DatosPaciente Datos { get; set; }
    public Contacto Contacto { get; set; }

    public Paciente(DatosPaciente datos, Contacto contacto)
    {
        Datos = datos;
        Contacto = contacto;
    }

    public void MostrarInformacion()
    {
        Console.WriteLine($"Cedula: {Datos.Cedula}");
        Console.WriteLine($"Nombre: {Datos.Nombre}");
        Console.WriteLine($"Edad: {Datos.Edad}");
        Console.WriteLine($"Telefono: {Contacto.Telefono}");
        Console.WriteLine($"Correo: {Contacto.Correo}");
        Console.WriteLine($"Direccion: {Contacto.Direccion}");
    }
}

public class Turno
{
    public string Dia { get; set; }
    public string Hora { get; set; }
    public string CedulaPaciente { get; set; }

    public Turno(string dia, string hora, string cedulaPaciente)
    {
        Dia = dia;
        Hora = hora;
        CedulaPaciente = cedulaPaciente;
    }

    public void MostrarTurno()
    {
        Console.WriteLine($"Dia: {Dia} | Hora: {Hora} | Cedula paciente: {CedulaPaciente}");
    }
}

public class AgendaClinica
{
    private Paciente[] pacientes = new Paciente[50];
    private string[,] turnos = new string[5, 4];
    private int totalPacientes = 0;

    private string[] dias = { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes" };
    private string[] horas = { "08:00", "09:00", "10:00", "11:00" };

    public AgendaClinica()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                turnos[i, j] = "Disponible";
            }
        }
    }

    public void RegistrarPaciente()
    {
        if (totalPacientes >= pacientes.Length)
        {
            Console.WriteLine("No se pueden registrar mas pacientes.");
            return;
        }

        Console.Write("Ingrese cedula: ");
        string cedula = Console.ReadLine() ?? "";

        Console.Write("Ingrese nombre: ");
        string nombre = Console.ReadLine() ?? "";

        Console.Write("Ingrese edad: ");
        int edad = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Ingrese telefono: ");
        string telefono = Console.ReadLine() ?? "";

        Console.Write("Ingrese correo: ");
        string correo = Console.ReadLine() ?? "";

        Console.Write("Ingrese direccion: ");
        string direccion = Console.ReadLine() ?? "";

        DatosPaciente datos = new DatosPaciente(cedula, nombre, edad);
        Contacto contacto = new Contacto(telefono, correo, direccion);

        pacientes[totalPacientes] = new Paciente(datos, contacto);
        totalPacientes++;

        Console.WriteLine("\nPaciente registrado correctamente.");
    }

    public void ListarPacientes()
    {
        Console.WriteLine("\n===== LISTA DE PACIENTES =====");

        if (totalPacientes == 0)
        {
            Console.WriteLine("No existen pacientes registrados.");
            return;
        }

        for (int i = 0; i < totalPacientes; i++)
        {
            Console.WriteLine($"\nPaciente #{i + 1}");
            pacientes[i].MostrarInformacion();
        }
    }

    public void BuscarPaciente()
    {
        Console.Write("\nIngrese la cedula del paciente: ");
        string cedula = Console.ReadLine() ?? "";

        for (int i = 0; i < totalPacientes; i++)
        {
            if (pacientes[i].Datos.Cedula == cedula)
            {
                Console.WriteLine("\nPaciente encontrado:");
                pacientes[i].MostrarInformacion();
                return;
            }
        }

        Console.WriteLine("Paciente no encontrado.");
    }

    public void AsignarTurno()
    {
        Console.Write("\nIngrese cedula del paciente: ");
        string cedula = Console.ReadLine() ?? "";

        if (!ExistePaciente(cedula))
        {
            Console.WriteLine("No se puede asignar turno porque el paciente no esta registrado.");
            return;
        }

        MostrarDiasHoras();

        Console.Write("Seleccione dia (0-4): ");
        int dia = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Seleccione hora (0-3): ");
        int hora = int.Parse(Console.ReadLine() ?? "0");

        if (dia < 0 || dia > 4 || hora < 0 || hora > 3)
        {
            Console.WriteLine("Dia u hora fuera de rango.");
            return;
        }

        if (turnos[dia, hora] != "Disponible")
        {
            Console.WriteLine("Ese turno ya esta ocupado.");
            return;
        }

        turnos[dia, hora] = cedula;
        Turno turno = new Turno(dias[dia], horas[hora], cedula);

        Console.WriteLine("\nTurno asignado correctamente:");
        turno.MostrarTurno();
    }

    public void MostrarTurnos()
    {
        Console.WriteLine("\n===== MATRIZ DE TURNOS =====");
        Console.WriteLine("Filas: dias de atencion | Columnas: horarios disponibles\n");

        Console.Write("Dia/Hora\t");

        for (int h = 0; h < horas.Length; h++)
        {
            Console.Write(horas[h] + "\t");
        }

        Console.WriteLine();

        for (int i = 0; i < dias.Length; i++)
        {
            Console.Write(dias[i] + "\t");

            for (int j = 0; j < horas.Length; j++)
            {
                Console.Write(turnos[i, j] + "\t");
            }

            Console.WriteLine();
        }
    }

    public void ReporteGeneral()
    {
        Console.WriteLine("\n===== REPORTE GENERAL DEL SISTEMA =====");
        Console.WriteLine($"Total de pacientes registrados: {totalPacientes}");
        Console.WriteLine("Estructuras aplicadas:");
        Console.WriteLine("- Vector de objetos Paciente para almacenar pacientes.");
        Console.WriteLine("- Matriz bidimensional para organizar turnos por dias y horarios.");
        Console.WriteLine("- Struct Contacto para agrupar telefono, correo y direccion.");
        Console.WriteLine("- Record DatosPaciente para almacenar cedula, nombre y edad.");
        Console.WriteLine("- Clases Paciente, Turno y AgendaClinica para aplicar POO.");
    }

    private bool ExistePaciente(string cedula)
    {
        for (int i = 0; i < totalPacientes; i++)
        {
            if (pacientes[i].Datos.Cedula == cedula)
            {
                return true;
            }
        }

        return false;
    }

    private void MostrarDiasHoras()
    {
        Console.WriteLine("\nDias disponibles:");
        for (int i = 0; i < dias.Length; i++)
        {
            Console.WriteLine($"{i}. {dias[i]}");
        }

        Console.WriteLine("\nHoras disponibles:");
        for (int i = 0; i < horas.Length; i++)
        {
            Console.WriteLine($"{i}. {horas[i]}");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        AgendaClinica agenda = new AgendaClinica();
        int opcion = 0;

        do
        {
            Console.WriteLine("\n===== AGENDA DE TURNOS DE PACIENTES =====");
            Console.WriteLine("1. Registrar paciente");
            Console.WriteLine("2. Listar pacientes");
            Console.WriteLine("3. Buscar paciente");
            Console.WriteLine("4. Asignar turno");
            Console.WriteLine("5. Mostrar matriz de turnos");
            Console.WriteLine("6. Reporte general");
            Console.WriteLine("7. Salir");
            Console.Write("Seleccione una opcion: ");

            opcion = int.Parse(Console.ReadLine() ?? "0");

            switch (opcion)
            {
                case 1:
                    agenda.RegistrarPaciente();
                    break;
                case 2:
                    agenda.ListarPacientes();
                    break;
                case 3:
                    agenda.BuscarPaciente();
                    break;
                case 4:
                    agenda.AsignarTurno();
                    break;
                case 5:
                    agenda.MostrarTurnos();
                    break;
                case 6:
                    agenda.ReporteGeneral();
                    break;
                case 7:
                    Console.WriteLine("Saliendo del sistema.");
                    break;
                default:
                    Console.WriteLine("Opcion no valida.");
                    break;
            }

        } while (opcion != 7);
    }
}