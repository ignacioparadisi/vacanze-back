using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class DeleteReservationRoomCommand : CommandResult<int>
    {
        /// <summary>
        /// Id de la reservación que se va a eliminar
        /// </summary>
        private int _id;
        /// <summary>
        /// Id de la reservación que fue eliminada
        /// </summary>
        private int _result;

        public DeleteReservationRoomCommand(int id)
        {
            _id = id;
        }

        /// <summary>
        /// Elimina una reservación de habitación mediante el DAO de reservación de habitaciones
        /// </summary>
        public void Execute()
        {
            var getReservationCommand = CommandFactory.CreateGetReservationRoomCommand(_id);
            getReservationCommand.Execute();
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var resRoomDao = (PostgresReservationRoomDAO) daoFactory.GetReservationRoomDAO();
            _result = resRoomDao.Delete(_id);
        }

        /// <summary>
        ///  Retorna el ID de la reservacion eliminada
        /// </summary>
        /// <returns>ID de la reservación elminada</returns>
        public int GetResult()
        {
            return _result;
        }
    }
}