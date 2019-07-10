using NUnit.Framework;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Locations;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApiTest.Grupo6
{
    [TestFixture]
    public class LocationComandoTest
    {        
        private int LOCATION_ID;
        private Location location;
 
        [Test]
        [Order(1)]
        public void AddLocationCommandTest()
        {
            location = EntityFactory.CreateLocation(1, "Venezolania","cagua");
            AddLocationCommand command =  CommandFactory.createAddLocationCommand(location);
            command.Execute();
            LOCATION_ID = command.GetResult(); 
            Assert.NotNull(LOCATION_ID);   
        }

        [Test]
        [Order(2)]
        public void GetLocationByIdCommandTest()
        {
            GetLocationByIdCommand commandId =  CommandFactory.GetLocationByIdCommand(77);
            commandId.Execute ();
            location = commandId.GetResult();
            Assert.NotNull(location);       
        }

        [Test]
        [Order(3)]
        public void GetLocationByIdCommand_LocationNotFoundException_Test()
        {   
            Assert.Throws<LocationNotFoundException>(() =>
            {
                GetLocationByIdCommand commandId =  CommandFactory.GetLocationByIdCommand(999999);
                commandId.Execute(); 
            }); 
        }

        [Test]
        [Order(4)]
        public void GetCitiesByCountryCommandTest()
        {
                GetCitiesByCountryCommand commandIByCountry =  CommandFactory.GetCitiesByCountryCommand(77);
                commandIByCountry.Execute ();
                Assert.NotNull(commandIByCountry.GetResult());        
        }
        [Test]
        [Order(5)]
        public void GetCountriesCommandCommandTest()
        {
                GetCountriesCommand GetCountriesCommand =  CommandFactory.GetCountriesCommand();
                GetCountriesCommand.Execute ();
                Assert.NotNull(GetCountriesCommand.GetResult());        
        }
        [Test]
        [Order(6)]
        public void GetLocationsCommandTest()
        {
                GetLocationsCommand GetLocationsCommand =  CommandFactory.GetLocationsCommand();
                GetLocationsCommand.Execute ();
                Assert.NotNull(GetLocationsCommand.GetResult());        
        }        
        [Test]
        [Order(7)]
        public void DeleteLocationCommand()
        {
            Assert.DoesNotThrow(() =>
            {
                DeleteLocationCommand DeleteLocationCommand =  CommandFactory.DeleteLocationCommand(LOCATION_ID);
                DeleteLocationCommand.Execute ();
            });
        }
    }
}