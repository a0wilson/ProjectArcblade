using System.Collections.Generic;

namespace ProjectArcBlade.Models
{
    public interface IAddressRepository
    {
        void Add(Address entity);
        IEnumerable<Address> GetAll();
        Address Find(int id);
        void Remove(int id);
        void Update(Address entity);
    }
}
