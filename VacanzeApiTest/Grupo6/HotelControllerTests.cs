using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo6;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6;
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo6;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo6;

namespace vacanze_back.VacanzeApiTest.Grupo6
{
    [TestFixture]
    public class HotelControllerTests
    {
        [SetUp]
        public void Setup()
        {
            _hotelsController = new HotelsController(null);
            _insertedHotels = new List<int>();
            _hotelentity = HotelBuilder.Create()
                .WithName("Hotel Cagua")
                .WithAmountOfRooms(2000)
                .WithCapacityPerRoom(2)
                .WithPricePerRoom(200)
                .WithPhone("04243240208")
                .WithWebsite("HC.com")
                .WithStars(2)
                .LocatedAt(HotelTestSetup.LOCATION_ID)
                .WithStatus(true)
                .WithAddressDescription("Calle Los Almendrones")
                .WithPictureUrl("alguncodigoenbase64")
                .Build();
            HotelMapper _HotelMapper = MapperFactory.createHotelMapper();
            _hotel= _HotelMapper.CreateDTO(_hotelentity);
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var hotelId in _insertedHotels)             
            {
                DeleteHotelCommand DeleteHotel =  CommandFactory.DeleteHotelCommand(hotelId);
                DeleteHotel.Execute ();   
            }
            _insertedHotels.Clear();
        }

        private HotelsController _hotelsController;
        private HotelDTO _hotel;
        private Hotel _hotelentity;
        private List<int> _insertedHotels;
        
        [Test] //esta prueba no a entiendo herick
        [Order(0)]
        public void Create_ValidHotelData_CreatedAtActionReturned()
        {
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            // Get ID of the Hotel that was saved to delete it from the database at teardown
            var createdAction = (CreatedAtActionResult) result.Result;
            var idToDelete = ((HotelDTO) createdAction.Value).Id;
            _insertedHotels.Add(idToDelete);
        }

        [Test]
        [Order(1)]
        public void Delete_InvalidHotelId_OkResultReturned()
        {
            var result = _hotelsController.Delete(0);
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        [Order(2)]
        public void Delete_ValidHotelId_OkResultReturned()
        {
         
            AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotelentity);
            AddHotel.Execute ();
            var savedHotelId = AddHotel.GetResult(); 
            
            var deleteResult = _hotelsController.Delete(savedHotelId);
            Assert.IsInstanceOf<OkResult>(deleteResult);

            GetHotelByIdCommand GetHotelById =  CommandFactory.GetHotelByIdCommand(savedHotelId);

            Assert.Throws<HotelNotFoundException>(() => GetHotelById.Execute ());
        }

        [Test]
        [Order(3)]
        public void Get_InvalidLocationId_EmptyListReturned()
        {
            var result = _hotelsController.Get(0);
            Assert.AreEqual(0, result.Value.Count());
        }

        [Test]
        [Order(4)]
        public void Get_NoLocationSpecified_HotelsReturned()
        {
            var result = _hotelsController.Get();
            Assert.NotNull(result.Value);
        }

        [Test]
        [Order(5)]
        public void Get_ValidLocationId_HotelsInLocationReturned()
        {            
            AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotelentity);
            AddHotel.Execute ();
            var savedHotelId = AddHotel.GetResult(); 
            _insertedHotels.Add(savedHotelId);
            var result = _hotelsController.Get(HotelTestSetup.LOCATION_ID);
            Assert.AreEqual(1, result.Value.Count());
        }

        [Test]
        [Order(6)]
        public void GetById_InvalidHotelId_NotFoundResultReturned()
        {
            var result = _hotelsController.GetById(0);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        [Order(7)]
        public void GetById_ValidHotelId_OkResultReturned()
        {            
            AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotelentity);
            AddHotel.Execute ();
            var savedHotelId = AddHotel.GetResult(); 

            _insertedHotels.Add(savedHotelId);
            var result = _hotelsController.GetById(savedHotelId);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        [Order(8)]
        public void Update_HotelWithHigherBoundStarAmount_BadRequestReturned()
        {
            AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotelentity);
            AddHotel.Execute ();
            var insertedHotelId = AddHotel.GetResult(); 
            
            _insertedHotels.Add(insertedHotelId);
            _hotel.Stars = 7;
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        [Order(9)]
        public void Update_HotelWithInvalidLocation_BadRequestReturned()
        {
            AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotelentity);
            AddHotel.Execute ();
            var insertedHotelId = AddHotel.GetResult(); 

            _insertedHotels.Add(insertedHotelId);
            _hotel.Location.Id = 0;
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        [Order(10)]
        public void Update_HotelWithLowerBoundStarAmount_BadRequestReturned()
        {
            
            AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotelentity);
            AddHotel.Execute ();
            var insertedHotelId = AddHotel.GetResult(); 

            _insertedHotels.Add(insertedHotelId);
            _hotel.Stars = 0;
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        [Order(11)]
        public void Update_HotelWithNegativeAmountOfRooms_BadRequestReturned()
        {
            
            AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotelentity);
            AddHotel.Execute ();
            var insertedHotelId = AddHotel.GetResult(); 

            _insertedHotels.Add(insertedHotelId);
            _hotel.AmountOfRooms = -10;
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        [Order(12)]
        public void Update_HotelWithNegativePricePerRoom_BadRequestReturned()
        {
            
            AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotelentity);
            AddHotel.Execute ();
            var insertedHotelId = AddHotel.GetResult(); 

            _insertedHotels.Add(insertedHotelId);
            _hotel.PricePerRoom = -10;
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        [Order(13)]
        public void Update_HotelWithNegativeRoomCapacity_BadRequestReturned()
        {
            
            AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotelentity);
            AddHotel.Execute ();
            var insertedHotelId = AddHotel.GetResult(); 

            _insertedHotels.Add(insertedHotelId);
            _hotel.RoomCapacity = -10;
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        [Order(14)]
        public void Update_HotelWithoutAddressSpecs_BadRequestReturned()
        {
            AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotelentity);
            AddHotel.Execute ();
            var insertedHotelId = AddHotel.GetResult(); 

            _insertedHotels.Add(insertedHotelId);
            _hotel.AddressSpecification = null;
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        [Order(15)]
        public void Update_HotelWithoutLocation_BadRequestReturned()
        {
            
            AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotelentity);
            AddHotel.Execute ();
            var insertedHotelId = AddHotel.GetResult(); 

            _insertedHotels.Add(insertedHotelId);
            _hotel.Location = null;
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        [Order(16)]
        public void Update_HotelWithoutName_BadRequestReturned()
        {
            
            AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotelentity);
            AddHotel.Execute ();
            var insertedHotelId = AddHotel.GetResult(); 

            _insertedHotels.Add(insertedHotelId);
            _hotel.Name = null;
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        [Order(17)]
        public void Update_InvalidHotelId_NotFoundResultReturned()
        {
            var result = _hotelsController.Update(0, _hotel);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        [Order(18)]
        public void Update_ValidHotel_OkResultReturned()
        {
            AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotelentity);
            AddHotel.Execute ();
            var insertedHotelId = AddHotel.GetResult(); 

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
        [Order(19)]
        public void Update_ValidHotel_UpdatedDataReturned()
        {
            
            AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotelentity);
            AddHotel.Execute ();
            var insertedHotelId = AddHotel.GetResult(); 

            _insertedHotels.Add(insertedHotelId);
            _hotel.Name = "Upated Name";
            _hotel.PricePerRoom = 999;
            _hotel.Phone = "+58 4241364429";
            _hotel.Website = "http://updatedhotel.com";
            _hotel.AmountOfRooms = 99;
            _hotel.AddressSpecification = "New Address specification";
            var result = _hotelsController.Update(insertedHotelId, _hotel);
            var casted = (OkObjectResult) result.Result;
            var updatedHotel = (HotelDTO) casted.Value;
            Assert.AreEqual(_hotel.Name, updatedHotel.Name);
            Assert.AreEqual(_hotel.PricePerRoom, updatedHotel.PricePerRoom);
            Assert.AreEqual(_hotel.Phone, updatedHotel.Phone);
            Assert.AreEqual(_hotel.Website, updatedHotel.Website);
            Assert.AreEqual(_hotel.AmountOfRooms, updatedHotel.AmountOfRooms);
            Assert.AreEqual(_hotel.AddressSpecification, updatedHotel.AddressSpecification);
        }
        [Test]
        [Order(20)]
        public void GetHotelImageTest()
        {
            
            AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotelentity);
            AddHotel.Execute ();
            var insertedHotelId = AddHotel.GetResult(); 

            _insertedHotels.Add(insertedHotelId);
            var base64ImageCode = _hotelsController.GetHotelImage(insertedHotelId);
            Assert.IsNotNull(base64ImageCode);
        }
        [Test]
        [Order(21)]
        public void Create_HotelWithHigherBoundStarAmount_BadRequestReturned()
        {
            _hotel.Stars = 7;
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        [Order(22)]
        public void Create_HotelWithInvalidLocation_BadRequestReturned()
        {
            _hotel.Location.Id = 0;
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        [Order(23)]
        public void Create_HotelWithLowerBoundStarAmount_BadRequestReturned()
        {
            _hotel.Stars = 0;
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        [Order(24)]
        public void Create_HotelWithNegativeAmountOfRooms_BadRequestReturned()
        {
            _hotel.AmountOfRooms = -10;
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        [Order(25)]
        public void Create_HotelWithNegativePricePerRoom_BadRequestReturned()
        {
            _hotel.PricePerRoom = -10;
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        [Order(26)]
        public void Create_HotelWithNegativeRoomCapacity_BadRequestReturned()
        {
            _hotel.RoomCapacity = -10;
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        [Order(27)]
        public void Create_HotelWithoutAddressSpecs_BadRequestReturned()
        {
            _hotel.AddressSpecification = null;
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        [Order(28)]
        public void Create_HotelWithoutLocation_BadRequestReturned()
        {
            _hotel.Location = null;
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        [Order(29)]
        public void Create_HotelWithoutName_BadRequestReturned()
        {
            _hotel.Name = null;
            var result = _hotelsController.Create(_hotel);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
    }
}