using System;
using System.Collections.Generic;
using System.Text;

using System;

namespace Proyecto1_Clinica
{
    internal class ColaPrioridad
    {
        private Cola emergencias = new Cola();
        private Cola urgentes = new Cola();
        private Cola consultas = new Cola();
        private Cola citas = new Cola();

        public void Encolar(Paciente paciente)
        {
            switch (paciente.Prioridad)
            {
                case 1:
                    emergencias.Encolar(paciente);
                    break;

                case 2:
                    urgentes.Encolar(paciente);
                    break;

                case 3:
                    consultas.Encolar(paciente);
                    break;

                case 4:
                    citas.Encolar(paciente);
                    break;
            }
        }

        public Paciente Desencolar()
        {
            if (emergencias.Cantidad() > 0)
                return emergencias.Desencolar();

            if (urgentes.Cantidad() > 0)
                return urgentes.Desencolar();

            if (consultas.Cantidad() > 0)
                return consultas.Desencolar();

            if (citas.Cantidad() > 0)
                return citas.Desencolar();

            return null;
        }

        public Paciente Buscar(int expediente)
        {
            Paciente paciente = emergencias.Buscar(expediente);

            if (paciente != null) return paciente;

            paciente = urgentes.Buscar(expediente);
            if (paciente != null) return paciente;

            paciente = consultas.Buscar(expediente);
            if (paciente != null) return paciente;

            return citas.Buscar(expediente);
        }

        public bool Eliminar(int expediente)
        {
            if (emergencias.Eliminar(expediente)) return true;
            if (urgentes.Eliminar(expediente)) return true;
            if (consultas.Eliminar(expediente)) return true;
            if (citas.Eliminar(expediente)) return true;

            return false;
        }

        public int Cantidad()
        {
            return emergencias.Cantidad()
                + urgentes.Cantidad()
                + consultas.Cantidad()
                + citas.Cantidad();
        }

        public void Mostrar()
        {
            Console.WriteLine("\n=== EMERGENCIAS ===");
            emergencias.Mostrar();

            Console.WriteLine("\n=== URGENTES ===");
            urgentes.Mostrar();

            Console.WriteLine("\n=== CONSULTA GENERAL ===");
            consultas.Mostrar();

            Console.WriteLine("\n=== CITAS PROGRAMADAS ===");
            citas.Mostrar();
        }
    }
}