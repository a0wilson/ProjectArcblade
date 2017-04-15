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

        public void Add(Venue entity)
        {
            _context.Venues.Add(entity);
            _context.SaveChanges();
        }

        public Venue Find(int id)
        {
            return _context.Venues.FirstOrDefault(entity => entity.Id == id);
        }

        public IEnumerable<Venue> GetAll()
        {
            return _context.Venues.ToList();
        }

        public void Remove(int id)
        {
            var entity = _context.Venues.First(e => e.Id == id);
            _context.Venues.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Venue entity)
        {
            _context.Venues.Update(entity);
            _context.SaveChanges();
        }
    }
}
