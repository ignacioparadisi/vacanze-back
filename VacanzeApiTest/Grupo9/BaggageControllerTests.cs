using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo9;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo9;
using vacanze_back.VacanzeApi.Persistence;

namespace vacanze_back.VacanzeApiTest.Grupo9
{
    [TestFixture]
    public class BaggageControllerTests
    {

        private BaggageController _baggageControllerTest;
        private Baggage _baggage;
        private List<int> _insertedBaggages;
        private PostgresBaggageDao _postgresBaggageDaoTest;

        [SetUp]
        public void Setup()
        {
            _postgresBaggageDaoTest = new PostgresBaggageDao();
            _baggageControllerTest = new BaggageController(null);
            _insertedBaggages = new List<int>();
            _baggage = BaggageBuilder.Create()
                .WithStatus("EXTRAVIADO")
                .WithDescription("Bolso negro extraviado en el areopuerto de maiquetia")
                .Build();
        }

        /*CREATE OR REPLACE FUNCTION AddBaggage(
	_bag_res_fli_fk INTEGER,
    _bag_res_cru_fk INTEGER,
    _bag_descr VARCHAR(100),
    _bag_status VARCHAR(100)*/

        [TearDown]
        public void TearDown()
        {
            
            PgConnection.Instance.ExecuteFunction("AddBaggage(@_bag_res_fli_fk," +
                                                             "@_bag_res_cru_fk," +
                                                             "@_bag_descr," +
                                                             "@_bag_status)",
                                                             "null",
                                                             "null",
                                                             "DESCRICIONDEPRUEBAS",
                                                             "STATUSDEPRUEBAS");

        }

/*

        [TearDown]
        public void tearDown()
        {
            controller = null;
        }

        private ActionResult<IEnumerable<Baggage>> Baggage;
        private BaggageController controller;

        private BaggageRepository conec;
        private int response;

        [Test]
        [Order(1)]
        public void GetBaggageEspecificTest()
        {
            Baggage = controller.Get(5);
            response = Baggage.Value.Count();
            Assert.AreEqual(1, response);
        }

        [Test]
        [Order(3)]
        public void GetBaggageGetDocumentTest()
        {
            Baggage = controller.GetDocument("26055828");
            response = Baggage.Value.Count();
            Assert.True(response >= 0);
        }

        [Test]
        [Order(2)]
        public void GetBaggagestatusTest()
        {
            Baggage = controller.GetStatus("EXTRAVIADO");
            response = Baggage.Value.Count();
            Assert.True(response > 1);
        }

        [Test]
        [Order(5)]
        public void NullBaggageExceptionModifyBaggageStatusTest()
        {
            var p = new Baggage(1, "UNITARIA", "EXTRAVIADO");

            Assert.Throws<NullBaggageException>(() => conec.ModifyBaggageStatus(0, p));
        }

        [Test]
        [Order(4)]
        public void PutBaggageStatusTest()
        {
            var cs = new Baggage(1, "", "EXTRAVIADO");
            controller.Put(5, cs);
            var enumerable = controller.Get(5);
            var baggage = enumerable.Value.ToArray();
            Assert.AreEqual(baggage[0].Status, "EXTRAVIADO");
        }
    */
    }
}