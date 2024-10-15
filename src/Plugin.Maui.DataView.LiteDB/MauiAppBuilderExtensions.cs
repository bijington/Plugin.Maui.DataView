using LiteDB;

namespace Plugin.Maui.DataView.LiteDB;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UseLiteDB(this MauiAppBuilder builder, Func<LiteDatabase> databaseFactory)
    {
        DataProvider.Instance = new LiteDBDataProvider(databaseFactory);
        builder.Services.AddSingleton<IDataProvider>(DataProvider.Instance);
        
        return builder;
    }
}