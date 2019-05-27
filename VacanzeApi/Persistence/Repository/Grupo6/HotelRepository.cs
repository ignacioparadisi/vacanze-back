using System;
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
    }
}