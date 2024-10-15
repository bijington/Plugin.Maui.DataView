using Microsoft.Extensions.Logging;
using Plugin.Maui.DataView.LiteDB;
using Plugin.Maui.DataView.Sample.LiteDB;
using Plugin.Maui.DataView.SqliteNet;

namespace Plugin.Maui.DataView.Sample;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            // .UseLiteDB(LiteData.CreateDatabase)
            .UseSqlite(Data.CreateConnection)
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}