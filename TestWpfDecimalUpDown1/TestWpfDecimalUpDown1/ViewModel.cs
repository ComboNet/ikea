using CommunityToolkit.Mvvm.ComponentModel;
using MahApps.Metro.Controls;

namespace TestWpfDecimalUpDown1
{
    [ObservableObject]
    public partial class ViewModel
    {
        [ObservableProperty] public double value = 34.1;
        [ObservableProperty] public NumericInput _numericInput = NumericInput.Numbers;
        [ObservableProperty] public double _interval = 1;
    }
}


