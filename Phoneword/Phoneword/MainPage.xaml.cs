namespace Phoneword
{
    public partial class MainPage : ContentPage
    {
        string? translatedNumber;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnTranslate(object sender, EventArgs e)
        {
            string enteredNumber = PhoneNumberText.Text;
            translatedNumber = PhonewordTranslator.ToNumber(enteredNumber);

            if (!string.IsNullOrEmpty(translatedNumber))
            {
                CallButton.IsEnabled = true;
                CallButton.Text = "Call " + translatedNumber;
            }
            else
            {
                CallButton.IsEnabled = false;
                CallButton.Text = "Call";
            }
        }

        //Event Handlers:
        //In.NET(including.NET MAUI), async void is acceptable and recommended only for event handlers (like button clicks). This is because event handlers must match a specific delegate signature (e.g., void OnClick(object sender, EventArgs e)), so you can't return a Task directly.
        private async void OnCall(object sender, System.EventArgs e)
        {
            if (await DisplayAlert(
                "Dial a Number",
                "Would you like to call " + translatedNumber + "?",
                "Yes",
                "No"))
            {
                try
                {
                    if (PhoneDialer.Default.IsSupported && !string.IsNullOrWhiteSpace(translatedNumber))
                        PhoneDialer.Default.Open(translatedNumber);
                }
                catch (ArgumentNullException)
                {
                    await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
                }
                catch (Exception)
                {
                    // Other error has occurred.
                    await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
                }
            }
        }
    }
}
