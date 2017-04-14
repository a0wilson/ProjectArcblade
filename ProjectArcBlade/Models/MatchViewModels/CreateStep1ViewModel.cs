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
        
        [Required]
        [Display(Name = "Division")]
        public int DivisionId { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Season")]
        public int SeasonId { get; set; }

        [Display(Name = "Cup Match")]
        public bool IsCupMatch { get; set; }

        public List<SelectListItem> Seasons { get; set; }
        public List<SelectListItem> Divisions { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
