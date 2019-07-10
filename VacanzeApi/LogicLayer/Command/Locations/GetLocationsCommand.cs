using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Locations;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Locations {

    public class GetLocationsCommand : Command, CommandResult<List<Location>> {
        private List<Location> _location;

        public GetLocationsCommand () {
        }
        /// <summary>
        ///     Metodo para obtener todos los ubicaciones guardados.
        /// </summary>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        public void Execute () {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            LocationDAO locationDao = factory.GetLocationDAO();
            _location = locationDao.GetLocations();
        }
        /// <summary>
        ///     Metodo para devolver todas los ubicaciones guardados.
        /// </summary>
        /// <returns>Lista de Locations</returns>
        public List<Location> GetResult () {
            return _location;
        }

    }

}