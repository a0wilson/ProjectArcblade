using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class Rule
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Value { get; set; }
        
        public ICollection<LeagueRule> LeagueRules { get; set; }
        public ICollection<SportRule> SportRules { get; set; }
        public ICollection<CategoryRule> CategoryRules { get; set; }
    }
}
