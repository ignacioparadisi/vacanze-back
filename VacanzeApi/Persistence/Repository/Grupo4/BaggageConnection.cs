using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities.Grupo4;
using vacanze_back.VacanzeApi.Common.Exceptions;

<<<<<<< HEAD
namespace vacanze_back.VacanzeApi.Persistence.Grupo4
{
     public class BaggageConnection
     {
=======
namespace vacanze_back.VacanzeApi.Persistence.Connection.Grupo4
{
     public class BaggageConnection
     {


>>>>>>> 979d953386630d498c24041e0c89ee6e1993d0bc
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



<<<<<<< HEAD
          public void DeleteBaggage(int baggageId)
=======
          public void DeleteBaggage(long baggageId)
>>>>>>> 979d953386630d498c24041e0c89ee6e1993d0bc
          {

               var table = PgConnection.Instance.ExecuteFunction("DeleteBaggage(@bag_id)",
                   baggageId);

          }

<<<<<<< HEAD
          public void ModifyBaggageStatus(int baggageId, Baggage baggage)
=======
          public void ModifyBaggageStatus(long baggageId, Baggage baggage)
>>>>>>> 979d953386630d498c24041e0c89ee6e1993d0bc
          {
               var table = PgConnection.Instance.ExecuteFunction("modifyBaggageStatus(@bag_id,@bag_status)",
                  baggageId, baggage.MaletaStatus);

          }

<<<<<<< HEAD
          public void ModifyBaggageDescripcion(int baggageId, Baggage baggage)
          {
               var table = PgConnection.Instance.ExecuteFunction("modifyBaggageDescri(@bag_id, @bag_descr)",
=======
          public void ModifyBaggageDescripcion(long baggageId, Baggage baggage)
          {
               var table = PgConnection.Instance.ExecuteFunction("modifyclaimtitle(@bag_id, @bag_descr)",
>>>>>>> 979d953386630d498c24041e0c89ee6e1993d0bc
                 baggageId, baggage.Descripcion);
          }

          public static List<Baggage> GetBaggage()
          {
               var table = PgConnection.Instance.ExecuteFunction("getBaggage()");
               var BaggageList = new List<Baggage>();
               for (var i = 0; i < table.Rows.Count; i++)
               {
<<<<<<< HEAD
                    var id = Convert.ToInt32(table.Rows[i][0]);
                    var Vuelo = Convert.ToInt32(table.Rows[i][1]); ;
                    var Crucero = Convert.ToInt32(table.Rows[i][2]); ;
=======
                    var id = Convert.ToInt64(table.Rows[i][0]);
                    var Vuelo = Convert.ToInt64(table.Rows[i][1]); ;
                    var Crucero = Convert.ToInt64(table.Rows[i][2]); ;
>>>>>>> 979d953386630d498c24041e0c89ee6e1993d0bc
                    var descripcion = table.Rows[i][4].ToString();
                    var status = table.Rows[i][3].ToString();


                    var Baggage = new Baggage(id, Vuelo, Crucero, status, descripcion);
                    BaggageList.Add(Baggage);
               }

               return BaggageList;
          }
     }
}