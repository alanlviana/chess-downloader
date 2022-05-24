using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChessDownloader.NET.Models
{
    public interface IChessScrapper
    {
        Task<IList<Game>> GetGamesByUsernameAsync(string username, IProgress<string> progressMessage);
    }
}
