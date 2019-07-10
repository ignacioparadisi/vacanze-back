using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo13
{
    public interface IReservationVehicleDAO
    {
        ReservationVehicle Find(int id);
        int Delete(int id);
        ReservationVehicle Update(ReservationVehicle reservation);
        ReservationVehicle AddReservation(ReservationVehicle reservation);
        List<ReservationVehicle> GetAllByUserId(int userId);
    }
}