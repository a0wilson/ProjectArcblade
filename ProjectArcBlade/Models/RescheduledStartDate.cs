using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class RescheduledStartDate
    {
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public Match Match { get; set; }
    }
}
