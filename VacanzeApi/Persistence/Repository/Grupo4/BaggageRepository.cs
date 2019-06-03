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

          public static String AddBaggage(int bag_res_fli_fk, int bag_res_cru_fk, string status, string descripcion)
          {

               var table = PgConnection.Instance.ExecuteFunction("addbaggage(@bag_res_fli_fk,@bag_res_cru_fk,@bag_status,@bag_descr)",
                   bag_res_fli_fk, bag_res_cru_fk, status, descripcion);


               return "Maleta Agregada";
          }

          public static int AddBaggageReturnId(int bag_res_fli_fk, int bag_res_cru_fk, string status, string descripcion)
          {

               var table = PgConnection.Instance.ExecuteFunction("addbaggage(@bag_res_fli_fk,@bag_res_cru_fk,@bag_status,@bag_descr)",
                   DBNull.Value, DBNull.Value, status, descripcion);


               return Convert.ToInt32(table.Rows[0][0]);
          }
          public static int DeleteBaggage(int baggageId)
          {

               var table = PgConnection.Instance.ExecuteFunction("deletebaggage(@bag_id)",
                   baggageId);
               var baggage = table.Rows[0][0];
               if (baggage == DBNull.Value)
               {
                    throw new UserNotFoundException("La Maleta  no se encuentra registrada.");
               }

               return Convert.ToInt32(baggage);

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
                    if (table.Rows[i][1] != DBNull.Value)
                    {
                         Vuelo = Convert.ToInt32(table.Rows[i][1]);
                    }
                    else
                    {
                         Vuelo = 0;
                    }
                    if (table.Rows[i][2] != DBNull.Value)
                    {
                         Crucero = Convert.ToInt32(table.Rows[i][2]);
                    }
                    else
                    {
                         Crucero = 0;
                    }
                    status = table.Rows[i][4].ToString();
                    id = Convert.ToInt32(table.Rows[i][0]);
                    descripcion = table.Rows[i][3].ToString();


                    var Baggage = new Baggage(id, Vuelo, Crucero, status, descripcion);
                    BaggageList.Add(Baggage);
               }

               return BaggageList;
          }
     }
}