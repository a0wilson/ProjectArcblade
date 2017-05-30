using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class SetTemplate
    {
        public int Id { get; set; }
        public MatchTemplate MatchTemplate { get; set; }
        public int Number { get; set; }
        public HomeGroupTemplate HomeGroupTemplate { get; set; }
        public AwayGroupTemplate AwayGroupTemplate { get; set; }

        public ICollection<GameTemplate> GameTemplates { get; set; }
    }
}
