using System.Net.Http;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo9;

namespace vacanze_back.VacanzeApiTest.Grupo9
{
    [TestFixture]
    public class BaggageValidatorTest
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void ValidateUpdate_BaggageWithStatusAndDescription_AttributeValueExThrown()
        {
            var baggage = BaggageBuilder.Create()
                .WithDescription("epale")
                .WithStatus("epale2")
                .Build();
            Assert.Throws<AttributeValueException>(() =>
            {
                BaggageValidator.Validate(baggage,HttpMethod.Put);
            });
        }
        
        [Test]
        public void Validate_BaggageWithWrongStatus_AttributeValueExThrown()
        {
            var baggage = BaggageBuilder.Create()
                .WithDescription("epale")
                .WithStatus("epale2")
                .Build();
            Assert.Throws<AttributeValueException>(() =>
            {
                BaggageValidator.Validate(baggage);
            });
        }
    }
}