using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo13
{
    public interface IReservationVehicleDAO
    {
        ReservationVehicle Find(int id);
        void Delete(ReservationVehicle reservation);
        void Update(ReservationVehicle reservation);
        ReservationVehicle AddReservation(ReservationVehicle reservation);
        List<ReservationVehicle> GetAllByUserId(int user_id);
    }
}