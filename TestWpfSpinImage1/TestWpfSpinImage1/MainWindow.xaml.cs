using System.ComponentModel;
using System.Runtime.CompilerServices;
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

namespace TestWpfSpinImage1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = new MainViewModel();
    }
}

public class MainViewModel : INotifyPropertyChanged
{
    private double _rotationAngle;
    public double RotationAngle
    {
        get => _rotationAngle;
        set { _rotationAngle = value; OnPropertyChanged(); }
    }

    private double _rotationSpeed = 2; // degrees per tick
    public double RotationSpeed
    {
        get => _rotationSpeed;
        set { _rotationSpeed = value; OnPropertyChanged(); }
    }

    private readonly DispatcherTimer _timer;

    public MainViewModel()
    {
        _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(30) };
        _timer.Tick += (s, e) =>
        {
            RotationAngle += RotationSpeed;
            if (RotationAngle >= 360) RotationAngle = 0;
        };
        _timer.Start();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}