using System;
using System.Collections.Generic;
using System.Text;

namespace P3_Simu_LLama_Telefonicas
{
    public class ListaActivas
    {
        private Nodo primero;
        private Nodo ultimo;

        public void Agregar(Contacto contacto)
        {
            Nodo nuevo = new Nodo(contacto);

            if (primero == null)
            {
                primero = nuevo;
                ultimo = nuevo;
            }
            else
            {
                ultimo.Siguiente = nuevo;
                ultimo = nuevo;
            }
        }

        public Contacto Buscar(int id)
        {
            Nodo actual = primero;

            while (actual != null)
            {
                if (actual.Datos.Id == id)
                    return actual.Datos;

                actual = actual.Siguiente;
            }

            return null;
        }

        public Contacto Eliminar(int id)
        {
            Nodo actual = primero;
            Nodo anterior = null;

            while (actual != null)
            {
                if (actual.Datos.Id == id)
                {
                    if (anterior == null)
                        primero = actual.Siguiente;
                    else
                        anterior.Siguiente = actual.Siguiente;

                    if (actual == ultimo)
                        ultimo = anterior;

                    return actual.Datos;
                }

                anterior = actual;
                actual = actual.Siguiente;
            }

            return null;
        }

        public void Mostrar()
        {
            if (primero == null)
            {
                Console.WriteLine("No hay llamadas activas.");
                return;
            }

            Nodo actual = primero;

            Console.WriteLine("=== LLAMADAS ACTIVAS ===");

            while (actual != null)
            {
                Console.WriteLine(actual.Datos);
                actual = actual.Siguiente;
            }
        }
    }
}