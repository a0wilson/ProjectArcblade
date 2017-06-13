using System.Collections.Generic;

namespace ProjectArcBlade.Models
{
    public class JoinCondition : Lookup
    {
        public ICollection<ResultRule> ResultRules { get; set; }
    }
}
