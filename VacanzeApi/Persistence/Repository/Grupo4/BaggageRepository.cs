using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities.Grupo4;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo4
{
     public class BaggageRepository
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



          public static void DeleteBaggage(int baggageId)
          {

               var table = PgConnection.Instance.ExecuteFunction("deletebaggage(@bag_id)",
                   baggageId);

          }

          public void ModifyBaggageStatus(int baggageId, Baggage baggage)
          {
               var table = PgConnection.Instance.ExecuteFunction("modifyBaggageStatus(@bag_id,@bag_status)",
                  baggageId, baggage.MaletaStatus);

          }

          public void ModifyBaggageDescripcion(int baggageId, Baggage baggage)
          {
               var table = PgConnection.Instance.ExecuteFunction("modifyBaggageDescri(@bag_id, @bag_descr)",
                 baggageId, baggage.Descripcion);
          }

          public static List<Baggage> GetBaggage()
          {
               var table = PgConnection.Instance.ExecuteFunction("getallbaggage()");
               var Vuelo = 0;
               var Crucero = 0;
               var status = "";
               var descripcion = "";
               var id = 0;

               var BaggageList = new List<Baggage>();
               for (var i = 0; i < table.Rows.Count; i++)
               {
                    status = table.Rows[i][4].ToString();
                    id = Convert.ToInt32(table.Rows[i][0]);
                    descripcion = table.Rows[i][3].ToString();
                    Vuelo = Convert.ToInt32(table.Rows[i][1]);
                    Crucero = Convert.ToInt32(table.Rows[i][2]);
                    var Baggage = new Baggage(id, Vuelo, Crucero, status, descripcion);
                    BaggageList.Add(Baggage);
               }

               return BaggageList;
          }
     }
}