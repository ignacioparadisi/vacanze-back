using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6 {

    public class GetHotelByIdCommand : CommandResult<Hotel> {
        private int _id;

        private Hotel _hotel;
        /// <summary>
        ///     Metodo para obtener la id del  Hotel correspondiente a los datos guardados.
        /// </summary>
        /// <param name="id">ID del hotel del cual se quiere un objeto</param>
        public GetHotelByIdCommand (int id) {
            _id = id ;
        }
        /// <summary>
        ///     Metodo para ejecutar la orden del  objeto Hotel correspondiente a los datos guardados para el ID recibido.
        /// </summary>
        /// <exception cref="HotelNotFoundException">Lanzada si no existe un Hotel para el ID recibido</exception>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la bse de
        ///     datos
        /// </exception>
        public void Execute () {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            HotelDAO HotelDao = factory.GetHotelDAO();
            _hotel = HotelDao.GetHotelById(_id);
        }
        /// <summary>
        ///     Metodo para obtener objeto Hotel correspondiente a los datos guardados para el ID recibido.
        /// </summary>
        /// <param name="id">ID del hotel del cual se quiere un objeto</param>
        public Hotel GetResult () {
            return _hotel;
        }

    }

}