using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    [NotMapped]
    public class NameValuePair
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
