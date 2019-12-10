using MXGP.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Motorcycles.Models
{
    public class SpeedMotorcycle : Motorcycle
    {
        private const double InitialCubicCentimiters = 125;
        private const int MinimumHorsePower = 50;
        private const int MaximumHorsePower = 69;
        public SpeedMotorcycle(string model, int horsePower)
            : base(model, horsePower, InitialCubicCentimiters)
        {
            if (horsePower < MinimumHorsePower || horsePower > MaximumHorsePower)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidHorsePower, horsePower));
            }
        }
    }
}
