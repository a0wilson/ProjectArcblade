using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class CreateStep2ViewModel
    {
        [Display(Name = "Cup")]
        public int CupId { get; set; }

        [Display(Name = "Home Team")]
        public int HomeTeamId { get; set; }

        [Display(Name ="Home Team Handicap")]
        public int HomeTeamHandicap { get; set; }

        [Display(Name = "Start Time ")]
        public DateTime StartTime { get; set; }
        [Display(Name = "Start Date")]
        public DateTime ScheduledDate { get; set; }

        public Address Address { get; set; }

        public List<SelectListItem> HomeTeams { get; set; }
        public List<SelectListItem> Cups { get; set; }
       
    }
}
