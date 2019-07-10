using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo3;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo3;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo3;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo3;
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
           
            GetAirplaneCommand f=CommandFactory.GetAirplaneCommand();
            f.Execute();
            Assert.NotNull(f.GetResult());
        }


        [Test, Order(2) ]
        public void FindTest()
        {
             GetAirplaneByIdCommand command = CommandFactory.GetFindPlaneIdCommand(1);
             command.Execute();
             Assert.NotNull(command.GetResult()); 
        }


        
    }
}