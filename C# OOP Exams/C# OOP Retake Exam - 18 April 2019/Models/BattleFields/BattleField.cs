using PlayersAndMonsters.Models.BattleFields.Contracts;
using PlayersAndMonsters.Models.Players.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayersAndMonsters.Models.BattleFields
{
    public class BattleField : IBattleField
    {
        public void Fight(IPlayer attackPlayer, IPlayer enemyPlayer)
        {
            if (attackPlayer.IsDead == true)
            {
                throw new ArgumentException("Player is dead!");
            }

            if (enemyPlayer.IsDead==true)
            {
                throw new ArgumentException("Player is dead!");
            }

            if (attackPlayer.GetType().Name=="Beginner")
            {
                attackPlayer.Health += 40;
                attackPlayer.CardRepository.Cards.ToList().ForEach(c => c.DamagePoints += 30);
            }

            if (enemyPlayer.GetType().Name == "Beginner")
            {
                enemyPlayer.Health += 40;
                enemyPlayer.CardRepository.Cards.ToList().ForEach(c=>c.DamagePoints+=30);
            }

            attackPlayer.Health += attackPlayer.CardRepository.Cards.Sum(c => c.HealthPoints);
            enemyPlayer.Health += enemyPlayer.CardRepository.Cards.Sum(c => c.HealthPoints);

            while (true)
            {
                if (attackPlayer.IsDead == true || enemyPlayer.IsDead == true)
                {
                    break;
                }

                int attackerDamage = attackPlayer.CardRepository.Cards.Sum(c => c.DamagePoints);
                enemyPlayer.TakeDamage(attackerDamage);
                if (enemyPlayer.IsDead == true)
                {
                    break;
                }

                int enemyDamage = enemyPlayer.CardRepository.Cards.Sum(c => c.DamagePoints);
                attackPlayer.TakeDamage(enemyDamage);
                if (enemyPlayer.IsDead==true)
                {
                    break;
                }
            }

        }
    }
}
