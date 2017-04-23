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
        public int HomeTeamId { get; set; }
        public int HomeClubId { get; set; }
        public string HomeTeamName { get; set; }
        public string VenueName { get; set; }
        public int AwayTeamId { get; set; }
        public string AwayTeamName { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public string DisplayDate { get; set; }
        public string DivisionName { get; set; }
        public Data.Constants.MatchScheduleRange Range { get; set; }
    }
}
