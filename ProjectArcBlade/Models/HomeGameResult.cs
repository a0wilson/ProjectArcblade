using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class HomeGameResult
    {
        public int Id { get; set; }
        public Game Game { get; set; }
        public HomeMatchTeamGroup HomeMatchTeamGroup { get; set; }
        public ResultType ResultType { get; set; }

        public int GameId { get; set; }
        public ICollection<HomeGameResultScore> HomeGameResultScores { get; set; }
    }
}
