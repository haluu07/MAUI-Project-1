using Microsoft.Maui.Controls;
using System.Linq;
using System.Diagnostics;

namespace Maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            UpdateDarkModeIcon();
            
            // Log để kiểm tra
            Debug.WriteLine("MainPage initialized");
            this.Loaded += OnPageLoaded;
        }

        private void OnPageLoaded(object? sender, EventArgs e)
        {
            Debug.WriteLine("Page loaded event fired");
            // CeilingFanExpandedSection should be available as a field after InitializeComponent
            if (CeilingFanExpandedSection != null)
            {
                Debug.WriteLine("CeilingFanExpandedSection field found!");
            }
            else
            {
                Debug.WriteLine("CeilingFanExpandedSection field is NULL!");
            }
        }

        private void OnDarkModeTapped(object sender, EventArgs e)
        {
            // Toggle theme
            if (Application.Current != null)
            {
                if (Application.Current.UserAppTheme == AppTheme.Dark)
                {
                    Application.Current.UserAppTheme = AppTheme.Light;
                }
                else
                {
                    Application.Current.UserAppTheme = AppTheme.Dark;
                }
            }
            
            UpdateDarkModeIcon();
        }

        private void UpdateDarkModeIcon()
        {
            // Icon sẽ tự động cập nhật qua AppThemeBinding
            // Nhưng có thể thêm animation hoặc visual feedback ở đây nếu cần
        }

        private async void OnCeilingFanSwitchToggled(object sender, ToggledEventArgs e)
        {
            Debug.WriteLine($"=== Ceiling Fan Switch Toggled: {e.Value} ===");
            
            var expandedSection = CeilingFanExpandedSection;
            
            if (expandedSection == null)
            {
                expandedSection = this.FindByName<Border>("CeilingFanExpandedSection");
            }
            
            if (expandedSection == null)
            {
                Debug.WriteLine("❌ ERROR: CeilingFanExpandedSection not found!");
                return;
            }
            
            // Update icon color and background
            UpdateCeilingFanIcon(e.Value);
            
            if (e.Value) // Switch ON
            {
                // Expand: Slide down animation
                expandedSection.IsVisible = true;
                expandedSection.Opacity = 0;
                expandedSection.TranslationY = -50; // Start above
                
                await Task.WhenAll(
                    expandedSection.FadeToAsync(1, 300, Easing.CubicOut),
                    expandedSection.TranslateToAsync(0, 0, 300, Easing.CubicOut)
                );
                
                Debug.WriteLine("✓ Expanded section shown with animation");
            }
            else // Switch OFF
            {
                // Collapse: Slide up animation
                await Task.WhenAll(
                    expandedSection.FadeToAsync(0, 250, Easing.CubicIn),
                    expandedSection.TranslateToAsync(0, -50, 250, Easing.CubicIn)
                );
                
                expandedSection.IsVisible = false;
                // Reset position for next animation
                expandedSection.TranslationY = -50;
                expandedSection.Opacity = 0;
                Debug.WriteLine("✓ Expanded section hidden with animation");
            }
        }

        private void UpdateCeilingFanIcon(bool isOn)
        {
            var iconBorder = CeilingFanIconBorder;
            var iconPath = CeilingFanIconPath;
            
            if (iconBorder == null)
            {
                iconBorder = this.FindByName<Border>("CeilingFanIconBorder");
            }
            
            if (iconPath == null)
            {
                // Try to find Path by name using reflection or visual tree
                var pathElement = this.FindByName<Microsoft.Maui.Controls.Shapes.Path>("CeilingFanIconPath");
                if (pathElement != null)
                {
                    iconPath = pathElement;
                }
            }
            
            if (isOn)
            {
                // ON: Blue gradient background and blue icon
                if (iconBorder != null)
                {
                    iconBorder.Background = new LinearGradientBrush
                    {
                        StartPoint = new Point(0, 0),
                        EndPoint = new Point(1, 1),
                        GradientStops = new GradientStopCollection
                        {
                            new GradientStop { Color = Color.FromArgb("#E3F2FD"), Offset = 0 },
                            new GradientStop { Color = Color.FromArgb("#BBDEFB"), Offset = 1 }
                        }
                    };
                }
                
                if (iconPath != null)
                {
                    iconPath.Fill = Color.FromArgb("#2196F3");
                }
            }
            else
            {
                // OFF: Gray background and gray icon
                if (iconBorder != null)
                {
                    iconBorder.Background = new SolidColorBrush(Color.FromArgb("#F0F0F0"));
                }
                
                if (iconPath != null)
                {
                    iconPath.Fill = Color.FromArgb("#666666");
                }
            }
        }

        private void OnCeilingFanSpeedChanged(object sender, ValueChangedEventArgs e)
        {
            var speedLabel = this.FindByName<Label>("CeilingFanSpeedValueLabel");
            if (speedLabel != null)
            {
                speedLabel.Text = ((int)e.NewValue).ToString();
            }
        }
    }
}
