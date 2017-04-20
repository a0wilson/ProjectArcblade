using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class PointScore
    {
        public int Id { get; set; }
        public Season Season { get; set; }
        public ResultType ResultType { get; set; }
        [Required]
        public int PointValue { get; set; }
    }
}
