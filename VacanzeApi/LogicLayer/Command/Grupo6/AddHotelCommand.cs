using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6 {

    public class AddHotelCommand : CommandResult<int> {
        private int _id;
        private Hotel _hotel;

        /// <summary>
        ///     Metodo para recibir un Hotel a ser creado.
        /// </summary>
        /// <param name="hotel">Datos a ser guardados</param>
        public AddHotelCommand (Hotel _hotel) {
            this._hotel = _hotel;
        }
        /// <summary>
        ///     Metodo para ejecutar la accion de crear un Hotel.
        /// </summary>
        /// <exception cref="RequiredAttributeException">Algun atributo requerido estaba como null</exception>
        /// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido</exception>
        public void Execute () {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            HotelDAO HotelDao = factory.GetHotelDAO();
            _id = HotelDao.AddHotel(_hotel);
        }

        /// <summary>
        ///     Metodo para devolver la id de un hotel al ser creado.
        /// </summary>
        /// <returns>ID del registro del hotel creado en la base de datos</returns>
        public int GetResult () {
            return _id;
        }

    }

}