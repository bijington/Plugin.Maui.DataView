namespace Plugin.Maui.DataView;

public interface IDataProvider
{
    ValueTask<IReadOnlyList<object>> LoadData(Table table);

    ValueTask<IReadOnlyList<object>> QueryData(Table table, string query);

    IEnumerable<Table> TableDefinitions { get; }
}