using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class CreateStep1ViewModel
    {
        [Required, Display(Name = "Match Type")]
        public int MatchTypeId { get; set; }
        [Required, Display(Name = "Division")]
        public int DivisionId { get; set; }
        [Required, Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Required, Display(Name = "Season")]
        public int SeasonId { get; set; }
        [HiddenInput]
        public int LeagueId { get; set; }

        public List<SelectListItem> MatchTypes { get; set; }
        public List<SelectListItem> Seasons { get; set; }
        public List<SelectListItem> Divisions { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
