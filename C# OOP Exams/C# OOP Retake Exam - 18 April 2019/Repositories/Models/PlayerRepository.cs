using PlayersAndMonsters.Models.Players.Contracts;
using PlayersAndMonsters.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayersAndMonsters.Repositories.Models
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly List<IPlayer> players;
        public int Count => this.players.Count;

        public PlayerRepository()
        {
            this.players = new List<IPlayer>();
        }
        public IReadOnlyCollection<IPlayer> Players => this.players.AsReadOnly();

        public void Add(IPlayer player)
        {
            if (player==null)
            {
                throw new ArgumentException("Player cannot be null");
            }

            if (this.players.Any(n=>n.Username==player.Username))
            {
                throw new ArgumentException($"Player {player.Username} already exists!");
            }

            this.players.Add(player);
        }

        public IPlayer Find(string username)
        {
            return this.players.FirstOrDefault(n => n.Username == username);
        }

        public bool Remove(IPlayer player)
        {
            if (player==null)
            {
                throw new ArgumentException("Player cannot be null");
            }

            if (this.players.Any(n=>n.Username==player.Username))
            {
                this.players.Remove(player);
                return true;
            }

            return false;
        }
    }
}
