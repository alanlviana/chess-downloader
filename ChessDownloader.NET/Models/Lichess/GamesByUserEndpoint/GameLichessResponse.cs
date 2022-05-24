namespace ChessDownloader.NET.Models.Lichess.GamesByUserEndpoint
{
    class GameLichessResponse
    {
        public string Id { get; set; }
        public bool Rated { get; set; }
        public string Variant { get; set; }
        public string Speed { get; set; }
        public string Perf { get; set; }
        public string CreatedAt { get; set; }
        public string LastMoveAt { get; set; }
        public string Status { get; set; }
        public MatchPlayersLichess Players { get; set; }
        public string Winner { get; set; }
        public string Moves { get; set; }
        public string Pgn { get; set; }
        public ClockLichess Clock { get; set; }

        public static implicit operator Game(GameLichessResponse gl)
        {
            var game = new Game
            {
                Pgn = gl.Pgn,
                Rated = gl.Rated,
                White = gl.Players.White != null ? gl.Players.White : null,
                Black = gl.Players.Black != null ? gl.Players.Black : null,
                Source = ChessSource.Lichess
            };
            return game;
        }

    }
}
