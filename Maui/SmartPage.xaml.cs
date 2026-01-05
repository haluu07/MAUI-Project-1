namespace Maui
{
    public partial class SmartPage : ContentPage
    {
        public DeviceViewModel RgbLights { get; set; }
        public DeviceViewModel TableFan { get; set; }
        public DeviceViewModel PendantLights { get; set; }
        public DeviceViewModel SplitAC { get; set; }
        public DeviceViewModel TV { get; set; }

        public SmartPage()
        {
            InitializeComponent();
            
            // Initialize devices
            RgbLights = new DeviceViewModel 
            { 
                Name = "RGB Lights", 
                Location = "Kitchen", 
                DeviceType = "light",
                SelectedColor = Colors.Purple
            };
            TableFan = new DeviceViewModel 
            { 
                Name = "Table Fan", 
                Location = "Study Room", 
                DeviceType = "fan",
                SpeedLevel = 2
            };
            PendantLights = new DeviceViewModel 
            { 
                Name = "Pendant Lights", 
                Location = "Dining Room", 
                DeviceType = "light",
                SelectedColor = Colors.Purple
            };
            SplitAC = new DeviceViewModel 
            { 
                Name = "Split AC", 
                Location = "Master Bedroom", 
                DeviceType = "ac",
                Temperature = 22
            };
            TV = new DeviceViewModel 
            { 
                Name = "TV", 
                Location = "Bedroom", 
                DeviceType = "tv" 
            };

            BindingContext = this;
        }

        private async void OnCompactTabTapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void OnPremiumTabTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PremiumPage());
        }
    }
}
