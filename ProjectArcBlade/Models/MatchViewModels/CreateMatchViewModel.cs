using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class CreateMatchViewModel
    {
        [Display(Name = "Home Team")]
        public int HomeTeamId { get; set; }

        [Display(Name = "Away Team")]
        public int AwayTeamId { get; set; }

        [Display(Name = "Season")]
        public int SeasonId { get; set; }

        [Display(Name = "Start Time ")]
        public DateTime StartTime { get; set; }
        [Display(Name ="Start Date")]
        public DateTime ScheduledDate { get; set; }

        public List<SelectListItem> HomeTeams { get; set; }
        public List<SelectListItem> AwayTeams { get; set; }
        public List<SelectListItem> Seasons { get; set; }
    }
}
