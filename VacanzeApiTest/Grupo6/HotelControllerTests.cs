using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo6;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo6;

namespace vacanze_back.VacanzeApiTest.Grupo6
{
    [TestFixture]
    public class HotelControllerTests
    {
        [SetUp]
        public void Setup()
        {
            _hotelsController = new HotelsController();
            _insertedHotels = new List<int>();
            _hotel = HotelBuilder.Create()
                .WithName("Hotel Cagua")
                .WithAmountOfRooms(2000)
                .WithCapacityPerRoom(2)
                .WithPricePerRoom(200)
                .WithPhone("04243240208")
                .WithWebsite("HC.com")
                .WithStars(2)
                .LocatedAt(LocationRepository.GetLocationById(HotelTestSetup.LOCATION_ID))
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

        private HotelsController _hotelsController;
        private Hotel _hotel;
        private List<int> _insertedHotels;

        [Test]
        public void Create_HotelWithHigherBoundStarAmount_BadRequestReturned()
        {
            _hotel.Stars = 7;
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Create_HotelWithInvalidLocation_BadRequestReturned()
        {
            _hotel.Location.Id = 0;
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Create_HotelWithLowerBoundStarAmount_BadRequestReturned()
        {
            _hotel.Stars = 0;
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Create_HotelWithNegativeAmountOfRooms_BadRequestReturned()
        {
            _hotel.AmountOfRooms = -10;
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Create_HotelWithNegativePricePerRoom_BadRequestReturned()
        {
            _hotel.PricePerRoom = -10;
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Create_HotelWithNegativeRoomCapacity_BadRequestReturned()
        {
            _hotel.RoomCapacity = -10;
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Create_HotelWithoutAddressSpecs_BadRequestReturned()
        {
            _hotel.AddressSpecification = null;
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Create_HotelWithoutLocation_BadRequestReturned()
        {
            _hotel.Location = null;
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Create_HotelWithoutName_BadRequestReturned()
        {
            _hotel.Name = null;
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Create_ValidHotelData_CreatedAtActionReturned()
        {
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            // Get ID of the Hotel that was saved to delete it from the database at teardown
            var createdAction = (CreatedAtActionResult) result.Result;
            var idToDelete = ((Hotel) createdAction.Value).Id;
            _insertedHotels.Add(idToDelete);
        }

        [Test]
        public void Delete_InvalidHotelId_OkResultReturned()
        {
            var result = _hotelsController.Delete(0);
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void Delete_ValidHotelId_OkResultReturned()
        {
            var savedHotelId = HotelRepository.AddHotel(_hotel);
            var deleteResult = _hotelsController.Delete(savedHotelId);
            Assert.IsInstanceOf<OkResult>(deleteResult);
            Assert.Throws<HotelNotFoundException>(() => HotelRepository.GetHotelById(savedHotelId));
        }

        [Test]
        public void Get_InvalidLocationId_EmptyListReturned()
        {
            var result = _hotelsController.Get(0);
            Assert.AreEqual(0, result.Value.Count());
        }

        [Test]
        public void Get_NoLocationSpecified_HotelsReturned()
        {
            var result = _hotelsController.Get();
            Assert.NotNull(result.Value);
        }

        [Test]
        public void Get_ValidLocationId_HotelsInLocationReturned()
        {
            var savedHotelId = HotelRepository.AddHotel(_hotel);
            _insertedHotels.Add(savedHotelId);
            var result = _hotelsController.Get(HotelTestSetup.LOCATION_ID);
            Assert.AreEqual(1, result.Value.Count());
        }

        [Test]
        public void GetById_InvalidHotelId_NotFoundResultReturned()
        {
            var result = _hotelsController.GetById(0);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public void GetById_ValidHotelId_OkResultReturned()
        {
            var savedHotelId = HotelRepository.AddHotel(_hotel);
            _insertedHotels.Add(savedHotelId);
            var result = _hotelsController.GetById(savedHotelId);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }


        [Test]
        public void Update_HotelWithHigherBoundStarAmount_BadRequestReturned()
        {
            var insertedHotelId = HotelRepository.AddHotel(_hotel);
            _insertedHotels.Add(insertedHotelId);
            _hotel.Stars = 7;
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Update_HotelWithInvalidLocation_BadRequestReturned()
        {
            var insertedHotelId = HotelRepository.AddHotel(_hotel);
            _insertedHotels.Add(insertedHotelId);
            _hotel.Location.Id = 0;
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Update_HotelWithLowerBoundStarAmount_BadRequestReturned()
        {
            var insertedHotelId = HotelRepository.AddHotel(_hotel);
            _insertedHotels.Add(insertedHotelId);
            _hotel.Stars = 0;
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Update_HotelWithNegativeAmountOfRooms_BadRequestReturned()
        {
            var insertedHotelId = HotelRepository.AddHotel(_hotel);
            _insertedHotels.Add(insertedHotelId);
            _hotel.AmountOfRooms = -10;
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Update_HotelWithNegativePricePerRoom_BadRequestReturned()
        {
            var insertedHotelId = HotelRepository.AddHotel(_hotel);
            _insertedHotels.Add(insertedHotelId);
            _hotel.PricePerRoom = -10;
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Update_HotelWithNegativeRoomCapacity_BadRequestReturned()
        {
            var insertedHotelId = HotelRepository.AddHotel(_hotel);
            _insertedHotels.Add(insertedHotelId);
            _hotel.RoomCapacity = -10;
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Update_HotelWithoutAddressSpecs_BadRequestReturned()
        {
            var insertedHotelId = HotelRepository.AddHotel(_hotel);
            _insertedHotels.Add(insertedHotelId);
            _hotel.AddressSpecification = null;
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Update_HotelWithoutLocation_BadRequestReturned()
        {
            var insertedHotelId = HotelRepository.AddHotel(_hotel);
            _insertedHotels.Add(insertedHotelId);
            _hotel.Location = null;
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Update_HotelWithoutName_BadRequestReturned()
        {
            var insertedHotelId = HotelRepository.AddHotel(_hotel);
            _insertedHotels.Add(insertedHotelId);
            _hotel.Name = null;
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Update_InvalidHotelId_NotFoundResultReturned()
        {
            var result = _hotelsController.Update(0, _hotel);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public void Update_ValidHotel_OkResultReturned()
        {
            var insertedHotelId = HotelRepository.AddHotel(_hotel);
            _insertedHotels.Add(insertedHotelId);
            _hotel.Name = "Upated Name";
            _hotel.PricePerRoom = 999;
            _hotel.Phone = "+58 4241364429";
            _hotel.Website = "http://updatedhotel.com";
            _hotel.AmountOfRooms = 99;
            _hotel.AddressSpecification = "New Address specification";
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void Update_ValidHotel_UpdatedDataReturned()
        {
            var insertedHotelId = HotelRepository.AddHotel(_hotel);
            _insertedHotels.Add(insertedHotelId);
            _hotel.Name = "Upated Name";
            _hotel.PricePerRoom = 999;
            _hotel.Phone = "+58 4241364429";
            _hotel.Website = "http://updatedhotel.com";
            _hotel.AmountOfRooms = 99;
            _hotel.AddressSpecification = "New Address specification";
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            var casted = (OkObjectResult) result.Result;
            var updatedHotel = (Hotel) casted.Value;
            Assert.AreEqual(_hotel.Name, updatedHotel.Name);
            Assert.AreEqual(_hotel.PricePerRoom, updatedHotel.PricePerRoom);
            Assert.AreEqual(_hotel.Phone, updatedHotel.Phone);
            Assert.AreEqual(_hotel.Website, updatedHotel.Website);
            Assert.AreEqual(_hotel.AmountOfRooms, updatedHotel.AmountOfRooms);
            Assert.AreEqual(_hotel.AddressSpecification, updatedHotel.AddressSpecification);
        }
        [Test]
        public void GetHotelImageTest()
        {
            var base64ImageCode = _hotelsController.GetHotelImage(29);
            Assert.IsNotNull(base64ImageCode);
        }
    }
}