using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class MatchTemplateSeason
    {
        public int Id { get; set; }
        public MatchTemplate MatchTemplate { get; set; }
        public Season Season { get; set; }
    }
}
