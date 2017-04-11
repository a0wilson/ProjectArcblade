using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public interface ILeagueRepository
    {
        void Add(League entity);
        IEnumerable<League> GetAll();
        League Find(int id);
        void Remove(int id);
        void Update(League entity);
    }
}
