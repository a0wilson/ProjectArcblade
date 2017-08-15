namespace ProjectArcBlade.Models
{
    public class PromotionMatch
    {
        public int Id { get; set; }
        public ClubPlayer ClubPlayer { get; set; }
        public Season Season { get; set; }
        public Match Match { get; set; }
    }
}
