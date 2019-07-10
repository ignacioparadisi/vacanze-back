using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo8;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo8;
using System;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo8;

namespace vacanze_back.VacanzeApiTest.Grupo8
{
    	[TestFixture]
	public class PostgresCruiserDAOTest
	{
		private PostgresCruiserDAO _cruiserDao;
		private Cruiser _cruiser;
		private List<int> _addedCruiserList;
        private Layover _layover;
		[SetUp]
		public void Setup()
		{
			_cruiser = new Cruiser("The great seeker of wonder" , true , 220 , 200 , "A big one" , "A tender line", "I suppose a picture is in order");
			_addedCruiserList = new List<int>();
			_cruiserDao = (PostgresCruiserDAO) DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetCruiserDAO();
		}

		[Test]
		public void GetCruiserTest()
		{
			var id = _cruiserDao.AddCruiser(_cruiser);
			_addedCruiserList.Add(id);
			var cruiser = _cruiserDao.GetCruiser(id);
			Assert.AreEqual(id, cruiser.Id);
		}
		
		[Test]
		public void GetCruiserTest_Returns_CruiserNotFoundExeption()
		{
			Assert.Throws<CruiserNotFoundException>(() => _cruiserDao.GetCruiser(-1));
		}

		[Test]

		public void GetCruisersTest()
		{
			var id = _cruiserDao.AddCruiser(_cruiser);
			_addedCruiserList.Add(id);
			List<Cruiser> cruiserList = _cruiserDao.GetCruisers();
			Assert.IsTrue(cruiserList.Count > 0);
		}
		
		
		[Test]
		public void AddCruiserTest()
		{
			var id = _cruiserDao.AddCruiser(_cruiser);
			_addedCruiserList.Add(id);
			var cruiser = _cruiserDao.GetCruiser(id);
			Assert.AreEqual(id,cruiser.Id);
		}
		
		[Test]
		public void UpdateCruiserTest()
		{
			var id = _cruiserDao.AddCruiser(_cruiser);
			_addedCruiserList.Add(id);
			var toUpdateCruiser = new Cruiser(id, "The great seeker of wonder reforged" , true , 500 , 250 , "A big one made even larger" , "A fine line", "I suppose a picture is in order... soon");
			var updatedCruiser = _cruiserDao.UpdateCruiser(toUpdateCruiser);
			var actualCruiser = _cruiserDao.GetCruiser(id);
			Assert.AreEqual(updatedCruiser.Name,actualCruiser.Name);
		}

		[Test]
		public void UpdateCruiserTest_Returns_CruiserNotFoundExeption()
		{
			_cruiser.Id = -1;
			Assert.Throws<CruiserNotFoundException>(() => _cruiserDao.UpdateCruiser(_cruiser));
		}

        [Test]
		public void UpdateCruiserTest_Returns_NullCruiserException()
		{
			Assert.Throws<NullCruiserException>(() => _cruiserDao.UpdateCruiser(null));
		}

        [Test]
		public void DeleteCruiserTest()
		{
			var id = _cruiserDao.AddCruiser(_cruiser);
			var deletedCruiser = _cruiserDao.DeleteCruiser(id);
			Assert.AreEqual(id,deletedCruiser);
		}

        [Test]
		public void DeleteCruiserTest_Returns_CruiserNotFoundException()
		{
			Assert.Throws<CruiserNotFoundException>(() => _cruiserDao.DeleteCruiser(-1));
		}

        [Test]
		public void AddLayoverTest()
		{
			var id = _cruiserDao.AddCruiser(_cruiser);
			_addedCruiserList.Add(id);
            _layover = new Layover(id,"02/12/2019","05/01/2020", 250, 1, 2);
            var layover = _cruiserDao.AddLayover(_layover);
			Assert.NotNull(layover);
		}

        [Test]
		public void AddLayoverTest_Returns_CruiserNotFoundException()
		{
            _layover = new Layover(0,"02/12/2019","05/01/2020", 250, 1, 2);
			Assert.Throws<CruiserNotFoundException>(() => _cruiserDao.AddLayover(_layover));
		}

        [Test]
		public void GetLayoversTest()
		{
			var id = _cruiserDao.AddCruiser(_cruiser);
			_addedCruiserList.Add(id);
            _layover = new Layover(id,"02/12/2019","05/01/2020", 250, 1, 2);
            var layover = _cruiserDao.AddLayover(_layover);
            List<Layover> layoverList = _cruiserDao.GetLayovers(id);
			Assert.IsTrue(layoverList.Count > 0);
		}

        [Test]
		public void DeleteLayover()
		{
			var id = _cruiserDao.AddCruiser(_cruiser);
			_addedCruiserList.Add(id);
            _layover = new Layover(id,"02/12/2019","05/01/2020", 250, 1, 2);
            var layover = _cruiserDao.AddLayover(_layover);
            var deletedLayover = _cruiserDao.DeleteLayover(layover.Id);
			Assert.AreEqual(layover.Id, deletedLayover);
		}

        [Test]
		public void DeleteLayover_Returns_LayoverNotFoundException()
		{
			Assert.Throws<LayoverNotFoundException>(() => _cruiserDao.DeleteLayover(-1));
		}

        [Test]
		public void GetLayoversForResTest()
		{
			var id = _cruiserDao.AddCruiser(_cruiser);
			_addedCruiserList.Add(id);
            _layover = new Layover(id,"02/12/2019","05/01/2020", 250, 1, 2);
            var layover = _cruiserDao.AddLayover(_layover);
            List<Layover> layoverList = _cruiserDao.GetLayoversForRes(1,2);
			Assert.IsTrue(layoverList.Count > 0);
		}

        [Test]
		public void GetLayoversForResTest_Returns_LayoverNotFoundException()
		{
			Assert.Throws<LayoverNotFoundException>(() => _cruiserDao.GetLayoversForRes(-1,-1));
		}
		
		[TearDown]
		public void TearDown()
		{
			foreach (var cruiserId in _addedCruiserList)
			{
				_cruiserDao.DeleteCruiser(cruiserId);
			}
		}
	}
}