using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Locations;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Locations {

    public class AddLocationCommand : Command, CommandResult<int> {
        private int _id;
        public int Id { get { return _id; } set{ _id = value; } }

        private Location _location;

        public AddLocationCommand (Location location) {
            this._location = location;
        }

        public void Execute () {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            LocationDAO locationDao = factory.GetLocationDAO(); 
            _id = locationDao.AddLocation(_location);
        }

        public int GetResult () {
            return _id;
        }

    }

}