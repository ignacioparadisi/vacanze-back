using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Locations;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Locations {

    public class GetCountriesCommand : Command, CommandResult<List<Location>> {
        private List<Location> _location;

        public GetCountriesCommand () {
        }

        public void Execute () {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            LocationDAO locationDao = factory.GetLocationDAO();
            _location = locationDao.GetCountries();
        }

        public List<Location> GetResult () {
            return _location;
        }

    }

}