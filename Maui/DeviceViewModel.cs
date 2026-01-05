using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Maui
{
    public class DeviceViewModel : INotifyPropertyChanged
    {
        private bool _isOn;
        private double _brightness = 80;
        private double _temperature = 22;
        private int _speedLevel = 2;
        private Color _selectedColor = Colors.Purple;

        public string Name { get; set; }
        public string Location { get; set; }
        public string DeviceType { get; set; } // "light", "fan", "ac", "tv"

        public bool IsOn
        {
            get => _isOn;
            set
            {
                if (_isOn != value)
                {
                    _isOn = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Brightness
        {
            get => _brightness;
            set
            {
                if (_brightness != value)
                {
                    _brightness = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Temperature
        {
            get => _temperature;
            set
            {
                if (_temperature != value)
                {
                    _temperature = value;
                    OnPropertyChanged();
                }
            }
        }

        public int SpeedLevel
        {
            get => _speedLevel;
            set
            {
                if (_speedLevel != value)
                {
                    _speedLevel = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SpeedLevelText));
                }
            }
        }

        public string SpeedLevelText => $"{SpeedLevel}/5";

        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                if (_selectedColor != value)
                {
                    _selectedColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
