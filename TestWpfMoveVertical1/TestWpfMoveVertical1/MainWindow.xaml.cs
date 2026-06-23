using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel;
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
using System.Windows.Threading;
using TestWpfMoveVertical1.Model;
using TestWpfMoveVertical1.Service;

namespace TestWpfMoveVertical1;

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
    private bool _isLoaded = false;
    private DispatcherTimer _timer = new();
    private CancellationTokenSource cts = new();

    #region Properties

    public Dictionary<int, string> LoopModes { get; } = new()
    {
        {0, "Stop" },
        {1, "Continue with last setpoint" },
        {2, "Repeat the loop" },
    };
    public int SelectedLoopMode { get; set; } = 0;

    private ObservableCollection<MyTimeSpanPoint> _points;
    public ObservableCollection<MyTimeSpanPoint> Points
    {
        get { return _points; }
        set
        {
            _points = value;
            OnPropertyChanged(nameof(Points));
            Values = new ObservableCollection<TimeSpanPoint>(Points.Select(p => new TimeSpanPoint(p.TimeSpan, p.Value)));
            RampIndexes = LoopStartIndexes = new ObservableCollection<int>(Values.ToList().Select((point, index) => index + 1).ToList());
        }
    }
    [ObservableProperty] private ObservableCollection<TimeSpanPoint> _values = new();
    private int _selectedPointIndex = 0;
    public int SelectedPointIndex
    {
        get { return _selectedPointIndex; }
        set
        {
            if (_selectedPointIndex == value) return;
            _selectedPointIndex = value;
            OnPropertyChanged(nameof(SelectedPointIndex));
            if (_isLoaded)
            {
                if (SelectedPointIndex.Equals(-1)) return;
                var values = MainService.TimeSpanPoint2TimeSerie(
                    new ObservableCollection<TimeSpanPoint>(Points.Select(p =>
                        new TimeSpanPoint(p.TimeSpan, p.Value))));
                if (ProfileSeries != null && Points.Count > 0)
                {
                    ProfileSeries.FirstOrDefault(s => s.Name.Equals("Mark")).Values =
                        new[] { values.ToArray()[SelectedPointIndex] };
                    Sections[2].Xi = Sections[2].Xj = null;
                    // ChartOption.Sections[1].Xi = ChartOption.Sections[1].Xj = values.ToArray()[SelectedPointIndex].TimeSpan.Ticks;
                    /* LoopIndexStatus = (LoopStartIndexes is not null && LoopStartIndexes.Count() > 0)
                        ? (SelectedPointIndex + 1).ToString() + "/" + LoopStartIndexes.Last()
                        : string.Empty;*/
                }
            }
        }
    }
    [ObservableProperty] private ObservableCollection<int> _loopStartIndexes;
    private int _selectedLoopStartIndex;
    public int SelectedLoopStartIndex
    {
        get { return _selectedLoopStartIndex; }
        set
        {
            if (_selectedLoopStartIndex == value) return;
            _selectedLoopStartIndex = value;
            OnPropertyChanged(nameof(SelectedLoopStartIndex));
            var values = MainService.TimeSpanPoint2TimeSerie(
                    new ObservableCollection<TimeSpanPoint>(Points.Select(p =>
                        new TimeSpanPoint(p.TimeSpan, p.Value))));
            Sections[0].Xi = Sections[0].Xj = values.ToArray()[SelectedLoopStartIndex - 1].TimeSpan.Ticks;
        }
    }
    [ObservableProperty] private ObservableCollection<int> _rampIndexes;
    private int _selectedRampIndex;
    public int SelectedRampIndex
    {
        get { return _selectedRampIndex; }
        set
        {
            _selectedRampIndex = value;
            OnPropertyChanged(nameof(SelectedRampIndex));
        }
    }
    [ObservableProperty]
    private ObservableCollection<ISeries> _profileSeries = new()
    {
        new StepLineSeries<TimeSpanPoint>
        {
            Name = "Profile",
            Values = new ObservableCollection<TimeSpanPoint>(),
            GeometrySize = 16,
            ZIndex = 0,
            Fill=null,
        },
        new ScatterSeries<TimeSpanPoint>()
        {
            Name = "Mark",
            Values = new ObservableCollection<TimeSpanPoint>(),
            GeometrySize = 18,
            Fill = new SolidColorPaint(SKColors.Red),
            Stroke = new SolidColorPaint(SKColors.Red) { StrokeThickness = 2 },
            ZIndex = 1,
        },
    };
    [ObservableProperty]
    ObservableCollection<RectangularSection> _sections = new()
    {
        // Loop start index
        new RectangularSection()
        {
            Xi  = null,
            Xj = null,
            Stroke = new SolidColorPaint
            {
                Color = MainService.HexToSKColor(App.Current.Resources["L"].ToString()),
                StrokeThickness = 2
            },
            Fill = null
        },
        // Ramp index
        new RectangularSection()
        {
            Xi  = null,
            Xj = null,
            Stroke = new SolidColorPaint
            {
                Color = MainService.HexToSKColor(App.Current.Resources["J"].ToString()),
                StrokeThickness = 2
            },
            LabelPaint = new SolidColorPaint(SKColors.Black),
            LabelSize = 16,
            Fill = null,
            ZIndex = 1
        },
        new RectangularSection()
        {
            Xi  = null,
            Xj = null,
            Fill = null
        }
    };
    [ObservableProperty] private ChartOption _chartOption;
    [ObservableProperty] private string _timeUpdate = string.Empty;
    [ObservableProperty] private bool _isRunning = false;
    [ObservableProperty] private string _btnLabel = "Play"; 
    #endregion

    public MainWindowViewModel()
    {
        _isLoaded = false;
        Points = new()
        {
            new MyTimeSpanPoint(TimeSpan.FromMinutes(1), 10),
            new MyTimeSpanPoint(TimeSpan.FromMinutes(2), 20),
            /*new MyTimeSpanPoint(TimeSpan.FromMinutes(3), 15),
            new MyTimeSpanPoint(TimeSpan.FromMinutes(4), 25),*/
        };
        ChartOption = AppData.ChartOption;
        ChartOption.YAxes[0].MinLimit = Points.Min(p => p.Value) - 5;
        ChartOption.YAxes[0].MaxLimit = Points.Max(p => p.Value) + 5;
        var values = MainService.TimeSpanPoint2TimeSerie(
                     new ObservableCollection<TimeSpanPoint>(Points.Select(p =>
                         new TimeSpanPoint(p.TimeSpan, p.Value))));
        ProfileSeries[0].Values = values;
        _timer.Interval = TimeSpan.FromSeconds(0.5);
        _isLoaded = true;
        SelectedPointIndex = 0;
    }

    public ICommand OnDataPointerDownCommand => new RelayCommand<IEnumerable<ChartPoint>>(points =>
    {
        var chartPoint = points.FirstOrDefault();
        if (chartPoint != null)
            SelectedPointIndex = chartPoint.Index;
    });

    [RelayCommand]
    public void UpdateRamp(int rampIndex)
    {
        var values = MainService.TimeSpanPoint2TimeSerie(
                    new ObservableCollection<TimeSpanPoint>(Points.Select(p =>
                        new TimeSpanPoint(p.TimeSpan, p.Value))));
        Sections[1].Xi = Sections[1].Xj = values.ToArray()[rampIndex - 1].TimeSpan.Ticks;
    }

    public ICommand PlayCommand => new RelayCommand(() => 
    {
        IsRunning = !IsRunning;
        BtnLabel = IsRunning ? "Stop" : "Play";
        var values = MainService.TimeSpanPoint2TimeSerie(
                       new ObservableCollection<TimeSpanPoint>(Points.Select(p =>
                           new TimeSpanPoint(p.TimeSpan, p.Value))));
        if (IsRunning)
        {
            var position = values.ToArray()[0].TimeSpan.Ticks;
            int index = 0;
            double value = 0.0;
            _timer.Tick += (s, e) =>
            {
                if (position.Equals(values.ToArray()[values.Count - 1].TimeSpan.Ticks))
                    _timer.Stop();
                Sections[1].Xi = Sections[1].Xj = position;
                if (position.Equals(values.ToArray()[index].TimeSpan.Ticks))
                {
                    value = values[index].Value.Value;
                    index++;
                }
                TimeUpdate = new TimeSpan(position).ToString(@"mm\:ss") + "," + value.ToString("N2");
                Sections[1].Label = TimeUpdate;
                position += TimeSpan.FromSeconds(0.5).Ticks;
            };
            _timer.Start();
        }
        else
        {
            _timer.Stop();
            var position = values.ToArray()[0].TimeSpan.Ticks;
            double value = values.ToArray()[0].Value.Value;
            Sections[1].Xi = Sections[1].Xj = position;
            TimeUpdate = new TimeSpan(position).ToString(@"mm\:ss") + "," + value.ToString("N2");
        }
    });
}

[ObservableObject]
public partial class MyTimeSpanPoint
{
    #region Properties

    [ObservableProperty] private TimeSpan _timeSpan;
    [ObservableProperty] private double _value;

    #endregion

    public MyTimeSpanPoint(TimeSpan timeSpan, double value)
    {
        this.TimeSpan = timeSpan;
        this.Value = value;
    }
}