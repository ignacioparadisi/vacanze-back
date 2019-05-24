namespace vacanze_back.Entities.Grupo6
{
    public class Hotel : Entity
    {
        private string Nombre { get; }
        private int CantidadDeHabitaciones { get; }
        private bool Activo { get; }
        private string Telefono { get; }
        private string SitioWeb { get; }
        // TODO: Cuando se cree la clase Lugar, implementar esta parte
        // private Lugar _direccion;

        public Hotel(long id, string nombre, int cantidadDeHabitaciones, bool activo,
            string telefono, string sitioWeb) : base(id)
        {
            Nombre = nombre;
            CantidadDeHabitaciones = cantidadDeHabitaciones;
            Activo = activo;
            Telefono = telefono;
            SitioWeb = sitioWeb;
        }

        public Hotel(string nombre, int cantidadDeHabitaciones, bool activo,
            string telefono, string sitioWeb) : base(0)
        {
            Nombre = nombre;
            CantidadDeHabitaciones = cantidadDeHabitaciones;
            Activo = activo;
            Telefono = telefono;
            SitioWeb = sitioWeb;
        }
    }
}