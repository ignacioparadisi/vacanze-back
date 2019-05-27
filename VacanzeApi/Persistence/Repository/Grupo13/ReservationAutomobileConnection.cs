using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo13
{
    public class ReservationAutomobileConnection : Connection
    { 
        const String SP_SELECT = "m13_getResAutos()";
        private Automobile _automobile;
        private ReservationAutomobileConnection _reservation;

                public List<Entity> GetAutomobileReservations()
                {
                    List<Entity> reservationAutomobileList = new List<Entity>();
                    ReservationAutomobileConnection reservationAutomobileConnection = new ReservationAutomobileConnection();
                    try
                    {
                            Connect();
                            StoredProcedure(SP_SELECT);
                            ExecuteReader();
                            for (int i = 0; i < numberRecords; i++)
                            {
                            ReservationAutomobile reservationAutomobile = new ReservationAutomobile(GetInt(i,0),GetDateTime(i,1),GetDateTime(i,2));

                            reservationAutomobileList.Add(reservationAutomobile);
                            }
                    }
                    catch (NpgsqlException e)
                    {
                        e.ToString();
                    }
                    catch (Exception e)
                    {
                        e.ToString();
                    }
                    finally
                    {
                        Disconnect();
                    }
                    return reservationAutomobileList;
                }
    }

}
