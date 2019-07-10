using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo12;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo12;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo12;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo12;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo12;
namespace vacanze_back.VacanzeApiTest.Grupo12
{
    [TestFixture]
    public class FlyReservationCommandsTests
    {
        

        [SetUp]
        public void SetUp(){
            
        }


        //Positivas
        [Test, Order(1) ]
        public void AddReservationFlightCommandTest()
        {
            
            FlightResDTO flightDTO = new FlightResDTO("","2019-7-6 23:00",1,1,1);

            //Crea la entidad por medio del mapper devuelvo del factory
            ReservationFlightMapper ResFlightMapper = MapperFactory.CreateReservationFlightMapper();
            Entity entity = ResFlightMapper.CreateEntity(flightDTO);

            //Instancia el comando por medio del factory pasandole la entidad al constructor
            AddReservationFlightCommand command = CommandFactory.CreateAddReservationFlightCommand ((FlightRes)entity);

            //Ejecuta y obtiene el resultado del comando
            command.Execute();

            Assert.AreNotEqual( null, command.GetResult() );

        }

        [Test, Order(2) ]
        public void DeleteReservationCommandTest()
        {
            
            FlightResDTO flightDTO = new FlightResDTO("","2019-7-6 23:00",1,1,1);

            //Crea la entidad por medio del mapper devuelvo del factory
            ReservationFlightMapper ResFlightMapper = MapperFactory.CreateReservationFlightMapper();
            Entity entity = ResFlightMapper.CreateEntity(flightDTO);

            //Instancia el comando por medio del factory pasandole la entidad al constructor
            AddReservationFlightCommand command = CommandFactory.CreateAddReservationFlightCommand ((FlightRes)entity);

            //Ejecuta y obtiene el resultado del comando
            command.Execute();
            int id_res =  command.GetResult();

            //Obtiene comando para borrar
            DeleteReservationCommand command2 = CommandFactory.CreateDeleteReservationCommand(id_res);
            command2.Execute();
   
        }

        [Test, Order(3) ]
        public void GetIdReturnCityCommandTest()
        {
            
            List<string> city_names = new List<string>();
            city_names.Add("Caracas");
            city_names.Add("Caracas");

            GetIdReturnCityCommand command = CommandFactory.CreateGetIdReturnCityCommand(city_names);
            command.Execute();

            List<int> city_ids = command.GetResult();
            Assert.AreNotEqual( null, city_ids );

        }

        [Test, Order(4) ]
        public void GetReservationFlightByUserCommandTest()
        {
            
            GetReservationFlightByUserCommand command = 
            CommandFactory.CreateGetReservationFlightByUserCommand(1);
            command.Execute();
            List<Entity> listFlight = command.GetResult();

            Assert.AreNotEqual( null, listFlight );
        }

        [Test, Order(5) ]
        public void GetReservationsByDateICommandTest()
        {
            
            GetReservationsByDateICommand command = CommandFactory.CreateGetReservationsByDateICommand(1, 2, "2019-01-01", 1);
            command.Execute();
            List<Entity> listFlight = command.GetResult();

            Assert.AreNotEqual( null, listFlight );
        }

         [Test, Order(6) ]
        public void GetReservationsByDateIVCommandTest()
        {
            
            GetReservationsByDateIVCommand command = CommandFactory.CreateGetReservationsByDateIVCommand(1, 2, "2019-01-01", "2019-01-01", 1);
            command.Execute();
            List<Entity> listFlight = command.GetResult();
            
            Assert.AreNotEqual( null, listFlight );
        }


        //Negativas
        [Test, Order(7) ]
        //Añadir reserva de vuelo con num de pasajeros inválido
        public void AddReservationFlightPassengersExCommandTest()
        {
            
            FlightResDTO flightDTO = new FlightResDTO("","2019-7-6 23:00",-1,1,1);

            //Crea la entidad por medio del mapper devuelvo del factory
            ReservationFlightMapper ResFlightMapper = MapperFactory.CreateReservationFlightMapper();
            Entity entity = ResFlightMapper.CreateEntity(flightDTO);

            //Instancia el comando por medio del factory pasandole la entidad al constructor
            AddReservationFlightCommand command = CommandFactory.CreateAddReservationFlightCommand ((FlightRes)entity);

            Assert.Throws<ValidationErrorException>(delegate { command.Execute(); } );

        }

        [Test, Order(8) ]
        //Añadir reserva de vuelo con usuario inválido
        public void AddReservationFlightUserExCommandTest()
        {
            
            FlightResDTO flightDTO = new FlightResDTO("","2019-7-6 23:00",1,-1,1);

            //Crea la entidad por medio del mapper devuelvo del factory
            ReservationFlightMapper ResFlightMapper = MapperFactory.CreateReservationFlightMapper();
            Entity entity = ResFlightMapper.CreateEntity(flightDTO);

            //Instancia el comando por medio del factory pasandole la entidad al constructor
            AddReservationFlightCommand command = CommandFactory.CreateAddReservationFlightCommand ((FlightRes)entity);

            Assert.Throws<ValidationErrorException>(delegate { command.Execute(); } );

        }

        [Test, Order(9) ]
        //Añadir reserva de vuelo con vuelo inválido
        public void AddReservationFlightFlyExCommandTest()
        {
            
            FlightResDTO flightDTO = new FlightResDTO("","2019-7-6 23:00",1,1,-1);

            //Crea la entidad por medio del mapper devuelvo del factory
            ReservationFlightMapper ResFlightMapper = MapperFactory.CreateReservationFlightMapper();
            Entity entity = ResFlightMapper.CreateEntity(flightDTO);

            //Instancia el comando por medio del factory pasandole la entidad al constructor
            AddReservationFlightCommand command = CommandFactory.CreateAddReservationFlightCommand ((FlightRes)entity);

            Assert.Throws<ValidationErrorException>(delegate { command.Execute(); } );

        }

        [Test, Order(10) ]
        //Borrar reserva con id de reserva inválido
        public void DeleteReservationIdExCommandTest()
        {
            
            //Obtiene comando para borrar
            DeleteReservationCommand command = CommandFactory.CreateDeleteReservationCommand(-1);
            Assert.Throws<ValidationErrorException>(delegate { command.Execute(); } );
   
        }

        [Test, Order(11) ]
        //Obtener con primera locación inexistente
        public void GetIdReturnCityFirstLocationExCommandTest()
        {
            
            List<string> city_names = new List<string>();
            city_names.Add("petare");
            city_names.Add("Caracas");

            GetIdReturnCityCommand command = CommandFactory.CreateGetIdReturnCityCommand(city_names);

            Assert.Throws<ValidationErrorException>(delegate { command.Execute(); } );

        }

        [Test, Order(12) ]
        //Obtener con segunda locación inexistente
        public void GetIdReturnCitySecondLocationExCommandTest()
        {
            
            List<string> city_names = new List<string>();
            city_names.Add("Caracas");
            city_names.Add("petare");
 
            GetIdReturnCityCommand command = CommandFactory.CreateGetIdReturnCityCommand(city_names);

            Assert.Throws<ValidationErrorException>(delegate { command.Execute(); } );

        }

        [Test, Order(13) ]
        //Obtener reservas de un usuario con id del usuario inválido
        public void GetReservationFlightByUserIdExCommandTest()
        {
            
            GetReservationFlightByUserCommand command = 
            CommandFactory.CreateGetReservationFlightByUserCommand(-1);

            Assert.Throws<ValidationErrorException>(delegate { command.Execute(); } );
        }

        [Test, Order(14) ]
        //Obtener reservas de ida con primera locación inválida
        public void GetReservationsByDateICommandFirstLocExTest()
        {
            
            GetReservationsByDateICommand command = CommandFactory.CreateGetReservationsByDateICommand(-1, 2, "2019-01-01", 1);

            Assert.Throws<ValidationErrorException>(delegate { command.Execute(); } );
        }

        [Test, Order(15) ]
        //Obtener reservas de ida con segunda locación inválida
        public void GetReservationsByDateICommandSecondLocExTest()
        {
            
            GetReservationsByDateICommand command = CommandFactory.CreateGetReservationsByDateICommand(1, -2, "2019-01-01", 1);

            Assert.Throws<ValidationErrorException>(delegate { command.Execute(); } );
        }

        [Test, Order(16) ]
        //Obtener reservas de ida con acientos inválidos
        public void GetReservationsByDateICommandPassengersExTest()
        {
            
            GetReservationsByDateICommand command = CommandFactory.CreateGetReservationsByDateICommand(1, 2, "2019-01-01", -1);

            Assert.Throws<ValidationErrorException>(delegate { command.Execute(); } );
        }

        [Test, Order(17) ]
        //Obtener reservas de ida y vuelta con primera locación inválida
        public void GetReservationsByDateIVCommandFirstLocExTest()
        {
            
            GetReservationsByDateICommand command = CommandFactory.CreateGetReservationsByDateICommand(-1, 2, "2019-01-01", 1);

            Assert.Throws<ValidationErrorException>(delegate { command.Execute(); } );
        }

        [Test, Order(18) ]
        //Obtener reservas de ida y vuelta con segunda locación inválida
        public void GetReservationsByDateIVCommandSecondLocExTest()
        {
            
            GetReservationsByDateICommand command = CommandFactory.CreateGetReservationsByDateICommand(1, -2, "2019-01-01", 1);

            Assert.Throws<ValidationErrorException>(delegate { command.Execute(); } );
        }

        [Test, Order(19) ]
        //Obtener reservas de ida y vuelta con acientos inválidos
        public void GetReservationsByDateIVCommandPassengersLocExTest()
        {
            
            GetReservationsByDateICommand command = CommandFactory.CreateGetReservationsByDateICommand(1, 2, "2019-01-01", -1);

            Assert.Throws<ValidationErrorException>(delegate { command.Execute(); } );
        }

       
    }
}