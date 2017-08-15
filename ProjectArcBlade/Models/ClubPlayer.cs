using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class ClubPlayer
    {
        public int Id { get; set; }
        [Display(Name = "Membership Number")]
        public string MembershipNumber { get; set; }
        [Display(Name = "Membership Type")]
        public MembershipType MembershipType { get; set; }
        [Display(Name = "Status")]
        public ClubPlayerStatus ClubPlayerStatus { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public Club Club { get; set; }
        public string AffiliationNumber { get; set; }
        public PlayerDetail PlayerDetail { get; set; }

        public ICollection<TeamPlayer> TeamPlayers { get; set; }
        public ICollection<HomeMatchTeamGroupPlayer> HomeMatchTeamGroupPlayers { get; set; }
        public ICollection<AwayMatchTeamGroupPlayer> AwayMatchTeamGroupPlayers { get; set; }
        public ICollection<AwayTeamScore> AwayGameResultScores { get; set; }
        public ICollection<HomeTeamScore> HomeGameResultScores { get; set; }
        public ICollection<ClubSubscriber> ClubSubscribers { get; set; }
        public ICollection<AwardNominee> AwardNominees { get; set; }
        public ICollection<HomeMatchTeamCaptain> HomeMatchTeamCaptaincies { get; set; }
        public ICollection<AwayMatchTeamCaptain> AwayMatchTeamCaptaincies { get; set; }
        public ICollection<TeamCaptain> TeamCaptaincies { get; set; }
        public ICollection<PromotionMatch> PromotionMatches { get; set; }

    }
}
