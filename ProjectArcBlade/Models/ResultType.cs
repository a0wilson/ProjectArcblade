using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class ResultType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<AwayMatchTeam> AwayMatchTeams { get; set; }
        public ICollection<AwayGameResult> AwayGameResults { get; set; }
        public ICollection<HomeMatchTeam> HomeMatchTeams { get; set; }
        public ICollection<HomeGameResult> HomeGameResults { get; set; }
        public ICollection<PointScore> PointScores { get; set; }

    }
}
