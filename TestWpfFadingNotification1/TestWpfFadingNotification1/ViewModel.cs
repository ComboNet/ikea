using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestWpfFadingNotification1
{
    [ObservableObject]
    public partial class ViewModel
    {
        [ObservableProperty] public bool _isVisible = false;
        public ICommand TrueCommand => new RelayCommand(() => { IsVisible = true; });
        public ICommand FalseCommand => new RelayCommand(() => { IsVisible = false; });
    }
}
