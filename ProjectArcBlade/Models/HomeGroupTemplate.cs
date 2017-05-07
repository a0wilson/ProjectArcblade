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
        public GameTemplate GameTemplate { get; set; }

        public int GameTemplateId { get; set; }
    }
}
