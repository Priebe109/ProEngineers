using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Linklaget;


namespace Exercise13.Test.Unit
{
    [TestFixture]
    internal class SlipTests
    {
        [Test]
        public void Slip_Afirstplace_ReturnsABC()
        {
            byte[] buf = new[] {(byte) 'A'};
            byte[] bufTest = new[] { (byte)'A',(byte)'B', (byte)'C' };
            Assert.That(Link.Slip(buf,1).Item1[0],Is.EqualTo(bufTest[0]));
            Assert.That(Link.Slip(buf, 1).Item1[1], Is.EqualTo(bufTest[1]));
            Assert.That(Link.Slip(buf, 1).Item1[2], Is.EqualTo(bufTest[2]));
        }

        [Test]
        public void Slip_Bfirstplace_ReturnABD()
        {
            byte[] buf = new[] { (byte)'B' };
            byte[] bufTest = new[] { (byte)'A', (byte)'B', (byte)'D' };
            Assert.That(Link.Slip(buf, 1).Item1[0], Is.EqualTo(bufTest[0]));
            Assert.That(Link.Slip(buf, 1).Item1[1], Is.EqualTo(bufTest[1]));
            Assert.That(Link.Slip(buf, 1).Item1[2], Is.EqualTo(bufTest[2]));
        }


        [Test]
        public void Slip_Brandomplace_ReturnA123BD()
        {
            byte[] buf = new[] { (byte)'1', (byte)'2', (byte)'3', (byte)'B' };
            byte[] bufTest = new[] { (byte)'A', (byte)'1', (byte)'2', (byte)'3', (byte)'B',(byte)'D', (byte)'A' };

            var slippedBuffer = Link.Slip(buf, buf.Length);

            Assert.That(slippedBuffer.Item1, Is.EqualTo(bufTest));
        }

        [Test]
        public void Slip_SlipAQ123BQW_ReturnABCQ123BDQWA()
        {
            byte[] buf = new[] { (byte)'A', (byte)'Q', (byte)'1', (byte)'2', (byte)'3', (byte)'B', (byte)'Q', (byte)'W' };
            byte[] bufTest = new[] { (byte)'A', (byte)'B', (byte)'C', (byte)'Q', (byte)'1', (byte)'2', (byte)'3', (byte)'B', (byte)'D', (byte)'Q', (byte)'W', (byte)'A' };

            var slippedBuffer = Link.Slip(buf, buf.Length);

            Assert.That(slippedBuffer.Item1, Is.EqualTo(bufTest));
        }
    }
}
