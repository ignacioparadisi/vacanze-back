using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Locations;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Locations {

    public class GetCitiesByCountryCommand : Command, CommandResult<List<Location>> {
        private int _id;

        private List<Location> _location;
        /// <summary>
        ///     Metodo para obtener la id de por pais
        /// </summary>
        /// <param name="id">ID del alguna ciudad del pais deseado, para buscar las demas ciudades</param>
        public GetCitiesByCountryCommand (int id) {
            _id = id ;
        }
        /// <summary>
        ///     Metodo para ejcutar todos las ciudades por pais
        /// </summary>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        public void Execute () {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            LocationDAO locationDao = factory.GetLocationDAO();
            _location = locationDao.GetCitiesByCountry(_id);
        }
        /// <summary>
        ///     Metodo para devolver todos las ciudades por pais
        /// </summary>
        /// <returns>Lista de Locations</returns>
        public List<Location> GetResult () {
            return _location;
        }

    }

}