using System.Collections.Generic;
namespace ProjectArcBlade.Models
{
    public class Operator : Lookup
    {
        public ICollection<ResultRule> ResultRules { get; set; }
    }
}
