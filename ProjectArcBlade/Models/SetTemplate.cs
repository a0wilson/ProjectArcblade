using System.Collections.Generic;

namespace ProjectArcBlade.Models
{
    public class SetTemplate
    {
        public int Id { get; set; }
        public MatchTemplate MatchTemplate { get; set; }
        public int Number { get; set; }
        public HomeGroupTemplate HomeGroupTemplate { get; set; }
        public AwayGroupTemplate AwayGroupTemplate { get; set; }
        public int NumberOfGames { get; set; }

        public ICollection<GameTemplate> GameTemplates { get; set; }
    }
}
