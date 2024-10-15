using SQLite;

namespace Plugin.Maui.DataView.Sample.Models;

public class Student
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
}