using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Motorcycles.Models
{
    public class SpeedMotorcycle : Motorcycle
    {
        private const double InitialCubicCentimeters = 125;
        private const int MinHorsePower = 50;
        private const int MaxHorsePower = 69;
        public SpeedMotorcycle(string model, int horsePower)
            : base(model, horsePower, InitialCubicCentimeters)
        {
            if (this.HorsePower < MinHorsePower || this.HorsePower > MaxHorsePower)
            {
                throw new ArgumentException($"Invalid horse power: {horsePower}.");
            }
        }
    }
}
