using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6 {

    public class GetHotelByIdCommand : Command, CommandResult<Hotel> {
        private int _id;

        private Hotel _hotel;

        public GetHotelByIdCommand (int id) {
            _id = id ;
        }

        public void Execute () {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            HotelDAO HotelDao = factory.GetHotelDAO();
            _hotel = HotelDao.GetHotelById(_id);
        }

        public Hotel GetResult () {
            return _hotel;
        }

    }

}