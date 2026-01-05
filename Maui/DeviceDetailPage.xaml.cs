namespace Maui
{
    public partial class DeviceDetailPage : ContentPage
    {
        public DeviceDetailPage(string deviceName = "Ceiling Fan", string location = "Living Room", bool isOn = false)
        {
            InitializeComponent();
            
            DeviceNameLabel.Text = deviceName;
            DeviceLocationLabel.Text = location;
            PowerSwitch.IsToggled = isOn;
        }

        private void OnSpeedChanged(object sender, ValueChangedEventArgs e)
        {
            SpeedValueLabel.Text = ((int)e.NewValue).ToString();
        }
    }
}

