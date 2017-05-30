using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class SetHomeResult : HomeResult
    {
        public Set Set { get; set; }

        public int SetId { get; set; }
    }
}
