using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViceCity.Models.Guns.Contracts;
using ViceCity.Models.Neghbourhoods.Contracts;
using ViceCity.Models.Players.Contracts;

namespace ViceCity.Models.Neghbourhoods
{
    public class GangNeighbourhood : INeighbourhood
    {       
        public void Action(IPlayer mainPlayer, ICollection<IPlayer> civilPlayers)
        {
            while (true)
            {
                IGun mainPlayerGun = mainPlayer.GunRepository.Models.FirstOrDefault(g => g.CanFire == true);
                if (mainPlayerGun==null)
                {
                    break;
                }

                IPlayer civilPlayer = civilPlayers.FirstOrDefault(c => c.IsAlive == true);
                if (civilPlayer==null)
                {
                    break;
                }

                civilPlayer.TakeLifePoints(mainPlayerGun.Fire());
            }
            while (true)
            {
                IPlayer civilPlayer = civilPlayers.FirstOrDefault(c => c.IsAlive == true);
                if (civilPlayer == null)
                {
                    break;
                }

                IGun civilPlayerGun = civilPlayer.GunRepository.Models.FirstOrDefault(g => g.CanFire == true);
                if (civilPlayerGun==null)
                {
                    break;
                }

                mainPlayer.TakeLifePoints(civilPlayerGun.Fire());
                if (mainPlayer.IsAlive==false)
                {
                    break;
                }
            }
        }
    }
}
