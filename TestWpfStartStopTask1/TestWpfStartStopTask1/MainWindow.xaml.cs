using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace TestWpfStartStopTask1
{
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
        private Task _task;
        private CancellationTokenSource _cts = new();
        private CancellationToken _ctk;

        [ObservableProperty] private ObservableCollection<string> _items = new();
        [ObservableProperty] private string _text = "Play";
        [ObservableProperty] private bool _isPlaying = false;

        public MainWindowViewModel() => _ctk = _cts.Token;

        public ICommand PlayCommand => new RelayCommand(() =>
        {
            IsPlaying = !IsPlaying;
            if(IsPlaying)
            {
                Text = "Stop";
                int i = 0;
                Items.Clear();
                _task = Task.Run(() =>
                {
                    while(true)
                    {
                        if (_ctk.IsCancellationRequested)
                            return;
                        App.Current.Dispatcher.Invoke(() => { Items.Add($"Working...{i}"); });
                        Thread.Sleep(500);
                        i++;
                    }
                });
            }
            else
            {
                Text = "Start";
                _cts.Cancel();
                _task.Wait();
            }
        });
    }
}