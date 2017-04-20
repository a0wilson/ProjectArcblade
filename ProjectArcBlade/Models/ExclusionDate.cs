using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class ExclusionDate
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        [Required]
        public DateTime DateToExclude { get; set; }
        public Season Season { get; set; }
        public LeagueClub LeagueClub { get; set; }
    }
}
