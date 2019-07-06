using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Locations;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Locations {

    public class GetLocationByIdCommand : Command, CommandResult<Location> {
        private int _id;

        private Location _location;
        /// <summary>
        ///     Metodo para obtener la  id para hacer la consulta de las locations.
        /// </summary>
        /// <param name="id">ID de la locations a buscar</param>
        public GetLocationByIdCommand (int id) {
            _id = id ;
        }
        /// <summary>
        ///     Metodo para ejecutar la ubicacion por id de una location.
        /// </summary>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        public void Execute () {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            LocationDAO locationDao = factory.GetLocationDAO();
            _location = locationDao.GetLocationById(_id);
        }
        /// <summary>
        ///     Metodo para devolver la ubicacion por id.
        /// </summary>
        /// <returns>objeto tipo Location</returns>
        public Location GetResult () {
            return _location;
        }

    }

}