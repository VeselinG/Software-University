using MXGP.Core.Contracts;
using MXGP.Models.Motorcycles.Contracts;
using MXGP.Models.Motorcycles.Models;
using MXGP.Models.Races;
using MXGP.Models.Races.Contracts;
using MXGP.Models.Riders;
using MXGP.Models.Riders.Contracts;
using MXGP.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MXGP.Core
{
    public class ChampionshipController : IChampionshipController
    {
        private Rider rider;
        private SpeedMotorcycle speedMotorcycle;   
        private PowerMotorcycle powerMotorcycle;
        private Race race;
        private RiderRepository riderRepository;
        private RaceRepository raceRepository;
        private MotorcycleRepository motorcycleRepository;

        public ChampionshipController()
        {
            this.riderRepository = new RiderRepository();
            this.raceRepository = new RaceRepository();
            this.motorcycleRepository = new MotorcycleRepository();
        }

        public string AddMotorcycleToRider(string riderName, string motorcycleModel)
        {
            if (this.riderRepository.GetByName(riderName)==null)
            {
                throw new InvalidOperationException($"Rider {riderName} could not be found.");
            }
            if (this.motorcycleRepository.GetByName(motorcycleModel)==null)
            {
                throw new InvalidOperationException($"Motorcycle {motorcycleModel} could not be found.");
            }
            IRider rider = this.riderRepository.GetByName(riderName);
            IMotorcycle motorcycle = this.motorcycleRepository.GetByName(motorcycleModel);
            rider.AddMotorcycle(motorcycle);
            this.motorcycleRepository.Remove(motorcycle);

            return $"Rider {riderName} received motorcycle {motorcycleModel}.";
        }

        public string AddRiderToRace(string raceName, string riderName)
        {
            if (this.raceRepository.GetByName(raceName)==null)
            {
                throw new InvalidOperationException($"Race {raceName} could not be found.");
            }
            if (this.riderRepository.GetByName(riderName)==null)
            {
                throw new InvalidOperationException($"Rider {riderName} could not be found.");
            }
            IRace race = this.raceRepository.GetByName(raceName);
            IRider rider = this.riderRepository.GetByName(riderName);
            race.AddRider(rider);
            this.riderRepository.Remove(rider);

            return $"Rider {riderName} added in {raceName} race.";
        }

        public string CreateMotorcycle(string type, string model, int horsePower)
        {
            if (type=="Speed")
            {
                if (!this.motorcycleRepository.GetAll().Any(n=>n.Model==model && n.GetType().Name=="SpeedMotorcycle"))
                {
                    this.speedMotorcycle = new SpeedMotorcycle(model, horsePower);
                    this.motorcycleRepository.Add(this.speedMotorcycle);
                    return $"{this.speedMotorcycle.GetType().Name} {this.speedMotorcycle.Model} is created.";
                }
                else
                {
                    throw new ArgumentException($"Motorcycle {model} is already created.");
                }
            }
            else
            {
                if (!this.motorcycleRepository.GetAll().Any(n => n.Model == model && n.GetType().Name == "PowerMotorcycle"))
                {
                    this.powerMotorcycle = new PowerMotorcycle(model, horsePower);
                    this.motorcycleRepository.Add(this.powerMotorcycle);
                    return $"{this.powerMotorcycle.GetType().Name} {this.powerMotorcycle.Model} is created.";
                }
                else
                {
                    throw new ArgumentException($"Motorcycle {model} is already created.");
                }
            }
        }

        public string CreateRace(string name, int laps)
        {
            if (this.raceRepository.GetByName(name)!=null)
            {
                throw new InvalidOperationException($"Race {name} is already created.");
            }
            this.race = new Race(name, laps);
            this.raceRepository.Add(this.race);
            return $"Race {name} is created.";
        }

        public string CreateRider(string riderName)
        {            
            if (this.riderRepository.GetByName(riderName)==null)
            {
                this.rider = new Rider(riderName);
                this.riderRepository.Add(this.rider);
                return $"Rider { riderName} is created.";
            }
            else
            {
                throw new ArgumentException($"Rider {riderName} is already created.");
            }            
        }

        public string StartRace(string raceName)
        {
            if (this.raceRepository.GetByName(raceName)==null)
            {
                throw new InvalidOperationException($"Race {raceName} could not be found.");
            }
            if (this.raceRepository.GetByName(raceName).Riders.Count < 3)
            {
                throw new InvalidOperationException($"Race {raceName} cannot start with less than 3 participants.");
            }
            else
            {
                int laps = this.raceRepository.GetByName(raceName).Laps;
                int count = 0;
                var repos = this.raceRepository.GetByName(raceName).Riders.OrderByDescending(n => n.Motorcycle.CalculateRacePoints(laps)).Take(3);
                this.raceRepository.Remove(this.raceRepository.GetByName(raceName));
                StringBuilder sb = new StringBuilder();
               
                foreach (var rider in repos)
                {
                    count++;
                    if (count == 1)
                    {
                        rider.WinRace();
                        sb.AppendLine($"Rider {rider.Name} wins {raceName} race.");
                    }
                    else if (count == 2)
                    {
                        sb.AppendLine($"Rider {rider.Name} is second in {raceName} race.");
                    }
                    else if(count == 3)
                    {
                        sb.AppendLine($"Rider {rider.Name} is third in {raceName} race.");
                    }
                }
                return sb.ToString().TrimEnd();
            }
        }
    }
}
