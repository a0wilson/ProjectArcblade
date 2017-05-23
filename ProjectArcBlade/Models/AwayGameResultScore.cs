using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class AwayGameResultScore
    {
        public int Id { get; set; }
        public AwayGameResult AwayGameResult { get; set; }
        public int Score { get; set; }
        public ScoreStatus ScoreStatus { get; set; }
        public ClubPlayer SubmittedBy { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}
