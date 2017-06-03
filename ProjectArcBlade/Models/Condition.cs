using System.Collections.Generic;

namespace ProjectArcBlade.Models
{
    public class Condition : Lookup
    {
        public ICollection<ResultRule> ResultRules { get; set; }
    }
}
