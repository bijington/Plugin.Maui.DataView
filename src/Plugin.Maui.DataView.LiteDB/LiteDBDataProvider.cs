using LiteDB;

namespace Plugin.Maui.DataView.LiteDB;

// All the code in this file is included in all platforms.
public class LiteDBDataProvider : IDataProvider
{
    private readonly LiteDatabase _database;
    
    public LiteDBDataProvider(Func<LiteDatabase> databaseFactory)
    {
        _database = databaseFactory();
    }

    public ValueTask<IReadOnlyList<object>> LoadData(Table table)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IReadOnlyList<object>> QueryData(Table table, string query)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Table> TableDefinitions
    {
        get
        {
            var b = _database.GetCollectionNames().Select(name => new Table(name, []));
            var course = _database.GetCollection("Course");
            var mm = _database.Mapper;
            var map = course.EntityMapper;
            var members = _database.GetCollection("Course").EntityMapper.Members;
            return _database.GetCollectionNames().Select(name => new Table(name, []));
        }
    }
}