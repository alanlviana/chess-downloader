using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessDownloader.NET.ChessCom.GamesListByArchiveEndpoint
{
    public class PlayerChessCom
    {
        public int Rating { get; set; }
        public string Result { get; set; }
        
        [JsonProperty("@id")]
        public string Id { get; set; }
        public string Username { get; set; }


        public static implicit operator Player(PlayerChessCom pc)
        {
            var player = new Player
            {
                Id = pc.Id,
                Rating = pc.Rating,
                Result = pc.Result,
                Username = pc.Username
            };
            return player;
        }
    }
}
