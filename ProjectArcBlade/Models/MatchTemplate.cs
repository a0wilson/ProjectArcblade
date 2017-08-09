using System.Collections.Generic;

namespace ProjectArcBlade.Models
{
    public class MatchTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfGroups { get; set; }
        public int NumberOfSets { get; set; }

        public ICollection<GroupTemplate> GroupTemplates { get; set; }
        public ICollection<MatchTemplateLink> MatchTemplateLinks { get; set; }
        public ICollection<SetTemplate> SetTemplates { get; set; }
    }
}
