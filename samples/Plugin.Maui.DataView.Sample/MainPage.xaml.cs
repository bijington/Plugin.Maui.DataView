namespace Plugin.Maui.DataView.Sample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;

        var data = new Data();
    }
}