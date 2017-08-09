namespace ProjectArcBlade.Models
{
    public class MatchTemplateLink
    {
        public int Id { get; set; }
        public MatchTemplate MatchTemplate { get; set; }
        public Season Season { get; set; }
        public Category Category { get; set; }
    }
}
