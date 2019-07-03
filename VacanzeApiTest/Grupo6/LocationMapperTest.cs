using NUnit.Framework;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.Common.Entities;

namespace vacanze_back.VacanzeApiTest.Grupo6
{
    [TestFixture]
    public class LocationMapperTest
    {
        [Test]
        public void CreateDTOTest()
        {
            LocationMapper _locationMapper = MapperFactory.createLocationMapper();
            Location entity = EntityFactory.CreateLocation(1, "Venezolania","cagua");
            var result = _locationMapper.CreateDTO(entity);
            Assert.IsInstanceOf<LocationDTO>(result);
        }
        [Test]
        public void CreateEntityTest()
        {   
            LocationMapper _locationMapper = MapperFactory.createLocationMapper();        
            LocationDTO LocationDTO = DTOFactory.CreateLocationDTO(1, "Venezolania","cagua");
            var result = _locationMapper.CreateEntity(LocationDTO);
            Assert.IsInstanceOf<Location>(result);
        }
        [Test]
        public void CreateDTOListTest()
        {   
            LocationMapper _locationMapper = MapperFactory.createLocationMapper();         
            Location entity = EntityFactory.CreateLocation(1, "Venezolania","cagua");
            List<Location> entities = new List<Location>();
            entities.Add(entity);
            entities.Add(entity);
            var result = _locationMapper.CreateDTOList(entities);
            Assert.IsInstanceOf<List<LocationDTO>>(result);
        }
        [Test]
        public void CreateEntityListTest()
        {   
            LocationMapper _locationMapper = MapperFactory.createLocationMapper();         
            LocationDTO LocationDTO = DTOFactory.CreateLocationDTO(1, "Venezolania","cagua");
            List<LocationDTO> dtos = new List<LocationDTO>();
            dtos.Add(LocationDTO);
            dtos.Add(LocationDTO);
            var result = _locationMapper.CreateEntityList(dtos);
            Assert.IsInstanceOf<List<Location>>(result);
        }
    }
}