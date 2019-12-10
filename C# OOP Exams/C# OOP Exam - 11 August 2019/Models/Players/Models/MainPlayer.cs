using System;
using System.Collections.Generic;
using System.Text;

namespace ViceCity.Models.Players.Models
{
    public class MainPlayer : Player
    {
        private const string InitialName = "Tommy Vercetti";
        private const int InitialLifePoints = 100;
        public MainPlayer() 
            : base(InitialName, InitialLifePoints)
        {

        }
    }
}
