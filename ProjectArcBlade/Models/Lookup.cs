using System.ComponentModel.DataAnnotations;

namespace ProjectArcBlade.Models
{
    public class Lookup
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
