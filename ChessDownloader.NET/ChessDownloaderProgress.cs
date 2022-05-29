using System;
using System.Collections.Generic;
using System.Text;

namespace ChessDownloader.NET
{
    public class ChessDownloaderProgress
    {
        public ChessDownloaderProgress(decimal position, decimal total, string progressMessage, bool undefined = false)
        {
            Position = position;
            Total = total;
            ProgressMessage = progressMessage;
            Undefined = undefined;
        }

        public decimal Position { get; set; }
        public decimal Total { get; set; }
        public bool Undefined { get; set; }
        public string ProgressMessage { get; set; }
    }
}
