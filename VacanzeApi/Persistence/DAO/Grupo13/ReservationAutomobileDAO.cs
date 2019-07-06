using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo13
{
    public interface ReservationAutomobileDAO
    {
        ReservationAutomobile Find(int id);
        void Delete(ReservationAutomobile reservation);
        void Update(ReservationAutomobile reservation);
        List<ReservationAutomobile> GetAutomobileReservations();
        ReservationAutomobile AddReservation(ReservationAutomobile reservation);
        List<ReservationAutomobile> GetAllByUserId(int user_id);
    }
}