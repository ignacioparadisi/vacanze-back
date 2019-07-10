using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo9;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo9;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo9;

namespace vacanze_back.VacanzeApiTest.Grupo9
{
    [TestFixture]
    public class BaggageControllerTests
    {

        private BaggageController _baggageController;
        private BaggageDTO _baggagedto;
        //private Baggage _baggage;
        private BaggageMapper _baggageMapper;
        private List<int> _insertedBaggages;
        private PostgresBaggageDao _postgresBaggageDaoTest;

        [SetUp]
        public void Setup()
        {
            _postgresBaggageDaoTest = new PostgresBaggageDao();
            _baggageController = new BaggageController(null);
            _insertedBaggages = new List<int>();
            var baggage = BaggageBuilder.Create()
                .WithStatus("EXTRAVIADO")
                .WithDescription("Bolso negro extraviado en el areopuerto de maiquetia")
                .Build();

            _baggageMapper = MapperFactory.CreateBaggageMapper();
            _baggagedto = _baggageMapper.CreateDTO(baggage);
        }


        [Test]
        public void GetBySerial_NoExistingBaggage_NotFoundResultReturned()
        {

            var result = _baggageController.Get(-10);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public void GetDocument_NoExistingDocument_NotFoundResult()
        {
            var result = _baggageController.GetDocument("20766589");
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }


        [TearDown]
        public void TearDown()
        {
            
        }
    }
}