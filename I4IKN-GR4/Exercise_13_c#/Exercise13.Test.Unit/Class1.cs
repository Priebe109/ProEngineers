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
    internal class LinkLayerTest
    {
        [Test]
        public void Slip_A_ReturnsABC()
        {
            byte[] buf = new[] {(byte) 'A'};
            byte[] bufTest = new[] { (byte)'A',(byte)'B', (byte)'C' };
            Assert.That(Link.Slip(buf,1).Item1[0],Is.EqualTo(bufTest[0]));
            Assert.That(Link.Slip(buf, 1).Item1[1], Is.EqualTo(bufTest[1]));
            Assert.That(Link.Slip(buf, 1).Item1[2], Is.EqualTo(bufTest[2]));
        }

        [Test]
        public void Slip_B_ReturnABD()
        {
            byte[] buf = new[] { (byte)'B' };
            byte[] bufTest = new[] { (byte)'A', (byte)'B', (byte)'D' };
            Assert.That(Link.Slip(buf, 1).Item1[0], Is.EqualTo(bufTest[0]));
            Assert.That(Link.Slip(buf, 1).Item1[1], Is.EqualTo(bufTest[1]));
            Assert.That(Link.Slip(buf, 1).Item1[2], Is.EqualTo(bufTest[2]));
        }

    }
}
