namespace ProjectArcBlade.Models
{
    public class HomeTeamScore
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public ScoreStatus ScoreStatus { get; set; }
        public Audit Audit { get; set; }        
    }
}
