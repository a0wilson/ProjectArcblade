using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class PlayerViewModel
    {
        public int MatchTeamGroupPlayerId { get; set; }
        public int MatchTeamId { get; set; }
        public int MatchTeamGroupId { get; set; }
        public string FullName { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
