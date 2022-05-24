using ChessDownloader.NET.Models.Lichess.GamesByUserEndpoint;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ChessDownloader.NET.Models.Lichess
{
    public class LichessScrapper : IChessScrapper
    {
        public async Task<IList<Game>> GetGamesByUsernameAsync(string username, IProgress<string> progressMessage)
        {
            string jsonND = await getJsonNDFromUsername(username, progressMessage);
            var games = GetAllGamesFromJsonND(jsonND, progressMessage);

            return games;
        }

        private static async Task<string> getJsonNDFromUsername(string username, IProgress<string> progressMessage)
        {
            var url = $"https://lichess.org/api/games/user/{username}?pgnInJson=true";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/x-ndjson"));
            var response = await client.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception("Username not found");
            }
            response.EnsureSuccessStatusCode();
            var jsonND = await response.Content.ReadAsStringAsync();
            return jsonND;
        }

        private List<Game> GetAllGamesFromJsonND(string jsonND, IProgress<string> progressMessage)
        {
            String line = null;
            var gameList = new List<Game>();
            using (var sr = new StringReader(jsonND))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    var gameLichess = JsonConvert.DeserializeObject<GameLichessResponse>(line);
                    gameList.Add(gameLichess);
                }
            }

            return gameList;
        }
    }
}
