namespace ChessDownloader.NET.Pgn
{
    public class PgnGame
    {
        public string Event { get; set; }
        public string Site { get; set; }
        public string Date { get; set; }
        public string White { get; set; }
        public string Black { get; set; }
        public string Result { get; set; }
        public string Timezone { get; set; }
        public string ECO { get; set; }
        public string UTCDate { get; set; }
        public string UTCTime { get; set; }
        public string WhiteElo { get; set; }
        public string BlackElo { get; set; }
        public string TimeControl { get; set; }
        public string Termination { get; set; }
        public string StartTime { get; set; }
        public string EndDate { get; set; }
        public string EndTime { get; set; }
        public string Link { get; set; }

    }
}
