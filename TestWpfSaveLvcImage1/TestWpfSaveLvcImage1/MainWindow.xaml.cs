using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.SKCharts;
using LiveChartsCore.SkiaSharpView.WPF;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.IO;
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

namespace TestWpfSaveLvcImage1;

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
    private Random r=new();
    [ObservableProperty] public ObservableCollection<ISeries> _series = new();
    [ObservableProperty] private ObservableCollection<double> _values = new();
    public MainWindowViewModel()
    {
        Values = new ObservableCollection<double>(Enumerable.Range(0, 30).Select(i => (double)r.NextDouble()));
        Series.Clear();
        Series.Add(new LineSeries<double>()
        {
            Values = Values,
            Fill = null,
            LineSmoothness = 0,
        });
    }
    public ICommand SaveCommand => new RelayCommand<object>(param =>
    {
        if (param is LiveChartsCore.SkiaSharpView.WPF.CartesianChart chart)
        {
            if (chart != null)
            {
                var skChart = new SKCartesianChart(chart) { Width = 1024, Height = 576, };
                string fileName = "chart.png";
                skChart.SaveImage(System.IO.Path.Combine(Directory.GetCurrentDirectory(), fileName));
            }
        }
    });
     
}