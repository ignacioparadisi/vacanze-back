using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo3;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo3;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo3;
namespace vacanze_back.VacanzeApiTest.Grupo3
{

         
  

    [TestFixture]
    public class AirplanesControllerTest
    {
       
        private AirplanesController controller;

        [SetUp]
        public void SetUp(){
           
            controller = new AirplanesController();   

        }

        [Test, Order(1) ]
        public void GetTest()
        {
           
            var result = controller.Get();
            Assert.IsInstanceOf<ActionResult<IEnumerable<Entity>>>(result); 
        }


        [Test, Order(2) ]
        public void FindTest()
        {
           
            var result = controller.Find(666);
            Assert.IsInstanceOf<ActionResult<Entity>>(result); 
        }


        
    }
}