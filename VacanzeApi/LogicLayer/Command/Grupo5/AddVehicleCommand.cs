using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5 
{
    public class AddVehicleCommand : CommandResult<int>
    {
        private int _id;
        public int Id { get { return _id; } set{ _id = value; } }

        private Vehicle _vehicle;
        public Vehicle Vehicle { get{ return _vehicle; } set{ _vehicle = value; } }

        public AddVehicleCommand(Vehicle _vehicle){
            this.Vehicle = _vehicle;
        }

        public void Execute(){
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IVehicleDAO vehicleDAO = daoFactory.GetVehicleDAO();
            _id = vehicleDAO.AddVehicle(_vehicle);
        }

        public int GetResult () {
            return _id;
        }
    }
}