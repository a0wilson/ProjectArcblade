using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class Venue
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required]
        [DataType(DataType.PostalCode)]
        public string Postcode { get; set; }
        public string County { get; set; }
        public string Country { get; set; }

        public ICollection<ClubVenue> ClubVenues { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Match> Matches { get; set; }
    }
}
