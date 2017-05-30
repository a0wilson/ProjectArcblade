using System.Collections.Generic;

namespace ProjectArcBlade.Models
{
    public class ClubPlayerStatus : Status 
    {
        public ICollection<ClubPlayer> ClubPlayers { get; set; }
    }
}
