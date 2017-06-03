using System.Collections.Generic;
namespace ProjectArcBlade.Models
{
    public class Operator : Lookup
    {
        ICollection<ResultRule> ResultRules { get; set; }
    }
}
