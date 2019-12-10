using System;

namespace MXGP
{
    using Models.Motorcycles;
    using MXGP.Core.Contracts;
    using MXGP.Models.Motorcycles.Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            //TODO Add IEngine
            var engine = new IEngine();
            engine.Run();
        }
    }
}
