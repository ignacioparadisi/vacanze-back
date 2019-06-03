using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;

namespace vacanze_back.VacanzeApiTest.Grupo13
{
    [TestFixture]
    public class ReservationAutomobilesControllerTest
    {
        private ReservationAutomobilesController  controller;
        ReservationAutomobile reservation;

        [SetUp]
        public void SetUp()
        {
            controller = new ReservationAutomobilesController();
            DateTime date = new DateTime(1994, 01, 01);
            DateTime date2 = new DateTime(1994, 01, 03);
            Auto auto = new Auto("Prueba","Unitaria",4,true,"license",12,"eds",1);
            auto.setId(7);
            reservation = new ReservationAutomobile(1,date,date2,auto,2);
        }

        [Test, Order(1)]
        public void GetAllByUserIDTest()
        {

            var result = controller.GetAllByUserID();
            Assert.IsInstanceOf<ActionResult<IEnumerable<Entity>>>(result);
        }

        [Test, Order(2)]
        public void GetFindTest()
        {

            var result = controller.Get(999);
            Assert.IsInstanceOf<ActionResult<Entity>>(result);
        }

        [Test, Order(3)]
        public void PostTest()
        {

            controller = new ReservationAutomobilesController();
            DateTime time = new DateTime(1990, 04, 14);
            DateTime time2 = new DateTime(1990, 04, 14);
            Auto auto = new Auto("prueba","unitaria",5,true,"ddad",1,"asdadads",1);
            auto.setId(0);
            reservation = new ReservationAutomobile(0, time, time2, auto, 2);

            var result = controller.Post(reservation);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);

        }

        [Test, Order(4)]
        public void PutTest()
        {
            ActionResult<Entity> res = controller.Put(reservation);
            Assert.NotNull(res);
        }

    }
}
