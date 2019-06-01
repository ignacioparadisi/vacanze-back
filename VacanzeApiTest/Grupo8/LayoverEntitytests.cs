using System;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;

namespace vacanze_back.VacanzeApiTest.Grupo8
{
    [TestFixture]
    public class LayoverEntitytests
    {
        private Layover layover
            ;

        [Test]
        public void NullCruiserIdTest()
        {
            var layover = new Layover(0,Convert.ToDateTime("2019-01-01"), Convert.ToDateTime("2019-01-02"),2000,1,2);
            Assert.Throws<InvalidAttributeException>(() => layover.Validate());
        }
        [Test]
        public void NullLocDepartureTest()
        {
            var layover = new Layover(1,Convert.ToDateTime("2019-01-01"), Convert.ToDateTime("2019-01-02"),2000,0,2);
            Assert.Throws<InvalidAttributeException>(() => layover.Validate());
        }
        [Test]
        public void NullLocArrivalTest()
        {
            var layover = new Layover(1,Convert.ToDateTime("2019-01-01"), Convert.ToDateTime("2019-01-02"),2000,1,0);
            Assert.Throws<InvalidAttributeException>(() => layover.Validate());
        }
    }
}