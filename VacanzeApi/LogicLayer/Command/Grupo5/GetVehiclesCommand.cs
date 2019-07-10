using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5 
{
    public class GetVehiclesCommand : CommandResult<List<Vehicle>> 
    {
        private List<Vehicle> _vehicles;

        public GetVehiclesCommand(){}

        public void Execute(){
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IVehicleDAO vehicleDAO = daoFactory.GetVehicleDAO();
            _vehicles = vehicleDAO.GetVehicles();
        }

        public List<Vehicle> GetResult(){
            return _vehicles;
        }
    }
}