using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class CreateStep3ViewModel
    {
        [Required]
        [Display(Name = "Away Team")]
        public int AwayTeamId { get; set; }
        
        [Display(Name ="Away Team Handicap")]
        public int AwayTeamHandicap { get; set; }

        public bool IsCupMatch { get; set; }

        public List<SelectListItem> AwayTeams { get; set; }
        
    }
}
