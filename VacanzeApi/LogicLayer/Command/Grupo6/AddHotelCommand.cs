using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6 {

    public class AddHotelCommand : Command, CommandResult<int> {
        private int _id;
        public int Id { get { return _id; } set{ _id = value; } }

        private Hotel _hotel;

        public AddHotelCommand (Hotel _hotel) {
            this._hotel = _hotel;
        }

        public void Execute () {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            HotelDAO HotelDao = factory.GetHotelDAO();
            _id = HotelDao.AddHotel(_hotel);
        }

        public int GetResult () {
            Console.WriteLine("estoy aqui add") ; 
            Console.WriteLine(_hotel.Name);
            Console.WriteLine(_id);
            return _id;
        }

    }

}