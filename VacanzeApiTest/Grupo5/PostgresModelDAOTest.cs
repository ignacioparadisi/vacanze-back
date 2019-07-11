using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Internal;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo5;

namespace vacanze_back.VacanzeApiTest.Grupo5
{
    [TestFixture]
    public class PostgresModelDAOTest
    {
        private DAOFactory daoFactory;
        private IModelDAO modelDAO;

        [SetUp]
        public void init()
        {
            daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            modelDAO = daoFactory.GetModelDAO();
        }

        [Test]
        public void TestAddModel_UniqueAttributeException()
        {
            Model model = new Model(1, "Corolla", 4, null);
            Assert.Throws<UniqueAttributeException>(() => modelDAO.AddModel(model));
        }

        [Test]
        public void TestGetModels_Successful()
        {
            Assert.NotZero(modelDAO.GetModels().Count);
        }

        [Test]
        public void TestGetModelsByBrand_BrandNotFoundException()
        {
            Assert.Throws<BrandNotFoundException>(() => modelDAO.GetModelsByBrand(-1));
        }

        [Test]
        public void TestGetModelsByBrand_Successful()
        {
            Assert.NotNull(modelDAO.GetModelsByBrand(1));
        }

        [Test]
        public void TestGetModelById_ModelNotFoundException()
        {
            Assert.Throws<ModelNotFoundException>(() => modelDAO.GetModelById(-1));
        }

        [Test]
        public void TestGetModelById_Successful()
        {
            Assert.NotNull(modelDAO.GetModelById(1));
        }

        [Test]
        public void TestUpdateModel_ModelNotFoundException()
        {
            Model model = new Model(-1 , 1, "Toyota", 4, null);
            Assert.Throws<ModelNotFoundException>(() => modelDAO.UpdateModel(model));
        }

        [Test]
        public void TestUpdateModel_BrandNotFoundException()
        {
            Model model = new Model(1 , -1, "Toyota", 4, null);
            Assert.Throws<BrandNotFoundException>(() => modelDAO.UpdateModel(model));
        }

        [Test]
        public void TestUpdateModel_Successful()
        {
            Model model = modelDAO.GetModelById(1);
            Model modelNew = new Model(1, 1, "AsHrsCaO", 5, "");
            Assert.NotNull(modelDAO.UpdateModel(modelNew));
            modelDAO.UpdateModel(model);
        }
    }
}