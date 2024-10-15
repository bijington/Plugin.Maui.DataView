using Plugin.Maui.DataView.Sample.Models;
using SQLite;

namespace Plugin.Maui.DataView.Sample;

public class Data
{
    private readonly SQLite.SQLiteConnection _connection = CreateConnection();

    internal static SQLiteConnection CreateConnection()
    {
        var path = FileSystem.AppDataDirectory;
        var connection = new SQLite.SQLiteConnection(Path.Combine(path, "data.db"));

        connection.CreateTable<Course>();
        connection.CreateTable<Student>();
        connection.CreateTable<Enrolment>();

        // for (var i = 0; i < 10000; i++)
        // {
        //     connection.Insert(new Student { Name = $"Student {i}"});
        // }
        
        return connection;
    }
}