using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo12;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo12;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo12;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo12;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo12;
namespace vacanze_back.VacanzeApiTest.Grupo12
{

    [TestFixture]
    public class FlyReservationMapperTests
    {
       
        private ReservationFlightMapper ResFlightMapper;
        private FlightResDTO flightDTO;
        private FlightRes entity;
        List<FlightResDTO> dtos;
        List<FlightRes> entities;

        [SetUp]
        public void SetUp(){

            //Instancia del mapper
            ResFlightMapper = MapperFactory.CreateReservationFlightMapper();
                 
            //Instancia el DTO
            flightDTO = new FlightResDTO("","2019-7-6 23:00",1,1,1);

            //Instancia el objeto reserva de vuelo
            entity = new FlightRes("","2019-7-6 23:00",1,1,1);

            //Instancia una lista de DTO
            dtos = new List<FlightResDTO>();
            dtos.Add(flightDTO);

            //Instancia una lsita de entidades
            entities = new List<FlightRes>();
            entities.Add(entity);
        }

        //Positivas
        [Test, Order(1) ]
        public void CreateEntityTest()
        {
           
            FlightRes entitycopy = ResFlightMapper.CreateEntity(flightDTO);

            List<String> a = new List<String>();
            a.Add(entity._seatNum.ToString()); 
            a.Add(entity._timestamp.ToString());
            a.Add(entity._numPas.ToString());
            a.Add(entity._id_user.ToString());
            a.Add(entity._id_fli.ToString());

            List<String> b = new List<String>();
            b.Add(entitycopy._seatNum.ToString()); 
            b.Add(entitycopy._timestamp.ToString());
            b.Add(entitycopy._numPas.ToString());
            b.Add(entitycopy._id_user.ToString());
            b.Add(entitycopy._id_fli.ToString());

           
            Assert.AreEqual(a, b);
        }

        [Test, Order(2) ]
        public void CreateDTOTest()
        {
           
            FlightResDTO flightDTOcopy = ResFlightMapper.CreateDTO(entity);

            List<String> a = new List<String>();
            a.Add(flightDTO._seatNum.ToString()); 
            a.Add(flightDTO._timestamp.ToString());
            a.Add(flightDTO._numPas.ToString());
            a.Add(flightDTO._id_user.ToString());
            //a.Add(flightDTO._id_fli.ToString());

            List<String> b = new List<String>();
            b.Add(flightDTOcopy._seatNum.ToString()); 
            b.Add(flightDTOcopy._timestamp.ToString());
            b.Add(flightDTOcopy._numPas.ToString());
            b.Add(flightDTOcopy._id_user.ToString());
            //b.Add(flightDTOcopy._id_fli.ToString());

           
            Assert.AreEqual(a, b);
        }

        [Test, Order(3) ]
        public void CreateDTOListTest()
        {
           
            List<FlightRes> entitiescopy = ResFlightMapper.CreateEntityList(dtos);

            Assert.AreEqual(entities, entitiescopy);
            //Assert.IsInstanceOf<ActionResult<IEnumerable<Entity>>>(result); 
        }

        [Test, Order(4) ]
        public void CreateEntityListTest()
        {
           
            List<FlightResDTO> dtoscopy = ResFlightMapper.CreateDTOList(entities);

            Assert.AreEqual(dtos, dtoscopy);
            //Assert.IsInstanceOf<ActionResult<IEnumerable<Entity>>>(result); 
        }

        
    }
}