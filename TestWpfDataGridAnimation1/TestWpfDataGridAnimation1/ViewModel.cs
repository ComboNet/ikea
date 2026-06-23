using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TestWpfDataGridAnimation1
{
    [ObservableObject]
    public partial class ViewModel
    {
        public ViewModel()
        {
            Items = new ObservableCollection<Data>()
            {
                new Data() { Id = 1, Name = "Hello" },
                new Data() { Id = 2, Name = "World" },
                new Data() { Id = 3, Name = "Narongsak" }
            };
        }
        [ObservableProperty] public ObservableCollection<Data> _items = new();
        public ICommand OkCommand => new RelayCommand(() =>
        {

        });
    }

    public class Data
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
