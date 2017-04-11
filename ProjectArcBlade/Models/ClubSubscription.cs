using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class ClubSubscription
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Club Club { get; set; }
        [Required]
        public Season Season { get; set; }
        [Required]
        public decimal Amount { get; set; }

        public ICollection<ClubSubscriber> ClubSubscribers { get; set; }

    }
}
