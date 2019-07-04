using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Locations;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Locations {

    public class GetCitiesByCountryCommand : Command, CommandResult<List<Location>> {
        private int _id;

        private List<Location> _location;

        public GetCitiesByCountryCommand (int id) {
            _id = id ;
        }

        public void Execute () {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            LocationDAO locationDao = factory.GetLocationDAO();
            _location = locationDao.GetCitiesByCountry(_id);
        }

        public List<Location> GetResult () {
            return _location;
        }

    }

}