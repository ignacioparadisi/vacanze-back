
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo8;

namespace vacanze_back.VacanzeApiTest.Grupo8
{
    [TestFixture]
    
    public class CruiserValidatorTest
    {
        [Test]
        public void EmptyNameTest()
        {
            var  cruiser = new Cruiser(null , true , 220 , 200 , "A big one" , "A tender line", "I suppose a picture is in order");
            Assert.Throws<InvalidAttributeException>(() => CruiserValidator.Validate(cruiser));
            var  cruiser2 = new Cruiser("The great seeker of wonder" , true , 220 , 200 , "A big one" , "A tender line", "I suppose a picture is in order");
        }
        
        [Test]
        public void InvalidCapacityTest()
        {
            var  cruiser = new Cruiser("The great seeker of wonder" , true , -1 , 200 , "A big one" , "A tender line", "I suppose a picture is in order");
            Assert.Throws<InvalidAttributeException>(() => CruiserValidator.Validate(cruiser));
        }
        
        [Test]
        public void InvalidLoadingShipCapTest()
        {
            var  cruiser = new Cruiser("The great seeker of wonder" , true , 220 , -1 , "A big one" , "A tender line", "I suppose a picture is in order");
            Assert.Throws<InvalidAttributeException>(() => CruiserValidator.Validate(cruiser));
        }
        
        [Test]
        public void EmptyModelTest()
        {
            var  cruiser = new Cruiser("The great seeker of wonder" , true , 220 , 250 , null , "A tender line", "I suppose a picture is in order");
            Assert.Throws<InvalidAttributeException>(() => CruiserValidator.Validate(cruiser));
        }
        
        [Test]
        public void EmptyLineTest()
        {
            var  cruiser = new Cruiser("The great seeker of wonder" , true , 220 , 250 , "A big one" , null, "I suppose a picture is in order");
            Assert.Throws<InvalidAttributeException>(() => CruiserValidator.Validate(cruiser));
        }
        
        [Test]
        public void EmptyPictureTest()
        {
            var  cruiser = new Cruiser("The great seeker of wonder" , true , 220 , 250 , "A big one" , "A tender line", null);
            Assert.Throws<InvalidAttributeException>(() => CruiserValidator.Validate(cruiser));
        }
        
       
    }
}