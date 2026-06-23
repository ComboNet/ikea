using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace TestWpfBlinking1
{
    [ObservableObject]
    public partial class ViewModel
    {
        [ObservableProperty] public bool _isEnabled = false;

        public ICommand EnableCommand => new RelayCommand(() => { IsEnabled = true; });
        public ICommand DisableCommand => new RelayCommand(() => { IsEnabled = false; });
    }
}
