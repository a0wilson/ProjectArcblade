﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class ClubUserStatus
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<ClubUser> ClubUsers { get; set; }
    }
}
