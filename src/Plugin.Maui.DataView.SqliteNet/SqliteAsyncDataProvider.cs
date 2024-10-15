using SQLite;

namespace Plugin.Maui.DataView.SqliteNet;

public class SqliteAsyncDataProvider : IDataProvider
{
    private readonly SQLiteAsyncConnection _connection;

    public SqliteAsyncDataProvider(Func<SQLiteAsyncConnection> connectionFactory)
    {
        _connection = connectionFactory();
    }

    public async ValueTask<IReadOnlyList<object>> LoadData(Table table)
    {
        var matchingMapping = _connection.TableMappings.FirstOrDefault(mapping => mapping.TableName == table.Name);
        
        return await _connection.QueryAsync(matchingMapping , "SELECT * FROM " + table.Name);
    }
    
    public async ValueTask<IReadOnlyList<object>> QueryData(Table table, string query)
    {
        try
        {
            query = query.Replace("‘", "'").Replace("’", "'");
            var matchingMapping = _connection.TableMappings.FirstOrDefault(mapping => mapping.TableName == table.Name);
        
            return await _connection.QueryAsync(matchingMapping, query);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public IEnumerable<Table> TableDefinitions =>
        _connection.TableMappings
            .Where(mapping => mapping.TableName != nameof(SQLiteConnection.ColumnInfo))
            .Select(mapping =>
                new Table(mapping.TableName, mapping.Columns.Select(c => new Column(c.Name)).ToList()));
}