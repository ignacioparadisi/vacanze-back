using System.Collections.Generic;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo6;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo6;

namespace vacanze_back.VacanzeApiTest.Grupo6
{
    [TestFixture]
    public class HotelCommandoTest
    {
        [SetUp]
        public void Setup()
        {
            _hotel = HotelBuilder.Create()
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

        private Hotel _hotel;
        private readonly List<int> _insertedHotels = new List<int>();

        [Test]
        public void AddHotel_HotelWithInvalidLocation_ExceptionThrown()
        {
            Assert.Throws<InvalidAttributeException>(() =>
            {
                _hotel.Location.Id = 99999;
                    AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotel);
                    AddHotel.Execute (); 
            });
        }

        [Test]
        public void AddHotel_ValidHotel_NoExceptionThrown()
        {
            Assert.DoesNotThrow(() =>
            {
                
                AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotel);
                AddHotel.Execute ();
                var addedHotelId = AddHotel.GetResult(); 

                GetHotelByIdCommand GetHotelById =  CommandFactory.GetHotelByIdCommand(addedHotelId);
                GetHotelById.Execute ();
                _insertedHotels.Add(addedHotelId);
            });
        }

        [Test]
        public void DeleteHotel_InvalidHotelId_NoExceptionThrown()
        {
            Assert.DoesNotThrow(() => { 
                DeleteHotelCommand DeleteHotel =  CommandFactory.DeleteHotelCommand(12093);
                DeleteHotel.Execute ();           
            });
        }

        [Test]
        public void DeleteHotel_ValidHotelId_HotelNotFound()
        {
            Assert.Throws<HotelNotFoundException>(() =>
            {
                AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotel);
                AddHotel.Execute ();
                var addedHotelId = AddHotel.GetResult(); 

                GetHotelByIdCommand GetHotelById =  CommandFactory.GetHotelByIdCommand(addedHotelId);
                GetHotelById.Execute ();
            
                DeleteHotelCommand DeleteHotel =  CommandFactory.DeleteHotelCommand(addedHotelId);
                DeleteHotel.Execute ();                  
                
                GetHotelById =  CommandFactory.GetHotelByIdCommand(addedHotelId);
                GetHotelById.Execute();
            });
        }

        [Test]
        public void GetHotelById_InvalidHotelId_ExceptionThrown()
        {
            Assert.Throws<HotelNotFoundException>(() => 
            {
                GetHotelByIdCommand GetHotelById =  CommandFactory.GetHotelByIdCommand(10928);
                GetHotelById.Execute();
            });
        }

        [Test]
        public void GetHotelById_ValidHotelId_CorrectDataReturned()
        {
            
            AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotel);
            AddHotel.Execute();
            var hotelId = AddHotel.GetResult(); 
            _insertedHotels.Add(hotelId);
                            
            GetHotelByIdCommand GetHotelById =  CommandFactory.GetHotelByIdCommand(hotelId);
            GetHotelById.Execute();
            var getHotel = GetHotelById.GetResult();

            Assert.AreEqual(hotelId, getHotel.Id);
        }

        [Test]
        public void GetHotelsByCity_HotelsInCity_CorrectDataReturned()
        {
            int i=0;
            while (i<3)
            {             
                AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotel);
                AddHotel.Execute();
                var hotelId = AddHotel.GetResult();
                 _insertedHotels.Add(hotelId);
                i++;
            }

            GetHotelsByCityCommand GetHotelsByCity =  CommandFactory.GetHotelsByCityCommand(HotelTestSetup.LOCATION_ID);
            GetHotelsByCity.Execute();
            var found = GetHotelsByCity.GetResult();

            Assert.AreEqual(3, found.Count);
        }

        [Test]
        public void GetHotelsTest()
        {
            int i=0;
            while (i<3)
            {             
                AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotel);
                AddHotel.Execute();
                var hotelId = AddHotel.GetResult();
                 _insertedHotels.Add(hotelId);
                i++;
            }
                       
            GetHotelsCommand GetHotels =  CommandFactory.GetHotelsCommand();
            GetHotels.Execute();
            var hotelsList = GetHotels.GetResult();
            
            Assert.True(hotelsList.Count >= 3);
        }

        [Test]
        public void UpdateHotel_ExistingHotel_DataIsUpdated()
        {
            AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotel);
            AddHotel.Execute();
            var insertedHotelId = AddHotel.GetResult(); 
            _insertedHotels.Add(insertedHotelId);

            _hotel.Name = "Upated Name";
            _hotel.PricePerRoom = 999;
            _hotel.Phone = "+58 4241364429";
            _hotel.Website = "http://updatedhotel.com";
            _hotel.AmountOfRooms = 99;
            _hotel.AddressSpecification = "New Address specification";

            UpdateHotelCommand UpdateHotel =  CommandFactory.UpdateHotelCommand(insertedHotelId, _hotel);
            UpdateHotel.Execute();
            var updatedHotel = UpdateHotel.GetResult(); 

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
            AddHotelCommand AddHotel =  CommandFactory.createAddHotelCommand(_hotel);
            AddHotel.Execute();
            var insertedHotelId = AddHotel.GetResult(); 
            _insertedHotels.Add(insertedHotelId);
            
            GetHotelImageCommand GetHotelImage =  CommandFactory.GetHotelImageCommand(insertedHotelId);
            GetHotelImage.Execute();
            var base64ImageCode = GetHotelImage.GetResult(); 
            Assert.IsNotNull(base64ImageCode);
        }
        [Test]
        public void ValidHotelCommand_NoExceptionThrown()
        {
            Assert.DoesNotThrow(() =>
            {         
                HotelValidatorCommand command =  CommandFactory.HotelValidatorCommand(_hotel);
                command.Execute ();
            });
        }
        [Test]
        public void ValidHotelDTOCommand_NoExceptionThrown()
        {
            Assert.DoesNotThrow(() =>
            {         
                
                HotelMapper _HotelMapper = MapperFactory.createHotelMapper();
                HotelDTO result = _HotelMapper.CreateDTO(_hotel);
                HotelDTOValidatorCommand command =  CommandFactory.HotelDTOValidatorCommand(result);
                command.Execute ();
            });
        }
        [Test]
        public void ValidHotelCommand_ExceptionThrown()
        {
            Assert.Throws<RequiredAttributeException>(() =>
            {         
                HotelValidatorCommand command =  CommandFactory.HotelValidatorCommand(null);
                command.Execute ();
            });
        }
        [Test]
        public void ValidHotelDTOCommand_ExceptionThrown()
        {
            Assert.Throws<RequiredAttributeException>(() =>
            {         
                HotelDTOValidatorCommand command =  CommandFactory.HotelDTOValidatorCommand(null);
                command.Execute ();
            });
        }
        [Test]
        public void ValidHotelCommand_LocationExceptionThrown()
        {
            Assert.Throws<RequiredAttributeException>(() =>
            {         
                _hotel.Location = null;
                HotelValidatorCommand command =  CommandFactory.HotelValidatorCommand(_hotel);
                command.Execute ();
            });
        }
        [Test]
        public void ValidHotelDTOCommand_LocationExceptionThrown()
        {
            Assert.Throws<RequiredAttributeException>(() =>
            {      
                HotelMapper _HotelMapper = MapperFactory.createHotelMapper();
                HotelDTO result = _HotelMapper.CreateDTO(_hotel);
                result.Location= null;
                HotelDTOValidatorCommand command =  CommandFactory.HotelDTOValidatorCommand(result);
                command.Execute ();
            });
        }
    }
}