//Funciones:
//Registrar cliente - Registrar numero de personas - Agregar un cliente a la lista de espera
//Asignar mesa - Cancelar reservacion - Buscar cliente - Eliminar cliente - Mostrar lista de espera
//Mostrar mesas disponibles - Mostrar mesas ocupadas

//Ejemplo:
// Lista de espera
//1. Familia Lopez - 4 personas / 2. Ana Martinez - 2 personas / 3. Grupo Perez - 6 personas
//4. Carlos Ramirez - 3 personas
//Cuando una mesa queda disponible: Mesa 4 disponible 
//Atendiendo: Familia Lopez - 4 personas





using System;
using P2_Restaurante;

namespace Proyecto2_Restaurante
{
    internal class Program
    {
        static Cola espera = new Cola();
        static Mesa[] mesas = new Mesa[15];

        static void CrearMesas()
        {
            for (int i = 0; i < mesas.Length; i++)
            {
                mesas[i] = new Mesa(i + 1);
            }
        }

        static void Main()
        {
            CrearMesas();

            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("=== RESTAURANTE ===");
                Console.WriteLine("1. Agregar cliente");
                Console.WriteLine("2. Asignar mesa");
                Console.WriteLine("3. Cancelar reservacion");
                Console.WriteLine("4. Buscar cliente");
                Console.WriteLine("5. Eliminar cliente");
                Console.WriteLine("6. Mostrar espera");
                Console.WriteLine("7. Mostrar mesas disponibles");
                Console.WriteLine("8. Mostrar mesas ocupadas");
                Console.WriteLine("0. Salir");
                Console.Write("Opcion: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                    opcion = -1;

                if (opcion != 0)
                {
                    Console.Clear();

                    switch (opcion)
                    {
                        case 1:
                            Agregar();
                            break;

                        case 2:
                            AsignarMesa();
                            break;

                        case 3:
                            CancelarReservacion();
                            break;

                        case 4:
                            BuscarCliente();
                            break;

                        case 5:
                            EliminarCliente();
                            break;

                        case 6:
                            espera.Mostrar();
                            break;

                        case 7:
                            MostrarDisponibles();
                            break;

                        case 8:
                            MostrarOcupadas();
                            break;

                        default:
                            Console.WriteLine("Opcion no valida.");
                            break;
                    }

                    Console.WriteLine("\nPresiona una tecla para regresar al menu...");
                    Console.ReadKey();
                }

            } while (opcion != 0);

            Console.Clear();
            Console.WriteLine("Cerrando restaurante...");
        }

        static void Agregar()
        {
            Console.WriteLine("=== AGREGAR CLIENTE ===");

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Nombre no valido.");
                return;
            }

            if (espera.Buscar(nombre) != null)
            {
                Console.WriteLine("Ya existe un cliente con ese nombre en espera.");
                return;
            }

            foreach (Mesa mesa in mesas)
            {
                if (!mesa.Disponible &&
                    mesa.Ocupando.Nombre.Equals(
                        nombre, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Ese cliente ya tiene una mesa asignada.");
                    return;
                }
            }

            Console.Write("Numero de personas: ");

            if (!int.TryParse(Console.ReadLine(), out int personas))
            {
                Console.WriteLine("Numero de personas no valido.");
                return;
            }

            if (personas < 1 || personas > 6)
            {
                Console.WriteLine("Numero de personas invalido (1-6).");
                return;
            }

            espera.Encolar(new Cliente(nombre, personas));

            Console.WriteLine("Cliente agregado a la lista de espera.");
        }

        static void AsignarMesa()
        {
            Console.WriteLine("=== ASIGNAR MESA ===");

            Cliente cliente = espera.VerFrente();

            if (cliente == null)
            {
                Console.WriteLine("No hay clientes en espera.");
                return;
            }

            for (int i = 0; i < mesas.Length; i++)
            {
                if (mesas[i].Disponible &&
                    mesas[i].Capacidad >= cliente.Personas)
                {
                    AsignarSiguiente(mesas[i]);
                    return;
                }
            }

            Console.WriteLine("No hay mesas disponibles con capacidad suficiente.");
        }

        static void AsignarSiguiente(Mesa mesa)
        {
            Cliente cliente = espera.VerFrente();

            if (cliente == null)
            {
                Console.WriteLine($"Mesa {mesa.Numero} disponible.");
                return;
            }

            mesa.Asignar(cliente);
            espera.Desencolar();

            Console.WriteLine($"Mesa {mesa.Numero} asignada.");
            Console.WriteLine($"Atendiendo: {cliente}");
        }

        static void CancelarReservacion()
        {
            Console.WriteLine("=== CANCELAR RESERVACION ===");

            Console.Write("Nombre del cliente: ");
            string nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Nombre no valido.");
                return;
            }

            for (int i = 0; i < mesas.Length; i++)
            {
                if (!mesas[i].Disponible &&
                    mesas[i].Ocupando.Nombre.Equals(
                        nombre, StringComparison.OrdinalIgnoreCase))
                {
                    mesas[i].Liberar();

                    Console.WriteLine($"Reservacion de {nombre} cancelada.");

                    AsignarSiguiente(mesas[i]);
                    return;
                }
            }

            if (espera.Eliminar(nombre))
            {
                Console.WriteLine("Cliente eliminado de la lista de espera.");
                return;
            }

            Console.WriteLine("No se encontro al cliente.");
        }

        static void BuscarCliente()
        {
            Console.WriteLine("=== BUSCAR CLIENTE ===");

            Console.Write("Nombre del cliente: ");
            string nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Nombre no valido.");
                return;
            }

            Cliente cliente = espera.Buscar(nombre);

            if (cliente != null)
            {
                Console.WriteLine("Cliente encontrado en lista de espera:");
                Console.WriteLine(cliente);
                return;
            }

            for (int i = 0; i < mesas.Length; i++)
            {
                if (!mesas[i].Disponible &&
                    mesas[i].Ocupando.Nombre.Equals(
                        nombre, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Cliente encontrado.");
                    Console.WriteLine($"Mesa: {mesas[i].Numero}");
                    Console.WriteLine(mesas[i].Ocupando);
                    return;
                }
            }

            Console.WriteLine("Cliente no encontrado.");
        }

        static void EliminarCliente()
        {
            Console.WriteLine("=== ELIMINAR CLIENTE ===");

            Console.Write("Nombre del cliente: ");
            string nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Nombre no valido.");
                return;
            }

            if (espera.Eliminar(nombre))
                Console.WriteLine("Cliente eliminado de la lista de espera.");
            else
                Console.WriteLine("Cliente no encontrado en la lista de espera.");
        }

        static void MostrarDisponibles()
        {
            Console.WriteLine("=== MESAS DISPONIBLES ===");

            bool hayMesas = false;

            foreach (Mesa mesa in mesas)
            {
                if (mesa.Disponible)
                {
                    Console.WriteLine(
                        $"Mesa {mesa.Numero} - Capacidad: {mesa.Capacidad}");

                    hayMesas = true;
                }
            }

            if (!hayMesas)
                Console.WriteLine("No hay mesas disponibles.");
        }

        static void MostrarOcupadas()
        {
            Console.WriteLine("=== MESAS OCUPADAS ===");

            bool hayMesas = false;

            foreach (Mesa mesa in mesas)
            {
                if (!mesa.Disponible)
                {
                    Console.WriteLine(
                        $"Mesa {mesa.Numero} - Atendiendo: {mesa.Ocupando}");

                    hayMesas = true;
                }
            }

            if (!hayMesas)
                Console.WriteLine("No hay mesas ocupadas.");
        }
    }
}
