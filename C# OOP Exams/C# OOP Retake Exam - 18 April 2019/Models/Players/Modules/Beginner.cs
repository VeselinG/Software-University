using System;
using System.Collections.Generic;
using System.Text;
using PlayersAndMonsters.Repositories.Contracts;

namespace PlayersAndMonsters.Models.Players.Modules
{
    public class Beginner : Player
    {
        private const int InitialHealthPoints = 50;
        public Beginner(ICardRepository cardRepository, string username)
            : base(cardRepository, username, InitialHealthPoints)
        {

        }
    }
}
