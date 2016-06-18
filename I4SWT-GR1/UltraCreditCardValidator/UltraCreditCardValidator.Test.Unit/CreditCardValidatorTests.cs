using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace UltraCreditCardValidator.Test.Unit
{
    [TestFixture]
    public class CreditCardValidatorTests
    {
        private CreditCardValidator _uut;

        [SetUp]
        public void CreateValidator()
        {
            _uut = new CreditCardValidator();
        }

        [TestCase("John Thomas Snow", true)]
        [TestCase("John T. Snow", true)]
        [TestCase("John Snow", true)]
        [TestCase("J Snow", false)]
        [TestCase("J. Snow", false)]
        [TestCase("John S.", false)]
        public void ValidateName_ParameterizedName_ReturnsTrueIfNameIsValid(string name, bool shouldSucceed)
        {
            var succeeded = _uut.ValidateName(name);
            Assert.That(succeeded, Is.EqualTo(shouldSucceed));
        }

        [TestCase("Visa", true)]
        [TestCase("MasterCard", true)]
        [TestCase("American Express", true)]
        [TestCase("Dankort", false)]
        public void ValidateCreditCardType_ParameterizedType_ReturnsTrueIfTypeIsValid(string type, bool shouldSucceed)
        {
            var succeeded = _uut.ValidateCreditCardType(type);
            Assert.That(succeeded, Is.EqualTo(shouldSucceed));
        }
    }
}
