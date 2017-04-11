using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class HomeMatchTeamCaptain
    {
        public int Id { get; set; }
        public HomeMatchTeamGroupPlayer HomeMatchTeamGroupPlayer { get; set; }
    }
}
