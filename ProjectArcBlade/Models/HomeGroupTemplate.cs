using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class HomeGroupTemplate
    {
        public int Id { get; set; }
        public GroupTemplate GroupTemplate { get; set; }
        public SetTemplate SetTemplate { get; set; }

        public int SetTemplateId { get; set; }
    }
}
