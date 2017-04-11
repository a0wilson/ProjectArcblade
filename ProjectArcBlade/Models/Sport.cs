using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class Sport
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<League> Leagues { get; set; }
    }
}
