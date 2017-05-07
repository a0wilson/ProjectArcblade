using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    [NotMapped]
    public class MatchSchedule
    {
        public int MatchId { get; set; }
        public int HomeTeamId { get; set; }
        public int HomeClubId { get; set; }
        public int VenueId { get; set; }
        public int CategoryId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime StartTime { get; set; }
        public Data.Constants.MatchScheduleRange Range { get; set; }
    }
}
