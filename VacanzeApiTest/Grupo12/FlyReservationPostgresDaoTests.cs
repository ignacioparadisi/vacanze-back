using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo12;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo12;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo12;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo12;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo12;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo12;
namespace vacanze_back.VacanzeApiTest.Grupo12
{

    [TestFixture]
    public class FlyReservationPostgresDaoTests
    {
       
        private ReservationFlightDAO dao;
        private Entity entity;

        [SetUp]
        public void SetUp(){

            //Instancia del DAO
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            dao = factory.GetReservationFlightDAO();

            //Instancia el objeto reserva de vuelo
             entity = new FlightRes("A1","2019-7-6 23:00",1,1,1);

        }

        //Positivas
        [Test, Order(1) ]
        //Agrega una entidad desde el dao
        public void AddReservationFlightTest()
        {     
            int a = dao.AddReservationFlight(entity);

            Assert.Less(0, a);
        }

        [Test, Order(2) ]
        //Trae reservas de un usuario (en este caso se inserta una del 1 y se buscan de el)
        public void GetReservationFlightTest()
        {
           
            int a = dao.AddReservationFlight(entity);

            var result = dao.GetReservationFlight(1);

            Assert.IsInstanceOf<List<Entity>>(result); 
            
        }

        [Test, Order(3) ]
        //Elimina reserva
        public void DeleteReservationFlightTest()
        {
           
            int a = dao.AddReservationFlight(entity);

            dao.DeleteReservationFlight(a);

           // Assert.IsInstanceOf<List<Entity>>(result); 
            
        }

        [Test, Order(4) ]
        //Trae id de una loc
        public void GetIDLocationTest()
        {
           
            int a = dao.GetIDLocation("Caracas");

            Assert.Less(0, a);
            
        }

        /* [Test, Order(5) ]
        //
        public void ConSeatNumTest()
        {

            var table = PgConnection.Instance.ExecuteFunction(
                "addflight( @_plane, @_price, @_departure,@_arrival, @_loc_arrival, @_loc_departure )",
                1, 100, "2019-01,01", "2019-01-02", 1, 2
            );

        
            int id_fli = Convert.ToInt32(table.Rows[0][0]);

            string a = dao.ConSeatNum(2,id_fli);

            Assert.AreEqual("A1,A2,", a);
            
        }*/

        [Test, Order(5) ]
        //Valida la existencia de un vuelo
        public void FindFlightTest()
        {
           
            bool a = dao.FindFlight(1);

            Assert.AreEqual(true, a);
            
        }

        [Test, Order(6) ]
        //Valida la existencia de una reservación
        public void FindReservationTest()
        {       

            int a = dao.AddReservationFlight(entity);

            bool b = dao.FindReservation(a);

            Assert.AreEqual(true, b);
            
        }

        [Test, Order(7) ]
        //Valida la existencia de una reservación
        public void FindUserTest()
        {       

            bool a = dao.FindUser(1);

            Assert.AreEqual(true, a);
            
        }

        [Test, Order(8) ]
        //Trae reservas de ida
        public void GetFlightValidateIDaoTest()
        {       

            int a = dao.AddReservationFlight(entity);

            var result = dao.GetFlightValidateI(1,2,"2019-7-6 23:00",1);

            Assert.IsInstanceOf<List<Entity>>(result); 
            
        }

        [Test, Order(9) ]
        //Trae reservas de ida y vuelta
        public void GetReservationFlightIVDaoTest()
        {       

            int a = dao.AddReservationFlight(entity);

            var result = dao.GetReservationFlightIV(1,2,"2019-7-6 23:00","2019-7-6 23:00",1);

            Assert.IsInstanceOf<List<Entity>>(result); 
            
        }


        //Negativos
        [Test, Order(10) ]
        //Trae reservas de un usuario 
        public void GetReservationFlightExTest()
        {

            Assert.Throws<EmptyListReservation>(delegate { dao.GetReservationFlight(-1); } );
            
        }

        [Test, Order(11) ]
        //Elimina reserva
        public void DeleteReservationFlightExTest()
        {
           
            dao.DeleteReservationFlight(-1);

           // Assert.IsInstanceOf<List<Entity>>(result); 
            
        }

        [Test, Order(12) ]
        //Trae id de una loc
        public void GetIDLocationExTest()
        {
           
            int a = dao.GetIDLocation("petare");

            Assert.AreEqual(-1, a);
            
        }

        /* [Test, Order(13) ]
        //
        public void ConSeatNumTest()
        {

            var table = PgConnection.Instance.ExecuteFunction(
                "addflight( @_plane, @_price, @_departure,@_arrival, @_loc_arrival, @_loc_departure )",
                1, 100, "2019-01,01", "2019-01-02", 1, 2
            );

        
            int id_fli = Convert.ToInt32(table.Rows[0][0]);

            string a = dao.ConSeatNum(2,id_fli);

            Assert.AreEqual("A1,A2,", a);
            
        }*/

        [Test, Order(14) ]
        //Valida la existencia de un vuelo
        public void FindFlightExTest()
        {
           
            bool a = dao.FindFlight(-1);

            Assert.AreEqual(false, a);
            
        }

        [Test, Order(15) ]
        //Valida la existencia de una reservación
        public void FindReservationExTest()
        {       
            bool b = dao.FindReservation(-1);

            Assert.AreEqual(false, b);
            
        }

        [Test, Order(16) ]
        //Valida la existencia de una reservación
        public void FindUserExTest()
        {       

            bool a = dao.FindUser(-1);

            Assert.AreEqual(false, a);
            
        }

        [Test, Order(17) ]
        //Trae reservas de ida
        public void GetFlightValidateIDaoExTest()
        {       
            List<Entity> l = dao.GetFlightValidateI(1,2,"2018-01-01",1);

            Assert.AreEqual(0, l.Count);

            //Assert.IsInstanceOf<List<Entity>>(result); 
           // Assert.Throws<EmptyListFlight>(delegate { dao.GetFlightValidateI(1,2,"2018-01-01",1); } );    
            
        }

        [Test, Order(18) ]
        //Trae reservas de ida y vuelta
        public void GetReservationFlightIVDaoExTest()
        {       

            List<Entity> l = dao.GetReservationFlightIV(1,2,"2018-01-01", "2018-01-01",1);
            Assert.AreEqual(0, l.Count);

            //Assert.Throws<EmptyListFlight>(delegate { dao.GetReservationFlightIV(1,2,"2018-01-01", "2018-01-01",1); } );    

        }
        
        
    }
}