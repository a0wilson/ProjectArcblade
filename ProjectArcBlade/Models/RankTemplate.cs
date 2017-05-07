using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class RankTemplate
    {
        public int Id { get; set; }
        public Rank Rank { get; set; }
        public GroupTemplate GroupTemplate { get; set; }

    }
}
