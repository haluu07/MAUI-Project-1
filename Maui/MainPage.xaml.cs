using Microsoft.Maui.Controls;

namespace Maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            UpdateDarkModeIcon();
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
    }
}
