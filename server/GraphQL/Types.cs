namespace GPPlanetGQL.GraphQL
{
    public static class Types
    {
        public class Online
        {
            public int Max { get; set; }
            public int Min { get; set; }
            public int Avg { get; set; }
            public DateTime Time { get; set; }
        }

        public class NewPlayer
        {
            public int Inc { get; set; }
            public int Total { get; set; }
            public DateTime Time { get; set; }
        }

        public enum OnlineTypes
        {
            Hour,
            Day,
            Week,
            Month
        }
        public static DateTime ChangeTime(this DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }
    }

}
