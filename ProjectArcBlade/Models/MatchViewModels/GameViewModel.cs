using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.MatchViewModels
{   
    public class GameViewModel
    {
        public int Id { get; set; }
        public int SetId { get; set; }
        public int SetNumber { get; set; }
        public int AwayAwayScore { get; set; }
        public int HomeAwayScore { get; set; }
        public string AwayResult { get; set; }
        public int HomeHomeScore { get; set; }
        public int AwayHomeScore { get; set; }
        public string HomeResult { get; set; }
    }
}
