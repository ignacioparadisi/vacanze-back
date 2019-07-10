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
    public class PostgresVehicleDAOTest
    {
        private DAOFactory daoFactory;
        private IVehicleDAO vehicleDAO;

        [SetUp]
        public void init(){
            daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            vehicleDAO = daoFactory.GetVehicleDAO();
        }

        [Test]
        public void TestAddVehicle_ModelNotFoundException(){
            Vehicle vehicle = new Vehicle(
                100000, //ModelId
                30,
                "AC34793",
                1230.2,
                true
            );

            Assert.Throws<ModelNotFoundException>(
                () => vehicleDAO.AddVehicle(vehicle)
            );
        }

        [Test]
        public void TestAddVehicle_LocationNotFoundException(){
            Vehicle vehicle = new Vehicle(
                1,
                100000, //LocationId
                "AC34793",
                1230.2,
                true
            );

            Assert.Throws<LocationNotFoundException>(
                () => vehicleDAO.AddVehicle(vehicle)
            );
        }

        [Test]
        public void TestAddVehicle_License_UniqueAttributeException(){
            Vehicle vehicle = new Vehicle(
                1,
                30, 
                "AFGLK17",
                1230.2,
                true
            );

            Assert.Throws<UniqueAttributeException>(
                () => vehicleDAO.AddVehicle(vehicle)
            );
        }

        [Test]
        public void TestAddVehicle_Successfully(){
            Vehicle vehicle = new Vehicle(
                1,
                30, 
                "AC34793",
                1230.2,
                true
            );

            Assert.NotZero(vehicleDAO.AddVehicle(vehicle));
        }
    }
}