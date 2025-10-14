namespace WeatherClient;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void btnRefresh_Clicked(object sender, EventArgs e)
    {
        btnRefresh.IsEnabled = false;
        actIsBusy.IsRunning = true;

        var weather = await Services.WeatherServer.GetWeather(txtPostalCode.Text);
        BindingContext = weather;

        btnRefresh.IsEnabled = true;
        actIsBusy.IsRunning = false;
    }
}
