namespace ProjectArcBlade.Data
{
    public static class Constants
    {
        public const string NotApplicable = "-- n/a --";

        public enum MatchScheduleRange
        {
            Initial,
            Return,
            Any
        }

        public enum TeamType
        {
            Home,
            Away
        }
        
        public static class DateFormat
        {
            public static string Long = "dddd, MMM dd, yyyy";
            public static string Short = "dd/MM/yyyy";
        }

        public static class TimeFormat
        {
            public static string Short = "HH:mm";
        }

        public static class ResultType
        {
            
            public const string Win = "Win";
            public const string Loss = "Loss";
            public const string Draw = "Draw";
            public const string Forfeit = "Forfeit";
            public const string Pending = "Pending";
            public const string NoEntry = "No Entry";
            public const string Conceded = "Conceded";
            public const string Invalid = "Invalid";
        }

        public static class JoinCondition
        {
            public const string And = "and";
            public const string Or = "or";
        }

        public static class Condition
        {
            public const string Equal = "equal";
            public const string NotEqual = "not equal";
            public const string GreaterThan = "greater than";
            public const string LessThan = "less than";
        }

        public static class Operator
        {
            public const string Add = "add";
            public const string Subtract = "subtract";
            public const string Divide = "divide";
            public const string Multiply = "multiply";
        }

        public static class MatchType
        {
            public const string League = "League";
            public const string Cup = "Cup";
            public const string Casual = "Casual";
        }

        public static class MatchStatus
        {
            public const string New = "New";
            public const string InProgress = "In Progress";
            public const string Complete = "Complete";
        }

        public static class SetStatus
        {
            public const string New = "New";
            public const string InProgress = "In Progress";
            public const string Complete = "Complete";
        }

        public static class TeamStatus
        {
            public const string New = "New";
            public const string InProgress = "In Progress";
            public const string Complete = "Complete";
            public const string Active = "Active";
            public const string Inactive = "Inactive";
        }

        public static class ScoreStatus
        {
            public const string NoEntry = "No Entry";
            public const string Accepted = "Accepted";
            public const string Contested = "Contested";
        }
        
        public static class Gender
        {
            public const int Male = 1;
            public const int Female = 2;
            //public const int Other = 3;
        }

        public static class Category
        {
            public const int Mens = 1;
            public const int Womens = 2;
            public const int Mixed = 3;
        }

        public static class DayOfTheWeek
        {
            public const int Monday = 1;
            public const int Tuesday = 2;
            public const int Wednesday = 3;
            public const int Thursday = 4;
            public const int Friday = 5;
            public const int Saturday = 6;
            public const int Sunday = 7;
        }
        
        public static class CreateMatchStrings
        {
            public const string MatchTypeId = "MatchTypeId";
            public const string LeagueId = "LeagueId";
            public const string SeasonId = "SeasonId";
            public const string CategoryId = "CategoryId";
            public const string DivisionId = "DivisionId";
            public const string IsCupMatch = "IsCupMatch";
            public const string HomeTeamId = "HomeTeamId";
            public const string HomeTeamHandicap = "HomeTeamHandicap";
            public const string AwayTeamId = "AwayTeamId";
            public const string AwayTeamHandicap = "AwayTeamHandicap";
            public const string StartTime = "StartTime";
            public const string ScheduledDate = "ScheduledDate";
        }

        public static class OverviewStrings
        {
            public const string LeaguePostion = "League Postition";
            public const string LeaguePoints = "League Points";
            public const string TotalWon = "Total Matches Won";
            public const string TotalLost = "Total Matches Lost";
            public const string TotalDrawn = "Total Matches Drawn ";
            public const string TotalConceded = "Total Matches Conceded";
            public const string TotalPlayed = "Total Matches Played";
            public const string TotalRemaining = "Total Matches Remaining";
            public const string TotalMatches = "Total Matches";
        }

        public static class TeamStrings
        {
            public const string AssignedMultipleTimes = "{0} has been assigned multiple times";
        }
    }
    
}
