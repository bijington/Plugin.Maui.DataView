using LiteDB;
using Plugin.Maui.DataView.Sample.Models;

namespace Plugin.Maui.DataView.Sample.LiteDB;

public class LiteData
{
    internal static LiteDatabase CreateDatabase()
    {
        var path = FileSystem.AppDataDirectory;
        var database = new LiteDatabase(Path.Combine(path, "litedata.db"));

        database.GetCollection<Course>();//.Insert(new Course { Name = "Maths" });
        database.GetCollection<Student>();//.Insert(new Student { Name = "Shaun" });
        database.GetCollection<Enrolment>();
        
        return database;
    }
}