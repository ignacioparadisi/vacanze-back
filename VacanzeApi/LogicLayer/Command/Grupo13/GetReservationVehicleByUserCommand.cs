using System.Collections.Generic;
using System.Linq;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class GetReservationVehicleByUserCommand : CommandResult<List<ReservationVehicle>>
    {

        private List<ReservationVehicle> _reservations;
        private int userId;

        public GetReservationVehicleByUserCommand(int id)
        {
            userId = id;
        }
        public List<ReservationVehicle> GetResult()
        {
            return _reservations;
        }

        public void Execute()
        {
            var dao = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetReservationVehicleDAO();
            _reservations = dao.GetAllByUserId(userId);
            if (!_reservations.Any())
            {
                throw new UserDoesntHaveReservationsException();
            }

        }
    }
}