﻿using System;
using UseCase.NeuralVortex.Spelvärld;
using UseCase.NeuralVortex.Visning;

namespace UseCase.NeuralVortex
{
    public class Positionskonverterare : IPositionskonverterare
    {
        private readonly Skärmyta _brickyta;

        public Positionskonverterare(Skärmyta brickyta)
        {
            if(brickyta.Bredd < 1)
            {
                throw new ArgumentException($"Bredden för brickyta för positionskonverterare får inte vara mindre än 1 (angiven yta {brickyta.Bredd}x{brickyta.Höjd}).");
            }
            if (brickyta.Höjd < 1)
            {
                throw new ArgumentException($"Höjden för brickyta för positionskonverterare får inte vara mindre än 1 (angiven yta {brickyta.Bredd}x{brickyta.Höjd}).");
            }
            _brickyta = brickyta;
        }

        public Skärmposition TillPunkt(Spelvärldsposition position)
        {
            return new Skärmposition(position.X * _brickyta.Bredd, position.Y * _brickyta.Höjd);
        }

        public Spelvärldsområde TillOmråde(Skärmområde område)
        {
            return new Spelvärldsområde(område.Vänster / _brickyta.Bredd, område.Botten / _brickyta.Höjd, område.Höger / _brickyta.Bredd, område.Topp / _brickyta.Höjd);
        }
    }
}