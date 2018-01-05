﻿using Adapter.OpenTK.Grafik;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCase.NeuralVortex.Visning;

namespace Adapter.OpenTK.Spec.Grafik
{
    [TestFixture]
    public class BrickfältSpec
    {
        [Ignore("Fixa kameran först")]
        [Test]
        public void Bricka_borde_visa_en_bild_på_angiven_position()
        {
            var glMock = new GrafikkommandonMock();
            var bricka = new Brickfält();
            bricka.Visa();

            Assert.That(glMock.Texturverifierare.Count, Is.EqualTo(1));
            Assert.That(glMock.Texturverifierare[0].StämmerHörn1(1, 22));
            Assert.That(glMock.Texturverifierare[0].StämmerHörn2(11, 2));

            Assert.That(glMock.Hörnverifierare.Count, Is.EqualTo(1));
            Assert.That(glMock.Hörnverifierare[0].StämmerHörn1(20, 30));
            Assert.That(glMock.Hörnverifierare[0].StämmerHörn2(30, 50));
        }
    }
}
