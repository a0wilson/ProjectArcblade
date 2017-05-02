using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class AwayGameResult
    {
        public int Id { get; set; }
        public Game Game { get; set; }        
        public AwayMatchTeamGroup AwayMatchTeamGroup { get; set; }
        public ResultType ResultType { get; set; }

        public int GameId { get; set; }
        public ICollection<AwayGameResultScore> AwayGameResultScores { get; set; }
    }
}
