using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class ScoreSheetPoint
    {
        public int Id { get; set; }
        public int HomeScore { get; set; }
        public int AwaySocre { get; set; }
        public ScoreSheet ScoreSheet { get; set; }
    }
}
