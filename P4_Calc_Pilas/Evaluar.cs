using System;
using System.Collections.Generic;
using System.Text;


namespace P4_Calc_Pilas
{
    public class PilaNumeros
    {
        private Nodo tope;
        private int cantidad;

        private class Nodo
        {
            public double Dato;
            public Nodo Siguiente;

            public Nodo(double dato)
            {
                Dato = dato;
            }
        }

        public void Apilar(double dato)
        {
            Nodo nuevo = new Nodo(dato);
            nuevo.Siguiente = tope;
            tope = nuevo;
            cantidad++;
        }

        public double Desapilar()
        {
            if (tope == null)
                return 0;

            double dato = tope.Dato;
            tope = tope.Siguiente;
            cantidad--;

            return dato;
        }

        public int Cantidad()
        {
            return cantidad;
        }
    }
}

