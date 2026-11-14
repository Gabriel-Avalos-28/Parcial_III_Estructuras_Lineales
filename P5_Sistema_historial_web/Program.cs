//Crear un simulador web simulado, el sistema debera almacenar las paginas visitadas y permitir: pagina actual --> historial
//Funciones: Visitar pagina, Regresar, Avanzar, Mostrar historial eliminar historial, buscar pagina, mostrar ultima pagina visitada
//Ejemplo: Google --> Wikipedia --> Youtube --> Github (Al presionar atras) --> Youtube --> wikipedia
//Extension profesional: Pila Atra y adelante


using P5_Sistema_historial_web;

namespace P5_Sistema_historialweb
{
    internal class Program
    {
        static Pila atras = new Pila();
        static Pila adelante = new Pila();
        static Historial historial = new Historial();

        static string actual = "Inicio";

        static void Main()
        {
            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("=== NAVEGADOR WEB ===");
                Console.WriteLine($"Pagina actual: {actual}");
                Console.WriteLine("\n1. Visitar pagina");
                Console.WriteLine("2. Atras");
                Console.WriteLine("3. Adelante");
                Console.WriteLine("4. Mostrar historial");
                Console.WriteLine("5. Limpiar historial");
                Console.WriteLine("6. Buscar pagina");
                Console.WriteLine("7. Ultima pagina visitada");
                Console.WriteLine("0. Salir");
                Console.Write("\nOpcion: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                    opcion = -1;

                switch (opcion)
                {
                    case 1:
                        Console.Write("Nueva pagina: ");
                        string nueva = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(nueva))
                        {
                            atras.Push(actual);
                            actual = nueva;

                            adelante.Limpiar();
                            historial.Agregar(actual);

                            Console.WriteLine("Pagina visitada.");
                        }
                        break;

                    case 2:
                        string anterior = atras.Pop();

                        if (anterior != null)
                        {
                            adelante.Push(actual);
                            actual = anterior;
                        }
                        else
                        {
                            Console.WriteLine("No hay paginas anteriores.");
                        }
                        break;

                    case 3:
                        string siguiente = adelante.Pop();

                        if (siguiente != null)
                        {
                            atras.Push(actual);
                            actual = siguiente;
                        }
                        else
                        {
                            Console.WriteLine("No hay paginas siguientes.");
                        }
                        break;

                    case 4:
                        historial.Mostrar();
                        break;

                    case 5:
                        historial.Limpiar();
                        break;

                    case 6:
                        Console.Write("Pagina a buscar: ");
                        string busqueda = Console.ReadLine();

                        historial.Buscar(busqueda);
                        break;

                    case 7:
                        string ultima = historial.UltimaPagina();

                        if (ultima != null)
                            Console.WriteLine("Ultima pagina visitada: " + ultima);
                        else
                            Console.WriteLine("El historial esta vacio.");
                        break;

                    case 0:
                        Console.WriteLine("Cerrando navegador...");
                        break;

                    default:
                        Console.WriteLine("Opcion no valida.");
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresiona una tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 0);
        }
    }
}
