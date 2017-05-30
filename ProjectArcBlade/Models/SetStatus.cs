using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class SetStatus : Status
    {
        public ICollection<Set> Sets { get; set; }
    }
}
