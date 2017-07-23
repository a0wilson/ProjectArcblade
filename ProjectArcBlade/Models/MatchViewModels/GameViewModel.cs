using ProjectArcBlade.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.MatchViewModels
{   
    public class GameViewModel
    {
        public int Id { get; set; }
        public int SetId { get; set; }
        public int SetNumber { get; set; }
        public int GameNumber { get; set; }
        public int? AwayAwayScore { get; set; }
        public int? HomeAwayScore { get; set; }
        public int? HomeHomeScore { get; set; }
        public int? AwayHomeScore { get; set; }
        public string HomeGroup { get; set; }
        public string AwayGroup { get; set; }
        public string AwayResult { get; set; }       
        public string HomeResult { get; set; }
        public string HomeAwayScoreStatus { get; set; }
        public string HomeHomeScoreStatus { get; set; }
        public string AwayAwayScoreStatus { get; set; }        
        public string AwayHomeScoreStatus { get; set; }
        
        public bool IsHomeTeam { get; set; }
        public Rule Rule { get; set; }

        public bool HomeAwayScoreEntered { get { return HomeAwayScoreStatus != Constants.ScoreStatus.NoEntry; } }
        public bool HomeHomeScoreEntered { get { return HomeHomeScoreStatus != Constants.ScoreStatus.NoEntry; } }
        public bool AwayAwayScoreEntered { get { return AwayAwayScoreStatus != Constants.ScoreStatus.NoEntry; } }        
        public bool AwayHomeScoreEntered { get { return AwayHomeScoreStatus != Constants.ScoreStatus.NoEntry; } }

        public bool HomeAwayScoreContested { get { return HomeAwayScoreStatus == Constants.ScoreStatus.Contested; } }
        public bool HomeHomeScoreContested { get { return HomeHomeScoreStatus == Constants.ScoreStatus.Contested; } }
        public bool AwayAwayScoreContested { get { return AwayAwayScoreStatus == Constants.ScoreStatus.Contested; } }       
        public bool AwayHomeScoreContested { get { return AwayHomeScoreStatus == Constants.ScoreStatus.Contested; } }

        public bool IsContestedGame { get { return (HomeAwayScoreContested || HomeHomeScoreContested || AwayAwayScoreContested || AwayHomeScoreContested); } }

        public int? AggregatedHomeScore { get { return IsHomeTeam ? (HomeHomeScoreEntered ? HomeHomeScore : AwayHomeScoreEntered ? AwayHomeScore : null) : (AwayHomeScoreEntered ? AwayHomeScore : HomeHomeScoreEntered ? HomeHomeScore : null); } }
        public int? AggregatedAwayScore { get { return IsHomeTeam ? (HomeAwayScoreEntered ? HomeAwayScore : AwayAwayScoreEntered ? AwayAwayScore : null) : (AwayAwayScoreEntered ? AwayAwayScore : HomeAwayScoreEntered ? HomeAwayScore : null); } }

        public int AggregatedHomeScoreWithDefault { get { return AggregatedHomeScore == null ? 0 : AggregatedHomeScore.Value; } }
        public int AggregatedAwayScoreWithDefault { get { return AggregatedAwayScore == null ? 0 : AggregatedAwayScore.Value; } }

        public string HomeVsAwayGameScoreDisplay { get { return AggregatedHomeScoreWithDefault == 0 && AggregatedAwayScoreWithDefault == 0 ? "-- / --" : String.Format("{0} / {1}", AggregatedHomeScoreWithDefault, AggregatedAwayScoreWithDefault); } }

        public string AggregatedHomeResult
        {
            get
            {
                if (HomeResult == Constants.ResultType.Conceded) return HomeResult;
                if (GetResultType(Rule, AggregatedHomeScore, AggregatedAwayScore) == Constants.ResultType.Win) return Constants.ResultType.Win;
                if (GetResultType(Rule, AggregatedAwayScore, AggregatedHomeScore) == Constants.ResultType.Win) return Constants.ResultType.Loss;                
                return Constants.ResultType.Pending;
            }
        }
        public string AggregatedAwayResult
        {
            get
            {
                if (AwayResult == Constants.ResultType.Conceded) return AwayResult;
                if (GetResultType(Rule, AggregatedAwayScore, AggregatedHomeScore) == Constants.ResultType.Win ) return Constants.ResultType.Win;
                if (GetResultType(Rule, AggregatedHomeScore, AggregatedAwayScore) == Constants.ResultType.Win) return Constants.ResultType.Loss;
                return Constants.ResultType.Pending;
            }
        }

        public string AggregatedHomeScoreStatus
        {
            get
            {
                if (HomeHomeScore == null && AwayHomeScore == null) return Constants.ScoreStatus.NoEntry;
                if (HomeHomeScore != null && AwayHomeScore != null && HomeHomeScore != AwayHomeScore) return Constants.ScoreStatus.Contested;
                return Constants.ScoreStatus.Accepted;
            }
        }

        public string AggregatedAwayScoreStatus
        {
            get
            {
                if (HomeAwayScore == null && AwayAwayScore == null) return Constants.ScoreStatus.NoEntry;
                if (HomeAwayScore != null && AwayAwayScore != null && HomeAwayScore != AwayAwayScore) return Constants.ScoreStatus.Contested;
                return Constants.ScoreStatus.Accepted;
            }
        }

        /// <summary>
        /// Returns a result type for score1 based on score2.
        /// Uses the ResltRules attached to a matchTemplate as the conditions.
        /// Will return result of win, loss, or draw, if unable to establish result, return pending.
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="score1"></param>
        /// <param name="score2"></param>
        /// <returns></returns>
        public string GetResultType(Rule rule, int? score1, int? score2)
        {
            var resultTypePending = Constants.ResultType.Pending;
            var resultTypeDraw = Constants.ResultType.Draw;
            var resultTypeWin = Constants.ResultType.Win;
            var resultTypeLoss = Constants.ResultType.Loss;
            var resultTypeInvalid = Constants.ResultType.Invalid;

            if (score1 != null && score2 != null)
            {
                if (rule != null)
                {
                    //first check for invalid, then draw then loss then win otherwise pending (default)
                    var invalidResultRules = rule.ResultRules.Where(rr => rr.ResultType.Name == Constants.ResultType.Invalid).ToList();
                    var drawResultRules = rule.ResultRules.Where(rr => rr.ResultType.Name == Constants.ResultType.Draw).ToList();
                    var lossResultRules = rule.ResultRules.Where(rr => rr.ResultType.Name == Constants.ResultType.Loss).ToList();
                    var winResultRules = rule.ResultRules.Where(rr => rr.ResultType.Name == Constants.ResultType.Win).ToList();

                    if (ValidateResultRule(invalidResultRules, score1.Value, score2.Value)) return resultTypeInvalid;
                    if (ValidateResultRule(drawResultRules, score1.Value, score2.Value)) return resultTypeDraw;
                    if (ValidateResultRule(lossResultRules, score1.Value, score2.Value)) return resultTypeLoss;
                    if (ValidateResultRule(winResultRules, score1.Value, score2.Value)) return resultTypeWin;
                }
            }
            return resultTypePending;
        }

        private bool ValidateResultRule(List<ResultRule> resultRules, int score1, int score2)
        {
            var ruleResult = new bool[resultRules.Count];
            string joinCondition = Constants.JoinCondition.And; //default to and.

            var i = 0;
            foreach (var rule in resultRules)
            {
                var ruleOperand = 0;

                joinCondition = rule.JoinCondition.Name;
                if (rule.UseOperator)
                {
                    switch (rule.Operator.Name)
                    {
                        case Constants.Operator.Add:
                            ruleOperand = score1 + score2;
                            break;
                        case Constants.Operator.Subtract:
                            ruleOperand = score1 - score2;
                            break;
                        case Constants.Operator.Divide:
                            ruleOperand = score1 / score2;
                            break;
                        case Constants.Operator.Multiply:
                            ruleOperand = score1 * score2;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    if (rule.ScoreOne) ruleOperand = score1;
                    if (rule.ScoreTwo) ruleOperand = score2;
                }

                switch (rule.Condition.Name)
                {
                    case Constants.Condition.Equal:
                        ruleResult[i] = ruleOperand == rule.Value;
                        break;
                    case Constants.Condition.NotEqual:
                        ruleResult[i] = ruleOperand != rule.Value;
                        break;
                    case Constants.Condition.LessThan:
                        ruleResult[i] = ruleOperand < rule.Value;
                        break;
                    case Constants.Condition.GreaterThan:
                        ruleResult[i] = ruleOperand > rule.Value;
                        break;
                    default:
                        break;
                }

                i++;
            }

            switch (joinCondition)
            {
                case Constants.JoinCondition.And:
                    return ruleResult.Where(r => r == true).Count() == ruleResult.Count();
                case Constants.JoinCondition.Or:
                    return ruleResult.Where(r => r == true).Count() > 0;
            }

            return false;
        }
    }
}
