using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Core.Contracts
{
    class IEngine
    {
        private readonly ChampionshipController championshipController;

        public IEngine()
        {
            this.championshipController = new ChampionshipController();
        }

        public void Run()
        {
            string line = Console.ReadLine();

            while (line != "End")
            {
                string[] commandItems = line.Split(" ",StringSplitOptions.RemoveEmptyEntries);
                string result = string.Empty;

                try
                {
                    switch (commandItems[0])
                    {
                        case "CreateRider":
                            result += this.championshipController.CreateRider(commandItems[1]);
                            break;

                        case "CreateMotorcycle":
                            result += this.championshipController.CreateMotorcycle(commandItems[1], commandItems[2], int.Parse(commandItems[3]));
                            break;

                        case "AddMotorcycleToRider":
                            result += this.championshipController.AddMotorcycleToRider(commandItems[1], commandItems[2]);                                                                
                            break;

                        case "AddRiderToRace":
                            result += this.championshipController.AddRiderToRace(commandItems[1], commandItems[2]);
                            break;

                        case "CreateRace":
                            result += this.championshipController.CreateRace(commandItems[1], int.Parse(commandItems[2]));
                            break;

                        case "StartRace":
                            result += this.championshipController.StartRace(commandItems[1]);
                            break;

                        default:
                            break;
                    }

                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                line = Console.ReadLine();
            }
        }
    }
}
