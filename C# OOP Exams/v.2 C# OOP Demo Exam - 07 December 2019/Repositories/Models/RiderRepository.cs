using MXGP.Models.Riders.Contracts;
using MXGP.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MXGP.Repositories.Models
{
    public class RiderRepository : IRepository<IRider>
    {
        private readonly List<IRider> Models;

        public RiderRepository()
        {
            this.Models= new List<IRider>();
        }
        public void Add(IRider model)
        {
            this.Models.Add(model);
        }

        public IReadOnlyCollection<IRider> GetAll()
        {
            return this.Models;
        }

        public IRider GetByName(string name)
        {
            return this.Models.FirstOrDefault(n => n.Name == name);
        }

        public bool Remove(IRider model)
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
