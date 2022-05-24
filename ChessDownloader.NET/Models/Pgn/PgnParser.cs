namespace ChessDownloader.NET.Models.Pgn
{
    public class PgnParser
    {
        private const string EVENT_KEY = "Event";
        private const string SITE_KEY = "Site";
        private const string DATE_KEY = "Date";
        private const string WHITE_KEY = "White";
        private const string BLACK_KEY = "Black";
        private const string RESULT_KEY = "Result";
        private const string TIMEZONE_KEY = "Timezone";
        private const string ECO_KEY = "ECO";
        private const string UTCDATE_KEY = "UTCDate";
        private const string UTCTIME_KEY = "UTCTime";
        private const string WHITEELO_KEY = "WhiteElo";
        private const string BLACKELO_KEY = "BlackElo";
        private const string TIMECONTROL_KEY = "TimeControl";
        private const string TERMINATION_KEY = "Termination";
        private const string STARTTIME_KEY = "StartTime";
        private const string ENDDATE_KEY = "EndDate";
        private const string ENDTIME_KEY = "EndTime";
        private const string LINK_KEY = "Link";

        private readonly string[] PgnLines;

        public PgnParser(string pgnRaw)
        {
            PgnLines = pgnRaw.Split("\n");
        }

        public Pgn Parse()
        {
            var pgn = new Pgn();
            foreach (var line in PgnLines)
            {
                if (HasKey(EVENT_KEY, line))
                {
                    pgn.Event = Extract(EVENT_KEY, line);
                    continue;
                }

                if (HasKey(SITE_KEY, line))
                {
                    pgn.Site = Extract(SITE_KEY, line);
                    continue;
                }

                if (HasKey(DATE_KEY, line))
                {
                    pgn.Date = Extract(DATE_KEY, line);
                    continue;
                }

                if (HasKey(WHITE_KEY, line))
                {
                    pgn.White = Extract(WHITE_KEY, line);
                    continue;
                }

                if (HasKey(BLACK_KEY, line))
                {
                    pgn.Black = Extract(BLACK_KEY, line);
                    continue;
                }

                if (HasKey(RESULT_KEY, line))
                {
                    pgn.Result = Extract(RESULT_KEY, line);
                    continue;
                }

                if (HasKey(TIMEZONE_KEY, line))
                {
                    pgn.Timezone = Extract(TIMEZONE_KEY, line);
                    continue;
                }

                if (HasKey(ECO_KEY, line))
                {
                    pgn.ECO = Extract(ECO_KEY, line);
                    continue;
                }

                if (HasKey(UTCDATE_KEY, line))
                {
                    pgn.UTCDate = Extract(UTCDATE_KEY, line);
                    continue;
                }

                if (HasKey(UTCTIME_KEY, line))
                {
                    pgn.UTCTime = Extract(UTCTIME_KEY, line);
                    continue;
                }

                if (HasKey(WHITEELO_KEY, line))
                {
                    pgn.WhiteElo = Extract(WHITEELO_KEY, line);
                    continue;
                }

                if (HasKey(BLACKELO_KEY, line))
                {
                    pgn.BlackElo = Extract(BLACKELO_KEY, line);
                    continue;
                }

                if (HasKey(TIMECONTROL_KEY, line))
                {
                    pgn.TimeControl = Extract(TIMECONTROL_KEY, line);
                    continue;
                }

                if (HasKey(TERMINATION_KEY, line))
                {
                    pgn.Termination = Extract(TERMINATION_KEY, line);
                    continue;
                }

                if (HasKey(STARTTIME_KEY, line))
                {
                    pgn.StartTime = Extract(STARTTIME_KEY, line);
                    continue;
                }

                if (HasKey(ENDDATE_KEY, line))
                {
                    pgn.EndDate = Extract(ENDDATE_KEY, line);
                    continue;
                }

                if (HasKey(ENDTIME_KEY, line))
                {
                    pgn.EndTime = Extract(ENDTIME_KEY, line);
                    continue;
                }

                if (HasKey(LINK_KEY, line))
                {
                    pgn.Link = Extract(LINK_KEY, line);
                    continue;
                }
            }
            return pgn;
        }

        private bool HasKey(string key, string line)
        {
            var keyTemplate = $"[{key} ";
            return line.StartsWith(keyTemplate);
        }

        private string Extract(string key, string line)
        {
            var newLine = line.Replace("[" + key + " \"", "");
            return newLine.Replace("\"]", "");
        }
    }
}
