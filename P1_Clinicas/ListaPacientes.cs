using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Clinica
{
    internal class ListaPacientes
    {
        private Nodo primero;

        public void Agregar(Paciente paciente)
        {
            Nodo nuevo = new Nodo(paciente);

            if (primero == null)
            {
                primero = nuevo;
            }
            else
            {
                Nodo actual = primero;

                while (actual.Siguiente != null)
                    actual = actual.Siguiente;

                actual.Siguiente = nuevo;
            }
        }

        public Paciente Buscar(int expediente)
        {
            Nodo actual = primero;

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
            Nodo actual = primero;
            Nodo anterior = null;

            while (actual != null)
            {
                if (actual.Datos.Expediente == expediente)
                {
                    if (anterior == null)
                        primero = actual.Siguiente;
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
            if (primero == null)
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }

            Nodo actual = primero;

            while (actual != null)
            {
                Console.WriteLine(actual.Datos);
                actual = actual.Siguiente;
            }
        }
    }
}