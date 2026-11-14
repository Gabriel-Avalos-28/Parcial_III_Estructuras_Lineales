using System;

namespace P2_Restaurante
{
    public class Cola
    {
        private Nodo frente;
        private Nodo final;

        public void Encolar(Cliente cliente)
        {
            Nodo nuevo = new Nodo(cliente);

            if (frente == null)
            {
                frente = nuevo;
                final = nuevo;
            }
            else
            {
                final.Siguiente = nuevo;
                final = nuevo;
            }
        }

        public Cliente Desencolar()
        {
            if (frente == null)
                return null;

            Cliente cliente = frente.Datos;
            frente = frente.Siguiente;

            if (frente == null)
                final = null;

            return cliente;
        }

        public Cliente VerFrente()
        {
            if (frente == null)
                return null;

            return frente.Datos;
        }

        public void Mostrar()
        {
            if (frente == null)
            {
                Console.WriteLine("No hay clientes en espera.");
                return;
            }

            Nodo actual = frente;
            int contador = 1;

            Console.WriteLine("=== LISTA DE ESPERA ===");

            while (actual != null)
            {
                Console.WriteLine($"{contador}. {actual.Datos}");
                actual = actual.Siguiente;
                contador++;
            }
        }

        public Cliente Buscar(string nombre)
        {
            Nodo actual = frente;

            while (actual != null)
            {
                if (actual.Datos.Nombre.Equals(
                    nombre, StringComparison.OrdinalIgnoreCase))
                {
                    return actual.Datos;
                }

                actual = actual.Siguiente;
            }

            return null;
        }

        public bool Eliminar(string nombre)
        {
            Nodo actual = frente;
            Nodo anterior = null;

            while (actual != null)
            {
                if (actual.Datos.Nombre.Equals(
                    nombre, StringComparison.OrdinalIgnoreCase))
                {
                    if (anterior == null)
                        frente = actual.Siguiente;
                    else
                        anterior.Siguiente = actual.Siguiente;

                    if (actual == final)
                        final = anterior;

                    return true;
                }

                anterior = actual;
                actual = actual.Siguiente;
            }

            return false;
        }
    }
}
