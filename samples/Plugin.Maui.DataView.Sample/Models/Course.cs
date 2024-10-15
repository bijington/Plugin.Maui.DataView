using SQLite;

namespace Plugin.Maui.DataView.Sample.Models;

public class Course
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}