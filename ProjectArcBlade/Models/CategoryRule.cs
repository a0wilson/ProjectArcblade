using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class CategoryRule
    {
        public int Id { get; set; }
        public Rule Rule { get; set; }
        public Category Category { get; set; }
    }
}
