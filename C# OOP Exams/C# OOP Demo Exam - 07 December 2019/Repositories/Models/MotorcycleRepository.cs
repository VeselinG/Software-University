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
        private readonly List<IMotorcycle> motorcycles;

        public MotorcycleRepository()
        {
            this.motorcycles = new List<IMotorcycle>();
        }
        public void Add(IMotorcycle model)
        {
            this.motorcycles.Add(model);
        }

        public IReadOnlyCollection<IMotorcycle> GetAll() => this.motorcycles;
        

        public IMotorcycle GetByName(string name)
        {
            return motorcycles.FirstOrDefault(n => n.Model == name);
        }

        public bool Remove(IMotorcycle model)
        {
            if (motorcycles.FirstOrDefault(n=>n.Model==model.Model)==null)
            {
                return false;
            }
            else
            {
                motorcycles.Remove(model);
                return true;
            }            
        }
    }
}
