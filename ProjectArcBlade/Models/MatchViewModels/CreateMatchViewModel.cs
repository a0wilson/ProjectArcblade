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
        [Display(Name = "Division")]
        public string DivisionName { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Season")]
        public string SeasonName { get; set; }

        [Display(Name = "Cup Match")]
        public bool IsCupMatch { get; set; }

        [Display(Name = "Home Team")]
        public string HomeTeamName { get; set; }
      
        [Display(Name = "Away Team")]
        public string AwayTeamName { get; set; }

        [Display(Name = "Start Time ")]
        [DataType(DataType.Date)]
        public string StartTime { get; set; }

        [Display(Name ="Start Date")]
        [DataType(DataType.Time)]
        public string ScheduledDate { get; set; }

        public int HomeTeamHandicap { get; set; }
        public int AwayTeamHandicap { get; set; }

    }
}
