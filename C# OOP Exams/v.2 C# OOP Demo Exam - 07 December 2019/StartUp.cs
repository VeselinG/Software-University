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
            IEngine engine = new IEngine();
            engine.Run();
        }
    }
}
