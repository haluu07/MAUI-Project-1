namespace Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            // Register route for DeviceDetailPage
            Routing.RegisterRoute(nameof(DeviceDetailPage), typeof(DeviceDetailPage));
        }
    }
}
