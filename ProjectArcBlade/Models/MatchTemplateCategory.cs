using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class MatchTemplateCategory
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public MatchTemplate MatchTemplate { get; set; }
        
    }
}
