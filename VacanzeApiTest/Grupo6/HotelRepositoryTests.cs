using System.Collections.Generic;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Persistence.Repository;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo6;

namespace vacanze_back.VacanzeApiTest.Grupo6
{
    [TestFixture]
    public class HotelRepositoryTests
    {
        private Hotel hotel;

        [SetUp]
        public void Setup()
        {
            int idLoc = LocationRepository.AddLocation(new Location("Venezeula", "Cagua"));
            Location location = LocationRepository.GetLocationById(idLoc);
            hotel = HotelBuilder.Create()
                .WithName("Hotel Cagua")
                .WithAmountOfRooms(2000)
                .WithCapacityPerRoom(2)
                .WithPricePerRoom(200)
                .WithPhone("04243240208")
                .WithWebsite("HC.com")
                .WithStars(2)
                .LocatedAt(location)
                .WithStatus(true)
                .WithAddressDescription("Calle Los Almendrones")
                .Build();
        }
        
        [Test]
        public void GetHotelsTest()
        {
            var addedHotel = HotelRepository.AddHotel(hotel);
            List<Hotel> hotelsList = HotelRepository.GetHotels();
            Assert.True(hotelsList.Count > 0);
        }

        [Test]
        public void AddHotelTest()
        {
            var addedHotel = HotelRepository.AddHotel(hotel);
            Assert.AreNotEqual(0,  addedHotel);
        }
        
        [Test]
        public void GetHotelTest()
        {
            var hotelId = HotelRepository.AddHotel(hotel);
            var getHotel = HotelRepository.GetHotelById(hotelId);
            Assert.AreEqual(hotelId, getHotel.Id);
        }
        
        
    }
}