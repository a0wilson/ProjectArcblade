namespace ProjectArcBlade.Data
{
    public static class Constants
    {
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

        public static class CreateMatchStrings
        {
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
    }
    
}
