using System.Net;
using System.Net.Sockets;
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

namespace TestWpfUDPClient1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int port = 40999;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new UdpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("192.168.56.69"), 11000); // endpoint where server is listening
            client.Connect(ep);

            // send data
            client.Send(new byte[] { 1, 2, 3, 4, 5 }, 5);

            // then receive data
            var receivedData = client.Receive(ref ep);

            Console.Write("receive data from " + ep.ToString());

            Console.Read();


            /*using(UdpClient client = new UdpClient()) 
            {
                try
                {
                    client.Connect("192.168.56.69", 40999);
                    Byte[] bytes = Encoding.ASCII.GetBytes("{\"HABITAT\":{}}\r\n");
                    client.Send(bytes, bytes.Length);
                    IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    Byte[] receives = client.Receive(ref ipEndPoint);
                    string returnData = Encoding.ASCII.GetString(receives);
                    *//*StringBuilder sb = new();
                    for (int i = 0; i < bytes.Length; i++)
                        sb.Append(bytes[i].ToString("x2"));*//*
                    listBox1.Items.Add(returnData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }*/
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}