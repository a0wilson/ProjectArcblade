using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class ClubPlayerStatus
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<ClubPlayer> ClubPlayers { get; set; }
    }
}
