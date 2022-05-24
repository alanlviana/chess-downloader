using Newtonsoft.Json;
using System;

namespace ChessDownloader.NET.Models.ChessCom.GamesListByArchiveEndpoint
{
    public class GameChessCom
    {
        public string Url { get; set; }
        public string Pgn { get; set; }
        public string Time_control { get; set; }
        public long End_time { get; set; }
        public bool Rated { get; set; }
        public string Fen { get; set; }

        [JsonProperty("start_time")]
        public string Start_time { get; set; }
        public string Time_class { get; set; }
        public string Rules { get; set; }
        public PlayerChessCom White { get; set; }
        public PlayerChessCom Black { get; set; }

        public static implicit operator Game(GameChessCom gc)
        {
            var game = new Game
            {
                Url = gc.Url,
                Pgn = gc.Pgn,
                TimeControl = gc.Time_control,
                EndTime = gc.End_time,
                Rated = gc.Rated,
                Fen = gc.Fen,
                TimeClass = gc.Time_class,
                Rules = gc.Rules,
                White = gc.White,
                Black = gc.Black,
                Source = ChessSource.ChessCom
            };
            return game;
        }
    }
}
