using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
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

namespace TestWpfLvcYellowBorder1;

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
    [ObservableProperty] public ObservableCollection<ISeries> _series = new();
    [ObservableProperty]
    public ObservableCollection<ICartesianAxis> _xAxes = new()
    {
        new DateTimeAxis(TimeSpan.FromSeconds(1), date => date.ToString("HH:mm:ss"))
    };
    [ObservableProperty] public ObservableCollection<RectangularSection> _sections = new();

    public MainWindowViewModel()
    {
        var now = DateTime.Now;
        Sections.Clear();
        Sections.Add(new RectangularSection()
        {
            Yi = 25,
            Yj = 25,
            Fill = null,
            Stroke = new SolidColorPaint(SKColors.Yellow)
        });
        Series.Clear();
        Series.Add(new LineSeries<DateTimePoint>()
        {
            Values = Enumerable.Range(-30, 30).Select(v => new DateTimePoint(now.AddSeconds(v), new Random().Next(0, 100))),
            GeometryFill = null,
            GeometrySize = 0,
            Fill = null,
            LineSmoothness = 0,
        });
    }
}