
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo8;

namespace vacanze_back.VacanzeApiTest.Grupo8
{
    [TestFixture]
    
    public class LayoverValidatorTest
    {
        [Test]
        public void EmptyArrivalTest()
        {
            var  _layover = new Layover(0,null,"05/01/2020", 250, 1, 2);
            Assert.Throws<InvalidAttributeException>(() => LayoverValidator.Validate(_layover));
        }
        
        [Test]
        public void EmptyDepartureTest()
        {
            var  _layover = new Layover(0,"05/01/2020",null, 250, 1, 2);
            Assert.Throws<InvalidAttributeException>(() => LayoverValidator.Validate(_layover));
        }
        
        [Test]
        public void InvalidLocationDepartureTest()
        {
            var  _layover = new Layover(0,"05/01/2020","05/01/2020", 250, 0, 2);
            Assert.Throws<InvalidAttributeException>(() => LayoverValidator.Validate(_layover));
        }
        
        [Test]
        public void InvalidLocationArrivalTest()
        {
            var  _layover = new Layover(0,"05/01/2020","05/01/2020", 250, 1, 0);
            Assert.Throws<InvalidAttributeException>(() => LayoverValidator.Validate(_layover));
        }
        
       
    }
}