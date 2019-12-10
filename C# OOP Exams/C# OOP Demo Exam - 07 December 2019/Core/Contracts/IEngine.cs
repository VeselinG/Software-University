using MXGP.IO;
using MXGP.IO.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Core.Contracts
{
    public class IEngine
    {
        private IReader reader;
        private IWriter writer;
        private ChampionshipController controller;

        public IEngine()
        {
            this.reader = new ConsoleReader();
            this.writer = new ConsoleWriter();
            this.controller = new ChampionshipController();
        }

        public void Run()
        {
            while (true)
            {
                var input = reader.ReadLine().Split();
                if (input[0] == "Exit")
                {
                    Environment.Exit(0);
                }
                try
                {
                    if (input[0] == "CreateRider")
                    {
                        Console.WriteLine(controller.CreateRider(input[1]));
                    }
                    else if (input[0] == "CreateMotorcycle")
                    {
                        Console.WriteLine(controller.CreateMotorcycle(input[1],input[2],int.Parse(input[3])));
                    }
                    else if (input[0] == "AddMotorcycleToRider")
                    {
                        Console.WriteLine(controller.AddMotorcycleToRider(input[1],input[2]));
                    }
                    else if (input[0] == "AddRiderToRace")
                    {
                        Console.WriteLine(controller.AddRiderToRace(input[1],input[2]));
                    }
                    else if (input[0] == "CreateRace")
                    {
                        Console.WriteLine(controller.CreateRace(input[1],int.Parse(input[2])));
                    }
                    else if (input[0] == "StartRace")
                    {
                        Console.WriteLine(controller.StartRace(input[1]));
                    }
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                    
                }
            }
        }
    }
}
