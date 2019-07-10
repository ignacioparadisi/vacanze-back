using System.Collections.Generic;
using NUnit.Framework;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo9;

namespace vacanze_back.VacanzeApiTest.Grupo9
{
    [TestFixture]
    public class BaggageMapperTest
    {
        private BaggageMapper _baggageMapper;
        private BaggageDTO _baggageDto;
        private List<BaggageDTO> _baggageDtos;

        [Test]
        public void SetUp()
        {
            _baggageMapper = MapperFactory.CreateBaggageMapper();
            _baggageDto = new BaggageDTO()
            {
                Id = 1,
                Description = "Maleta negra",
                Status = "ENCONTRADO"
            };
            _baggageDtos = new List<BaggageDTO> {_baggageDto};
        }

        [Test]
        public void CreateEntityListTest()
        {
            var entityList = _baggageMapper.CreateEntityList(_baggageDtos);
            Assert.AreEqual(_baggageDtos[0].Description, entityList[0].Description);
        }
    }
}