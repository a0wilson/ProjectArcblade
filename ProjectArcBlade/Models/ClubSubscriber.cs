using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class ClubSubscriber
    {
        public int Id { get; set; }
        public ClubSubscription ClubSubscription { get; set; }
        public ClubUser ClubUser { get; set; }
    }
}
