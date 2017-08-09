using System.Collections.Generic;

namespace ProjectArcBlade.Models
{
    public class Rule
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ResultRule> ResultRules { get; set; }
        public ICollection<GameTemplate> GameTemplates { get; set; }
        public ICollection<Game> Games { get; set; }
        
    }
}
