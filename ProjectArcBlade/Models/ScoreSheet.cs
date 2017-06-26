using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class ScoreSheet
    {
        public int Id { get; set; }
        public bool SignedOff { get; set; }
        public Audit Audit { get; set; }
        
        //Navigation properties.
        public ICollection<ScoreSheetSet> ScoreSheetSets { get; set; }
        public ICollection<ScoreSheetGame> ScoreSheetGames { get; set; }
        public ICollection<ScoreSheetPoint> ScoreSheetPoints { get; set; }


    }
}
