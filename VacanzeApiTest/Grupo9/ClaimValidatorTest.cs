using System.Net.Http;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo9;

namespace vacanze_back.VacanzeApiTest.Grupo9
{
    [TestFixture]
    public class ClaimValidatorTest
    {
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ValidateUpdate_ClaimWithStatusAndTitle_AttributeValueExThrown()
        {
            var claim = ClaimBuilder.Create()
                .WithStatus("ABIERTO")
                .WithTitle("Epale")
                .WithDescription("Epale")
                .Build();
            
            Assert.Throws<AttributeValueException>(() =>
            {
                ClaimValidator.Validate(claim, HttpMethod.Put);
            });
        }
        
        [Test]
        public void ValidateUpdate_ClaimWithoutDescription_AttributeValueExThrown()
        {
            var claim = new Claim { Title = "Algun titulo", Status = null};
            Assert.Throws<AttributeValueException>(() => ClaimValidator.Validate(claim, HttpMethod.Put));
        }
        
        [Test]
        public void ValidateUpdate_ClaimWithoutTitle_AttributeValueExThrown()
        {
            var claim = new Claim() { Description = "Alguna desc", Status = null};
            Assert.Throws<AttributeValueException>(() =>
            {
                ClaimValidator.Validate(claim, HttpMethod.Put);
            });
        }


    }
}