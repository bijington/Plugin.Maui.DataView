using SQLite;

namespace Plugin.Maui.DataView.SqliteNet;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UseSqlite(this MauiAppBuilder builder, Func<SQLiteConnection> connectionFactory)
    {
        DataProvider.Instance = new SqliteDataProvider(connectionFactory);
        builder.Services.AddSingleton<IDataProvider>(DataProvider.Instance);
        
        return builder;
    }
    
    public static MauiAppBuilder UseSqliteAsync(this MauiAppBuilder builder, Func<SQLiteAsyncConnection> connectionFactory)
    {
        DataProvider.Instance = new SqliteAsyncDataProvider(connectionFactory);
        builder.Services.AddSingleton<IDataProvider>(DataProvider.Instance);
        
        return builder;
    }
}