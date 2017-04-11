﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class AwayGameResult
    {
        public int Id { get; set; }
        public AwayMatchTeamGroup AwayMatchTeamGroup { get; set; }

        [Display(Name = "Result")]
        public ResultType ResultType { get; set; }

        public ICollection<AwayGameResultScore> AwayGameResultScores { get; set; }
    }
}
