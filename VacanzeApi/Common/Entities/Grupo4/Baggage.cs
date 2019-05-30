namespace vacanze_back.VacanzeApi.Common.Entities.Grupo4
{
     public class Baggage : Entity
     {
          public int MaletaId { get; set; }
          public int MaletaFkVuelo { get; set; }
          public int MaletaFkCrucero { get; set; }
          public string MaletaStatus { get; set; }

          public string Descripcion { get; set; }




          // TODO: Cuando se cree la clase Baggage, implementar esta parte

          public Baggage(int id, int maletaFkVuelo, int maletaFkCrucero,
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