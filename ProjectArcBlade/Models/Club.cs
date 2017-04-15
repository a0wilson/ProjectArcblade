using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class Club
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ClubUser> ClubUsers { get; set; }
        public ICollection<ClubSubscription> ClubSubscriptions { get; set; }
        public ICollection<ClubVenue> ClubVenues { get; set; }
        
    }
}
