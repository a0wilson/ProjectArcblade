using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class SportRule
    {
        public int Id { get; set; }
        public Rule Rule { get; set; }
        public Sport Sport { get; set; }
    }
}
