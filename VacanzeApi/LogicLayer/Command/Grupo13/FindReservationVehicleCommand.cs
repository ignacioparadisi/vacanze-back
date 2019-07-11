using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class FindReservationVehicleCommand : CommandResult<ReservationVehicle>
    {

        /// <summary>
        ///  ID de la reservación de vehículo a ser encontrado
        /// </summary>
        private int _id;
        /// <summary>
        ///  Reservación de vehículo encontrado
        /// </summary>
        private ReservationVehicle _reservationVehicle;

        public FindReservationVehicleCommand(int reservationId)
        {
            _id = reservationId;
        }
        
        /// <summary>
        /// Retorna la reservación de vehículo encontrada
        /// </summary>
        /// <returns>Reservación de vehículo encontrada</returns>
        public ReservationVehicle GetResult()
        {
            return _reservationVehicle;
        }

        /// <summary>
        /// Busca una reservación de vehiculo por id mediante el DAO de reservación de vehículos
        /// </summary>
        /// <exception cref="VehicleReservationNotFoundException">Se retorna cuando el no existe una
        ///             una reservación con el ID especificado </exception>
        public void Execute()
        {
            var dao = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetReservationVehicleDAO();
            _reservationVehicle = dao.Find(_id);
            if (_reservationVehicle.Id == 0)
            {
                throw new VehicleReservationNotFoundException();
            }
        }
    }
}