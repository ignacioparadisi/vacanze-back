using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5 
{
    public class UpdateVehicleStatusCommand : CommandResult<bool> {
        private bool _updated;
        private bool _status;
        private int _vehicleId;

        public UpdateVehicleStatusCommand(int _vehicleId, bool _status){
            this._status = _status;
            this._vehicleId = _vehicleId;
        }

        public void Execute(){
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IVehicleDAO vehicleDAO = daoFactory.GetVehicleDAO();
            _updated = vehicleDAO.UpdateVehicleStatus(_vehicleId, _status);
        }

        public bool GetResult(){
            return _updated;
        }
    }
}