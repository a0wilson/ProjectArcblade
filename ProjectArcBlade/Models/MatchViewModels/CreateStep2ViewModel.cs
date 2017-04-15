using Microsoft.AspNetCore.Mvc;
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
        public bool IsCupMatch { get; set; }

        [Display(Name = "Home Team")]
        public int HomeTeamId { get; set; }

        [Display(Name ="Home Team Handicap")]
        public int HomeTeamHandicap { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime ScheduledDate { get; set; }

        public Venue Address { get; set; }

        public List<SelectListItem> HomeTeams { get; set; }
        public List<SelectListItem> Cups { get; set; }
        
    }
}
