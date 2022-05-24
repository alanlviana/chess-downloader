using CommandLine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessDownloader.NET.CommandLine
{
    class ChessDownloaderOptions {

        [Option('s', "source", Required = true, HelpText = "Set source of games, can be 'lichess' or 'chesscom'")]
        public string Source { get; set; }

        [Option('u', "username", Required = true, HelpText = "Set username in source to download games. ")]
        public string Username { get; set; }

        [Option('o', "output", Default = "pgn", HelpText = "Set output folder to save all games files.")]
        public string Output { get; set; }
    }
}
