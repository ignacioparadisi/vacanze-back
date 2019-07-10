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
            var claim = new Claim {
                Status = "ABIERTO",
                Title = "Epale",
                Description = "Epale"
            };
            
            Assert.Throws<AttributeValueException>(() =>
            {
                ClaimValidator.Validate(claim, HttpMethod.Put);
            });
        }
        
        [Test]
        public void ValidateUpdate_ClaimWithoutDescription_AttributeValueExThrown()
        {
            var claim = new Claim { Status = null};
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

        [Test]
        public void ValidatePost_ClaimWithoutDescription_RequiredAttributeExceptionThrown()
        {
            var claim = new Claim {
                Status = "ABIERTO",
                Title = "Epale",
                BaggageId = 10
            };
            
            Assert.Throws<RequiredAttributeException>(() =>
            {
                ClaimValidator.Validate(claim, HttpMethod.Post);
            });
        }
        
        [Test]
        public void ValidatePost_ClaimWithoutTitle_RequiredAttributeExceptionThrown()
        {
            var claim = new Claim {
                Status = "ABIERTO",
                Description = "Epale",
                BaggageId = 10
            };
            
            Assert.Throws<RequiredAttributeException>(() =>
            {
                ClaimValidator.Validate(claim, HttpMethod.Post);
            });
        }
        
        [Test]
        public void ValidatePost_ClaimWithoutBaggage_RequiredAttributeExceptionThrown()
        {
            var claim = new Claim {
                Status = "ABIERTO",
                Title = "Epale",
                BaggageId = 0
            };
            
            Assert.Throws<RequiredAttributeException>(() =>
            {
                ClaimValidator.Validate(claim, HttpMethod.Post);
            });
        }

        [Test]
        public void Validate_TitleSize_AttributeSizeException()
        {
            var claim = new Claim {
                Status = "ABIERTO",
                Title = "Epale.............................................................",
                BaggageId = 10
            };
            
            Assert.Throws<RequiredAttributeException>(() =>
            {
                ClaimValidator.Validate(claim, HttpMethod.Post);
            });
        }
        
        [Test]
        public void Validate_InvalidStatus_AttributeSizeException()
        {
            var claim = new Claim {
                Status = "CUALQUIERA",
                Title = "Epale",
                BaggageId = 10
            };
            
            Assert.Throws<RequiredAttributeException>(() =>
            {
                ClaimValidator.Validate(claim, HttpMethod.Post);
            });
        }
        
        [Test]
        public void Validate_ValidClaim_NoExceptionThrown()
        {
            var claim = new Claim {
                Status = "ABIERTO",
                Title = "Epale",
                BaggageId = 10
            };
            
            Assert.DoesNotThrow(() =>
            {
                ClaimValidator.Validate(claim);
            });
        }
    }
}