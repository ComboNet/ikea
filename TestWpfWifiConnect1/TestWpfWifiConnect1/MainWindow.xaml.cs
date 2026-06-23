using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestWpfWifiConnect1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    [ObservableObject]
    public partial class MainViewModel
    {
        [ObservableProperty] public string _message;
        SimpleTcpClient client = new SimpleTcpClient();

        public MainViewModel()
        {
            try
            {
                client.StringEncoder = Encoding.UTF8;
                client.DataReceived += Client_DataReceived;
                client.Connect("192.168.56.36", 40999);
                // client.Connect("192.168.10.250", 40999);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Client_DataReceived(object? sender, SimpleTCP.Message e)
        {
            Message = e.MessageString;
        }

        public ICommand SubOnCommand => new RelayCommand(() =>
        {
            try
            {
                client.WriteLineAndGetReply("{\"SUBS\":{ \"sts\": 1}}" + "\r\n", TimeSpan.FromSeconds(0.5));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });

        public ICommand SubOffCommand => new RelayCommand(() =>
        {
            try
            {
                client.WriteLineAndGetReply("{\"SUBS\":{ \"sts\": 1}}" + "\r\n", TimeSpan.FromSeconds(0.5));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });

        public ICommand OnCommand => new RelayCommand(() =>
        {
            try
            {
                client.WriteLineAndGetReply("{\"ACID\":{ \"sts\": 1}}" + "\r\n", TimeSpan.FromSeconds(0.5));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });

        public ICommand OffCommand => new RelayCommand(() =>
        {
            try
            {
                client.WriteLineAndGetReply("{\"ACID\":{ \"sts\": 0}}" + "\r\n", TimeSpan.FromSeconds(0.5));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });
    }
}
