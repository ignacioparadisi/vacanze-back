using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo4;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo4;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo4;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApiTest.Grupo4
{
     [TestFixture]
     public class BaggageTest
     {
          ActionResult<IEnumerable<Baggage>> Baggage;
          BaggageController controller;

          BaggageRepository conec;
          int response;
          [SetUp]
          public void setup()
          {
               controller = new BaggageController();
               conec = new BaggageRepository();
          }


          [Test]
          public void BaggageDeleteByIdTest()
          {
               var response = BaggageRepository.AddBaggageReturnId(0, 0, "Maleta verde4", "EXTRAVIADO");
               var id = BaggageRepository.DeleteBaggage(response);
               Assert.AreEqual(id, response);
          }

          [Test]
          public void AddBaggageTest()
          {
               var response = BaggageRepository.AddBaggage(3, 3, "Maleta verde", "EXTRAVIADO");
               Assert.AreEqual(response, "Maleta Agregada");
          }

     }



}