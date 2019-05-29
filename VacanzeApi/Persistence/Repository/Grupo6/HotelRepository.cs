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
                "addhotel(@name, @amountOfRooms, @capacityPerRoom, @active, @addressSpecs, " +
                "@roomPrice, @website, @phone, @picture, @stars, @location)",
                hotel.Name,
                hotel.AmountOfRooms,
                hotel.RoomCapacity,
                hotel.IsActive,
                hotel.AddressSpecification,
                hotel.PricePerRoom,
                hotel.Website,
                hotel.Phone,
                hotel.Picture,
                hotel.Stars,
                hotel.Location.Id
            );
            var savedId = Convert.ToInt64(table.Rows[0][0]);
            return savedId;
        }

        public static List<Hotel> GetHotels()
        {
            var table = PgConnection.Instance.ExecuteFunction("getHotels()");
            var hotelList = new List<Hotel>();
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt64(table.Rows[i][0]);
                var name = table.Rows[i][1].ToString();
                var amountOfRooms = Convert.ToInt32(table.Rows[i][2]);
                var roomCapacity = Convert.ToInt32(table.Rows[i][3]);
                var isActive = Convert.ToBoolean(table.Rows[i][4]);
                var addressSpecs = table.Rows[i][5].ToString();
                var pricePerRoom = Convert.ToDecimal(table.Rows[i][6]);
                var website = table.Rows[i][7].ToString();
                var phone = table.Rows[i][8].ToString();
                var picture = table.Rows[i][9].ToString();
                var stars = Convert.ToInt32(table.Rows[i][10]);
                var locationId = Convert.ToInt32(table.Rows[i][11]);

                var hotel = HotelBuilder.Create()
                    .IdentifiedBy(id)
                    .WithName(name)
                    .WithAmountOfRooms(amountOfRooms)
                    .WithCapacityPerRoom(roomCapacity)
                    .WithPricePerRoom(pricePerRoom)
                    .WithPhone(phone)
                    .WithWebsite(website)
                    .WithStars(stars)
                    .WithBase64Picture(picture)
                    .LocatedAt(LocationRepository.GetLocationById(locationId))
                    .WithStatus(isActive)
                    .WithAddressDescription(addressSpecs)
                    .Build();
                hotelList.Add(hotel);
            }

            return hotelList;
        }
    }
}