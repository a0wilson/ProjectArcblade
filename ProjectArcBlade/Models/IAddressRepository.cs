using System.Collections.Generic;

namespace ProjectArcBlade.Models
{
    public interface IAddressRepository
    {
        void Add(Venue entity);
        IEnumerable<Venue> GetAll();
        Venue Find(int id);
        void Remove(int id);
        void Update(Venue entity);
    }
}
