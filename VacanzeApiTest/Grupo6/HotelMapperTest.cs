using NUnit.Framework;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo6;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo6;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;

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
                .LocatedAt(77)
                .WithStatus(true)
                .WithAddressDescription("Calle Los Almendrones")
                .WithPictureUrl("alguncodigoenbase64")
                .Build();
        }
        [Test]       
        [Order(1)]
        public void CreateDTOTest()
        {
            HotelMapper _HotelMapper = MapperFactory.createHotelMapper();
            var result = _HotelMapper.CreateDTO(_hotel);
            Assert.IsInstanceOf<HotelDTO>(result);
        }
        [Test]        
        [Order(2)]
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
        [Order(3)]
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
        [Order(4)]    
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
    }
}