namespace P3_Simu_LLama_Telefonicas
{
    public class Nodo
    {
        public Contacto Datos;
        public Nodo Siguiente;

        public Nodo(Contacto contacto)
        {
            Datos = contacto;
            Siguiente = null;
        }
    }
}