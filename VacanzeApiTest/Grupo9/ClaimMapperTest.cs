using System.Collections.Generic;
using NUnit.Framework;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo9;

namespace vacanze_back.VacanzeApiTest.Grupo9
{
    [TestFixture]
    public class ClaimMapperTest
    {
        private ClaimMapper _claimMapper;
        private ClaimDto _claimDto;
        private List<ClaimDto> _claimDtoList;
        
        [SetUp]
        public void Setup()
        {
            _claimMapper = MapperFactory.CreateClaimMapper();
            _claimDto = new ClaimDto()
            {
                BaggageId = 1,
                Description = "Description",
                Id = 10,
                Status = "CERRADO",
                Title = "Titulo!"
            };
            _claimDtoList = new List<ClaimDto> {_claimDto};
        }
        
        [Test]
        public void CreateEntityListTest()
        {
            var entityList = _claimMapper.CreateEntityList(_claimDtoList);
            Assert.AreEqual(_claimDtoList[0].Title, entityList[0].Title);
        }
    }
}