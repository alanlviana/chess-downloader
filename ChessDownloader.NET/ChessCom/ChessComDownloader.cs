using ChessDownloader.NET.ChessCom.ArchiveListEndpoint;
using ChessDownloader.NET.ChessCom.GamesListByArchiveEndpoint;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChessDownloader.NET.ChessCom
{
    public class ChessComDownloader : IChessDownloader
    {
        private IProgress<ChessDownloaderProgress> ChessDownloaderProgress;
        private decimal Total = 100;
        private decimal Position = 0;

        public async Task<IList<Game>> GetGamesByUsernameAsync(string username, IProgress<ChessDownloaderProgress> progressMessage)
        {
            ChessDownloaderProgress = progressMessage;
            var archiveListJson = await GetArchiveListJson(username);
            var archiveListResult = GetArchiveListFromJson(archiveListJson);
            var games = await GetAllGamesFromArchiveList(archiveListResult);
            return games;
        }

        private async Task<IList<Game>> GetAllGamesFromArchiveList(ArchiveListResult archiveListResult)
        {
            var gameList = new List<Game>();
            var archiveCount = archiveListResult.Archives.Length;

            for(var i = 0; i < archiveCount; i++)
            {
                var archive = archiveListResult.Archives[i];
                var archiveGameList = await GetAllGamesFromArchiveList(archive);
                Position = Math.Round((decimal)(i + 1) / (decimal)archiveCount * 100, 2);
                gameList.AddRange(archiveGameList);
                ReportProgress($"{gameList.Count} games downloaded!");
            }
            return gameList;
        }

        private void ReportProgress(string message)
        {
            ChessDownloaderProgress?.Report(new ChessDownloaderProgress(Position, Total, message));
        }

        private async Task<IList<Game>> GetAllGamesFromArchiveList(string archive)
        {
            var gamesListJson = await GetGameListJsonFromArchiveUrl(archive);
            var gamesListResult = GetGameListResultFromJson(gamesListJson);
            var gamesList = GetAllFromGameListResult(gamesListResult);
            return gamesList;
        }

        private IList<Game> GetAllFromGameListResult(GamesListResult gamesListResult)
        {
            return gamesListResult.Games.Select(g => {
                Game game = g;
                return game;
            }).ToList();
        }

        private GamesListResult GetGameListResultFromJson(string gamesListJson)
        {
            var gamesList = JsonConvert.DeserializeObject<GamesListResult>(gamesListJson);
            return gamesList;
        }

        private async Task<string> GetGameListJsonFromArchiveUrl(string archive)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(archive);
            var gameListString = await response.Content.ReadAsStringAsync();
            return gameListString;
        }

        private ArchiveListResult GetArchiveListFromJson(string archiveListJson)
        {
            var archiveList = JsonConvert.DeserializeObject<ArchiveListResult>(archiveListJson);
            ReportProgress($"This user has {archiveList.Archives.Length} archives.");
            return archiveList;
        }

        private async Task<string> GetArchiveListJson(string username)
        {
            string lichess_url = $"https://api.chess.com/pub/player/{username}/games/archives";
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(lichess_url);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception("Username not found");
            }
            response.EnsureSuccessStatusCode();
            var archiveListString = await response.Content.ReadAsStringAsync();
            ReportProgress($"Archive list downloaded.");
            return archiveListString;
        }
    }
}
