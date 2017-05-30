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

        public ICollection<GroupTemplate> GroupTemplates { get; set; }
        public ICollection<MatchTemplateSeason> MatchTemplateSeasons { get; set; }
        public ICollection<MatchTemplateCategory> MatchTemplateCategories { get; set; }
        public ICollection<SetTemplate> SetTemplates { get; set; }
    }
}
