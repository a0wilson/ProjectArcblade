using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class DisplayMatchViewModel
    {
        public int Id { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string Venue { get; set; }
    }
}
