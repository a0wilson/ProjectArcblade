﻿using System.Collections.Generic;

namespace ProjectArcBlade.Models
{
    public class GroupTemplate
    {
        public int Id { get; set; }
        public MatchTemplate MatchTemplate { get; set; }
        public Group Group { get; set; }

        public ICollection<RankTemplate> RankTemplates { get; set; }
        public ICollection<HomeGroupTemplate> HomeGroupTemplates { get; set; }
        public ICollection<AwayGroupTemplate> AwayGroupTemplates { get; set; }
    }
}
