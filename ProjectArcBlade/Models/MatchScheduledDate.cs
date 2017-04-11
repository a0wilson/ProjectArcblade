using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class MatchScheduledDate
    {
        public int Id { get; set; }
        [Required]
        public DateTime ScheduledDate { get; set; }
        [Required]
        public Match Match { get; set; }
    }
}
