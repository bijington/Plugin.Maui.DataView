using System.Collections.ObjectModel;

namespace Plugin.Maui.DataView.Sample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;

        var data = new Data();

        Viewer.Tables = new ObservableCollection<Table>(DataProvider.Instance.TableDefinitions);
    }
}