using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class AvailableDay
    {
        public int Id { get; set; }
        [Required]
        public string Day { get; set; }
        [Required]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        [Required]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }
        [Required]
        public Club Club { get; set; }
    }
}
