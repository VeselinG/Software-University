using MXGP.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Motorcycles.Models
{
    public class PowerMotorcycle : Motorcycle
    {
        private const double InitialCubicCentimiters = 450;
        private const int MinimumHorsePower = 70;
        private const int MaximumHorsePower = 100;
        public PowerMotorcycle(string model, int horsePower)
            : base(model, horsePower, InitialCubicCentimiters)
        {
            if (horsePower<MinimumHorsePower || horsePower>MaximumHorsePower)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidHorsePower,horsePower));
            }            
        }

        
    }
}
