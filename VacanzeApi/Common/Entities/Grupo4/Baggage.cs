namespace vacanze_back.VacanzeApi.Common.Entities.Grupo4
{
     public class Baggage : Entity
     {
<<<<<<< HEAD
          public int MaletaId { get; set; }
          public int MaletaFkVuelo { get; set; }
          public int MaletaFkCrucero { get; set; }
          public string MaletaStatus { get; set; }

=======
          public long MaletaId { get; set; }
          public long MaletaFkVuelo { get; set; }
          public long MaletaFkCrucero { get; set; }
          public string MaletaStatus { get; set; }
          public string CantidadPasajeros { get; set; }
>>>>>>> 979d953386630d498c24041e0c89ee6e1993d0bc
          public string Descripcion { get; set; }




          // TODO: Cuando se cree la clase Baggage, implementar esta parte

<<<<<<< HEAD
          public Baggage(int id, int maletaFkVuelo, int maletaFkCrucero,
=======
          public Baggage(long id, long maletaFkVuelo, long maletaFkCrucero,
>>>>>>> 979d953386630d498c24041e0c89ee6e1993d0bc
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