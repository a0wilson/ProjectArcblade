using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.SeasonViewModels
{
    public class ScheduleAllMatchesViewModel
    {
        public ICollection<MatchSchedule> MatchSchedules { get; set; }
        public ICollection<string> DivisionNames { get; set; }
    }
}
