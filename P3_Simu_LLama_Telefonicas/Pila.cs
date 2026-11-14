using System;
using System.Collections.Generic;
using System.Text;

namespace P3_Simu_LLama_Telefonicas
{
    public class Pila
    {
        private Nodo tope;

        public void Apilar(Contacto contacto)
        {
            Nodo nuevo = new Nodo(contacto);

            nuevo.Siguiente = tope;
            tope = nuevo;
        }

        public Contacto Desapilar()
        {
            if (tope == null)
                return null;

            Contacto contacto = tope.Datos;
            tope = tope.Siguiente;

            return contacto;
        }

        public Contacto Buscar(int id)
        {
            Nodo actual = tope;

            while (actual != null)
            {
                if (actual.Datos.Id == id)
                    return actual.Datos;

                actual = actual.Siguiente;
            }

            return null;
        }

        public bool Eliminar(int id)
        {
            Nodo actual = tope;
            Nodo anterior = null;

            while (actual != null)
            {
                if (actual.Datos.Id == id)
                {
                    if (anterior == null)
                        tope = actual.Siguiente;
                    else
                        anterior.Siguiente = actual.Siguiente;

                    return true;
                }

                anterior = actual;
                actual = actual.Siguiente;
            }

            return false;
        }

        public void Mostrar()
        {
            if (tope == null)
            {
                Console.WriteLine("No hay llamadas finalizadas.");
                return;
            }

            Nodo actual = tope;

            Console.WriteLine("=== LLAMADAS FINALIZADAS ===");

            while (actual != null)
            {
                Console.WriteLine(actual.Datos);
                actual = actual.Siguiente;
            }
        }
    }
}
