using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.SeasonViewModels
{
    public class ScheduleAllMatchesViewModel
    {
        public ICollection<Match> Matches { get; set; }
        public ICollection<NameValuePair> Divisions { get; set; }
    }
}
