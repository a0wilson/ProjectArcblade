using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class LeagueClub
    {
        public int Id { get; set; }
        public League League { get; set; }
        public Club Club { get; set; }

        public ICollection<Team> Teams { get; set; }
        public ICollection<ExclusionDate> ExclusionDates { get; set; }
        public ICollection<AwayGameResultScore> AwayGameResultScores { get; set; }
        public ICollection<HomeGameResultScore> HomeGameResultScores { get; set; }

        public override string ToString()
        {
            return Club.Name;
        }
    }
}
