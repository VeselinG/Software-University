using System;
using System.Collections.Generic;
using System.Text;
using ViceCity.Models.Guns.Contracts;
using ViceCity.Models.Players.Contracts;
using ViceCity.Repositories;
using ViceCity.Repositories.Contracts;

namespace ViceCity.Models.Players.Models
{
    public abstract class Player : IPlayer
    {
        private string name;
        private int lifePoints;

        protected Player(string name, int lifePoints)
        {
            this.Name = name;
            this.LifePoints = lifePoints;
            this.IsAlive = true;
            this.GunRepository = new GunRepository();
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Player's name cannot be null or a whitespace!");
                }
                name = value;
            }
        }

        public bool IsAlive { get; private set; }

        public IRepository<IGun> GunRepository { get; private set; }

        public int LifePoints
        { 
            get => lifePoints;
            private set
            {
                if (value<0)
                {
                    throw new ArgumentException("Player life points cannot be below zero!");
                }
                lifePoints = value;
            }
        }

        public void TakeLifePoints(int points)
        {
            if (this.LifePoints-points<=0)
            {
                this.LifePoints = 0;
                this.IsAlive = false;
            }
            else
            {
                this.LifePoints -= points;
            }
        }
    }
}
