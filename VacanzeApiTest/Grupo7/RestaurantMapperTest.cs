using System.Collections.Generic;
using NUnit.Framework;
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
        }
        [Test]
        public void CreateEntityListTest()
        {
            var restaurantList = _restaurantMapper.CreateEntityList(_restaurantDtoList);
            Assert.AreEqual(_restaurantDtoList[0].Name,restaurantList[0].Name);
        }
    }
}