using ChessDownloader.NET.Models.ChessCom.ArchiveListEndpoint;
using ChessDownloader.NET.Models.ChessCom.GamesListByArchiveEndpoint;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChessDownloader.NET.Models.ChessCom
{
    public class ChessComScrapper : IChessScrapper
    {
        public async Task<IList<Game>> GetGamesByUsernameAsync(string username, IProgress<string> progressMessage)
        {
            var archiveListJson = await GetArchiveListJson(username, progressMessage);
            var archiveListResult = GetArchiveListFromJson(archiveListJson, progressMessage);
            var games = await GetAllGamesFromArchiveList(archiveListResult, progressMessage);
            return games;
        }

        private async Task<List<Game>> GetAllGamesFromArchiveList(ArchiveListResult archiveListResult, IProgress<string> progressMessage)
        {
            var gameList = new List<Game>();
            var archiveCount = archiveListResult.Archives.Length;

            for(var i = 0; i < archiveCount; i++)
            {
                var archive = archiveListResult.Archives[i];
                var archiveGameList = await GetAllGamesFromArchiveList(archive);
                gameList.AddRange(archiveGameList);
                progressMessage?.Report($"[{i+1}/{archiveCount}] {gameList.Count} games downloaded!");
            }
            return gameList;
        }

        private async Task<List<Game>> GetAllGamesFromArchiveList(string archive)
        {
            var gamesListJson = await GetGameListJsonFromArchiveUrl(archive);
            var gamesListResult = GetGameListResultFromJson(gamesListJson);
            var gamesList = GetAllFromGameListResult(gamesListResult);
            return gamesList;
        }

        private List<Game> GetAllFromGameListResult(GamesListResult gamesListResult)
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

        private ArchiveListResult GetArchiveListFromJson(string archiveListJson, IProgress<string> progressMessage)
        {
            var archiveList = JsonConvert.DeserializeObject<ArchiveListResult>(archiveListJson);
            progressMessage?.Report($"This user has {archiveList.Archives.Length} archives.");
            return archiveList;
        }

        private async Task<string> GetArchiveListJson(string username, IProgress<string> progressMessage)
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
            progressMessage?.Report($"Archive list downloaded.");
            return archiveListString;
        }
    }
}
