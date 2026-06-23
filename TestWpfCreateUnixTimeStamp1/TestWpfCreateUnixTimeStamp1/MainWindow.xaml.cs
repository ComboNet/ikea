using System;
using System.Windows;

namespace TestWpfCreateUnixTimeStamp1
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Int32 unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            // DateTime now = DateTime.UtcNow;
            listBox1.Items.Add(unixTimestamp.ToString());
        }
    }
}
