using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Locations;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Locations {

    public class AddLocationCommand : Command, CommandResult<int> {
        private int _id;

        private Location _location;
        /// <summary>
        ///     Metodo para obtener una location
        /// </summary>
        /// <param name="location">Objeto tipo Location a agregar</param>
        public AddLocationCommand (Location location) {
            this._location = location;
        }
        /// <summary>
        ///     Metodo para eecutar el agregar una ubicacion
        /// </summary>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        public void Execute () {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            LocationDAO locationDao = factory.GetLocationDAO(); 
            _id = locationDao.AddLocation(_location);
        }
        /// <summary>
        ///     Metodo para retornar la ide de  una ubicacion
        /// </summary>
        /// <returns>ID de la ubicacion creada</returns>
        public int GetResult () {
            return _id;
        }

    }

}