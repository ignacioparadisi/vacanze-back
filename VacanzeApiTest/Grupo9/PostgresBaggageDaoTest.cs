using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo9;

namespace vacanze_back.VacanzeApiTest.Grupo9
{
    [TestFixture]
    public class PostgresBaggageDaoTest
    {
        private PostgresBaggageDao _baggageDao;
        private Baggage _baggage;

        [SetUp]
        public void SetUp()
        {
            _baggage = BaggageBuilder.Create()
                .WithStatus("EXTRAVIADO")
                .WithDescription("maleta roja")
                .WithId(6)
                .Build();
            _baggageDao = (PostgresBaggageDao) DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetBaggageDao();
        }

        [Test]
        public void GetByIdTest()
        {
            var baggage = _baggageDao.GetById(_baggage.Id);
            Assert.AreEqual(baggage.Id,_baggage.Id);
        }
        
        [Test]
        public void GetBaggageTest_Returns_BaggageNotFoundExeption()
        {
            Assert.Throws<BaggageNotFoundException>(()=> _baggageDao.GetById(-1));
        }

        [Test]
        public void GetByPassport()
        {
            var passportId = "20766589";
            var baggageList = _baggageDao.GetByPassport(passportId);
            Assert.IsTrue(baggageList.Count > 0);
        }

        [Test]
        public void GetBySatus()
        {
            var baggageList = _baggageDao.GetByStatus(_baggage.Status);
            Assert.IsTrue(baggageList.Count > 0);
        }

        [Test]
        public void UpdateBaggageTest()
        {
            var toUpdatedBaggage = BaggageBuilder.Create()
                .WithStatus("RECLAMADO")
                .Build();
            var updatedBaggage = _baggageDao.Update(_baggage.Id, toUpdatedBaggage);
            Assert.AreEqual(toUpdatedBaggage.Status,updatedBaggage.Status);
        }
        
        [Test]
        public void UpdateBaggageTest_Returns_BaggageNotFoundExeption()
        {
            Assert.Throws<BaggageNotFoundException>(()=> _baggageDao.Update(-1,null));
        }
    }
}