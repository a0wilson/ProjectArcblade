using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class Match
    {
        public int Id { get; set; }
        public Season Season { get; set; }
        public DateTime? StartTime { get; set; }
        [DisplayFormat(DataFormatString = "dddd, MMM dd, yyyy")]
        public DateTime? StartDate { get; set; }
        public Venue Venue { get; set; }
        public MatchType MatchType { get; set;}
        public MatchStatus MatchStatus { get; set; }

        //Navigation properties
        public AwayMatchTeam AwayMatchTeam { get; set; }
        public HomeMatchTeam HomeMatchTeam { get; set; }
        public ICollection<RescheduledStartDate> RescheduledStartDates { get; set; }
        public ICollection<CupMatch> CupMatches { get; set; }
        public ICollection<AwardNominee> AwardNominees { get; set; }
        public ICollection<Game> Games { get; set; }

        //unmapped properites
        [NotMapped]
        public bool isHomeTeam { get; set; }

    }
}
