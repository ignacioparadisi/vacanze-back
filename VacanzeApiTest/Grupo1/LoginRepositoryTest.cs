namespace vacanze_back.VacanzeApiTest.Grupo1
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using vacanze_back.VacanzeApi.Persistence.Repository.Grupo1;

    [TestFixture]
    public class LoginTest
    {
        LoginRepository loginRepository;

        [SetUp]
        public void SetUp()
        {
            loginRepository = new LoginRepository();
        }

        //Ingresando al sistema como Administrador

        [Test]
        public void SessionLogin_AsAdmin_Success()
        {
            var login = loginRepository.SessionLogin("admin@vacanze.com","admin123");

            Assert.IsNotNull(login);
        }

        //Ingresando al sistema como Cliente

        [Test]
        public void SessionLogin_AsCliente_Success()
        {
            var login = loginRepository.SessionLogin("cliente@vacanze.com", "cliente123");

            Assert.IsNotNull(login);
        }

        //Ingresando al sistema como CheckIn

        [Test]
        public void SessionLogin_AsChechin_Success()
        {
            var login = loginRepository.SessionLogin("checkin@vacanze.com", "checkin123");

            Assert.IsNotNull(login);
        }

        //Ingresando al sistema como Cargador

        [Test]
        public void SessionLogin_AsCargador_Success()
        {
            var login = loginRepository.SessionLogin("cargador@vacanze.com", "cargador123");

            Assert.IsNotNull(login);
        }

        //Ingresando al sistema como Reclamo

        [Test]
        public void SessionLogin_AsReclamo_Success()
        {
            var login = loginRepository.SessionLogin("reclamo@vacanze.com", "reclamo123");

            Assert.IsNotNull(login);
        }

        //Ingresando al sistema con dos roles

        [Test]
        public void SessionLogin_WithTwoRoles_Success()
        {
            var login = loginRepository.SessionLogin("userp2@vacanze.com", "userp2123");

            Assert.IsNotNull(login);
        }

        //Ingresando al sistema con tres roles

        [Test]
        public void SessionLogin_WithThreeRoles_Success()
        {
            var login = loginRepository.SessionLogin("userp3@vacanze.com", "userp3123");

            Assert.IsNotNull(login);
        }

        //[Test]
        //public void SessionLoginFail()
        //{
        //    var loginCorrect = loginRepository.SessionLogin("admin@vacanze.com", "admin123");
        //    var login = loginRepository.SessionLogin("admin@vacanze.com", "admin1234");

        //    Assert.AreNotEqual(loginCorrect, login);
        //}
    }
}
