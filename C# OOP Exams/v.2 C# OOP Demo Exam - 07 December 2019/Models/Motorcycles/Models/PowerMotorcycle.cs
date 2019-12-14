using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Motorcycles.Models
{
    public class PowerMotorcycle : Motorcycle
    {
        private const double InitialCubicCentimeters = 450;
        private const int MinHorsePower = 70;
        private const int MaxHorsePower = 100;

        public PowerMotorcycle(string model, int horsePower)
            : base(model, horsePower, InitialCubicCentimeters)
        {
            if (this.HorsePower < MinHorsePower || this.HorsePower > MaxHorsePower) 
            {
                throw new ArgumentException($"Invalid horse power: {horsePower}.");
            }
        }       
    }
}
