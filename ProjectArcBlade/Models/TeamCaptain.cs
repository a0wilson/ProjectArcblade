using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class TeamCaptain
    {
        public int Id { get; set; }
        public Team Team { get; set; }
        public ClubPlayer ClubPlayer { get; set; }
        
        public int TeamId { get; set; }
    }
}
