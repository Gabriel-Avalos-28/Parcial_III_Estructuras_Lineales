using System;
using System.Collections.Generic;
using System.Text;

namespace P3_Simu_LLama_Telefonicas
{
    public class Contacto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Celular { get; set; }

        public Contacto(int id, string nombre, string celular)
        {
            Id = id;
            Nombre = nombre;
            Celular = celular;
        }

        public override string ToString()
        {
            return $"Llamada #{Id} - {Nombre} - Celular: {Celular}";
        }
    }
}