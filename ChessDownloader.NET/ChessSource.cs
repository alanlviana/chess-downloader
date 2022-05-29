using System;
using System.Collections.Generic;
using System.Text;

namespace ChessDownloader.NET
{
    public class ChessSource
    {
        private ChessSource(string name) { Name = name; }
        public string Name { get; private set; }
        public static ChessSource ChessCom { get { return new ChessSource("ChessCom"); } }
        public static ChessSource Lichess { get { return new ChessSource("Lichess"); } }
    }
}
