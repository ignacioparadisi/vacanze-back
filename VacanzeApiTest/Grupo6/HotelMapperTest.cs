using NUnit.Framework;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo6;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo6;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Locations;

namespace vacanze_back.VacanzeApiTest.Grupo6
{
    [TestFixture]
    public class HotelMapperTest
    {
        private Hotel _hotel;
        [SetUp]       
        [Order(0)]
        public void Setup()
        {
            _hotel = HotelBuilder.Create()
                .WithName("Hotel herick")
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
        [Test]
        [Order(1)]
        public void HotelBuider_RequiredAttributeException()
        {
            Assert.Throws<RequiredAttributeException>(() => 
            {
                _hotel = HotelBuilder.Create()
                    .WithName("Hotel herick")
                    .WithAmountOfRooms(2000)
                    .WithCapacityPerRoom(2)
                    .WithPricePerRoom(200)
                    .WithPhone("04243240208")
                    .WithWebsite("HC.com")
                    .WithStars(2)
                    .LocatedAt(0)
                    .WithStatus(true)
                    .WithAddressDescription("Calle Los Almendrones")
                    .WithPictureUrl("alguncodigoenbase64")
                    .Build();
            });
        }

        [Test]       
        [Order(2)]
        public void CreateDTOTest()
        {
            HotelMapper _HotelMapper = MapperFactory.createHotelMapper();
            var result = _HotelMapper.CreateDTO(_hotel);
            Assert.IsInstanceOf<HotelDTO>(result);
        }
        [Test]        
        [Order(3)]
        public void CreateEntityTest()
        {   
            HotelMapper _HotelMapper = MapperFactory.createHotelMapper();        
            HotelDTO HotelDTO = DTOFactory.CreateHotelDTO(_hotel.Id, _hotel.Name, _hotel.AmountOfRooms, 
                                            _hotel.RoomCapacity , _hotel.IsActive, _hotel.AddressSpecification,
                                            _hotel.PricePerRoom, _hotel.Website, _hotel.Phone, _hotel.Picture,
                                            _hotel.Stars, _hotel.Location.Id);
            var result = _HotelMapper.CreateEntity(HotelDTO);
            Assert.IsInstanceOf<Hotel>(result);
        }
        [Test]       
        [Order(4)]
        public void CreateDTOListTest()
        {   
            HotelMapper _HotelMapper = MapperFactory.createHotelMapper();        
            List<Hotel> entities = new List<Hotel>();
            entities.Add(_hotel);
            entities.Add(_hotel);
            var result = _HotelMapper.CreateDTOList(entities);
            Assert.IsInstanceOf<List<HotelDTO>>(result);
        }
        [Test]
        [Order(5)]    
        public void CreateEntityListTest()
        {   
            HotelMapper _HotelMapper = MapperFactory.createHotelMapper();        
            HotelDTO HotelDTO = DTOFactory.CreateHotelDTO(_hotel.Id, _hotel.Name, _hotel.AmountOfRooms, 
                                            _hotel.RoomCapacity , _hotel.IsActive, _hotel.AddressSpecification,
                                            _hotel.PricePerRoom, _hotel.Website, _hotel.Phone, _hotel.Picture,
                                            _hotel.Stars, _hotel.Location.Id);
            List<HotelDTO> dtos = new List<HotelDTO>();
            dtos.Add(HotelDTO);
            dtos.Add(HotelDTO);
            var result = _HotelMapper.CreateEntityList(dtos);
            Assert.IsInstanceOf<List<Hotel>>(result);
        }
        [Test]       
        [Order(6)]
        public void CreateDTOTest_RequiredAttributeException()
        {
             Assert.Throws<RequiredAttributeException>(() =>
            {
                HotelMapper _HotelMapper = MapperFactory.createHotelMapper();
                var result = _HotelMapper.CreateDTO(null);
                Assert.IsInstanceOf<HotelDTO>(result);
            });
        }
        [Test]        
        [Order(7)]
        public void CreateEntityTest_RequiredAttributeException()
        {                
            Assert.Throws<RequiredAttributeException>(() =>
            {           
            HotelMapper _HotelMapper = MapperFactory.createHotelMapper();   
            var result = _HotelMapper.CreateEntity(null);
            });
        }
        [Test]       
        [Order(8)]
        public void CreateDTOTest_RequiredAttributeException_location()
        {
             Assert.Throws<RequiredAttributeException>(() =>
            {
                _hotel.Location = null;
                HotelMapper _HotelMapper = MapperFactory.createHotelMapper();
                var result = _HotelMapper.CreateDTO(_hotel);
            });
        }
                [Test]       
        [Order(9)]
        public void CreateEtityTest_RequiredAttributeException_location()
        {
             Assert.Throws<RequiredAttributeException>(() =>
            {
                       
                HotelDTO HotelDTO = DTOFactory.CreateHotelDTO(_hotel.Id, _hotel.Name, _hotel.AmountOfRooms, 
                                            _hotel.RoomCapacity , _hotel.IsActive, _hotel.AddressSpecification,
                                            _hotel.PricePerRoom, _hotel.Website, _hotel.Phone, _hotel.Picture,
                                            _hotel.Stars, _hotel.Location.Id);
                
                HotelDTO.Location = null;                     
                HotelMapper _HotelMapper = MapperFactory.createHotelMapper();
                var result = _HotelMapper.CreateEntity(HotelDTO);
            });
        }
        
        [Test]       
        [Order(10)]
        public void EntitySetAvaibleRoomTest()
        {
            _hotel.AvailableRooms = 100;
           Assert.AreEqual(_hotel.AvailableRooms,100);
        }
        
        [Test]       
        [Order(11)]
        public void EntityGetAvaibleRoomTest()
        { 
            _hotel.AvailableRooms = 100;
            var respuesta = _hotel.AvailableRooms;
            Assert.AreEqual(respuesta, 100);
        }
        
        [Test]       
        [Order(12)]
        public void EntityShouldSerializableAvaibleTest()
        {
            _hotel.AvailableRooms = -1;
            var respuesta = _hotel.ShouldSerializeAvailableRooms();
            Assert.AreEqual(false,respuesta);
        }
        
        [Test]       
        [Order(13)]
        public void DtoGetAvaibleRoomTest()
        {
            HotelDTO HotelDTO = DTOFactory.CreateHotelDTO(_hotel.Id, _hotel.Name, _hotel.AmountOfRooms, 
                _hotel.RoomCapacity , _hotel.IsActive, _hotel.AddressSpecification,
                _hotel.PricePerRoom, _hotel.Website, _hotel.Phone, _hotel.Picture,
                _hotel.Stars, _hotel.Location.Id);
            HotelDTO.AvailableRooms = 1000;
            var respuesta = HotelDTO.AvailableRooms;
            Assert.AreEqual(respuesta,1000);
        }
                  
         
        [Test]       
        [Order(14)]
        public void DtoShouldSerializableTest()
        {
            GetLocationByIdCommand command = CommandFactory.GetLocationByIdCommand(_hotel.Location.Id);
            Location location= command.GetResult();
            HotelDTO HotelDTO =new HotelDTO( _hotel.Name, _hotel.AmountOfRooms, 
                _hotel.RoomCapacity , _hotel.IsActive, _hotel.AddressSpecification,
                _hotel.PricePerRoom, _hotel.Website, _hotel.Phone, _hotel.Picture,
                _hotel.Stars, location);
            HotelDTO.AvailableRooms = 1000;
            var respuesta = HotelDTO.ShouldSerializeAvailableRooms();
            Assert.AreEqual(respuesta,true);
        }
    }
}