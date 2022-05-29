using CommandLine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChessDownloader.NET.CommandLine
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var parserResult = Parser.Default.ParseArguments<ChessDownloaderOptions>(args);

            await parserResult.WithParsedAsync((options) => RunAndReturnExitCodeAsync(options));
            await parserResult.WithNotParsedAsync((errors) => HandleParseErrorAsync(errors));
        }

        private static async Task<int> RunAndReturnExitCodeAsync(ChessDownloaderOptions options)
        {
            try
            {
                var chessDownloader = new ChessDownloaderConsole();
                return await chessDownloader.DownloadAllGames(options.Source, options.Username, options.Output);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        private static Task HandleParseErrorAsync(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                if (error is VersionRequestedError)
                {
                    continue;
                }
            }
            return Task.CompletedTask;
        }

        
    }
}


