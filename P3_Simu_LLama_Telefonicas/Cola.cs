using System;
using System.Collections.Generic;
using System.Text;

namespace P3_Simu_LLama_Telefonicas
{
    public class Cola
    {
        private Nodo frente;
        private Nodo final;

        public void Encolar(Contacto contacto)
        {
            Nodo nuevo = new Nodo(contacto);

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

        public Contacto Desencolar()
        {
            if (frente == null)
                return null;

            Contacto contacto = frente.Datos;
            frente = frente.Siguiente;

            if (frente == null)
                final = null;

            return contacto;
        }

        public Contacto Buscar(int id)
        {
            Nodo actual = frente;

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
            Nodo actual = frente;
            Nodo anterior = null;

            while (actual != null)
            {
                if (actual.Datos.Id == id)
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

        public void Mostrar()
        {
            if (frente == null)
            {
                Console.WriteLine("No hay llamadas en espera.");
                return;
            }

            Nodo actual = frente;

            Console.WriteLine("=== LLAMADAS EN ESPERA ===");

            while (actual != null)
            {
                Console.WriteLine(actual.Datos);
                actual = actual.Siguiente;
            }
        }
    }
}