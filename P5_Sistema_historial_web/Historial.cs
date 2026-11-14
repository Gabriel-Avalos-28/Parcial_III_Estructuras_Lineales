using System;
using System.Collections.Generic;
using System.Text;


namespace P5_Sistema_historial_web
{
    public class Historial
    {
        private class NodoHistorial
        {
            public string Pagina;
            public NodoHistorial Siguiente;

            public NodoHistorial(string pagina)
            {
                Pagina = pagina;
                Siguiente = null;
            }
        }

        private NodoHistorial primero;
        private NodoHistorial ultimo;

        public void Agregar(string pagina)
        {
            NodoHistorial nuevo = new NodoHistorial(pagina);

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

        public void Mostrar()
        {
            if (primero == null)
            {
                Console.WriteLine("El historial esta vacio.");
                return;
            }

            NodoHistorial actual = primero;

            Console.WriteLine("=== Historial de navegacion ===");

            while (actual != null)
            {
                Console.WriteLine(actual.Pagina);
                actual = actual.Siguiente;
            }
        }

        public void Buscar(string pagina)
        {
            NodoHistorial actual = primero;
            bool encontrado = false;

            while (actual != null)
            {
                if (actual.Pagina.Equals(pagina, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Pagina encontrada: " + actual.Pagina);
                    encontrado = true;
                }

                actual = actual.Siguiente;
            }

            if (!encontrado)
                Console.WriteLine("La pagina no se encuentra en el historial.");
        }

        public void Limpiar()
        {
            primero = null;
            ultimo = null;

            Console.WriteLine("Historial eliminado.");
        }

        public string UltimaPagina()
        {
            if (ultimo == null)
                return null;

            return ultimo.Pagina;
        }
    }
}