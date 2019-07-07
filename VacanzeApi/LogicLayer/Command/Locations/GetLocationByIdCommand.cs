using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Locations;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Locations {

    public class GetLocationByIdCommand : Command, CommandResult<Location> {
        private int _id;
        public int Id { get { return _id; } set{ _id = value; } }

        private Location _location;

        public GetLocationByIdCommand (int id) {
            _id = id ;
        }

        public void Execute () {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            LocationDAO locationDao = factory.GetLocationDAO();
            _location = locationDao.GetLocationById(_id);
        }

        public Location GetResult () {
            return _location;
        }

    }

}