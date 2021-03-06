﻿using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCase.NeuralVortex.Spelvärld;

namespace UseCase.NeuralVortex.Spec
{
    [TestFixture]
    public class DödaKritisktSkadadeSpec
    {
        [Test]
        public void Avslutar_inte_spelet_om_huvudkaraktären_saknas()
        {
            var spelvärld = Substitute.For<ISpelvärld>();
            var dödaKritisktSkadade = new DödaKritisktSkadade(spelvärld);
            Assert.That(dödaKritisktSkadade.Döda(), Is.EqualTo(SpeletsFortsättning.Fortsätt));
        }

        [Test]
        public void Avslutar_spelet_om_huvudkaraktären_är_kritiskt_skadad()
        {
            var spelvärld = Substitute.For<ISpelvärld>();
            spelvärld.Huvudkaraktär.Returns(new Huvudkaraktär(0));
            var dödaKritisktSkadade = new DödaKritisktSkadade(spelvärld);
            Assert.That(dödaKritisktSkadade.Döda(), Is.EqualTo(SpeletsFortsättning.Avsluta));
        }

        [Test]
        public void Avslutar_inte_spelet_om_huvudkaraktären_inte_är_kritiskt_skadad()
        {
            var spelvärld = Substitute.For<ISpelvärld>();
            spelvärld.Huvudkaraktär.Returns(new Huvudkaraktär());
            var dödaKritisktSkadade = new DödaKritisktSkadade(spelvärld);
            Assert.That(dödaKritisktSkadade.Döda(), Is.EqualTo(SpeletsFortsättning.Fortsätt));
        }

        [Test]
        public void Dödar_fiende_som_är_kritiskt_skadad()
        {
            var kritisktSkadadFiende = new Fiende { ÄrKritisktSkadad = true };
            var spelvärld = Substitute.For<ISpelvärld>();
            spelvärld.Fienden.Returns(new[] { kritisktSkadadFiende });
            new DödaKritisktSkadade(spelvärld).Döda();
            spelvärld.Received().DödaFiende(kritisktSkadadFiende);
        }

        [Test]
        public void Dödar_inte_fiende_som_inte_är_kritiskt_skadad()
        {
            var spelvärld = Substitute.For<ISpelvärld>();
            spelvärld.Fienden.Returns(new[] { new Fiende() });
            new DödaKritisktSkadade(spelvärld).Döda();
            spelvärld.DidNotReceive().DödaFiende(Arg.Any<Fiende>());
        }

        [Test]
        public void Dödar_endst_kritiskt_skadad_fiende_av_två()
        {
            var kritisktSkadadFiende = new Fiende { ÄrKritisktSkadad = true };
            var spelvärld = Substitute.For<ISpelvärld>();
            spelvärld.Fienden.Returns(new[] { new Fiende(), kritisktSkadadFiende });
            new DödaKritisktSkadade(spelvärld).Döda();
            spelvärld.Received().DödaFiende(kritisktSkadadFiende);
        }

        [Test]
        public void Dödar_alla_kritiskt_skadade_fienden_av_fem()
        {
            var spelvärld = Substitute.For<ISpelvärld>();
            var kritisktSkadadFiende1 = new Fiende { ÄrKritisktSkadad = true };
            var kritisktSkadadFiende2 = new Fiende { ÄrKritisktSkadad = true };
            spelvärld.Fienden.Returns(new[] { new Fiende(), kritisktSkadadFiende1, new Fiende(), kritisktSkadadFiende2, new Fiende() });
            new DödaKritisktSkadade(spelvärld).Döda();
            spelvärld.Received().DödaFiende(kritisktSkadadFiende1);
            spelvärld.Received().DödaFiende(kritisktSkadadFiende2);
        }

        [Test]
        public void Gör_undantag_från_att_skapas_utan_spelvärld()
        {
            try
            {
                new DödaKritisktSkadade(null);
                Assert.Fail("Inget undantag gjordes.");
            }
            catch(ArgumentException undantag)
            {
                Assert.That(undantag.Message.ToLower(), Does.Contain("utan spelvärld"));
            }
        }
    }
}
