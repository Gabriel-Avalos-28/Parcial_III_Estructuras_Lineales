using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Clinica
{
    internal class Cola
    {
        private Nodo frente;
        private Nodo final;
        private int cantidad;

        public Cola()
        {
            frente = null;
            final = null;
            cantidad = 0;
        }

        public void Encolar(Paciente paciente)
        {
            Nodo nuevo = new Nodo(paciente);

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

            cantidad++;
        }

        public Paciente Desencolar()
        {
            if (frente == null)
                return null;

            Paciente paciente = frente.Datos;
            frente = frente.Siguiente;

            if (frente == null)
                final = null;

            cantidad--;

            return paciente;
        }

        public Paciente Buscar(int expediente)
        {
            Nodo actual = frente;

            while (actual != null)
            {
                if (actual.Datos.Expediente == expediente)
                    return actual.Datos;

                actual = actual.Siguiente;
            }

            return null;
        }

        public bool Eliminar(int expediente)
        {
            Nodo actual = frente;
            Nodo anterior = null;

            while (actual != null)
            {
                if (actual.Datos.Expediente == expediente)
                {
                    if (anterior == null)
                        frente = actual.Siguiente;
                    else
                        anterior.Siguiente = actual.Siguiente;

                    if (actual == final)
                        final = anterior;

                    cantidad--;
                    return true;
                }

                anterior = actual;
                actual = actual.Siguiente;
            }

            return false;
        }

        public int Cantidad()
        {
            return cantidad;
        }

        public void Mostrar()
        {
            if (frente == null)
            {
                Console.WriteLine("Sin pacientes.");
                return;
            }

            Nodo actual = frente;
            int contador = 1;

            while (actual != null)
            {
                Console.WriteLine($"{contador}. {actual.Datos}");
                actual = actual.Siguiente;
                contador++;
            }
        }
    }
}