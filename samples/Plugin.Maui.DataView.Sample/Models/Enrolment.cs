using SQLite;

namespace Plugin.Maui.DataView.Sample.Models;

public class Enrolment
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    public int CourseId { get; set; }
    
    public int StudentId { get; set; }
    
    public DateTime Enrolled { get; set; }
}