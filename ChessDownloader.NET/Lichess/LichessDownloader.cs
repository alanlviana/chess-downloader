using ChessDownloader.NET.Lichess.GamesByUserEndpoint;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessDownloader.NET.Lichess
{
    public class LichessDownloader : IChessDownloader
    {
        private IProgress<ChessDownloaderProgress> ChessDownloaderProgress;

        public async Task<IList<Game>> GetGamesByUsernameAsync(string username, IProgress<ChessDownloaderProgress> progressMessage)
        {
            ChessDownloaderProgress = progressMessage;
            string jsonND = await getJsonNDFromUsername(username);
            var games = GetAllGamesFromJsonND(jsonND);

            return games;
        }

        private async Task<string> getJsonNDFromUsername(string username)
        {
            var url = $"https://lichess.org/api/games/user/{username}?pgnInJson=true";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/x-ndjson"));
            var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception("Username not found");
            }
            response.EnsureSuccessStatusCode();

            var memoryStream = new MemoryStream();
            using (var downloadStream = await response.Content.ReadAsStreamAsync())
            {
                byte[] buffer = new byte[8192];
                int bytesRead;

                while ((bytesRead = downloadStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memoryStream.Write(buffer, 0, bytesRead); 
                    ChessDownloaderProgress?.Report(new ChessDownloaderProgress(memoryStream.Length, 0, $"{memoryStream.Length} bytes were downloaded.", true));
                }
            }
            memoryStream.Position = 0;
            using var streamReader = new StreamReader(memoryStream);
            var jsonND = await streamReader.ReadToEndAsync();
            return jsonND;
        }

        

        private List<Game> GetAllGamesFromJsonND(string jsonND)
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
