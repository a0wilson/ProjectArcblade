using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class PreviewMatchViewModel
    {
        public int referrerTeamId { get; set; }
        public Match Match { get; set; }
        public List<AwayMatchTeamGroupPlayer> AwayTeamPlayers { get; set; }
        public List<HomeMatchTeamGroupPlayer> HomeTeamPlayers { get; set; }
        public int HomeTeamCaptainId { get; set; }
        public int AwayTeamCaptainId { get; set; }

    }
}
