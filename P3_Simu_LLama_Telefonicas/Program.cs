//Funciones :
//Registrar llamada - Atender llamada - Finalizar llamada - Mostrar llamadas activas - Recuperar ultima llamada - Buscar llamada - Eliminar llamada

//Estructura :
//Main()
//MenuPrincipal()
//RegistrarCliente()
//BuscarCliente()
//Al regresar: Regresando de : BuscarCliente()
//Ahora: RegistrarCliente()


//Funciones :
//Registrar llamada - Atender llamada - Finalizar llamada - Mostrar llamadas activas - Recuperar ultima llamada - Buscar llamada - Eliminar llamada

using System;

namespace P3_Simu_LLama_Telefonicas
{
    internal class Program
    {
        static Cola espera = new Cola();
        static ListaActivas activas = new ListaActivas();
        static Pila finalizadas = new Pila();

        static int contador = 1;

        static void Main()
        {
            MenuPrincipal();
        }

        static void MenuPrincipal()
        {
            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("=== CENTRAL TELEFONICA ===");
                Console.WriteLine("1. Registrar llamada");
                Console.WriteLine("2. Atender llamada");
                Console.WriteLine("3. Finalizar llamada");
                Console.WriteLine("4. Mostrar llamadas activas");
                Console.WriteLine("5. Recuperar ultima llamada");
                Console.WriteLine("6. Buscar llamada");
                Console.WriteLine("7. Eliminar llamada");
                Console.WriteLine("8. Mostrar llamadas en espera");
                Console.WriteLine("0. Salir");
                Console.Write("\nOpcion: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                    opcion = -1;

                switch (opcion)
                {
                    case 1:
                        RegistrarCliente();
                        break;

                    case 2:
                        AtenderLlamada();
                        break;

                    case 3:
                        FinalizarLlamada();
                        break;

                    case 4:
                        MostrarLlamadasActivas();
                        break;

                    case 5:
                        RecuperarUltimaLlamada();
                        break;

                    case 6:
                        BuscarCliente();
                        break;

                    case 7:
                        EliminarLlamada();
                        break;

                    case 8:
                        MostrarEspera();
                        break;

                    case 0:
                        Console.WriteLine("Cerrando central telefonica...");
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

        static void RegistrarCliente()
        {
            Console.Write("Nombre del cliente: ");
            string nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Nombre no valido.");
                return;
            }

            Console.Write("Numero celular: ");
            string celular = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(celular))
            {
                Console.WriteLine("Numero no valido.");
                return;
            }

            Contacto contacto = new Contacto(contador, nombre, celular);

            espera.Encolar(contacto);

            Console.WriteLine($"\nLlamada #{contador} registrada correctamente.");

            contador++;
        }

        static void AtenderLlamada()
        {
            Contacto contacto = espera.Desencolar();

            if (contacto == null)
            {
                Console.WriteLine("No hay llamadas en espera.");
                return;
            }

            activas.Agregar(contacto);

            Console.WriteLine("Llamada atendida.");
            Console.WriteLine(contacto);
        }

        static void FinalizarLlamada()
        {
            Console.Write("Numero de llamada: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Identificador no valido.");
                return;
            }

            Contacto contacto = activas.Eliminar(id);

            if (contacto == null)
            {
                Console.WriteLine("No se encontro una llamada activa con ese numero.");
                return;
            }

            finalizadas.Apilar(contacto);

            Console.WriteLine("Llamada finalizada.");
            Console.WriteLine(contacto);
        }

        static void MostrarLlamadasActivas()
        {
            activas.Mostrar();
        }

        static void RecuperarUltimaLlamada()
        {
            Contacto contacto = finalizadas.Desapilar();

            if (contacto == null)
            {
                Console.WriteLine("No hay llamadas finalizadas para recuperar.");
                return;
            }

            activas.Agregar(contacto);

            Console.WriteLine("Ultima llamada recuperada.");
            Console.WriteLine(contacto);
        }

        static void BuscarCliente()
        {
            Console.Write("Numero de llamada: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Identificador no valido.");
                return;
            }

            Contacto contacto = espera.Buscar(id);

            if (contacto != null)
            {
                Console.WriteLine("Llamada encontrada: EN ESPERA");
                Console.WriteLine(contacto);
                return;
            }

            contacto = activas.Buscar(id);

            if (contacto != null)
            {
                Console.WriteLine("Llamada encontrada: ACTIVA");
                Console.WriteLine(contacto);
                return;
            }

            contacto = finalizadas.Buscar(id);

            if (contacto != null)
            {
                Console.WriteLine("Llamada encontrada: FINALIZADA");
                Console.WriteLine(contacto);
                return;
            }

            Console.WriteLine("No se encontro la llamada.");
        }

        static void EliminarLlamada()
        {
            Console.Write("Numero de llamada: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Identificador no valido.");
                return;
            }

            if (espera.Eliminar(id))
            {
                Console.WriteLine("Llamada eliminada de la lista de espera.");
                return;
            }

            Contacto contacto = activas.Eliminar(id);

            if (contacto != null)
            {
                Console.WriteLine("Llamada eliminada de las llamadas activas.");
                return;
            }

            if (finalizadas.Eliminar(id))
            {
                Console.WriteLine("Llamada eliminada del historial.");
                return;
            }

            Console.WriteLine("No se encontro la llamada.");
        }

        static void MostrarEspera()
        {
            espera.Mostrar();
        }
    }
}
