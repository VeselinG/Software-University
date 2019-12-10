using MXGP.Models.Motorcycles.Contracts;
using MXGP.Models.Riders.Contracts;
using MXGP.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Riders
{
    public class Rider : IRider
    {
        private string name;
        private IMotorcycle motorcycle;
        private int numberOfWins;
        private bool canParticipate;

        public Rider(string name)
        {
            this.Name = name;

        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidName, value, 5));
                }
                this.name = value;
            }
        }

        public IMotorcycle Motorcycle { get => motorcycle; private set => motorcycle = value; }

        public int NumberOfWins { get => numberOfWins; private set => numberOfWins = value; }

        public bool CanParticipate 
        {
            get
            {
                if (this.motorcycle == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
            

        public void AddMotorcycle(IMotorcycle motorcycle)
        {
            if (motorcycle == null)
            {
                throw new ArgumentNullException(ExceptionMessages.MotorcycleInvalid);
            }
            this.Motorcycle = motorcycle;
        }

        public void WinRace()
        {
            this.numberOfWins++;
        }
    }
}
