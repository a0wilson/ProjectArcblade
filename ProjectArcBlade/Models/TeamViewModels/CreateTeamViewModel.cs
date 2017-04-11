using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.TeamViewModels
{
    public class CreateTeamViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Club")]
        public int LeagueClubId { get; set; }
        [Required]
        [Display(Name = "Division")]
        public int DivisionId { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Season")]
        public int SeasonId { get; set; }
        
        public List<SelectListItem> LeagueClubs { get; set; }
        public List<SelectListItem> Divisions { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Seasons { get; set; }

    }
}
