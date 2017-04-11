using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectArcBlade.Data;

namespace ProjectArcBlade.Models
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Address entity)
        {
            _context.Addresses.Add(entity);
            _context.SaveChanges();
        }

        public Address Find(int id)
        {
            return _context.Addresses.FirstOrDefault(entity => entity.Id == id);
        }

        public IEnumerable<Address> GetAll()
        {
            return _context.Addresses.ToList();
        }

        public void Remove(int id)
        {
            var entity = _context.Addresses.First(e => e.Id == id);
            _context.Addresses.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Address entity)
        {
            _context.Addresses.Update(entity);
            _context.SaveChanges();
        }
    }
}
