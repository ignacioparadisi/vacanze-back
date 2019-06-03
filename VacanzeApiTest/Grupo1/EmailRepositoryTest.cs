namespace vacanze_back.VacanzeApiTest.Grupo1
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using vacanze_back.VacanzeApi.Persistence.Repository.Grupo1;

    [TestFixture]
    public class EmailRepositoryTest
    {
        EmailRepository emailRepository;
        [SetUp]
        public void SetUp()
        {
            emailRepository = new EmailRepository();
        }

        [Test]
        public void EmailRecoverySuccess()
        {
            var email = emailRepository.Recovery("admin@vacanze.com");

            Assert.IsNotNull(email);
        }

        [Test]
        public void EmailRecoveryFailed()
        {
            var email = emailRepository.Recovery("admin@vacanze.com");

            Assert.IsNotNull(email);
        }

       
    }
}
