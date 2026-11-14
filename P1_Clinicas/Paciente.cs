using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Clinica
{
    internal class Paciente
    {
        public int Expediente { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public int Prioridad { get; set; }

        public Paciente(int expediente, string nombre, int edad, int prioridad)
        {
            Expediente = expediente;
            Nombre = nombre;
            Edad = edad;
            Prioridad = prioridad;
        }

        public string NombrePrioridad()
        {
            switch (Prioridad)
            {
                case 1: return "Emergencia";
                case 2: return "Urgente";
                case 3: return "Consulta general";
                case 4: return "Cita programada";
                default: return "Desconocida";
            }
        }

        public override string ToString()
        {
            return $"Expediente: {Expediente} | Nombre: {Nombre} | Edad: {Edad} | Prioridad: {NombrePrioridad()}";
        }
    }
}