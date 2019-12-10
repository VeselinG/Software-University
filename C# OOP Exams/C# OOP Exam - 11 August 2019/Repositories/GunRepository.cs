using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViceCity.Models.Guns.Contracts;
using ViceCity.Repositories.Contracts;

namespace ViceCity.Repositories
{
    public class GunRepository : IRepository<IGun>
    {
        private readonly List<IGun> GunsRepository;

        public GunRepository()
        {
            this.GunsRepository = new List<IGun>();
        }
        public IReadOnlyCollection<IGun> Models => this.GunsRepository.AsReadOnly();

        public void Add(IGun model)
        {
            if (!this.GunsRepository.Any(g=>g.Name==model.Name))
            {
                this.GunsRepository.Add(model);
            }
        }

        public IGun Find(string name)
        {
            return this.GunsRepository.FirstOrDefault(g => g.Name == name);
        }

        public bool Remove(IGun model)
        {
            if (this.GunsRepository.Any(g => g.Name == model.Name))
            {
                this.GunsRepository.Remove(model);
                return true;
            }
            return false;
        }
    }
}
