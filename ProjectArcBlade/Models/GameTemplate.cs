using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class GameTemplate
    { 
        public int Id { get; set; }
        public SetTemplate SetTemplate { get; set; }
        public int Number { get; set; }        
    }
}
