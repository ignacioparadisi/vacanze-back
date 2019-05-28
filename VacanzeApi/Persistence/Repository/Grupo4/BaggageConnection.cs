using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities.Grupo4;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Connection.Grupo4
{
     public class BaggageConnection
     {


          /// <summary>
          ///     Metodo para agregar un Baggage
          /// </summary>
          /// <param name="Baggage"></param>
          public static Baggage AddBaggage(Baggage baggage)
          {

               var table = PgConnection.Instance.ExecuteFunction("addBaggage(@bag_res_fli_fk,@bag_res_cru_fk,@bag_status,@bag_descr)",
                   baggage.MaletaFkVuelo, baggage.MaletaFkCrucero, baggage.MaletaStatus, baggage.Descripcion);

               var id = Convert.ToInt64(table.Rows[0][0]);
               baggage.Id = id;
               baggage.MaletaStatus = table.Rows[1][3].ToString();
               baggage.Descripcion = table.Rows[1][4].ToString();
               return baggage;
          }



          public void DeleteBaggage(long baggageId)
          {

               var table = PgConnection.Instance.ExecuteFunction("DeleteBaggage(@bag_id)",
                   baggageId);

          }

          public void ModifyBaggageStatus(long baggageId, Baggage baggage)
          {
               var table = PgConnection.Instance.ExecuteFunction("modifyBaggageStatus(@bag_id,@bag_status)",
                  baggageId, baggage.MaletaStatus);

          }

          public void ModifyBaggageDescripcion(long baggageId, Baggage baggage)
          {
               var table = PgConnection.Instance.ExecuteFunction("modifyclaimtitle(@bag_id, @bag_descr)",
                 baggageId, baggage.Descripcion);
          }

          public static List<Baggage> GetBaggage()
          {
               var table = PgConnection.Instance.ExecuteFunction("getBaggage()");
               var BaggageList = new List<Baggage>();
               for (var i = 0; i < table.Rows.Count; i++)
               {
                    var id = Convert.ToInt64(table.Rows[i][0]);
                    var Vuelo = Convert.ToInt64(table.Rows[i][1]); ;
                    var Crucero = Convert.ToInt64(table.Rows[i][2]); ;
                    var descripcion = table.Rows[i][4].ToString();
                    var status = table.Rows[i][3].ToString();


                    var Baggage = new Baggage(id, Vuelo, Crucero, status, descripcion);
                    BaggageList.Add(Baggage);
               }

               return BaggageList;
          }
     }
}