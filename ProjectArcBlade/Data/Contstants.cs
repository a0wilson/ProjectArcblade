namespace ProjectArcBlade.Data
{
    public static class Constants
    {
        public enum MatchScheduleRange
        {
            Initial,
            Return,
            Any
        }

        public static class DateFormat
        {
            public static string Long = "dddd, MMM dd, yyyy";
        }

        public static class TimeFormat
        {
            public static string Short = "HH:mm";
        }

        public static class Setting
        {
            public const int MaxGroupsPerTeam = 1;
            public const int MaxPlayersPerGroup = 2;
        }

        public static class MatchType
        {
            public const int League = 1;
            public const int Cup = 2;
            public const int Casual = 3;
        }

        public static class TeamStatus
        {
            public const int New = 1;
            public const int InProgress = 2;
            public const int Complete = 3;
            public const int Active = 4;
            public const int Inactive = 5;
        }

        public static class Gender
        {
            public const int Male = 1;
            public const int Female = 2;
            public const int Other = 3;
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

        public static class ResultType
        {
            public const int NoEntry = 1;
            public const int Win = 2;
            public const int Loss = 3;
            public const int Draw = 4;
            public const int Forfeit = 5;
            public const int Pending = 6;
            public const int Conceded = 7;
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
            public const string TotalForfeited = "Total Matches Forfeited";
            public const string TotalPlayed = "Total Matches Played";
            public const string TotalRemaining = "Total Matches Remaining";
            public const string TotalMatches = "Total Matches";
        }
    }
    
}
