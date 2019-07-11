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
    public class PostgresBrandDAOTest
    {
        private DAOFactory daoFactory;

        private IBrandDAO brandDAO;

        [SetUp]
        public void init()
        {
            daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            brandDAO = daoFactory.GetBrandDAO();
        }

        [Test]
        public void TestAddBrand_UniqueAttributeException()
        {
            Brand brand = new Brand("Toyota");

            Assert.Throws<UniqueAttributeException>(() => brandDAO.AddBrand(brand));
        }

        [Test]
        public void TestGetBrands_Successfuly()
        {
            Assert.NotZero(brandDAO.GetBrands().Count);
        }

        [Test]
        public void TestGetBrandById_BrandNotFoundException()
        {
            Assert.Throws<BrandNotFoundException>(() => brandDAO.GetBrandById(-1));
        }

        [Test]
        public void TestGetBrandById_Successfuly()
        {
            Assert.NotNull(brandDAO.GetBrandById(1));
        }

        [Test]
        public void TestUpdateBrand_BrandNotFoundException()
        {
            Brand brand = new Brand(-1, "No exist");
            Assert.Throws<BrandNotFoundException>(() => brandDAO.UpdateBrand(brand));
        }

        [Test]
        public void TestUpdateBrand_UniqueAttributeException()
        {
            Brand brand = brandDAO.GetBrandById(1);
            Assert.Throws<UniqueAttributeException>(() => brandDAO.UpdateBrand(brand));
        }

        [Test]
        public void TestUpdateBrand_Successful()
        {
            Brand brand = brandDAO.GetBrandById(1);
            String brandName = brand.BrandName;
            brand.BrandName = "aAFsaGc";
            Assert.True(brandDAO.UpdateBrand(brand));
            brand.BrandName = brandName;
            brandDAO.UpdateBrand(brand);
        }
    }
}