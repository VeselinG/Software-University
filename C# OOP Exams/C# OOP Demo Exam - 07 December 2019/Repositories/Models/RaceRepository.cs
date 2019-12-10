using MXGP.Models.Races.Contracts;
using MXGP.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MXGP.Repositories.Models
{
    public class RaceRepository : IRepository<IRace>
    {
        private readonly List<IRace> races;

        public RaceRepository()
        {
            this.races = new List<IRace>();
        }
        public void Add(IRace model)
        {
            this.races.Add(model);
        }

        public IReadOnlyCollection<IRace> GetAll() => this.races;
      

        public IRace GetByName(string name)
        {
            return races.FirstOrDefault(n => n.Name == name);
        }

        public bool Remove(IRace model)
        {
            if (races.FirstOrDefault(n => n.Name == model.Name) == null)
            {
                return false;
            }
            else
            {
                races.Remove(model);
                return true;
            }
        }
    }
}
