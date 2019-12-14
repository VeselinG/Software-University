using System;
using System.Collections.Generic;
using System.Text;

namespace PlayersAndMonsters.Models.Cards.Modules
{
    public class TrapCard : Card
    {
        private const int InitialDamagePoints = 120;
        private const int InitialHealthPoints = 5;
        public TrapCard(string name)
            : base(name, InitialDamagePoints, InitialHealthPoints)
        {
        }
    }
}
