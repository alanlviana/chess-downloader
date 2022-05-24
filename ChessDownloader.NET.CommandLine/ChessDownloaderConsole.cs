using ChessDownloader.NET.Models.ChessCom;
using ChessDownloader.NET.Models.Lichess;
using ChessDownloader.NET.Models.Pgn;
using ChessDownloader.NET.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ChessDownloader.NET.CommandLine
{
    class ChessDownloaderConsole
    {
        public async Task<int> DownloadAllGames(string source, string username, string output)
        {
            IChessScrapper chessScrapper = getScrapperBySourceName(source);

            var games = await chessScrapper.GetGamesByUsernameAsync(username, GetHandlerProcessMessage());

            SaveAllGames(output, games);

            return 0;
        }

        private void SaveAllGames(string output, IList<Game> games)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var finalPathOutput = Path.Combine(currentDirectory, output);

            foreach (var game in games)
            {
                var pgn = new PgnParser(game.Pgn).Parse();
                SavePGN(finalPathOutput, game, pgn);
            }
            Console.WriteLine($"{games.Count} games were saved at:");
            Console.WriteLine(finalPathOutput);
        }

        private void SavePGN(string output, Game game, Pgn pgn)
        {

            var white = pgn.White;
            var black = pgn.Black;
            var date = pgn.UTCDate.Replace(".", "");
            var time = pgn.UTCTime.Replace(":", "");
            var sourceName = game.Source.Name;

            var fileName = $"{date}.{time}.{sourceName}-{white}-{black}.pgn";
            Directory.CreateDirectory(output);
            var filePath = Path.Combine(output, fileName);
            File.WriteAllText(filePath, game.Pgn);
        }

        private static Progress<string> GetHandlerProcessMessage()
        {
            return new Progress<string>(progressValue => Console.WriteLine(progressValue));
        }

        private static IChessScrapper getScrapperBySourceName(string source)
        {
            IChessScrapper chessScrapper;
            switch (source)
            {
                case "chesscom":
                    chessScrapper = new ChessComScrapper();
                    break;
                case "lichess":
                    chessScrapper = new LichessScrapper();
                    break;

                default:
                    throw new Exception($"scrapper {source} not found");
            }

            return chessScrapper;
        }
    }
}
