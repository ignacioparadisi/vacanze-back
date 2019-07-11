using System.Collections.Generic;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo7;

namespace vacanze_back.VacanzeApiTest.Grupo7
{
    [TestFixture]
    public class RestaurantMapperTest
    {
        private RestaurantMapper _restaurantMapper;
        private List<RestaurantDto> _restaurantDtoList;
        private RestaurantDto _restaurantDto;
        private Restaurant _restaurant;
        private List<Restaurant> _restaurantList;
        [SetUp]
        public void Setup()
        {
            _restaurantDto = new RestaurantDto();
            _restaurantDto.Name = "Yosemite";
            _restaurantDto.Capacity = 200;
            _restaurantDto.IsActive = true;
            _restaurantDto.Qualify = 4;
            _restaurantDto.Specialty = "Tsingy";
            _restaurantDto.Price = 21;
            _restaurantDto.BusinessName = "Wadsdworth Longfellow";
            _restaurantDto.Picture = "A great picture of the Dead Sea could be here, you know?";
            _restaurantDto.Description =
                "Listen, strange women lying in ponds distributing swords is no basis for a system of government… You can’t expect to wield supreme power just ‘cause some watery tart threw a sword at you!";
            _restaurantDto.Phone = "021221546352";
            _restaurantDto.Location = 1;
            _restaurantDto.Address = "As it turns out, Mount Kilimanjaro is not wi-fi enabled, so I had to spend two weeks in Tanzania talking to the people on my trip.";
            _restaurantMapper = MapperFactory.CreateRestaurantMapper();
            _restaurantDtoList = new List<RestaurantDto>();
            _restaurantDtoList.Add(_restaurantDto);
            _restaurant = EntityFactory.CreateRestaurant(0, "Yosemite", 200, true, 4, "Tsingy",
                21, "Wadsdworth Longfellow", "A great picture of the Dead Sea could be here, you know?",
                "Listen, strange women lying in ponds distributing swords is no basis for a system of government… You can’t expect to wield supreme power just ‘cause some watery tart threw a sword at you!",
                "021221546352", 1,
                "As it turns out, Mount Kilimanjaro is not wi-fi enabled, so I had to spend two weeks in Tanzania talking to the people on my trip.");
            _restaurantList = new List<Restaurant>();
            _restaurantList.Add(_restaurant);
        }
        
        [Test]
        public void CreateEntityTest()
        {
            var restaurant = _restaurantMapper.CreateEntity(_restaurantDto);
            Assert.AreEqual(_restaurantDto.Name,restaurant.Name);
        }
        
        [Test]
        public void CreateDtoTest()
        {
            var restaurantDto = _restaurantMapper.CreateDTO(_restaurant);
            Assert.AreEqual(_restaurant.Name, restaurantDto.Name);
        }
        
        [Test]
        public void CreateDtoListTest()
        {
            var restaurantDtoList = _restaurantMapper.CreateDTOList(_restaurantList);
            Assert.AreEqual(_restaurantList.Count,restaurantDtoList.Count);
        }
        
        [Test]
        public void CreateEntityListTest()
        {
            var restaurantList = _restaurantMapper.CreateEntityList(_restaurantDtoList);
            Assert.AreEqual(_restaurantDtoList.Count,restaurantList.Count);
        }
    }
}