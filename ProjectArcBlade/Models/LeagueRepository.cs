using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectArcBlade.Data;

namespace ProjectArcBlade.Models
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly ApplicationDbContext _context;

        public LeagueRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(League entity)
        {
            _context.Leagues.Add(entity);
            _context.SaveChanges();
        }

        public League Find(int id)
        {
            return _context.Leagues.FirstOrDefault(entity => entity.Id == id);
        }

        public IEnumerable<League> GetAll()
        {
            return _context.Leagues.ToList();
        }

        public void Remove(int id)
        {
            var entity = _context.Leagues.First(e => e.Id == id);
            _context.Leagues.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(League entity)
        {
            _context.Leagues.Update(entity);
            _context.SaveChanges();
        }
    }
}
