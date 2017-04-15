﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class Match
    {
        public int Id { get; set; }
        public Season Season { get; set; }
                
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        [Display(Name = "Venue")]
        public Venue Venue { get; set; }

        public ICollection<HomeMatchTeam> MatchTeams { get; set; }
        public ICollection<MatchScheduledDate> MatchScheduledDates { get; set; }
        public ICollection<CupMatch> CupMatches { get; set; }
        public ICollection<AwardNominee> AwardNominees { get; set; }
    }
}
