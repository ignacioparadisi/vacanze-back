using System.Collections.Generic;
using NUnit.Framework;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo8;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;

namespace vacanze_back.VacanzeApiTest.Grupo8
{
    [TestFixture]
    public class CruiserMapperTest
    {
        private CruiserMapper _cruiserMapper;
        private List<CruiserDTO> _cruiserDTOList;
        private List<Cruiser> _cruiserList;
        private CruiserDTO _cruiserDTO;
        private Cruiser _cruiser;
        private LayoverMapper _layoverMapper;
        private List<LayoverDTO> _layoverDTOList;
        private List<Layover> _layoverList;
        private LayoverDTO _layoverDTO;
        private Layover _layover;
        [SetUp]
        public void Setup()
        {
            _cruiser = new Cruiser("The great seeker of wonder" , true , 220 , 200 , "A big one" , "A tender line", "I suppose a picture is in order");
            _cruiserMapper = MapperFactory.CreateCruiserMapper();
            _cruiserDTOList = new List<CruiserDTO>();
            _cruiserList = new List<Cruiser>();
            _layover = new Layover(0,"02/12/2019","05/01/2020", 250, 1, 2);
            _layoverMapper = MapperFactory.CreateLayoverMapper();
            _layoverDTOList = new List<LayoverDTO>();
            _layoverList = new List<Layover>();

        }

        [Test, Order(1)]
        public void CreateCruiserDTOTest()
        {
            _cruiserDTO = _cruiserMapper.CreateDTO(_cruiser);
            _cruiserDTOList.Add(_cruiserDTO);
            Assert.AreEqual(_cruiserDTO.Name, _cruiser.Name);
        }

        [Test, Order(2)]
        public void CreateCruiserEntityTest()
        {
            _cruiser = _cruiserMapper.CreateEntity(_cruiserDTO);
            Assert.AreEqual(_cruiserDTO.Name, _cruiser.Name);
        }

        [Test, Order(3)]
        public void CreateCruiserEntityListTest()
        {
            _cruiserDTOList.Add(_cruiserDTO);
            _cruiserList = _cruiserMapper.CreateEntityList(_cruiserDTOList);
            Assert.IsTrue(_cruiserList.Count > 0);
        }

        [Test, Order(4)]
        public void CreateCruiserDTOListTest()
        {
            _cruiserList.Add(_cruiser);
            _cruiserDTOList = _cruiserMapper.CreateDTOList(_cruiserList);
            Assert.IsTrue(_cruiserDTOList.Count > 0);
        }

        [Test, Order(5)]
        public void CreateLayoverDTOTest()
        {
            _layoverDTO = _layoverMapper.CreateDTO(_layover);
            _layoverDTOList.Add(_layoverDTO);
            Assert.AreEqual(_layoverDTO.DepartureDate, _layover.DepartureDate);
        }

        [Test, Order(6)]
        public void CreateLayoverEntityTest()
        {
            _layover = _layoverMapper.CreateEntity(_layoverDTO);
            Assert.AreEqual(_layoverDTO.DepartureDate, _layover.DepartureDate);
        }

        [Test, Order(7)]
        public void CreateLayoverEntityListTest()
        {
            _layoverDTOList.Add(_layoverDTO);
            _layoverList = _layoverMapper.CreateEntityList(_layoverDTOList);
            Assert.IsTrue(_layoverList.Count > 0);
        }

        [Test, Order(8)]
        public void CreateLayoverDTOListTest()
        {
            _layoverList.Add(_layover);
            _layoverDTOList = _layoverMapper.CreateDTOList(_layoverList);
            Assert.IsTrue(_layoverDTOList.Count > 0);
        }

    }
}