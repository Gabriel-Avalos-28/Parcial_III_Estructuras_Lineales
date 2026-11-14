
namespace P2_Restaurante
{
    public class Mesa
    {
        public int Numero { get; set; }
        public int Capacidad { get; set; }
        public bool Disponible { get; set; }
        public Cliente Ocupando { get; set; }

        public Mesa(int numero)
        {
            Numero = numero;
            Capacidad = 6;
            Disponible = true;
            Ocupando = null;
        }

        public void Asignar(Cliente cliente)
        {
            Ocupando = cliente;
            Disponible = false;
        }

        public void Liberar()
        {
            Ocupando = null;
            Disponible = true;
        }
    }
}