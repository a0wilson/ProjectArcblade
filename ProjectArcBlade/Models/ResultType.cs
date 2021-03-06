﻿using System.Collections.Generic;

namespace ProjectArcBlade.Models
{
    public class ResultType : Type
    {
        public ICollection<ResultRule> ResultRules { get; set; }
        public ICollection<AwayResult> AwayResults { get; set; }
        public ICollection<HomeResult> HomeResults { get; set; }
        public ICollection<PointScore> PointScores { get; set; }
    }
}
