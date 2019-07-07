using NUnit.Framework;

namespace vacanze_back.VacanzeApiTest.Grupo9
{
    [TestFixture]
    public class BaggageControllerTests
    {
        /*
        [SetUp]
        public void setup()
        {
            controller = new BaggageController();
            conec = new BaggageRepository();
        }


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