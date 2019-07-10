using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Locations;


namespace vacanze_back.VacanzeApiTest.Grupo6
{
    [SetUpFixture]
    public class HotelTestSetup
    { 
        public static int LOCATION_ID;
         
        [OneTimeSetUp]
        public void Init()
        {
            Location location = new Location();
            location = EntityFactory.CreateLocation(1, "Venezolaniooo","caguooa");
            AddLocationCommand command =  CommandFactory.createAddLocationCommand(location);
            command.Execute();
            LOCATION_ID = command.GetResult(); 
            Assert.NotNull(LOCATION_ID);
        }
        [OneTimeTearDown]
        public void End()
        {
            DeleteLocationCommand DeleteLocationCommand =  CommandFactory.DeleteLocationCommand(LOCATION_ID);
            DeleteLocationCommand.Execute ();
        }
    }
}