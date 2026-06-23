using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestWpfSwitchColorAnimation1
{
    [ObservableObject]
    public partial class ViewModel
    {
        #region Properties
        [ObservableProperty] public ObservableCollection<string> _items = new();
        [ObservableProperty] public bool _isTrigger = false;
        #endregion

        public ICommand OkCommand => new RelayCommand(() =>
        {
            IsTrigger = true;   
        });
    }
}
