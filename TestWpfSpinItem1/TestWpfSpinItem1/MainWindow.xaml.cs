using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestWpfSpinItem1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = new MainWindowViewModel();
    }
}

[ObservableObject]
public partial class MainWindowViewModel
{
    [ObservableProperty] public ObservableCollection<Item> _items = new();
    public MainWindowViewModel()
    {
        Items.Clear();
        for(int i=0; i<3; i++)      
            Items.Add(new Item(i, $"Item {i}"));
    }
}

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Item(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
}