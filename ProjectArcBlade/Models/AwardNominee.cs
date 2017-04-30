using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class AwardNominee
    {
        public int Id { get; set; }
        [Required]
        public ClubPlayer ClubPlayer { get; set; }
        public Award Award { get; set; }
        public Match Match { get; set; }

    }
}
