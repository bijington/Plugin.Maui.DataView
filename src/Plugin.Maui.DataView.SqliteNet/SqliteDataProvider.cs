using SQLite;

namespace Plugin.Maui.DataView.SqliteNet;

public class SqliteDataProvider : IDataProvider
{
    private readonly SQLiteConnection _connection;

    public SqliteDataProvider(Func<SQLiteConnection> connectionFactory)
    {
        _connection = connectionFactory();
    }
    
    public ValueTask<IReadOnlyList<object>> LoadData(Table table)
    {
        var matchingMapping = _connection.TableMappings.FirstOrDefault(mapping => mapping.TableName == table.Name);
        
        return ValueTask.FromResult<IReadOnlyList<object>>(_connection.Query(matchingMapping , "SELECT * FROM " + table.Name));
    }
    
    public ValueTask<IReadOnlyList<object>> QueryData(Table table, string query)
    {
        try
        {
            query = query.Replace("‘", "'").Replace("’", "'");
            var matchingMapping = _connection.TableMappings.FirstOrDefault(mapping => mapping.TableName == table.Name);
        
            return ValueTask.FromResult<IReadOnlyList<object>>(_connection.Query(matchingMapping, query));
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