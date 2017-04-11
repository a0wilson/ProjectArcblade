using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class League
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Sport Sport { get; set; }

        public ICollection<Season> Seasons { get; set; }
        public ICollection<LeagueClub> LeagueClubs { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
