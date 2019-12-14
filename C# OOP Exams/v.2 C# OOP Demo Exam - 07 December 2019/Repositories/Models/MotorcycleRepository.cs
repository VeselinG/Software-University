using MXGP.Models.Motorcycles.Contracts;
using MXGP.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MXGP.Repositories.Models
{
    public class MotorcycleRepository : IRepository<IMotorcycle>
    {
        private readonly List<IMotorcycle> Models;

        public MotorcycleRepository()
        {
            this.Models = new List<IMotorcycle>();
        }
        public void Add(IMotorcycle model)
        {
            this.Models.Add(model);
        }

        public IReadOnlyCollection<IMotorcycle> GetAll()
        {
            return this.Models;
        }

        public IMotorcycle GetByName(string name)
        {
            return this.Models.FirstOrDefault(n => n.Model == name);
        }

        public bool Remove(IMotorcycle model)
        {
            if (this.Models.FirstOrDefault(n=>n.Model == model.Model)!=null)
            {
                this.Models.Remove(model);
                return true;
            }
            return false;
        }
    }
}
