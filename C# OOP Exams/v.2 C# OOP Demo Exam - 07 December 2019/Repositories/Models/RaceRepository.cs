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
        private readonly List<IRace> Models;

        public RaceRepository()
        {
            this.Models = new List<IRace>();
        }
        public void Add(IRace model)
        {
            this.Models.Add(model);
        }

        public IReadOnlyCollection<IRace> GetAll()
        {
            return this.Models;
        }

        public IRace GetByName(string name)
        {
            return this.Models.FirstOrDefault(n => n.Name == name);
        }

        public bool Remove(IRace model)
        {
            if (this.Models.FirstOrDefault(n => n.Name == model.Name) != null)
            {
                this.Models.Remove(model);
                return true;
            }
            return false;
        }
    }
}
