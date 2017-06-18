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
        public Division Division { get; set; }
        public Category Category { get; set; }
        public DateTime? StartTime { get; set; }
        [DisplayFormat(DataFormatString = "dddd, MMM dd, yyyy")]
        public DateTime? StartDate { get; set; }
        public Venue Venue { get; set; }
        public MatchType MatchType { get; set; }
        public MatchStatus MatchStatus { get; set; }
        public MatchAwayResult MatchAwayResult { get; set; }
        public MatchHomeResult MatchHomeResult { get; set; }
        public AwayMatchTeam AwayMatchTeam { get; set; }
        public HomeMatchTeam HomeMatchTeam { get; set; }

        //Navigation properties
        public ICollection<RescheduledStartDate> RescheduledStartDates { get; set; }
        public ICollection<CupMatch> CupMatches { get; set; }
        public ICollection<AwardNominee> AwardNominees { get; set; }
        public ICollection<Set> Sets { get; set; }
        
        public int MinimumSetsToWinMatch
        {
            get
            {
                if (Sets != null)
                {
                    if (Sets.Count == 1) return 1;

                    if (Sets.Count > 1)
                    {
                        var rem = Sets.Count % 2;
                        if (rem == 0)
                        {
                            return (Sets.Count / 2) + 1;
                        }
                        else
                        {
                            return (Sets.Count + 1) / 2;
                        }
                    }
                }
                return 0;
            }
        }

    }
}
