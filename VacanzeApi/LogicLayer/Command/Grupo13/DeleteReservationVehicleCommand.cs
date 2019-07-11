using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class DeleteReservationVehicleCommand: CommandResult<int>
    {
        /// <summary>
        /// ID del vehículo a ser eliminado
        /// </summary>
        private readonly int _id;
        /// <summary>
        /// ID del vehículo que fue eliminado
        /// </summary>
        private int _result;

        public DeleteReservationVehicleCommand(int id)
        {
            _id = id;
        }

        /// <summary>
        /// Elimina una reservación de vehículo mediante el DAO de reservación de vehículos
        /// </summary>
        public void Execute()
        {
            var getReservationCommand = CommandFactory.CreateFindReservationVehicleCommand(_id);
            getReservationCommand.Execute();
            var dao = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetReservationVehicleDAO();
            _result = dao.Delete(_id);
        }

        /// <summary>
        /// Retorna el id del vehículo eliminado
        /// </summary>
        /// <returns>ID del vehículo eliminado</returns>
        public int GetResult()
        {
            return _result;
        }
    }
}