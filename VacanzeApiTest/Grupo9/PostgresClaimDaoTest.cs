using System.Collections.Generic;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo9;

namespace vacanze_back.VacanzeApiTest.Grupo9
{    
    [TestFixture]
    public class PostgresClaimDaoTest
    {
        private PostgresClaimDao _claimDao;
        private Claim _claim;
        private List<int> _addedClaimList;
        private Baggage _baggage;
        private User _user;
        private PostgresUserDAO _userDao;

        [SetUp]
        public void SetUp()
        {
            _claim = _claim = ClaimBuilder.Create()
                .WithStatus("ABIERTO")
                .WithDescription("Bolso negro extraviado en el areopuerto de maiquetia")
                .WithTitle("Bolso extraviado")
                .WithBagagge(6)
                .Build();
            _user = new User(0,26350800,"Angel","Rivero","email@vacanze.com","assadasad");
            _addedClaimList = new List<int>();
            _claimDao = (PostgresClaimDao) DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetClaimDao();
            
        }

        [Test]
        public void GetByIdTest()
        {
            var id = _claimDao.Add(_claim);
            _addedClaimList.Add(id);
            var claim = _claimDao.GetById(id);
            Assert.AreEqual(id,claim.Id);
        }
        
        [Test]
        public void GetClaimTest_Returns_ClaimNotFoundExeption()
        {
            Assert.Throws<ClaimNotFoundException>(()=> _claimDao.GetById(-1));
        }
        
        [Test]
        public void GetByStatusTest()
        {
            var id = _claimDao.Add(_claim);
            _addedClaimList.Add(id);
            var claimList = _claimDao.GetByStatus(_claim.Status);
            Assert.IsTrue(claimList.Count > 0);
        }

        [Test]
        public void GetByDocumentTest()
        {
            var claimList = _claimDao.GetByDocument("23613704");
            Assert.IsTrue(claimList.Count > 0);
        }

        [Test]
        public void AddClaimTest()
        {
            var id = _claimDao.Add(_claim);
            _addedClaimList.Add(id);
            var claim = _claimDao.GetById(id);
            Assert.AreEqual(id,claim.Id);
        }

        [Test]
        public void UpdateClaimTest()
        {
            var id = _claimDao.Add(_claim);
            _addedClaimList.Add(id);
            var toUpdateClaim = ClaimBuilder.Create()
                .WithStatus("CERRADO")
                .WithDescription("Bolso actualizado descripcion")
                .WithTitle("Bolso actualizado")
                .WithBagagge(6)
                .Build();
            _claimDao.Update(id,toUpdateClaim);
            var updatedClaim = _claimDao.GetById(id);
            Assert.AreEqual(toUpdateClaim.Title,updatedClaim.Title);
        }

        [Test]
        public void UpdateClaimTest_Returns_ClaimNotFoundExeption()
        {
            Assert.Throws<ClaimNotFoundException>(()=> _claimDao.Update(-1,null));
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var claimId in _addedClaimList) _claimDao.Delete(claimId);
            _addedClaimList.Clear();
        }

    }
}