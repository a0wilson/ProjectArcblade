using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class HomeMatchTeamGroupPlayer
    {
        public int Id { get; set; }
        public HomeMatchTeamGroup HomeMatchTeamGroup { get; set; }
        public ClubPlayer ClubPlayer { get; set; }

        public ICollection<HomeMatchTeamCaptain> HomeMatchTeamCaptains { get; set; }
    }
}
