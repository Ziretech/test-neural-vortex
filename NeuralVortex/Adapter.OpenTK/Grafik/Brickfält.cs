﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCase.NeuralVortex;
using UseCase.NeuralVortex.Spelvärld;
using UseCase.NeuralVortex.Visning;

namespace Adapter.OpenTK.Grafik
{
    public class Brickfält : IGrafik
    {
        private readonly IGrafikkommandon _gl;
        private readonly Kamera _kamera;
        private readonly IPositionskonverterare _konverterare;
        private readonly Bricka[] _definitioner;
        private readonly Skärmyta _brickstorlek;
        private readonly int _kartbredd;
        private readonly int[] _karta;

        public Brickfält(IGrafikkommandon glMock, Kamera kamera, IPositionskonverterare konverterare, Bricka[] definitioner, Skärmyta brickstorlek, int kartbredd, int[] karta)
        {
            _gl = glMock;
            _kamera = kamera;
            _definitioner = definitioner;
            _brickstorlek = brickstorlek;
            _kartbredd = kartbredd;
            _karta = karta;
            _konverterare = konverterare;
        }

        public void Visa(Skärmposition position)
        {
            var område = _konverterare.TillOmråde(_kamera.Synlighetsområde);

            for(var y = område.Botten; y <= område.Topp; y++)
                for(var x = område.Vänster; x <= område.Höger; x++)
                {
                    _definitioner[0].Visa(_konverterare.TillPunkt(new Spelvärldsposition(x, y)));
                }            
        }
    }
}
