using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class CupMatch
    {
        public int Id { get; set; }
        [Required]
        public Cup Cup { get; set; }
        [Required]
        public Match Match { get; set; }

        public ICollection<CupMatchHandicap> CupMatchHandicaps { get; set; }
    }
}
