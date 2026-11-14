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

            if (numero <= 5)
                Capacidad = 2;
            else if (numero <= 10)
                Capacidad = 4;
            else
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
