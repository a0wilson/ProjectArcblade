using System.Collections.Generic;

namespace ProjectArcBlade.Models
{
    public class JoinCondition : Lookup
    {
        ICollection<ResultRule> ResultRules { get; set; }
    }
}
