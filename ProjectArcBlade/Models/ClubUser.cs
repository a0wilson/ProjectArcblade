﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class ClubUser
    {
        public int Id { get; set; }
        [Display(Name = "Membership Number")]
        public string MembershipNumber { get; set; }
        [Display(Name = "Membership Type")]
        public MembershipType MembershipType { get; set; }
        [Display(Name = "Status")]
        public ClubUserStatus ClubUserStatus { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public Club Club { get; set; }
        [Required]
        public UserDetail UserDetail { get; set; }

        public ICollection<TeamPlayer> TeamPlayers { get; set; }
        public ICollection<HomeMatchTeamGroupPlayer> HomeMatchTeamGroupPlayers { get; set; }
        public ICollection<AwayMatchTeamGroupPlayer> AwayMatchTeamGroupPlayers { get; set; }
        public ICollection<AwayGameResultScore> AwayGameResultScores { get; set; }
        public ICollection<HomeGameResultScore> HomeGameResultScores { get; set; }
        public ICollection<ClubSubscriber> ClubSubscribers { get; set; }
        public ICollection<AwardNominee> AwardNominees { get; set; }
    }
}
