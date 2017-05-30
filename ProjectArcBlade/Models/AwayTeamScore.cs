using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class AwayTeamScore
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public ScoreStatus ScoreStatus { get; set; }
        public Audit Audit { get; set; }        
    }
}
