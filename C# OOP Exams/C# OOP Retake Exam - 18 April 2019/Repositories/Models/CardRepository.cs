using PlayersAndMonsters.Models.Cards.Contracts;
using PlayersAndMonsters.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayersAndMonsters.Repositories.Models
{
    public class CardRepository : ICardRepository
    {
        private readonly List<ICard> listOfCards;

        public CardRepository()
        {
            this.listOfCards = new List<ICard>();
        }
        public int Count => this.listOfCards.Count;

        public IReadOnlyCollection<ICard> Cards => listOfCards.AsReadOnly();

        public void Add(ICard card)
        {
            if (card==null)
            {
                throw new ArgumentException("Card cannot be null!");
            }

            if (this.listOfCards.Any(c=>c.Name==card.Name))
            {
                throw new ArgumentException($"Card {card.Name} already exists!");
            }

            this.listOfCards.Add(card);
        }

        public ICard Find(string name)
        {
            return this.listOfCards.FirstOrDefault(c => c.Name == name);
        }

        public bool Remove(ICard card)
        {
            if (card==null)
            {
                throw new ArgumentException("Card cannot be null!");
            }

            if (this.listOfCards.Any(c=>c.Name==card.Name))
            {
                this.listOfCards.Remove(card);
                return true;
            }

            return false;
        }
    }
}
