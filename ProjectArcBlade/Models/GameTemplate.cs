namespace ProjectArcBlade.Models
{
    public class GameTemplate
    { 
        public int Id { get; set; }
        public SetTemplate SetTemplate { get; set; }
        public Rule DefaultRule { get; set; }
        public int Number { get; set; }
        public int DefaultWinScore { get; set; }
        public int DefaultLossScore { get; set; }
    }
}
