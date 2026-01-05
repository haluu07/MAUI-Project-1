namespace Maui
{
    public partial class MainPage : ContentPage
    {
        public DeviceViewModel CeilingFan { get; set; }
        public DeviceViewModel AirConditioner { get; set; }
        public DeviceViewModel SmartTV { get; set; }
        public DeviceViewModel RgbLights { get; set; }
        public DeviceViewModel TableFan { get; set; }
        public DeviceViewModel PendantLights { get; set; }
        public DeviceViewModel SplitAC { get; set; }
        public DeviceViewModel TV { get; set; }

        public MainPage()
        {
            InitializeComponent();
            
            // Initialize devices
            CeilingFan = new DeviceViewModel 
            { 
                Name = "Ceiling Fan", 
                Location = "Living Room", 
                DeviceType = "fan",
                SpeedLevel = 2
            };
            AirConditioner = new DeviceViewModel 
            { 
                Name = "Air Conditioner", 
                Location = "Bedroom", 
                DeviceType = "ac",
                Temperature = 22
            };
            SmartTV = new DeviceViewModel 
            { 
                Name = "Smart TV", 
                Location = "Living Room", 
                DeviceType = "tv" 
            };
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

        private async void OnSmartTabTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SmartPage());
        }

        private async void OnPremiumTabTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PremiumPage());
        }
    }
}
