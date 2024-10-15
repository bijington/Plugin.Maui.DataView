using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Plugin.Maui.DataView;

public partial class DataViewer : ContentView
{
    public DataViewer()
    {
        InitializeComponent();
    }
    
    public IReadOnlyList<Column> Columns { get; set; } = [];
    
    public static readonly BindableProperty TablesProperty =
        BindableProperty.Create(nameof(Tables), typeof(ObservableCollection<Table>), typeof(DataViewer), null,  propertyChanged: TablesPropertyChanged);
    
    public ObservableCollection<Table> Tables
    {
        get => (ObservableCollection<Table>)GetValue(TablesProperty);
        set => SetValue(TablesProperty, value);
    }

    private static void TablesPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((DataViewer)bindable).UpdateTables();
    }

    private void UpdateTables()
    {
        TablePicker.ItemsSource = Tables;
    }

    private void OnTablePickerSelectedIndexChanged(object? sender, EventArgs e)
    {
        if (TablePicker.SelectedItem is Table table)
        {
            UpdateColumns(table);
            
            UpdateData(table);
        }
    }

    private void UpdateColumns(Table selectedTable)
    {
        foreach (var column in selectedTable.Columns)
        {
            column.Width = 200;
        }
        
        Header.ItemsSource = selectedTable.Columns;
        
        Columns = selectedTable.Columns;
        OnPropertyChanged(nameof(Columns));
        
        // DataGrid.ColumnDefinitions.Clear();
        // DataGrid.RowDefinitions.Clear();
        // DataGrid.Children.Clear();
        //
        // DataGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
        //
        // for (var columnIndex = 0; columnIndex < selectedTable.Columns.Count; columnIndex++)
        // {
        //     var column = selectedTable.Columns[columnIndex];
        //     DataGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
        //
        //     var columnHeader = new Button
        //     {
        //         Text = column.Name,
        //     };
        //     
        //     DataGrid.Children.Add(columnHeader);
        //     DataGrid.SetColumn(columnHeader, columnIndex);
        // }
    }

    private async void UpdateData(Table selectedTable)
    {
        var stopwatch = Stopwatch.StartNew();
        
        IReadOnlyList<object> data;
        
        Console.WriteLine($"Loading data from {selectedTable.Name} {stopwatch.ElapsedMilliseconds}ms");
        
        if (string.IsNullOrWhiteSpace(QueryEditor.Text) is false)
        {
            data = await DataProvider.Instance.QueryData(selectedTable, QueryEditor.Text);    
        }
        else
        {
            data = await DataProvider.Instance.LoadData(selectedTable);
        }
        
        Console.WriteLine($"Loaded data from {selectedTable.Name} {stopwatch.ElapsedMilliseconds}ms");

        List<Row> rows = [];
        
        for (var dataIndex = 0; dataIndex < data.Count; dataIndex++)
        {
            var row = data[dataIndex];
    
            List<Cell> cells = [];
            
            for (var columnIndex = 0; columnIndex < selectedTable.Columns.Count; columnIndex++)
            {
                var column = selectedTable.Columns[columnIndex];

                cells.Add(new (row.GetType()?.GetProperty(column.Name)?.GetValue(row), column));
            }
           
            rows.Add(new (cells));
        }
        
        Console.WriteLine($"Massaged data from {selectedTable.Name} {stopwatch.ElapsedMilliseconds}ms");

        Data.ItemsSource = rows;
    }

    private void OnRunButtonClicked(object? sender, EventArgs e)
    {
        if (TablePicker.SelectedItem is Table table)
        {
            UpdateColumns(table);
            
            UpdateData(table);
        }
    }

    private bool _scrollFromHeader;
    private bool _scrollFromData;
    
    private void OnHeaderScrolled(object? sender, ItemsViewScrolledEventArgs e)
    {
        if (_scrollFromHeader)
        {
            return;
        }
        
        _scrollFromHeader = true;
        Data.SendScrolled(e);
        _scrollFromHeader = false;
    }

    private void OnDataScrolled(object? sender, ItemsViewScrolledEventArgs e)
    {
        if (_scrollFromData)
        {
            return;
        }
        
        _scrollFromData = true;
        Header.SendScrolled(e);
        _scrollFromData = false;
    }

    private void OnHeaderButtonClicked(object? sender, EventArgs e)
    {
        
    }
}