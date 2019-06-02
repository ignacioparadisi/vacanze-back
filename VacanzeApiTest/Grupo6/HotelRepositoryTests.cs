using System.Collections.Generic;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;
using vacanze_back.VacanzeApi.Persistence.Repository;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo6;

namespace vacanze_back.VacanzeApiTest.Grupo6
{
    [TestFixture]
    public class HotelRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
            var location = LocationRepository.GetLocationById(HotelTestSetup.LOCATION_ID);
            _hotel = HotelBuilder.Create()
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

        [TearDown]
        public void TearDown()
        {
            foreach (var hotelId in _insertedHotels) HotelRepository.DeleteHotel(hotelId);
            _insertedHotels.Clear();
        }

        private Hotel _hotel;
        private readonly List<int> _insertedHotels = new List<int>();

        [Test]
        public void AddHotel_HotelWithInvalidLocation_ExceptionThrown()
        {
            Assert.Throws<InvalidAttributeException>(() =>
            {
                _hotel.Location.Id = 99999;
                HotelRepository.AddHotel(_hotel);
            });
        }

        [Test]
        public void AddHotel_ValidHotel_NoExceptionThrown()
        {
            Assert.DoesNotThrow(() =>
            {
                var addedHotelId = HotelRepository.AddHotel(_hotel);
                HotelRepository.GetHotelById(addedHotelId);
                _insertedHotels.Add(addedHotelId);
            });
        }

        [Test]
        public void DeleteHotel_InvalidHotelId_NoExceptionThrown()
        {
            Assert.DoesNotThrow(() => { HotelRepository.DeleteHotel(12093); });
        }

        [Test]
        public void DeleteHotel_ValidHotelId_HotelNotFound()
        {
            Assert.Throws<HotelNotFoundException>(() =>
            {
                var addedHotelId = HotelRepository.AddHotel(_hotel);
                HotelRepository.GetHotelById(addedHotelId);
                HotelRepository.DeleteHotel(addedHotelId);
                HotelRepository.GetHotelById(addedHotelId);
            });
        }

        [Test]
        public void GetHotelById_InvalidHotelId_ExceptionThrown()
        {
            Assert.Throws<HotelNotFoundException>(() => HotelRepository.GetHotelById(10928));
        }

        [Test]
        public void GetHotelById_ValidHotelId_CorrectDataReturned()
        {
            var hotelId = HotelRepository.AddHotel(_hotel);
            _insertedHotels.Add(hotelId);
            var getHotel = HotelRepository.GetHotelById(hotelId);
            Assert.AreEqual(hotelId, getHotel.Id);
        }

        [Test]
        public void GetHotelsByCity_HotelsInCity_CorrectDataReturned()
        {
            _insertedHotels.Add(HotelRepository.AddHotel(_hotel));
            _insertedHotels.Add(HotelRepository.AddHotel(_hotel));
            _insertedHotels.Add(HotelRepository.AddHotel(_hotel));

            var found = HotelRepository.GetHotelsByCity(HotelTestSetup.LOCATION_ID);
            Assert.AreEqual(3, found.Count);
        }

        [Test]
        public void GetHotelsTest()
        {
            var addedHotelId1 = HotelRepository.AddHotel(_hotel);
            var addedHotelId2 = HotelRepository.AddHotel(_hotel);
            var addedHotelId3 = HotelRepository.AddHotel(_hotel);
            _insertedHotels.Add(addedHotelId1);
            _insertedHotels.Add(addedHotelId2);
            _insertedHotels.Add(addedHotelId3);
            var hotelsList = HotelRepository.GetHotels();
            Assert.True(hotelsList.Count >= 3);
        }

        [Test]
        public void UpdateHotel_ExistingHotel_DataIsUpdated()
        {
            var insertedHotelId = HotelRepository.AddHotel(_hotel);
            _insertedHotels.Add(insertedHotelId);
            _hotel.Name = "Upated Name";
            _hotel.PricePerRoom = 999;
            _hotel.Phone = "+58 4241364429";
            _hotel.Website = "http://updatedhotel.com";
            _hotel.AmountOfRooms = 99;
            _hotel.AddressSpecification = "New Address specification";
            var updatedHotel = HotelRepository.UpdateHotel(insertedHotelId, _hotel);
            Assert.AreEqual(_hotel.Name, updatedHotel.Name);
            Assert.AreEqual(_hotel.PricePerRoom, updatedHotel.PricePerRoom);
            Assert.AreEqual(_hotel.Phone, updatedHotel.Phone);
            Assert.AreEqual(_hotel.Website, updatedHotel.Website);
            Assert.AreEqual(_hotel.AmountOfRooms, updatedHotel.AmountOfRooms);
            Assert.AreEqual(_hotel.AddressSpecification, updatedHotel.AddressSpecification);
        }
    }
}