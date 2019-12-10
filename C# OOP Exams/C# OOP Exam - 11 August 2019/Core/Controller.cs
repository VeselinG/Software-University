using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViceCity.Core.Contracts;
using ViceCity.Models.Guns.Contracts;
using ViceCity.Models.Guns.Models;
using ViceCity.Models.Neghbourhoods;
using ViceCity.Models.Players.Contracts;
using ViceCity.Models.Players.Models;
using ViceCity.Repositories;

namespace ViceCity.Core
{
    public class Controller : IController
    {
        private readonly List<IPlayer> PlayersList;
        private readonly GunRepository gunRepository;
        private MainPlayer mainPlayer;
        private GangNeighbourhood gangNeighbourhood;

        public Controller()
        {
            this.PlayersList = new List<IPlayer>();
            this.gunRepository = new GunRepository();
            this.mainPlayer = new MainPlayer();
            this.gangNeighbourhood = new GangNeighbourhood();
        }
        public string AddGun(string type, string name)
        {                               
            if (type == "Pistol")
            {
                IGun pistol = new Pistol(name);
                this.gunRepository.Add(pistol);
                return $"Successfully added {name} of type: {type}";
            }

            if (type=="Rifle")
            {
                IGun rifle = new Rifle(name);
                this.gunRepository.Add(rifle);
                return $"Successfully added {name} of type: {type}";
            }

            return "Invalid gun type!";
        }

        public string AddGunToPlayer(string name)
        {
            IGun gun = this.gunRepository.Models.FirstOrDefault();
            if (this.gunRepository.Models.Count==0)
            {
                return "There are no guns in the queue!";
            }
            if (name== "Vercetti")
            {
                this.mainPlayer.GunRepository.Add(gun);
                this.gunRepository.Remove(gun);
                return $"Successfully added {gun.Name} to the Main Player: Tommy Vercetti";
            }
            if (this.PlayersList.FirstOrDefault(n=>n.Name==name)==null)
            {
                return "Civil player with that name doesn't exists!";
            }

            IPlayer civilPlayer = this.PlayersList.FirstOrDefault(n => n.Name == name);
            civilPlayer.GunRepository.Add(gun);
            this.gunRepository.Remove(gun);

            return $"Successfully added {gun.Name} to the Civil Player: {civilPlayer.Name}";
        }

        public string AddPlayer(string name)
        {
            IPlayer civilPlayer = new CivilPlayer(name);
            this.PlayersList.Add(civilPlayer);
            return $"Successfully added civil player: {civilPlayer.Name}!";
        }

        public string Fight()
        {
            StringBuilder sb = new StringBuilder();

            this.gangNeighbourhood.Action(this.mainPlayer, this.PlayersList);
            if (this.mainPlayer.LifePoints==100 && this.PlayersList.All(c=>c.IsAlive))
            {
                sb.AppendLine("Everything is okay!");
            }
            else 
            {
                sb.AppendLine("A fight happened:");
                sb.AppendLine($"Tommy live points: {this.mainPlayer.LifePoints}!");
                sb.AppendLine($"Tommy has killed: {this.PlayersList.Where(c=>c.IsAlive==false).Count()} players!");
                sb.AppendLine($"Left Civil Players: {this.PlayersList.Where(c => c.IsAlive == true).Count()}!");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
