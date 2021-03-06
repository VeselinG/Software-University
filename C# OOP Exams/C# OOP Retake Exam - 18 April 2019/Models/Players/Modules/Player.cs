﻿using PlayersAndMonsters.Models.Players.Contracts;
using PlayersAndMonsters.Repositories.Contracts;
using PlayersAndMonsters.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayersAndMonsters.Models.Players.Modules
{
    public abstract class Player : IPlayer
    {
        private string username;
        private int health;

        protected Player(ICardRepository cardRepository, string username, int health)
        {
            CardRepository = cardRepository;
            Username = username;
            Health = health;
        }

        public ICardRepository CardRepository  {get; private set;} // TODO: check setter

        public string Username
        {
            get => username;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Player's username cannot be null or an empty string. "); // TODO: spase .
                }
                username = value;
            }
        }

        public int Health
        { 
            get => health;
            set
            {
                if (value<0)
                {
                    throw new ArgumentException("Player's health bonus cannot be less than zero. ");
                }
                health = value;
            }
        }

        public bool IsDead => this.Health <= 0 ? true : false;

        public void TakeDamage(int damagePoints)
        {
            if (damagePoints<0)
            {
                throw new ArgumentException("Damage points cannot be less than zero.");
            }
            if (this.Health-damagePoints<0)
            {
                this.Health =0;
            }
            else
            {
                this.Health -= damagePoints;
            }
            
        }
    }
}
