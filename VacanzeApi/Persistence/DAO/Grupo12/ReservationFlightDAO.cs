using System;
using System.Collections.Generic;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo12;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo12
{
    public interface ReservationFlightDAO
    {
        int AddReservationFlight(Entity entity);

        List<Entity> GetReservationFlight(int id);

        void DeleteReservationFlight(int id_reservation);

        int GetIDLocation(string name_city);      

        List<Entity> GetFlightValidateI(int id_city_i,int id_city_v,string date_i,int numpas);

        string ConSeatNum(int numseat,int _id_fli);

        bool FindFlight(int id);

        bool FindUser(int id);

        bool FindReservation(int id);

        bool FindLocation(int id);

        List<Entity> GetReservationFlightIV(int departure, int arrival, string departuredate,string arrivaldate,int numpas);
        

    }
}