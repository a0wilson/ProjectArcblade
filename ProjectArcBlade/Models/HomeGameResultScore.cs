using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class HomeGameResultScore
    {
        public int Id { get; set; }
        public HomeGameResult HomeGameResult { get; set; }
        public int Score { get; set; }
        public ScoreStatus ScoreStatus { get; set; }
        public LeagueClub SubmittedByLeagueClub { get; set; }
        public ClubPlayer SubmittedByClubPlayer { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}
