namespace PlayersAndMonsters.Core
{
    using System;
    using System.Text;
    using Contracts;
    using PlayersAndMonsters.Common;
    using PlayersAndMonsters.Models.BattleFields;
    using PlayersAndMonsters.Models.BattleFields.Contracts;
    using PlayersAndMonsters.Models.Cards.Contracts;
    using PlayersAndMonsters.Models.Cards.Modules;
    using PlayersAndMonsters.Models.Players.Contracts;
    using PlayersAndMonsters.Models.Players.Modules;
    using PlayersAndMonsters.Repositories.Contracts;
    using PlayersAndMonsters.Repositories.Models;

    public class ManagerController : IManagerController
    {
        private readonly ICardRepository cardRepository;
        private IPlayerRepository playerRepository;
        private IBattleField battleField;
        public ManagerController()
        {
            this.cardRepository = new CardRepository();
            this.playerRepository = new PlayerRepository();
            this.battleField = new BattleField();
        }

        public string AddPlayer(string type, string username)
        {
            if (type=="Beginner")
            {
                IPlayer begiinerPLayer = new Beginner(new CardRepository(), username);
                this.playerRepository.Add(begiinerPLayer);
                return string.Format(ConstantMessages.SuccessfullyAddedPlayer, type, username);
            }
            if (type == "Advanced")
            {
                IPlayer advancedPLayer = new Advanced(new CardRepository(), username);
                this.playerRepository.Add(advancedPLayer);
                return string.Format(ConstantMessages.SuccessfullyAddedPlayer, type, username);
            }

            return "";
           
        }

        public string AddCard(string type, string name)
        {
            if (type=="Magic")
            {
                ICard card = new MagicCard(name);
                this.cardRepository.Add(card);

                return string.Format(ConstantMessages.SuccessfullyAddedCard,"Magic",name);
            }
            if (type == "Trap")
            {
                ICard card = new TrapCard(name);
                this.cardRepository.Add(card);

                return string.Format(ConstantMessages.SuccessfullyAddedCard, "Trap", name);
            }
            return "";
        }

        public string AddPlayerCard(string username, string cardName)
        {
            IPlayer findedPlayer = this.playerRepository.Find(username);
            ICard findedCard = this.cardRepository.Find(cardName);

            findedPlayer.CardRepository.Add(findedCard);
            return string.Format(ConstantMessages.SuccessfullyAddedPlayerWithCards, findedCard.Name, findedPlayer.Username);
        }

        public string Fight(string attackUser, string enemyUser)
        {
            IPlayer attakerPlayer = this.playerRepository.Find(attackUser);
            IPlayer enemyPlayer = this.playerRepository.Find(enemyUser);

            this.battleField.Fight(attakerPlayer, enemyPlayer);

            return string.Format(ConstantMessages.FightInfo, attakerPlayer.Health, enemyPlayer.Health);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var user in this.playerRepository.Players)
            {
                sb.AppendLine(string.Format(ConstantMessages.PlayerReportInfo, user.Username, user.Health, user.CardRepository.Count));
                foreach (var card in user.CardRepository.Cards)
                {
                    sb.AppendLine(string.Format(ConstantMessages.CardReportInfo, card.Name, card.DamagePoints));
                }
                sb.AppendLine("###");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
