using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TestWpfTabFlipView1.Page;

namespace TestWpfTabFlipView1
{
    [ObservableObject]
    public partial class ViewModel
    {
        public ViewModel()
        {
            ItemsSource.Clear();
            ItemsSource.Add(page1);
            ItemsSource.Add(page2);
        }

        #region Fields
        public readonly Page1 page1 = new();
        public readonly Page2 page2 = new();
        #endregion

        #region Properties
        [ObservableProperty] public int _selectedIndex;
        [ObservableProperty] public ObservableCollection<System.Windows.Controls.Page> _itemsSource = new(); 
        #endregion
    }
}
