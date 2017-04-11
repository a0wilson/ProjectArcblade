using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class ScoreStatus
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<AwayGameResultScore> AwayGameResultScores { get; set; }
        public ICollection<HomeGameResultScore> HomeGameResultScores { get; set; }
    }
}
