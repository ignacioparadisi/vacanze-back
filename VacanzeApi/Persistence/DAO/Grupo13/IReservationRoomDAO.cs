using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo13
{
    public interface IReservationRoomDAO
    {
        ReservationRoom Find(int id);
        int GetAvailableRoomReservations(int id);
        ReservationRoom Add(ReservationRoom reservation);
        int Delete(int id);
        List<ReservationRoom> GetAllByUserId(int userId);
        void Update(ReservationRoom reservation);
    }
}