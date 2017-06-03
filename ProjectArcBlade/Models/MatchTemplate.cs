using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class MatchTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DefaultGameWinScore { get; set; }
        public int DefaultGameLossScore { get; set; }

        public ICollection<GroupTemplate> GroupTemplates { get; set; }
        public ICollection<MatchTemplateLink> MatchTemplateLinks { get; set; }
        public ICollection<SetTemplate> SetTemplates { get; set; }
    }
}
