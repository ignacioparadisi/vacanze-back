using System.Collections.Generic;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo6
{
    public interface HotelDAO
    {
        int AddHotel(Hotel hotel);
        List<Hotel> GetHotels();
        Hotel GetHotelById(int id);
        List<Hotel> GetHotelsByCity(int city);
        void DeleteHotel(int id);
        Hotel UpdateHotel(int id, Hotel newData);
        string GetHotelImage(int id);
    }
}