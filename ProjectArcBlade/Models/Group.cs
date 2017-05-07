using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class Group
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<TeamPlayer> TeamPlayers { get; set; }
        public ICollection<HomeMatchTeamGroup> HomeMatchTeamGroups { get; set; }
        public ICollection<AwayMatchTeamGroup> AwayMatchTeamGroups { get; set; }
        public ICollection<GroupTemplate> GroupTemplates { get; set; }
    }
}
