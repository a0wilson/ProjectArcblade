using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class CupMatchHandicap
    {
        public int Id { get; set; }
        [Required]
        public CupMatch CupMatch { get; set; }
        [Required]
        public Team Team { get; set; }
        [Required]
        public int Handicap { get; set; }
    }
}
