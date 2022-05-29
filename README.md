# chess-downloader

ChessDownloader.NET is a library that lets you download games from Chess.com and Lichess.org. You can download games by username.

# install

- 📦 [NuGet](https://nuget.org/packages/ChessDownloader.NET): `dotnet add package ChessDownloader.NET` (**main package**)

# usage

ChessDownloader.NET exposes its functionality through two entries points - the ChessComDownloader and LichessDownloader classes.
Create a instance of one of these classes and use the method GetGamesByUsernameAsync(string).

```csharp
using ChessDownloader.NET.ChessCom;
using ChessDownloader.NET.Lichess;

//IChessDownloader chessDownloader = new LichessDownloader();
IChessDownloader chessDownloader = new ChessComDownloader();

var games = await chessDownloader.GetGamesByUsernameAsync("alanlviana", GetHandlerProcessMessage());

private Progress<ChessDownloaderProgress> GetHandlerProcessMessage()
{
    return new Progress<ChessDownloaderProgress>(progressValue => {
    if (progressValue.Undefined)
    {
        Console.WriteLine($"{progressValue.ProgressMessage}");
    } else {
        Console.WriteLine($"{progressValue.Position}/{progressValue.Total}% - {progressValue.ProgressMessage}");
    }
    });
}
```