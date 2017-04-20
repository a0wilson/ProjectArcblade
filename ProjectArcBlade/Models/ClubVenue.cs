using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class ClubVenue
    {
        public int Id { get; set; }
        public Club Club { get; set; }
        public Venue Venue { get; set; }
        public DayOfTheWeek DayOfTheWeek { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MaxMatches { get; set; }

    }
}
