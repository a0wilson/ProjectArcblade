using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class TeamPlayer
    {
        public int Id { get; set; }
        public Team Team { get; set; }
        public ClubUser ClubUser { get; set; }
        public Group Group { get; set; }
        
        public ICollection<TeamCaptain> TeamCaptains { get; set; }

    }
}
