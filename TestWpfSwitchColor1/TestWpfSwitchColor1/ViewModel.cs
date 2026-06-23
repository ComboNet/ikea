using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace TestWpfSwitchColor1;

[ObservableObject]
public partial class ViewModel
{
    #region Properties
    [ObservableProperty] public string _background;
    [ObservableProperty] public int _alm_sts;
    [ObservableProperty] public int _sts;
    #endregion

    #region Commands
    public ICommand RedBlinkCommand => new RelayCommand(async () => { Alm_sts = 1; Background = "Red"; });
    public ICommand GreenCommand => new RelayCommand(async () => { Alm_sts = 0; Sts = 1; Background = "Green"; });
    public ICommand YellowCommand => new RelayCommand(async () => { Alm_sts = 0; Sts = 2; Background = "Yellow"; });
    public ICommand NullCommand => new RelayCommand(async () => { Alm_sts = 0; Sts = 0; Background = "{x:Null}"; }); 
    #endregion
}
