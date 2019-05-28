namespace vacanze_back.VacanzeApi.Common.Entities.Grupo4
{
     public class Baggage : Entity
     {
          public long MaletaId { get; set; }
          public long MaletaFkVuelo { get; set; }
          public long MaletaFkCrucero { get; set; }
          public string MaletaStatus { get; set; }
          public string CantidadPasajeros { get; set; }
          public string Descripcion { get; set; }




          // TODO: Cuando se cree la clase Baggage, implementar esta parte

          public Baggage(long id, long maletaFkVuelo, long maletaFkCrucero,
              string maletaStatus, string descripcion) : base(0)
          {
               MaletaId = id;
               MaletaFkVuelo = maletaFkVuelo;
               MaletaFkCrucero = maletaFkCrucero;
               MaletaStatus = maletaStatus;
               Descripcion = descripcion;
          }

     }
}