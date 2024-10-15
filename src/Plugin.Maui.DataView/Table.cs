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
        set => SetField(ref _width, value);
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}

internal class Row
{
    public Row(IReadOnlyList<Cell> cells)
    {
        Cells = cells;
    }
    
    public IReadOnlyList<Cell> Cells { get; }
}

internal class Cell
{
    public Cell(object? value, Column column)
    {
        Value = value;
        Column = column;
    }
    
    public Column Column { get; }
    
    public object? Value { get; }
}