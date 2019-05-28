namespace vacanze_back.VacanzeApi.Common.Entities.Grupo4
{
    public class Ticket : Entity
    {
        private string FechaSalida { get; set; }
        private string FechaEntrada { get; set; }
        private string TipoVuelo { get; set; }
        private string NombrePasajero { get; set; }
        private string CantidadPasajeros { get; set; }
        private string ClaseVuelo { get; set; }
        
       
       
        
        // TODO: Cuando se cree la clase ticket, implementar esta parte
      
        public Ticket(long id, string fechaSalida, string fechaEntrada, string tipoVuelo,
            string nombrePasajero, string cantidadPasajeros, string claseVuelo) : base(id)
        {
            FechaSalida = fechaSalida;
            FechaEntrada = fechaEntrada;
            TipoVuelo = tipoVuelo;
            NombrePasajero = nombrePasajero;
            CantidadPasajeros = cantidadPasajeros;
            ClaseVuelo = claseVuelo;
        }
        public Ticket(long id, string fechaSalida, string fechaEntrada, string tipoVuelo,
                   string nombrePasajero, string cantidadPasajeros, string claseVuelo) : base(0)
        {
            FechaSalida = fechaSalida;
            FechaEntrada = fechaEntrada;
            TipoVuelo = tipoVuelo;
            NombrePasajero = nombrePasajero;
            CantidadPasajeros = cantidadPasajeros;
            ClaseVuelo = claseVuelo;
        }
    }
}