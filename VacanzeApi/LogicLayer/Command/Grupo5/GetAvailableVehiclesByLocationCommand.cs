using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5 
{
    public class GetAvailableVehiclesByLocationCommand : CommandResult<List<Vehicle>> 
    {
        private int _locationId;
        private List<Vehicle> _vehicles;

        public GetAvailableVehiclesByLocationCommand(int _locationId){
            this._locationId = _locationId;
        }

        public void Execute(){
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IVehicleDAO vehicleDAO = daoFactory.GetVehicleDAO();
            _vehicles = vehicleDAO.GetAvailableVehiclesByLocation(_locationId);
        }

        public List<Vehicle> GetResult(){
            return _vehicles;
        }
    }
}