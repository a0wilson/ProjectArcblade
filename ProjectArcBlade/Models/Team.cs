using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class Team
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public LeagueClub LeagueClub { get; set; }
        public Division Division { get; set; }
        public Category Category { get; set; }
        public Season Season { get; set; }
        public TeamStatus TeamStatus { get; set; }
        public TeamCaptain TeamCaptain { get; set; }

        public ICollection<TeamPlayer> TeamPlayers { get; set; }
        public ICollection<HomeMatchTeam> HomeMatchTeams { get; set; }
        public ICollection<AwayMatchTeam> AwayMatchTeams { get; set; }
    }
}
