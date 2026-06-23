using System.IO.Ports;
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

namespace TestWpfReadExperiment1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        SerialPort serialPort;
        StringBuilder block = new();
        StringBuilder data = new();
        StringBuilder sb = new();
        String msg = string.Empty;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                serialPort = new SerialPort("COM10", 115200, Parity.None, 8);
                serialPort.DataReceived += SerialPort_DataReceived;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Current.Shutdown();
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            msg = serialPort.ReadExisting();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Clear(); 
            try
            {
                string compareString = String.Empty;
                if (!serialPort.IsOpen) serialPort.Open();
                msg = String.Empty;
                data.Clear();
                serialPort.Write("{\"FILES\":{\"dest\":0,\"name\":\"experiments.csv\",\"size\":{},\"type\":2}}\r\n");
                int count = 0;
                while (true)
                {
                    if(!String.IsNullOrEmpty(msg))
                    {
                        if (!compareString.Equals(msg))
                        {
                            count = 0;
                            App.Current.Dispatcher.Invoke(() => { listBox1.Items.Add(msg); });
                            if (data.ToString().Contains("$$$$")) break;
                            data.Append(msg);
                            compareString = msg;
                        }
                        else 
                            if (count++ > 3) break;
                    }
                    Thread.Sleep(500);
                }
                if (serialPort.IsOpen) serialPort.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}