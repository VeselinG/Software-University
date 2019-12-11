namespace MortalEngines.Core
{
    using Contracts;
    using MortalEngines.Entities.Contracts;
    using MortalEngines.Entities.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class MachinesManager : IMachinesManager
    {
        private readonly List<IPilot> pilots;
        private readonly List<IMachine> machines;
        private Pilot pilot; 
        private Tank tank; 
        private Fighter fighter;

        public MachinesManager()
        {
            this.pilots = new List<IPilot>();
            this.machines = new List<IMachine>();
        }

        public string HirePilot(string name)
        {            
            if (this.pilots.FirstOrDefault(n=>n.Name==name)==null)
            {
                this.pilot = new Pilot(name);
                this.pilots.Add(this.pilot);
                return $"Pilot {this.pilot.Name} hired";
            }
            return $"Pilot {name} is hired already";
        }

        public string ManufactureTank(string name, double attackPoints, double defensePoints)
        {
            if (this.machines.FirstOrDefault(n=>n.Name==name && n.GetType().Name=="Tank")==null)
            {
                this.tank = new Tank(name, attackPoints, defensePoints);
                this.machines.Add(this.tank);
                return $"Tank {this.tank.Name} manufactured - attack: {this.tank.AttackPoints:f2}; defense: {this.tank.DefensePoints:f2}";
            }
            return $"Machine {name} is manufactured already";
        }

        public string ManufactureFighter(string name, double attackPoints, double defensePoints)
        {
            if (this.machines.FirstOrDefault(n => n.Name == name && n.GetType().Name == "Fighter") == null)
            {
                this.fighter = new Fighter(name, attackPoints, defensePoints);
                this.machines.Add(this.fighter);
                return $"Fighter {this.fighter.Name} manufactured - attack: {this.fighter.AttackPoints:f2}; defense: {this.fighter.DefensePoints:f2}; aggressive: ON";
            }
            return $"Machine {name} is manufactured already";
        }

        public string EngageMachine(string selectedPilotName, string selectedMachineName)
        {
            if (this.pilots.FirstOrDefault(n=>n.Name==selectedPilotName)==null)
            {
                return $"Pilot {selectedPilotName} could not be found";
            }
            if (this.machines.FirstOrDefault(n=>n.Name==selectedMachineName)==null)
            {
                return $"Machine {selectedMachineName} could not be found";
            }
            if (this.machines.FirstOrDefault(n => n.Name == selectedMachineName).Pilot!=null)
            {
                return $"Machine {selectedMachineName} is already occupied";
            }

            IPilot pilot = this.pilots.FirstOrDefault(n => n.Name == selectedPilotName);
            IMachine machine = this.machines.FirstOrDefault(n => n.Name == selectedMachineName);
            pilot.AddMachine(machine);
            machine.Pilot = pilot;

            return $"Pilot {selectedPilotName} engaged machine {selectedMachineName}";
        }

        public string AttackMachines(string attackingMachineName, string defendingMachineName)
        {
            if (this.machines.FirstOrDefault(n=>n.Name==attackingMachineName)==null)
            {
                return $"Machine {attackingMachineName} could not be found";
            }
            if (this.machines.FirstOrDefault(n=>n.Name==defendingMachineName)==null)
            {
                return $"Machine {defendingMachineName} could not be found";
            }
            if (this.machines.FirstOrDefault(n => n.Name == attackingMachineName).HealthPoints<=0)
            {
                return $"Dead machine {attackingMachineName} cannot attack or be attacked";
            }
            if (this.machines.FirstOrDefault(n => n.Name == defendingMachineName).HealthPoints <= 0)
            {
                return $"Dead machine {defendingMachineName} cannot attack or be attacked";
            }

            IMachine attaackingMachine = this.machines.FirstOrDefault(n => n.Name == attackingMachineName);
            IMachine defendingMachine = this.machines.FirstOrDefault(n => n.Name == defendingMachineName);
            attaackingMachine.Attack(defendingMachine);
            
            return $"Machine {defendingMachineName} was attacked by machine {attackingMachineName} - current health: {defendingMachine.HealthPoints:f2}";
        }

        public string PilotReport(string pilotReporting)
        {
            return this.pilots.FirstOrDefault(n => n.Name == pilotReporting).Report();
        }

        public string MachineReport(string machineName)
        {
            return this.machines.FirstOrDefault(n => n.Name == machineName).ToString();
        }

        public string ToggleFighterAggressiveMode(string fighterName)
        {
            if (this.machines.FirstOrDefault(n => n.Name == fighterName && n.GetType().Name == "Fighter") != null)
            {
                IFighter wantedFighter = (Fighter)this.machines.FirstOrDefault(n => n.Name == fighterName && n.GetType().Name == "Fighter");
                wantedFighter.ToggleAggressiveMode();
                return $"Fighter {wantedFighter.Name} toggled aggressive mode";
            }
            return $"Machine {fighterName} could not be found";
        }

        public string ToggleTankDefenseMode(string tankName)
        {
            if (this.machines.FirstOrDefault(n=>n.Name==tankName && n.GetType().Name=="Tank")!=null)
            {
                ITank wantedTank = (Tank)this.machines.FirstOrDefault(n => n.Name == tankName && n.GetType().Name == "Tank");
                wantedTank.ToggleDefenseMode();
                return $"Tank {wantedTank.Name} toggled defense mode";
            }
            return $"Machine {tankName} could not be found";
        }
    }
}