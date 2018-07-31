﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCase.NeuralVortex.Spelvärld;

namespace UseCase.NeuralVortex.Kartgenerering
{
    public interface IKartritare
    {
        void SkapaYta(int index, Spelvärldsområde område);
        void Skapa(int index, Spelvärldsposition position);
    }
}
