namespace ChessDownloader.NET.Models
{
    public class Game
    {
        public string Url { get; set; }
        public string Pgn { get; set; }
        public string TimeControl { get; set; }
        public long EndTime { get; set; }
        public bool Rated { get; set; }
        public string Fen { get; set; }
        public string TimeClass { get; set; }
        public string Rules { get; set; }
        public Player White { get; set; }
        public Player Black { get; set; }
        public ChessSource Source { get; set; }
    }
}
