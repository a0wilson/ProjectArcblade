using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class GameTemplate
    { 
        public int Id { get; set; } 
        public int Order { get; set; }
        public HomeGroupTemplate HomeGroupTemplate { get; set; }
        public AwayGroupTemplate AwayGroupTemplate { get; set; }
        public MatchTemplate MatchTemplate { get; set; }
    }
}
