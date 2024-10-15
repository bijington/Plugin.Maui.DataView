using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Plugin.Maui.DataView;

public class Table
{
    public Table(string name, IReadOnlyList<Column> columns)
    {
        Name = name;
        Columns = columns;
    }
    
    public IReadOnlyList<Column> Columns { get; }
    public string Name { get; }
}

public class Column : INotifyPropertyChanged
{
    public Column(string name)
    {
        Name = name;
    }
    
    public string Name { get; }

    private double _width;

    public double Width
    {
        get => _width;
        set
        {
            _width = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Width)));
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}

public class Row
{
    public Row(IReadOnlyList<Cell> cells)
    {
        Cells = cells;
    }
    
    public IReadOnlyList<Cell> Cells { get; }
}

public class Cell
{
    public Cell(object value, Column column)
    {
        Value = value;
        Column = column;
    }
    
    public Column Column { get; }
    
    public object Value { get; }
}