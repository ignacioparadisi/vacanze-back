using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo6
{
    public static class HotelRepository
    {
        public static long AddHotel(Hotel hotel)
        {
            var table = PgConnection.Instance.ExecuteFunction(
                "addhotel(@name, @amountOfRooms, @active, @phone, @website, @location)",
                hotel.Name, hotel.AmountOfRooms, hotel.IsActive, hotel.Phone, hotel.Website, 1
            );

            var savedId = Convert.ToInt64(table.Rows[0][0]);
            return savedId;
        }

        public static List<Hotel> GetHotels()
        {
            var table = PgConnection.Instance.ExecuteFunction("consultarHoteles()");
            var hotelList = new List<Hotel>();
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt64(table.Rows[i][0]);
                var name = table.Rows[i][1].ToString();
                var amountOfRooms =
                    Convert.ToInt32(table.Rows[i][2]); //Convert.ToInt32(GetString(i, 2));
                var isActive = Convert.ToBoolean(table.Rows[i][3]);
                var phone = table.Rows[i][4].ToString();
                var website = table.Rows[i][5].ToString();
                var hotel = new Hotel(id, name, amountOfRooms, isActive, phone, website);
                hotelList.Add(hotel);
            }

            return hotelList;
        }
    }
}