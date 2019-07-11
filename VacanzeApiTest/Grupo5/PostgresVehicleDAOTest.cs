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
        public void TestGetVehicleById_VehicleNotFoundException(){
            Assert.Throws<VehicleNotFoundException>(
                () => vehicleDAO.GetVehicleById(100000)
            );
        }

        [Test]
        public void TestGetVehicleById_Successfully(){
            Assert.NotNull(vehicleDAO.GetVehicleById(1));
        }

        [Test]
        public void TestGetAvailableVehiclesByLocation_LocationNotFoundException(){
            Assert.Throws<LocationNotFoundException>(
                () => vehicleDAO.GetAvailableVehiclesByLocation(100000)
            );
        }

        [Test]
        public void TestGetAvailableVehiclesByLocation_NotVehiclesAvailableException(){
            Assert.Throws<NotVehiclesAvailableException>(
                () => vehicleDAO.GetAvailableVehiclesByLocation(161)
            );
        }

        [Test]
        public void TestGetAvailableVehiclesByLocation_Success(){
            Assert.NotZero(
                vehicleDAO.GetAvailableVehiclesByLocation(37).Count);
        }

        [Test]
        public void TestGetVehicles_Success(){
            Assert.NotZero(
                vehicleDAO.GetVehicles().Count);
        }

        [Test]
        public void TestUpdateVehicle_VehicleNotFoundException(){
            Vehicle vehicle = new Vehicle(
                100000,//VehicleId
                1,
                30, 
                "AC34793",
                1230.2,
                true
            );

            Assert.Throws<VehicleNotFoundException>(
                () => vehicleDAO.UpdateVehicle(vehicle)
            );
        }

        [Test]
        public void TestUpdateVehicle_ModelNotFoundException(){
            Vehicle vehicle = new Vehicle(
                1,
                100000,//ModelId
                30, 
                "AC34793",
                1230.2,
                true
            );

            Assert.Throws<ModelNotFoundException>(
                () => vehicleDAO.UpdateVehicle(vehicle)
            );
        }

        [Test]
        public void TestUpdateVehicle_LocationNotFoundException(){
            Vehicle vehicle = new Vehicle(
                1,
                1,
                100000,//LocationId
                "AC34793",
                1230.2,
                true
            );

            Assert.Throws<LocationNotFoundException>(
                () => vehicleDAO.UpdateVehicle(vehicle)
            );
        }

        [Test]
        public void TestUpdateVehicle_Success(){
            Vehicle vehicle = new Vehicle(
                2,
                1,
                29,
                "AC34793",
                1230.2,
                true
            );

            Assert.True(vehicleDAO.UpdateVehicle(vehicle));
        }

        [Test]
        public void TestUpdateVehicleStatus_VehicleNotFoundException(){
            Assert.Throws<VehicleNotFoundException>(
                () => vehicleDAO.UpdateVehicleStatus(100000,false)
            );
        }
        
        [Test]
        public void TestUpdateVehicleStatus_Success(){
            Assert.True(vehicleDAO.UpdateVehicleStatus(1,false));
        }
        

    }
}