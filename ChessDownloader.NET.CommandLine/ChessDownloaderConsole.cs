using ChessDownloader.NET.ChessCom;
using ChessDownloader.NET.Lichess;
using ChessDownloader.NET.Pgn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ChessDownloader.NET.CommandLine
{
    class ChessDownloaderConsole
    {
        private const int EXIT_CODE_SUCCESS = 0;

        public async Task<int> DownloadAllGames(string source, string username, string output)
        {
            IChessDownloader chessDownloader = getDownloaderBySourceName(source);

            var games = await chessDownloader.GetGamesByUsernameAsync(username, GetHandlerProcessMessage());

            await SaveAllGames(output, games);

            return EXIT_CODE_SUCCESS;
        }

        private Task SaveAllGames(string output, IList<Game> games)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var finalPathOutput = Path.Combine(currentDirectory, output);

            Parallel.ForEach(games, (game) =>
            {
                var pgn = new PgnParser(game.Pgn).Parse();
                SavePgn(finalPathOutput, game, pgn);
            });


            Console.WriteLine($"{games.Count} games were saved at:");
            Console.WriteLine(finalPathOutput);

            return Task.CompletedTask;
        }

        private void SavePgn(string output, Game game, PgnGame pgn)
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

        private Progress<ChessDownloaderProgress> GetHandlerProcessMessage()
        {
            return new Progress<ChessDownloaderProgress>(progressValue => {
                if (progressValue.Undefined)
                {
                    Console.WriteLine($"{progressValue.ProgressMessage}");
                } else {
                    Console.WriteLine($"{progressValue.Position}/{progressValue.Total}% - {progressValue.ProgressMessage}");
                }
            });
        }

        private static IChessDownloader getDownloaderBySourceName(string source)
        {
            IChessDownloader downloader;
            switch (source)
            {
                case "chesscom":
                    downloader = new ChessComDownloader();
                    break;
                case "lichess":
                    downloader = new LichessDownloader();
                    break;

                default:
                    throw new ArgumentException($"downloader {source} not found");
            }

            return downloader;
        }
    }
}
