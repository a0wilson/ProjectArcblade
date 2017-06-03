using System.ComponentModel.DataAnnotations;

namespace ProjectArcBlade.Models
{
    public class ResultRule
    {
        public int Id { get; set; }
        [Required]
        public Rule Rule { get; set; }
        [Required]
        public ResultType ResultType { get; set; }
        [Required]
        public Condition Condition { get; set; } //equal, not-equal, greater-than, less-than
        public Operator Operator { get; set; } //add, subtract, multiply, divide
        [Required]
        public JoinCondition JoinCondition { get; set; } //and, or
        [Required]
        public bool ScoreOne { get; set; }
        public bool ScoreTwo { get; set; }
        [Required]
        public int Value { get; set; }
        public bool UseOperator { get { return ScoreOne && ScoreTwo; } }
    }
}
