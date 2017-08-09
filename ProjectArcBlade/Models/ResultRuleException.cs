namespace ProjectArcBlade.Models
{
    public class ResultRuleException
    {
        public int Id { get; set; }
        public ResultRule TargetRule { get; set; }
        public ResultRule ExceptionRule { get; set; }
    }
}
