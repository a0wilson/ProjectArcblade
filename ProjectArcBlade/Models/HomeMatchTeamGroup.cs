using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class HomeMatchTeamGroup
    {
        public int Id { get; set; }
        public Group Group { get; set; }
        public HomeMatchTeam HomeMatchTeam { get; set; }

        public ICollection<HomeMatchTeamGroupPlayer> HomeMatchTeamGroupPlayers { get; set; }
        public ICollection<Set> Sets { get; set; }
    }
}
