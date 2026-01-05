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
            
            // Update icon color and background with animation
            UpdateIconColor(CeilingFanIconBorder, CeilingFanIconPath, e.Value, Color.FromArgb("#2196F3"), "#E3F2FD", "#BBDEFB");
            
            // Use helper method for smooth animation
            await ToggleExpandedSection(expandedSection, e.Value);
        }


        private void OnCeilingFanSpeedChanged(object sender, ValueChangedEventArgs e)
        {
            var speedLabel = this.FindByName<Label>("CeilingFanSpeedValueLabel");
            if (speedLabel != null)
            {
                speedLabel.Text = ((int)e.NewValue).ToString();
            }
        }

        // Custom easing function for smooth spring effect
        private static double SmoothEaseInOut(double t)
        {
            return t < 0.5 
                ? 4 * t * t * t 
                : 1 - System.Math.Pow(-2 * t + 2, 3) / 2;
        }

        // Helper method for toggle animation with smooth spring effect
        private async Task ToggleExpandedSection(Border expandedSection, bool isOn)
        {
            if (expandedSection == null) return;

            if (isOn)
            {
                expandedSection.IsVisible = true;
                expandedSection.Opacity = 0;
                expandedSection.Scale = 0.95f;
                expandedSection.TranslationY = -30;
                
                // Smooth spring-like animation with custom easing
                var easing = new Easing(SmoothEaseInOut);
                await Task.WhenAll(
                    expandedSection.FadeToAsync(1, 400, easing),
                    expandedSection.TranslateToAsync(0, 0, 400, easing),
                    expandedSection.ScaleToAsync(1, 400, easing)
                );
            }
            else
            {
                // Smooth collapse animation
                await Task.WhenAll(
                    expandedSection.FadeToAsync(0, 300, Easing.CubicIn),
                    expandedSection.TranslateToAsync(0, -30, 300, Easing.CubicIn),
                    expandedSection.ScaleToAsync(0.95f, 300, Easing.CubicIn)
                );
                
                expandedSection.IsVisible = false;
                expandedSection.TranslationY = -30;
                expandedSection.Opacity = 0;
                expandedSection.Scale = 0.95f;
            }
        }

        // Helper method for icon color update with smooth animation
        private async Task UpdateIconColorAsync(Border? iconBorder, Microsoft.Maui.Controls.Shapes.Path? iconPath, bool isOn, Color onColor, string gradientStart, string gradientEnd)
        {
            if (isOn)
            {
                if (iconBorder != null)
                {
                    // Animate scale for visual feedback
                    _ = iconBorder.ScaleToAsync(1.1f, 200, Easing.SpringOut).ContinueWith(_ => 
                        iconBorder.ScaleToAsync(1.0f, 200, Easing.SpringOut));
                    
                    iconBorder.Background = new LinearGradientBrush
                    {
                        StartPoint = new Point(0, 0),
                        EndPoint = new Point(1, 1),
                        GradientStops = new GradientStopCollection
                        {
                            new GradientStop { Color = Color.FromArgb(gradientStart), Offset = 0 },
                            new GradientStop { Color = Color.FromArgb(gradientEnd), Offset = 1 }
                        }
                    };
                }
                
                if (iconPath != null)
                {
                    // Smooth color transition
                    await iconPath.FadeToAsync(0.8f, 100);
                    iconPath.Fill = onColor;
                    await iconPath.FadeToAsync(1.0f, 200);
                }
            }
            else
            {
                if (iconBorder != null)
                {
                    iconBorder.Background = new SolidColorBrush(Color.FromArgb("#F0F0F0"));
                    _ = iconBorder.ScaleToAsync(0.95f, 150, Easing.CubicOut).ContinueWith(_ =>
                        iconBorder.ScaleToAsync(1.0f, 150, Easing.CubicOut));
                }
                
                if (iconPath != null)
                {
                    await iconPath.FadeToAsync(0.7f, 150);
                    iconPath.Fill = Color.FromArgb("#666666");
                    await iconPath.FadeToAsync(1.0f, 150);
                }
            }
        }
        
        // Synchronous version for backward compatibility
        private void UpdateIconColor(Border? iconBorder, Microsoft.Maui.Controls.Shapes.Path? iconPath, bool isOn, Color onColor, string gradientStart, string gradientEnd)
        {
            _ = UpdateIconColorAsync(iconBorder, iconPath, isOn, onColor, gradientStart, gradientEnd);
        }

        // Air Conditioner
        private async void OnAirConditionerSwitchToggled(object sender, ToggledEventArgs e)
        {
            UpdateIconColor(AirConditionerIconBorder, AirConditionerIconPath, e.Value, Color.FromArgb("#512BD4"), "#E1BEE7", "#CE93D8");
            await ToggleExpandedSection(AirConditionerExpandedSection, e.Value);
        }

        private void OnAirConditionerTempChanged(object sender, ValueChangedEventArgs e)
        {
            var tempLabel = this.FindByName<Label>("AirConditionerTempValueLabel");
            if (tempLabel != null)
            {
                tempLabel.Text = $"{(int)e.NewValue}°C";
            }
        }

        // RGB Lights
        private async void OnRGBLightsSwitchToggled(object sender, ToggledEventArgs e)
        {
            UpdateIconColor(RGBLightsIconBorder, RGBLightsIconPath, e.Value, Color.FromArgb("#FF9800"), "#FFE0B2", "#FFCC80");
            await ToggleExpandedSection(RGBLightsExpandedSection, e.Value);
        }

        private void OnRGBLightsBrightnessChanged(object sender, ValueChangedEventArgs e)
        {
            var brightnessLabel = this.FindByName<Label>("RGBLightsBrightnessValueLabel");
            if (brightnessLabel != null)
            {
                brightnessLabel.Text = $"{(int)e.NewValue}%";
            }
        }

        // Table Fan
        private async void OnTableFanSwitchToggled(object sender, ToggledEventArgs e)
        {
            UpdateIconColor(TableFanIconBorder, TableFanIconPath, e.Value, Color.FromArgb("#2196F3"), "#E3F2FD", "#BBDEFB");
            await ToggleExpandedSection(TableFanExpandedSection, e.Value);
        }

        private void OnTableFanSpeedChanged(object sender, ValueChangedEventArgs e)
        {
            var speedLabel = this.FindByName<Label>("TableFanSpeedValueLabel");
            if (speedLabel != null)
            {
                speedLabel.Text = ((int)e.NewValue).ToString();
            }
        }

        // Pendant Lights
        private async void OnPendantLightsSwitchToggled(object sender, ToggledEventArgs e)
        {
            UpdateIconColor(PendantLightsIconBorder, PendantLightsIconPath, e.Value, Color.FromArgb("#FF9800"), "#FFE0B2", "#FFCC80");
            await ToggleExpandedSection(PendantLightsExpandedSection, e.Value);
        }

        private void OnPendantLightsBrightnessChanged(object sender, ValueChangedEventArgs e)
        {
            var brightnessLabel = this.FindByName<Label>("PendantLightsBrightnessValueLabel");
            if (brightnessLabel != null)
            {
                brightnessLabel.Text = $"{(int)e.NewValue}%";
            }
        }

        // Split AC
        private async void OnSplitACSwitchToggled(object sender, ToggledEventArgs e)
        {
            UpdateIconColor(SplitACIconBorder, SplitACIconPath, e.Value, Color.FromArgb("#512BD4"), "#E1BEE7", "#CE93D8");
            await ToggleExpandedSection(SplitACExpandedSection, e.Value);
        }

        private void OnSplitACTempChanged(object sender, ValueChangedEventArgs e)
        {
            var tempLabel = this.FindByName<Label>("SplitACTempValueLabel");
            if (tempLabel != null)
            {
                tempLabel.Text = $"{(int)e.NewValue}°C";
            }
        }

        // Smart TV (no expanded section, just icon color change)
        private void OnSmartTVSwitchToggled(object sender, ToggledEventArgs e)
        {
            // Teal/Green gradient for TV icon when ON
            UpdateIconColor(SmartTVIconBorder, SmartTVIconPath, e.Value, Color.FromArgb("#00BCD4"), "#B2EBF2", "#80DEEA");
        }

        // Tab Navigation
        private void OnCompactTabTapped(object sender, EventArgs e)
        {
            SwitchToTab("Compact");
        }

        private void OnSmartTabTapped(object sender, EventArgs e)
        {
            SwitchToTab("Smart");
        }

        private void SwitchToTab(string tabName)
        {
            var compactTab = this.FindByName<Border>("CompactTab");
            var smartTab = this.FindByName<Border>("SmartTab");
            var compactSection = this.FindByName<VerticalStackLayout>("CompactControlSection");
            var smartSection = this.FindByName<VerticalStackLayout>("SmartControlsSection");

            // Update tab appearance
            if (tabName == "Compact")
            {
                // Highlight Compact tab
                if (compactTab != null)
                {
                    compactTab.BackgroundColor = Colors.White;
                    compactTab.Shadow = new Shadow
                    {
                        Brush = Colors.Black,
                        Offset = new Point(0, 1),
                        Radius = 4,
                        Opacity = 0.08f
                    };
                    var compactLabel = this.FindByName<Label>("CompactTabLabel");
                    if (compactLabel == null && compactTab.Content is Label label)
                    {
                        compactLabel = label;
                    }
                    if (compactLabel != null)
                    {
                        compactLabel.FontAttributes = FontAttributes.Bold;
                        compactLabel.TextColor = Color.FromArgb("#512BD4");
                    }
                }

                // Unhighlight Smart tab
                if (smartTab != null)
                {
                    smartTab.BackgroundColor = Colors.Transparent;
                    smartTab.Shadow = null;
                    var smartLabel = this.FindByName<Label>("SmartTabLabel");
                    if (smartLabel == null && smartTab.Content is Label label)
                    {
                        smartLabel = label;
                    }
                    if (smartLabel != null)
                    {
                        smartLabel.FontAttributes = FontAttributes.None;
                        smartLabel.TextColor = Color.FromArgb("#333333");
                    }
                }

                // Show Compact section, hide Smart section
                if (compactSection != null) compactSection.IsVisible = true;
                if (smartSection != null) smartSection.IsVisible = false;
            }
            else if (tabName == "Smart")
            {
                // Highlight Smart tab
                if (smartTab != null)
                {
                    smartTab.BackgroundColor = Colors.White;
                    smartTab.Shadow = new Shadow
                    {
                        Brush = Colors.Black,
                        Offset = new Point(0, 1),
                        Radius = 4,
                        Opacity = 0.08f
                    };
                    var smartLabel = this.FindByName<Label>("SmartTabLabel");
                    if (smartLabel == null && smartTab.Content is Label label)
                    {
                        smartLabel = label;
                    }
                    if (smartLabel != null)
                    {
                        smartLabel.FontAttributes = FontAttributes.Bold;
                        smartLabel.TextColor = Color.FromArgb("#512BD4");
                    }
                }

                // Unhighlight Compact tab
                if (compactTab != null)
                {
                    compactTab.BackgroundColor = Colors.Transparent;
                    compactTab.Shadow = null;
                    var compactLabel = this.FindByName<Label>("CompactTabLabel");
                    if (compactLabel == null && compactTab.Content is Label label)
                    {
                        compactLabel = label;
                    }
                    if (compactLabel != null)
                    {
                        compactLabel.FontAttributes = FontAttributes.None;
                        compactLabel.TextColor = Color.FromArgb("#333333");
                    }
                }

                // Show Smart section, hide Compact section
                if (compactSection != null) compactSection.IsVisible = false;
                if (smartSection != null) smartSection.IsVisible = true;
            }
        }

        // Smart Controls event handlers (same as Compact)
        private void OnSmartRGBLightsSwitchToggled(object sender, ToggledEventArgs e)
        {
            var iconBorder = this.FindByName<Border>("SmartRGBLightsIconBorder");
            var iconPath = this.FindByName<Microsoft.Maui.Controls.Shapes.Path>("SmartRGBLightsIconPath");
            UpdateIconColor(iconBorder, iconPath, e.Value, Color.FromArgb("#FF9800"), "#FFE0B2", "#FFCC80");
        }

        private void OnSmartTableFanSwitchToggled(object sender, ToggledEventArgs e)
        {
            var iconBorder = this.FindByName<Border>("SmartTableFanIconBorder");
            var iconPath = this.FindByName<Microsoft.Maui.Controls.Shapes.Path>("SmartTableFanIconPath");
            UpdateIconColor(iconBorder, iconPath, e.Value, Color.FromArgb("#2196F3"), "#E3F2FD", "#BBDEFB");
        }

        private void OnSmartPendantLightsSwitchToggled(object sender, ToggledEventArgs e)
        {
            var iconBorder = this.FindByName<Border>("SmartPendantLightsIconBorder");
            var iconPath = this.FindByName<Microsoft.Maui.Controls.Shapes.Path>("SmartPendantLightsIconPath");
            UpdateIconColor(iconBorder, iconPath, e.Value, Color.FromArgb("#FF9800"), "#FFE0B2", "#FFCC80");
        }

        private void OnSmartSplitACSwitchToggled(object sender, ToggledEventArgs e)
        {
            var iconBorder = this.FindByName<Border>("SmartSplitACIconBorder");
            var iconPath = this.FindByName<Microsoft.Maui.Controls.Shapes.Path>("SmartSplitACIconPath");
            UpdateIconColor(iconBorder, iconPath, e.Value, Color.FromArgb("#512BD4"), "#E1BEE7", "#CE93D8");
        }

        private void OnSmartTVSwitchToggled2(object sender, ToggledEventArgs e)
        {
            var iconBorder = this.FindByName<Border>("SmartTVIconBorder2");
            var iconPath = this.FindByName<Microsoft.Maui.Controls.Shapes.Path>("SmartTVIconPath2");
            UpdateIconColor(iconBorder, iconPath, e.Value, Color.FromArgb("#00BCD4"), "#B2EBF2", "#80DEEA");
        }
    }
}
