using System;
using System.Collections.Generic;
using System.Text;

namespace PlayersAndMonsters.Models.Cards.Modules
{
    public class MagicCard : Card
    {
        private const int InitialDamagePoints = 5;
        private const int InitialHealthPoints = 80;
        public MagicCard(string name)
            : base(name, InitialDamagePoints, InitialHealthPoints)
        {
        }
    }
}
