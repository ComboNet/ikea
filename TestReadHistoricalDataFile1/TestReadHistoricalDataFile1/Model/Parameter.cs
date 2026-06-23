using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace TestReadHistoricalDataFile1.Models
{
    [ObservableObject]
    public partial class Parameter
    {
        [ObservableProperty] public int _id;
        [ObservableProperty] public string _name;
        [ObservableProperty] public string _tag;
        [ObservableProperty] public string _fullName;
        [ObservableProperty] public string _category;
        [ObservableProperty] public List<string> _modes;
        [ObservableProperty] public List<string> _loopModes;
        [ObservableProperty] public List<string> _rotations;
        [ObservableProperty] public string _selectedMode;
        [ObservableProperty] public string _selectedLoopMode;
        [ObservableProperty] public string _selectedRotation;
        [ObservableProperty] public string _unit;
        [ObservableProperty] public double _currentValue;
        [ObservableProperty] public double _setValue;
        // [ObservableProperty] public string _mode;
        [ObservableProperty] public bool _alarmEnabled;
        [ObservableProperty] public double _alarmLo;
        [ObservableProperty] public double _alarmHi;
        [ObservableProperty] public string _alarmStatus;
        [ObservableProperty] public int _error;
        [ObservableProperty] public bool _isPopup;
        [ObservableProperty] public bool _isEnable;
        [ObservableProperty] public bool _isVisible;
        [ObservableProperty] public int _no;
        [ObservableProperty] public string _color;
        [ObservableProperty] public string _axisPosition;
        [ObservableProperty] public bool _isAutoScaling;
        [ObservableProperty] public double? _minValue;
        [ObservableProperty] public double? _maxValue;
        [ObservableProperty] public List<ChartDataPoint> _values;
        [ObservableProperty] public int _flowValue;
        [ObservableProperty] public double _offset;
    }
}
