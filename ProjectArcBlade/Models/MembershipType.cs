using System.Collections.Generic;

namespace ProjectArcBlade.Models
{
    public class MembershipType : Type
    {
        public ICollection<ClubPlayer> ClubPlayers { get; set; }
    }
}
