using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace TestWpfMoveTimeSpan1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Data> datas;
        TimeSpan initTimeSpan = TimeSpan.Zero;
        int x = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            datas = new List<Data>()
            {
                new Data() { TimeSpan = TimeSpan.FromSeconds(2), Value = 5 },
                new Data() { TimeSpan = TimeSpan.FromSeconds(4), Value = 3 },
                new Data() { TimeSpan = TimeSpan.FromSeconds(6), Value =2 },
                new Data() { TimeSpan = TimeSpan.FromSeconds(8), Value = 6 },
                new Data() { TimeSpan = TimeSpan.FromSeconds(10), Value = 8 },
            };
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.2);
            timer.Tick += (s, e) =>
            {
                if (datas.Exists(d => d.TimeSpan.Equals(initTimeSpan)))
                {
                    string strTimeSpan = datas.FirstOrDefault(d => d.TimeSpan.Equals(initTimeSpan)).TimeSpan.ToString();
                    string strValue = datas.FirstOrDefault(d => d.TimeSpan.Equals(initTimeSpan)).Value.ToString();
                    listBox1.Items.Add(strTimeSpan+ "," + strValue);
                    if(datas.Last().TimeSpan.Equals(initTimeSpan))
                    {
                        MessageBox.Show("Finish.");
                        timer.Stop();
                    }
                    
                }
                line1.X1 = line1.X2 = x;
                textBox1.Text = initTimeSpan.ToString();
                initTimeSpan += TimeSpan.FromSeconds(0.2);
                x += 10;
            };
            timer.Start();
        }
    }

    public class Data
    {
        public TimeSpan TimeSpan { get; set; }
        public int Value { get; set; }
    }
}
