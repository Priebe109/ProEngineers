using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linklaget;
using NUnit.Framework;

namespace Exercise13.Test.Unit
{
	[TestFixture]
	internal class DeslipTests
	{
	    [Test]
	    public void Deslip_SlippedACharacter_ResultIsA()
	    {
            var testBuffer = new[] { (byte)'A', (byte)'B', (byte)'C', (byte)'A' };
            Assert.That(Link.Deslip(testBuffer).Item1[0], Is.EqualTo((byte)'A'));
        }

        [Test]
        public void Deslip_SlippedBCharacter_ResultIsB()
        {
            var testBuffer = new[] { (byte)'A', (byte)'B', (byte)'D', (byte)'A' };
            Assert.That(Link.Deslip(testBuffer).Item1[0], Is.EqualTo((byte)'B'));
        }

        [Test]
        public void Deslip_SlippedCDECharacters_ResultIsCDE()
        {
            var testBuffer = new[] { (byte)'A', (byte)'C', (byte)'D', (byte)'E', (byte)'A' };
            var expectedResult = new[] { (byte)'C', (byte)'D', (byte)'E' };
            Assert.That(Link.Deslip(testBuffer).Item1, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Deslip_LongerSlippedArray_ResultIsCorrect()
        {
            var testBuffer = new[] { (byte)'A', (byte)'C', (byte)'D', (byte)'E', (byte)'B', (byte)'C', (byte)'D', (byte)'E', (byte)'B', (byte)'D', (byte)'A' };
            var expectedResult = new[] { (byte)'C', (byte)'D', (byte)'E', (byte)'A', (byte)'D', (byte)'E', (byte)'B' };
            Assert.That(Link.Deslip(testBuffer).Item1, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Deslip_ThreeACharacters_DeslipEndsAtSecondA()
        {
            var testBuffer = new[] { (byte)'A', (byte)'C', (byte)'D', (byte)'A', (byte)'B', (byte)'C', (byte)'A' };
            var expectedResult = new[] { (byte)'C', (byte)'D' };
            Assert.That(Link.Deslip(testBuffer).Item1, Is.EqualTo(expectedResult));
        }
    }
}