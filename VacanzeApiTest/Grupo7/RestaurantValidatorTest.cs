
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo7;

namespace vacanze_back.VacanzeApiTest.Grupo7
{
    [TestFixture]
    
    public class RestaurantValidatorTest
    {
        [Test]
        public void EmptyNameTest()
        {
            var restaurant = EntityFactory.CreateRestaurant(-1, "", 200, false, 4, "Tsingy", 21, "frf",
                "rterte", "trhrthtr", "021221546352", 1, "yrty rtyrtks yrtyr");
            Assert.Throws<InvalidAttributeException>(() => RestaurantValidator.Validate(restaurant));
        }
        
        [Test]
        public void InvalidCapacityTest()
        {
            var restaurant = EntityFactory.CreateRestaurant(-1, "Restaurant", 0, false, 4, "Tsingy", 21, "frf",
                "rterte", "trhrthtr", "021221546352", 1, "yrty rtyrtks yrtyr");
            Assert.Throws<InvalidAttributeException>(() => RestaurantValidator.Validate(restaurant));
        }
        
        [Test]
        public void InvalidQualifyTest()
        {
            var restaurant = EntityFactory.CreateRestaurant(-1, "Restaurant", 5, false, -1, "Tsingy", 21, "frf",
                "rterte", "trhrthtr", "021221546352", 1, "yrty rtyrtks yrtyr");
            Assert.Throws<InvalidAttributeException>(() => RestaurantValidator.Validate(restaurant));
        }
        
        [Test]
        public void EmptySpecialtyTest()
        {
            var restaurant = EntityFactory.CreateRestaurant(-1, "Restaurant", 5, false, 5, "", 21, "frf",
                "rterte", "trhrthtr", "021221546352", 1, "yrty rtyrtks yrtyr");
            Assert.Throws<InvalidAttributeException>(() => RestaurantValidator.Validate(restaurant));
        }
        
        [Test]
        public void InvalidPriceTest()
        {
            var restaurant = EntityFactory.CreateRestaurant(-1, "Restaurant", 5, false, 5, "rewrw", 0, "frf",
                "rterte", "trhrthtr", "021221546352", 1, "yrty rtyrtks yrtyr");
            Assert.Throws<InvalidAttributeException>(() => RestaurantValidator.Validate(restaurant));
        }
        
        [Test]
        public void EmptyBusinessNameTest()
        {
            var restaurant = EntityFactory.CreateRestaurant(-1, "Restaurant", 5, false, 5, "rewrw", 3, "",
                "rterte", "trhrthtr", "021221546352", 1, "yrty rtyrtks yrtyr");
            Assert.Throws<InvalidAttributeException>(() => RestaurantValidator.Validate(restaurant));
        }
        
        [Test]
        public void EmptyDescriptionTest()
        {
            var restaurant = EntityFactory.CreateRestaurant(-1, "Restaurant", 5, false, 5, "rewrw", 3, "frf",
                "erwer", "", "021221546352", 1, "yrty rtyrtks yrtyr");
            Assert.Throws<InvalidAttributeException>(() => RestaurantValidator.Validate(restaurant));
        }
        
        [Test]
        public void EmptyPhoneTest()
        {
            var restaurant = EntityFactory.CreateRestaurant(-1, "Restaurant", 5, false, 5, "rewrw", 3, "frf",
                "erwer", "fewfwe", "", 1, "yrty rtyrtks yrtyr");
            Assert.Throws<InvalidAttributeException>(() => RestaurantValidator.Validate(restaurant));
        }
        
        [Test]
        public void InvalidLocationTest()
        {
            var restaurant = EntityFactory.CreateRestaurant(-1, "Restaurant", 5, false, 5, "rewrw", 3, "frf",
                "erwer", "fewfwe", "frwefew", -1, "yrty rtyrtks yrtyr");
            Assert.Throws<InvalidAttributeException>(() => RestaurantValidator.Validate(restaurant));
        }
        
        [Test]
        public void EmptyAddressTest()
        {
            var restaurant = EntityFactory.CreateRestaurant(-1, "Restaurant", 5, false, 5, "rewrw", 3, "frf",
                "erwer", "fewfwe", "fefwee", 1, "");
            Assert.Throws<InvalidAttributeException>(() => RestaurantValidator.Validate(restaurant));
        }
    }
}