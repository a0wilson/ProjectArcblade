using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Team> Teams { get; set; }
        public ICollection<Match> Matches { get; set; }
        public ICollection<MatchTemplateLink> MatchTemplateLinks { get; set; }
    }
}
