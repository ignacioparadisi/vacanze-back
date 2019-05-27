using NUnit.Framework;
using vacanze_back.VacanzeApi.Common;

namespace vacanze_back.VacanzeApiTest.Grupo2
{
    [TestFixture]
    public class EncryptorTests
    {
        private const string ExpectedResult = "25d55ad283aa400af464c76d713c07ad";
        private const string TextToBeEncrypted = "12345678";

        [Test]
        public void EncryptTest()
        {
            var result = Encryptor.Encrypt(TextToBeEncrypted);
            Assert.AreEqual(ExpectedResult, result);
        }

        [Test]
        public void VerifyTrueTest()
        {
            Assert.True(Encryptor.Verify(TextToBeEncrypted, ExpectedResult));
        }

        [Test]
        public void VerifyFalseTest()
        {
            Assert.False(Encryptor.Verify(TextToBeEncrypted, TextToBeEncrypted));
        }
    }
}