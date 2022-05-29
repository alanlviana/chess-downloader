using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChessDownloader.NET
{
    public interface IChessDownloader
    {
        Task<IList<Game>> GetGamesByUsernameAsync(string username, IProgress<ChessDownloaderProgress> progressMessage = null);
    }
}
