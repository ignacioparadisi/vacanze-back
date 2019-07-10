using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Locations;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Locations {

    public class GetCountriesCommand : Command, CommandResult<List<Location>> {
        private List<Location> _location;

        public GetCountriesCommand () {
        }
        /// <summary>
        ///     Metodo para ejecutar todos los paises.
        /// </summary>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        public void Execute () {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            LocationDAO locationDao = factory.GetLocationDAO();
            _location = locationDao.GetCountries();
        }
        /// <summary>
        ///     Metodo para obtener todos los paises.
        /// </summary>
        /// <returns>Lista de Locations</returns>
        public List<Location> GetResult () {
            return _location;
        }

    }

}