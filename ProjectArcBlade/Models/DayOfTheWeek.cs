using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectArcBlade.Models
{
    public class DayOfTheWeek
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<ClubVenue> ClubVenues { get; set; }
    }
}
