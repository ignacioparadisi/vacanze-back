using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;

namespace vacanze_back.VacanzeApiTest.Grupo8
{
    [TestFixture]
    public class CruiserEntityTest
    {
        private Cruiser cruiser;

        [Test]
        public void NullNameTest()
        {
            cruiser = new Cruiser(null, true, 100, 1000, "Model1", "Line1", "Picture.jpg");
            Assert.Throws<InvalidAttributeException>(() => cruiser.Validate());
        }
        [Test]
        public void ZeroCapacityTest()
        {
            cruiser = new Cruiser("Concordia", true, 0, 1000, "Model1", "Line1", "Picture.jpg");
            Assert.Throws<InvalidAttributeException>(() => cruiser.Validate());
        }
        [Test]
        public void ZeroLoadingCapTest()
        {
            cruiser = new Cruiser("Concordia", true, 0, 1000, "Model1", "Line1", "Picture.jpg");
            Assert.Throws<InvalidAttributeException>(() => cruiser.Validate());
        }
        [Test]
        public void NullModelTest()
        { 
            cruiser = new Cruiser("Concordia", true, 100, 1000, null, "Line1", "Picture.jpg");
            Assert.Throws<InvalidAttributeException>(() => cruiser.Validate());
        }
        [Test]
        public void NullLineTest()
        { 
            cruiser = new Cruiser("Concordia", true, 100, 1000, "model1", null, "Picture.jpg");
            Assert.Throws<InvalidAttributeException>(() => cruiser.Validate());
        }
        [Test]
        public void NullPictureTest()
        { 
            cruiser = new Cruiser("Concordia", true, 100, 1000, "Model1", "Line1", null);
            Assert.Throws<InvalidAttributeException>(() => cruiser.Validate());
        }
        [Test]
        public void EmptyNameTest()
        { 
            cruiser = new Cruiser("", true, 100, 1000, "model1", "Line1", "picture.jpg");
            Assert.Throws<InvalidAttributeException>(() => cruiser.Validate());
        }
        [Test]
        public void EmptyModelTest()
        { 
            cruiser = new Cruiser("Concordia", true, 100, 1000, "", "Line1", "picture.jpg");
            Assert.Throws<InvalidAttributeException>(() => cruiser.Validate());
        }
        [Test]
        public void EmptyLineTest()
        { 
            cruiser = new Cruiser("Concordia", true, 100, 1000, "model1", "", "picture.jpg");
            Assert.Throws<InvalidAttributeException>(() => cruiser.Validate());
        }
        [Test]
        public void EmptyPictureTest()
        { 
            cruiser = new Cruiser("Concordia", true, 100, 1000, "model1", "line1","");
            Assert.Throws<InvalidAttributeException>(() => cruiser.Validate());
        }
    }
}