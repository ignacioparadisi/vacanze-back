namespace vacanze_back.VacanzeApi.Common.Entities.Grupo4
{
     public class Baggage : Entity
     {
          private int MaletaId { get; set; }
          private string MaletaFkVuelo { get; set; }
          private string MaletaFkCrucero { get; set; }
          private string MaletaStatus { get; set; }
          private string CantidadPasajeros { get; set; }
          private string Descripcion { get; set; }




          // TODO: Cuando se cree la clase Baggage, implementar esta parte

          public Baggage(int id, string maletaFkVuelo, string maletaFkCrucero,
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