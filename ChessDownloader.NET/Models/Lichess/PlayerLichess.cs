namespace ChessDownloader.NET.Models.Lichess
{
    class PlayerLichess
    {
        public UserLichess User { get; set; }
        public int Rating { get; set; }
        public int RatingDiff { get; set; }

        public static implicit operator Player(PlayerLichess pl)
        {
            var player = new Player
            {
                Id = pl.User != null ? pl.User.Id : null,
                Rating = pl.Rating,
                Username = pl.User != null ? pl.User.Name : null
            };
            return player;
        }

    }

    public class UserLichess
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }
}
