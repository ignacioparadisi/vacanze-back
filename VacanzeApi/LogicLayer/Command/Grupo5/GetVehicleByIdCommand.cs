using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5 
{
    public class GetVehicleByIdCommand : CommandResult<Vehicle> 
    {
        private int _vehicleId;
        private Vehicle _vehicle;

        public GetVehicleByIdCommand(int _vehicleId){
            this._vehicleId = _vehicleId;
        }

        public void Execute(){
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IVehicleDAO vehicleDAO = daoFactory.GetVehicleDAO();
            _vehicle = vehicleDAO.GetVehicleById(_vehicleId);
        }

        public Vehicle GetResult(){
            return _vehicle;
        }
    }
}