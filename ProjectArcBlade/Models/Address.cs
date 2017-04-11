using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        [Required]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        [Required]
        public string Postcode { get; set; }
        public string County { get; set; }
        public string Country { get; set; }

        public ICollection<Club> Clubs { get; set; }
        public ICollection<UserDetail> Users { get; set; }
        public ICollection<Match> Matches { get; set; }
    }
}
