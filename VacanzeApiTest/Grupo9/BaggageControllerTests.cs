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
        private BaggageMapper _baggageMapper;
        
        [SetUp]
        public void Setup()
        {
            _baggageController = new BaggageController(null);
            var baggage = BaggageBuilder.Create()
                .WithDescription("Bolso negro extraviado en el areopuerto de maiquetia")
                .WithId(6)
                .WithStatus("EXTRAVIADO")
                .Build();

            _baggageMapper = MapperFactory.CreateBaggageMapper();
            _baggagedto = _baggageMapper.CreateDTO(baggage);
        }
        
        [Test]
        public void GetDocument_NoExistingDocument_EmptyListReturned()
        {
            var result = _baggageController.GetDocument("-1");
            var castedResult = (OkObjectResult) result.Result;
            var resultList =  (List<BaggageDTO>) castedResult.Value;
            Assert.AreEqual(0,resultList.Count);
        }

        [Test]
        public void GetById_ValidBaggageId_OkResultReturned()
        {
            var result = _baggageController.Get(_baggagedto.Id);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void GetById_InvalidBaggageId_NotFoundResultReturned()
        {
            var result = _baggageController.Get(-1);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public void GetDocument_ValidDocument_OkResultReturned()
        {
            var result = _baggageController.GetDocument("20766589");
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }
        
        [Test]
        public void GetByStatus_ValidBaggage_OkResultReturned()
        {
            var result = _baggageController.GetStatus(_baggagedto.Status);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void GetBySatus_NotFound_EmptyListReturned()
        {
            var result = _baggageController.GetStatus("StatusInvalido");
            var castedResult = (OkObjectResult) result.Result;
            var resultList =  (List<BaggageDTO>) castedResult.Value;
            Assert.AreEqual(0,resultList.Count);
        }
        
        [Test]
        public void Put_UpdatedStatus_OkResultReturned()
        {
            var fieldsToUpdate = new BaggageDTO()
            {
                Status = "RECLAMADO"
            };
            var result = _baggageController.Put(_baggagedto.Id, fieldsToUpdate);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }
        
        [Test]
        public void Put_UpdatedStatus_BaggageNotFound()
        {
            var baggageToUpdate = BaggageBuilder.Create().Build();
            var toUpdatedBaggageDTO = _baggageMapper.CreateDTO(baggageToUpdate);
            var result = _baggageController.Put(-1, toUpdatedBaggageDTO);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void Put_UpdatedStatus_AttributeRequired()
        {
            var baggageToUpdate = BaggageBuilder.Create().Build();
            var toUpdatedBaggageDTO = _baggageMapper.CreateDTO(baggageToUpdate);
            var result = _baggageController.Put(_baggagedto.Id, toUpdatedBaggageDTO);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [TearDown]
        public void TearDown()
        {
            
        }
    }
}