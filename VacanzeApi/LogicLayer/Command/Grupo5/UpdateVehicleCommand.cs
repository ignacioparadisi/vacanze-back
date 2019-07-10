using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5 
{
    public class UpdateVehicleCommand : CommandResult<bool> {
        private bool _updated;
        private Vehicle _vehicle;

        public UpdateVehicleCommand(Vehicle vehicle){
            this._vehicle = vehicle;
        }

        public void Execute(){
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IVehicleDAO vehicleDAO = daoFactory.GetVehicleDAO();
            _updated = vehicleDAO.UpdateVehicle(_vehicle);
        }

        public bool GetResult(){
            return _updated;
        }
    }
}