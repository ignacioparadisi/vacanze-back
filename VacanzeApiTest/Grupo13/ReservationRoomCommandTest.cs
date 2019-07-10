using NUnit.Framework;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo13;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApiTest.Grupo13
{
    [TestFixture]
    public class ReservationRoomCommandTest
    {
        private ReservationRoom _reservation;
        private Hotel _hotel;
        private DAOFactory _factorydao;
        private User _user;
        private List<int> _insertedReservations;
        
        /// <summary>
        /// Crea el usuario en base de datos para usar este usuario en las pruebas
        /// </summary>
        private void CreateUser()
        {
            // TODO: Cambiar por el Factory de usuario
            var roles = new List<Role>();
            roles.Add(EntityFactory.CreateRole(1, "Cliente"));
            _user = EntityFactory.CreateUser(0, 23456789, "Pedro", "Perez",
                "cliente1@vacanze.com", "12345678", roles);
            _user = _factorydao.GetUserDAO().AddUser(_user);
        }

        /// <summary>
        /// Crea un hotel en la base de datos para usarlo en las pruebas
        /// </summary>
        private void CreateHotel()
        {
            _hotel = EntityFactory.CreateHotel(0,
                "Prueba",
                30,
                5,
                true,
                "Direccion",
                13,
                "www.google.com",
                "04141234567",
                "https://www.google.com/search?q=hotel&source=lnms&tbm=isch&sa=X&ved=0ahUKEwiE99f53abjAhXsmeAKHaa7COMQ_AUIECgB&biw=1280&bih=678&dpr=2#imgrc=_p5ca9DimO_r-M:",
                5, 1);
            
            _hotel.Id = _factorydao.GetHotelDAO().AddHotel(_hotel);
        }
        
        [SetUp]
        public void Setup()
        {
            _insertedReservations = new List<int>();
            
            DateTime checkin = new DateTime(2019,8,10);
            DateTime checkout = new DateTime(2019,11,12);
            
            _factorydao = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            
            CreateUser();
            CreateHotel();

            _reservation = EntityFactory.CreateReservationRoom(0, checkin, checkout, _hotel.Id, _user.Id);
        }

        [TearDown]
        public void TearDown()
        {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            
            foreach (var id in _insertedReservations)
            {
                factory.GetReservationRoomDAO().Delete(id);
            }
            factory.GetHotelDAO().DeleteHotel(_hotel.Id);
            factory.GetUserDAO().DeleteUserById(_user.Id);
        }

        [Test]
        public void CreateReservationSuccessTest()
        {
            DateTime checkin = new DateTime(2019,7,10);
            DateTime checkout = new DateTime(2019,7,12);
            ReservationRoom reservationRoom =
                EntityFactory.CreateReservationRoom(0, checkin, checkout, _hotel.Id, _user.Id);
            CommandResult<ReservationRoom> command = CommandFactory.CreateAddReservationRoomCommand(reservationRoom);
            command.Execute();
            _insertedReservations.Add(reservationRoom.Id);
            Assert.True(command.GetResult().Id > 0);
        }

        [Test]
        public void CreateReservationNullHotelTest()
        {
            DateTime checkin = new DateTime(2019,7,10);
            DateTime checkout = new DateTime(2019,7,12);
            ReservationRoom reservationRoom =
                EntityFactory.CreateReservationRoom(0, checkin, checkout, 0, _user.Id);
            CommandResult<ReservationRoom> command = CommandFactory.CreateAddReservationRoomCommand(reservationRoom);
            Assert.Throws<ReservationHasNoHotelException>(() =>
            {
                command.Execute();
            });
        }
        
        [Test]
        public void CreateReservationNullUserTest()
        {
            DateTime checkin = new DateTime(2019,7,10);
            DateTime checkout = new DateTime(2019,7,12);
            ReservationRoom reservationRoom =
                EntityFactory.CreateReservationRoom(0, checkin, checkout, _hotel.Id, 0);
            CommandResult<ReservationRoom> command = CommandFactory.CreateAddReservationRoomCommand(reservationRoom);
            Assert.Throws<ReservationHasNoUserException>(() =>
            {
                command.Execute();
            });
        }

        [Test]
        public void GetReservationSuccessTest()
        {
            DateTime checkin = new DateTime(2019,7,10);
            DateTime checkout = new DateTime(2019,7,12);
            ReservationRoom reservationRoom =
                EntityFactory.CreateReservationRoom(0, checkin, checkout, _hotel.Id, _user.Id);
            CommandResult<ReservationRoom> command = CommandFactory.CreateAddReservationRoomCommand(reservationRoom);
            command.Execute();
            _insertedReservations.Add(reservationRoom.Id);
            CommandResult<ReservationRoom> command2 =
                CommandFactory.CreateGetReservationRoomCommand(command.GetResult().Id);
            command2.Execute();
            Assert.True(command2.GetResult().Id > 0);
        }
        
        
    }
    
    
}