using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Persistence.Repository;

namespace vacanze_back.VacanzeApiTest.Grupo6
{
    [SetUpFixture]
    public class HotelTestSetup
    {
        public static int LOCATION_ID;

        [OneTimeSetUp]
        public void Init()
        {
            LOCATION_ID = LocationRepository.AddLocation(new Location("LocationForTest", "Cagua"));
        }

        [OneTimeTearDown]
        public void End()
        {
            LocationRepository.DeleteLocation(LOCATION_ID);
        }
    }
}