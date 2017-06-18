using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class SetViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Status { get; set; }
        public string HomeResult { get; set; }
        public string AwayResult { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public string HomeGroup { get; set; }
        public string AwayGroup { get; set; }
        public int TeamId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        //calculated fields
        public bool IsHomeTeam { get { return HomeTeamId == TeamId ? true : false; } }
    }
}
