namespace vacanze_back.VacanzeApi.Common.Entities.Grupo4
{
    public class Baggage : Entity
    {
        private string MaletaId { get; set; }
        private string MaletaFkVuelo { get; set; }
        private string MaletaFkCrucero { get; set; }
        private string MaletaStatus { get; set; }
        private string CantidadPasajeros { get; set; }
        private string Descripcion { get; set; }
        
       
       
        
        // TODO: Cuando se cree la clase ticket, implementar esta parte
      
        public Baggage(long id, string maletaId, string maletaFkVuelo, string maletaFkCrucero,
            string maletaStatus, string cantidadPasajeros, string descripcion) : base(id)
        {
            MaletaId = maletaId;
            MaletaFkVuelo = maletaFkVuelo;
            MaletaFkCrucero = maletaFkCrucero;
            MaletaStatus = maletaStatus;
            Descripcion = descripcion;
        }
        public Baggage(long id, string maletaId, string maletaFkVuelo, string maletaFkCrucero,
           string maletaStatus, string cantidadPasajeros, string descripcion) : base(0)
        {
            MaletaId = maletaId;
            MaletaFkVuelo = maletaFkVuelo;
            MaletaFkCrucero = maletaFkCrucero;
            MaletaStatus = maletaStatus;
            Descripcion = descripcion;
        }
    }
}