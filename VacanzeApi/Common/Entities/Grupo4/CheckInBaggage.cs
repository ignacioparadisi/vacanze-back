namespace vacanze_back.VacanzeApi.Common.Entities.Grupo4
{
     public class CheckinBaggage : Entity
     {
          public int _id { get; set; }
          public int _maletaFkVuelo { get; set; }
          public int _maletaFkCrucero { get; set; }
          public string _maletaStatus { get; set; }
          public string _descripcion { get; set; }
          // TODO: Cuando se cree la clase Baggage, implementar esta parte

          public CheckinBaggage(int id, int maletaFkVuelo, int maletaFkCrucero,
              string maletaStatus, string descripcion) : base(0)
          {
              this._id=0;
              this._maletaFkVuelo = maletaFkVuelo;
              this._maletaFkCrucero  = maletaFkCrucero;
              this._maletaStatus = maletaStatus;
              this._descripcion = descripcion;
          }

     }
}

