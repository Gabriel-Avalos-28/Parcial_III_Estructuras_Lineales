using System;

namespace Proyecto1_Clinica
{
    internal class Program
    {
        static ListaPacientes registrados = new ListaPacientes();
        static ColaPrioridad espera = new ColaPrioridad();
        static ListaPacientes historial = new ListaPacientes();

        static void Main(string[] args)
        {
            MenuPrincipal();
        }

        static void MenuPrincipal()
        {
            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("===== CLÍNICA =====");
                Console.WriteLine("1. Registrar paciente");
                Console.WriteLine("2. Eliminar paciente");
                Console.WriteLine("3. Buscar paciente");
                Console.WriteLine("4. Mostrar pacientes en espera");
                Console.WriteLine("5. Atender siguiente paciente");
                Console.WriteLine("6. Asignar prioridad");
                Console.WriteLine("7. Mostrar historial de atendidos");
                Console.WriteLine("8. Consultar pacientes en espera");
                Console.WriteLine("9. Mostrar pacientes registrados");
                Console.WriteLine("0. Salir");
                Console.Write("\nOpcion: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                    opcion = -1;

                switch (opcion)
                {
                    case 1:
                        Registrar();
                        break;

                    case 2:
                        Eliminar();
                        break;

                    case 3:
                        Buscar();
                        break;

                    case 4:
                        MostrarEspera();
                        break;

                    case 5:
                        Atender();
                        break;

                    case 6:
                        AsignarPrioridad();
                        break;

                    case 7:
                        MostrarHistorial();
                        break;

                    case 8:
                        ConsultarCantidad();
                        break;

                    case 9:
                        MostrarRegistrados();
                        break;

                    case 0:
                        Console.WriteLine("Cerrando sistema...");
                        break;

                    default:
                        Console.WriteLine("Opcion no valida.");
                        Console.ReadKey();
                        break;
                }

            } while (opcion != 0);
        }

        static void Registrar()
        {
            Console.Clear();

            Console.Write("Expediente: ");
            if (!int.TryParse(Console.ReadLine(), out int expediente) || expediente <= 0)
            {
                Console.WriteLine("Expediente invalido.");
                Console.ReadKey();
                return;
            }

            if (registrados.Buscar(expediente) != null)
            {
                Console.WriteLine("Ese expediente ya esta registrado.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Nombre invalido.");
                Console.ReadKey();
                return;
            }

            Console.Write("Edad: ");
            if (!int.TryParse(Console.ReadLine(), out int edad) || edad < 0)
            {
                Console.WriteLine("Edad invalida.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n=== PRIORIDADES ===");
            Console.WriteLine("1. Emergencia");
            Console.WriteLine("2. Urgente");
            Console.WriteLine("3. Consulta general");
            Console.WriteLine("4. Cita programada");
            Console.Write("Prioridad: ");

            if (!int.TryParse(Console.ReadLine(), out int prioridad) ||
                prioridad < 1 || prioridad > 4)
            {
                Console.WriteLine("Prioridad invalida.");
                Console.ReadKey();
                return;
            }

            Paciente nuevo = new Paciente(expediente, nombre, edad, prioridad);

            registrados.Agregar(nuevo);
            espera.Encolar(nuevo);

            Console.WriteLine("\nPaciente registrado y agregado a la lista de espera.");
            Console.ReadKey();
        }

        static void Eliminar()
        {
            Console.Clear();

            Console.Write("Expediente del paciente: ");
            if (!int.TryParse(Console.ReadLine(), out int expediente))
            {
                Console.WriteLine("Expediente invalido.");
                Console.ReadKey();
                return;
            }

            Paciente paciente = registrados.Buscar(expediente);

            if (paciente == null)
            {
                Console.WriteLine("Paciente no encontrado.");
                Console.ReadKey();
                return;
            }

            espera.Eliminar(expediente);
            registrados.Eliminar(expediente);

            Console.WriteLine("Paciente eliminado de los registros.");
            Console.ReadKey();
        }

        static void Buscar()
        {
            Console.Clear();

            Console.Write("Expediente del paciente: ");
            if (!int.TryParse(Console.ReadLine(), out int expediente))
            {
                Console.WriteLine("Expediente invalido.");
                Console.ReadKey();
                return;
            }

            Paciente paciente = registrados.Buscar(expediente);

            if (paciente != null)
            {
                Console.WriteLine("\nPaciente encontrado:");
                Console.WriteLine(paciente);

                if (espera.Buscar(expediente) != null)
                    Console.WriteLine("Estado: En espera.");
                else
                    Console.WriteLine("Estado: Ya fue atendido o fue retirado de espera.");
            }
            else
            {
                Console.WriteLine("Paciente no encontrado.");
            }

            Console.ReadKey();
        }

        static void MostrarEspera()
        {
            Console.Clear();

            Console.WriteLine("===== PACIENTES EN ESPERA =====");
            espera.Mostrar();

            Console.ReadKey();
        }

        static void Atender()
        {
            Console.Clear();

            Paciente paciente = espera.Desencolar();

            if (paciente == null)
            {
                Console.WriteLine("No hay pacientes en espera.");
            }
            else
            {
                historial.Agregar(paciente);

                Console.WriteLine("Atendiendo al siguiente paciente:");
                Console.WriteLine(paciente);
            }

            Console.ReadKey();
        }

        static void AsignarPrioridad()
        {
            Console.Clear();

            Console.Write("Expediente del paciente: ");
            if (!int.TryParse(Console.ReadLine(), out int expediente))
            {
                Console.WriteLine("Expediente invalido.");
                Console.ReadKey();
                return;
            }

            Paciente paciente = registrados.Buscar(expediente);

            if (paciente == null)
            {
                Console.WriteLine("Paciente no encontrado.");
                Console.ReadKey();
                return;
            }

            if (espera.Buscar(expediente) == null)
            {
                Console.WriteLine("El paciente no se encuentra en espera.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n=== NUEVA PRIORIDAD ===");
            Console.WriteLine("1. Emergencia");
            Console.WriteLine("2. Urgente");
            Console.WriteLine("3. Consulta general");
            Console.WriteLine("4. Cita programada");
            Console.Write("Prioridad: ");

            if (!int.TryParse(Console.ReadLine(), out int prioridad) ||
                prioridad < 1 || prioridad > 4)
            {
                Console.WriteLine("Prioridad invalida.");
                Console.ReadKey();
                return;
            }

            espera.Eliminar(expediente);
            paciente.Prioridad = prioridad;
            espera.Encolar(paciente);

            Console.WriteLine("Prioridad actualizada correctamente.");
            Console.ReadKey();
        }

        static void MostrarHistorial()
        {
            Console.Clear();

            Console.WriteLine("===== HISTORIAL DE PACIENTES ATENDIDOS =====");
            historial.Mostrar();

            Console.ReadKey();
        }

        static void ConsultarCantidad()
        {
            Console.Clear();

            Console.WriteLine("Pacientes en espera: " + espera.Cantidad());

            Console.ReadKey();
        }

        static void MostrarRegistrados()
        {
            Console.Clear();

            Console.WriteLine("===== PACIENTES REGISTRADOS =====");
            registrados.Mostrar();

            Console.ReadKey();
        }
    }
}