using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class AwayResult
    {
        public int Id { get; set; }
        public ResultType ResultType { get; set; }
    }
}
