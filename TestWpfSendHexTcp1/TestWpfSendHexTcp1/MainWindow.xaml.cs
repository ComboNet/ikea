using Newtonsoft.Json;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Threading;

namespace TestWpfSendHexTcp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SimpleTcpClient client;
        StringBuilder stringBuilder = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        StringBuilder sbAppSync = new StringBuilder();
        string file = @"C:\txt\data.txt";
        int iCount = 0;
        string[] items;
        FILES_REPLY fileReply = new();
        string line = String.Empty;
        public List<HISTORY_DATA> data = new List<HISTORY_DATA>();
        string msg = String.Empty;
        DispatcherTimer timer;
        List<string> lines = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            client = new SimpleTcpClient().Connect("192.168.56.69", 40999);
            client.DataReceived += Client_DataReceived;
            if (File.Exists(file))
                File.Delete(file);
            listBox1.Items.Clear();
        }

        private void Client_DataReceived(object? sender, Message e)
        {
            msg = e.MessageString;
            /*listBox1.Dispatcher.Invoke(() =>
            {
                listBox1.Items.Add(msg);
            });*/

            // Historical detail 
            if (msg.Contains("success") && msg.Contains("FILES"))
                fileReply = JsonConvert.DeserializeObject<FILES_REPLY>(msg);
            else
            {
                sb2.Append(msg);
                stringBuilder.Append(msg);
            }
        }

        private void Apply(string strCommand, bool bReply)
        {
            if (bReply)
                client.WriteLineAndGetReply(strCommand, TimeSpan.FromSeconds(6));
            else
                client.WriteLine(strCommand);
        }

        private bool CheckSum(string s)
        {
            string checksum = s.Substring(s.IndexOf("$$$$") - 4, 4);
            bool b = (checksum.Equals(CalculateCheckSum(s.Substring(0, s.IndexOf(checksum))))) ? true : false;
            return b;
        }

        public async Task LoadAppSyncAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                listBox1.Dispatcher.Invoke(() =>
                {
                    var sb = new StringBuilder();
                    listBox1.Items.Clear();
                    Apply("{\"SUBS\":{ \"sts\": 0}}\r\n", true);
                    Apply("{\"APP_SYNC\":{}}\r\n", false);
                    iCount = 0;
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += (s, e) =>
                    {
                        if (listBox1.Items.Count > 0)
                            if (iCount == listBox1.Items.Count)
                            {
                                timer.Stop();
                                if (listBox1.Items.Count > 0)
                                {
                                    foreach (var item in listBox1.Items)
                                        if (!item.ToString().Contains("success"))
                                            if (item.ToString().Contains("APP_SYNC"))
                                                sb.Append(item.ToString());
                                    File.WriteAllText(@"C:\txt\appsync.txt", sb.ToString());
                                }
                            }
                            else
                                iCount = listBox1.Items.Count;
                    };
                    timer.Start();
                });
            });
        }

        public async Task LoadUsersAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                listBox1.Dispatcher.Invoke(() =>
                {
                    var sb = new StringBuilder();
                    listBox1.Items.Clear();
                    Apply("{\"SUBS\":{ \"sts\": 0}}\r\n", true);
                    Apply("{\"FILES\":{\"dest\":0,\"name\":\"users.csv\",\"size\":{},\"type\":0}}\r\n", false);
                    iCount = 0;
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += (s, e) =>
                    {
                        if (listBox1.Items.Count > 0)
                            if (iCount == listBox1.Items.Count)
                            {
                                // timer.Stop();
                                if (listBox1.Items.Count > 0)
                                {
                                    foreach (var item in listBox1.Items)
                                        if (!item.ToString().Contains("success"))
                                        {
                                            sb.Append(item.ToString());
                                            if (CheckSum(item.ToString()))
                                                timer.Stop();
                                        }
                                    File.WriteAllText(@"C:\txt\user.txt", sb.ToString());
                                }
                            }
                            else
                                iCount = listBox1.Items.Count;
                    };
                    timer.Start();
                });
            });
        }

        public async Task LoadRecipeAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                listBox1.Dispatcher.Invoke(() =>
                {
                    var sb = new StringBuilder();
                    listBox1.Items.Clear();
                    Apply("{\"SUBS\":{ \"sts\": 0}}\r\n", true);
                    Apply("{\"FILES\":{\"dest\":0,\"name\":\"recipes.csv\",\"size\":{},\"type\":1}}\r\n", false);
                    iCount = 0;
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += (s, e) =>
                    {
                        if (listBox1.Items.Count > 0)
                            if (iCount == listBox1.Items.Count)
                            {
                                if (listBox1.Items.Count > 0)
                                {
                                    foreach (var item in listBox1.Items)
                                        if (!item.ToString().Contains("success"))
                                        {
                                            timer.Stop();
                                            sb.Append(item.ToString());
                                        }
                                    File.WriteAllText(@"C:\txt\recipe.txt", sb.ToString());
                                }
                            }
                            else
                                iCount = listBox1.Items.Count;
                    };
                    timer.Start();
                });
            });
        }

        public async Task LoadExperimentAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                listBox1.Dispatcher.Invoke(() =>
                {
                    var sb = new StringBuilder();
                    listBox1.Items.Clear();
                    Apply("{\"SUBS\":{ \"sts\": 0}}\r\n", true);
                    Apply("{\"FILES\":{\"dest\":0,\"name\":\"experiments.csv\",\"size\":{},\"type\":2}}\r\n", false);
                    iCount = 0;
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += (s, e) =>
                    {
                        if (listBox1.Items.Count > 0)
                            if (iCount == listBox1.Items.Count)
                            {
                                if (listBox1.Items.Count > 0)
                                {
                                    foreach (var item in listBox1.Items)
                                        if (!item.ToString().Contains("success"))
                                        {
                                            sb.Append(item.ToString());
                                            if (item.ToString().Contains("$$$$"))
                                                timer.Stop();
                                        }
                                    File.WriteAllText(@"C:\txt\experiment.txt", sb.ToString());
                                }
                            }
                            else
                                iCount = listBox1.Items.Count;
                    };
                    timer.Start();
                });
            });
        }

        private async void button3_Click(object sender, RoutedEventArgs e)
        {
            await LoadAppSyncAsync();
        }

        private async void button4_Click(object sender, RoutedEventArgs e)
        {
            await LoadUsersAsync();
        }

        private async void button5_Click(object sender, RoutedEventArgs e)
        {
            await LoadRecipeAsync();
        }

        private async void button6_Click(object sender, RoutedEventArgs e)
        {
            await LoadExperimentAsync();
        }

        private string CalculateCheckSum(string str)
        {
            return str.Sum(i => Encoding.UTF8.GetBytes(i.ToString())[0]).ToString("X");
        }

        private async Task LoadDataAsync(string strCommand)
        {
            await Task.Factory.StartNew(() =>
            {
                int iCount = 0;
                sb2.Clear();
                client.WriteLineAndGetReply(strCommand, TimeSpan.FromSeconds(3));
                if (sb2.Length == 0) return;
                line = sb2.ToString().Split(Environment.NewLine).LastOrDefault();
                // line = sb2.ToString().Split(new[] { '\r', '\n' }).LastOrDefault();
                if (line.Contains("$$$$"))
                {
                    string checksum = line.Substring(line.IndexOf("$$$$") - 4, 4);
                    if (checksum.Equals(CheckSum(sb2.ToString().Substring(0, sb2.ToString().IndexOf(checksum)))))
                        return;
                }
                Thread.Sleep(300);
            });
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            stringBuilder.Clear();
            int count = 0;
            // Work very well
            listBox1.Items.Add(count);
            await LoadDataAsync("{ \"FILES\":{ \"dest\":0,\"name\":\"TestMotor_1695867148.csv\",\"size\":{ },\"type\":3} }\r\n");
            while (true)
            {
                count++;
                listBox1.Items.Add(count);
                await LoadDataAsync("\u0006");
                if (sb2.Length == 0) break;
            }
            await LoadDataAsync("\u0021");
            MessageBox.Show("Process finish.");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // IEnumerable<string> lines = System.IO.File.ReadAllLines(file);

            /*var ls = File.ReadAllLines(file);
            StringBuilder sb = new StringBuilder();
            foreach(var l in ls)
                sb.AppendLine(l);
            string[] delim = { Environment.NewLine, "\n" }; 
            string[] lines = sb.ToString().Split(delim, StringSplitOptions.None);*/

            StringBuilder sb = new StringBuilder();
            string[] delim = { Environment.NewLine, "\n" };
            string[] lines = stringBuilder.ToString().Split(delim, StringSplitOptions.None);
            listBox1.Items.Clear();
            if (lines.Count() > 0)
            {
                sb.Clear();
                foreach (string line in lines)
                {
                    var str = line.Trim();
                    if (line.Contains("$$$$"))
                        str = line.Replace(String.Concat(line.Substring(line.IndexOf("$$$$") - 4, 4), "$$$$"), "").Trim();
                    if (!line.Contains("TimeStamp"))
                    {
                        string[] ls = str.Split(",");
                        if (ls.Length == 21)
                        {
                            data.Add(new HISTORY_DATA()
                            {
                                TimeStamp = ls[0],
                                STIRRER = ls[1],
                                TEMP = ls[2],
                                PH = ls[3],
                                DO = ls[4],
                                ACID = ls[5],
                                BASE = ls[6],
                                AFOAM = ls[7],
                                LEVEL = ls[8],
                                FOAM_LIMIT = ls[9],
                                LEVEL_LIMIT = ls[10],
                                AIR = ls[11],
                                O2 = ls[12],
                                N2 = ls[13],
                                CO2 = ls[14],
                                SUBS_A = ls[15],
                                EC_PROBE = ls[16],
                                CO2_PROBE = ls[17],
                                TURBIDITY_PROBE = ls[18],
                                LIGHT = ls[19],
                                MARKER = ls[20],
                            });
                        }
                    }
                    sb.AppendLine(str);
                }
            }
            System.IO.File.WriteAllText(@"c:\txt\data.txt", sb.ToString());
            MessageBox.Show("Save finish. " + fileReply.FILES.size.ToString() + "," + data.Count.ToString());





            /*listBox1.Items.Clear();
            if(stringBuilder.Length > 0)
            {
                string[] delim = { Environment.NewLine, "\n" }; // "\n" added in case you manually appended a newline
                string[] lines = stringBuilder.ToString().Split(delim, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    if(line.Contains)
                    listBox1.Items.Add(line);
                }
            }*/

            /*System.IO.File.WriteAllText(@"c:\txt\data.txt", stringBuilder.ToString());
            MessageBox.Show("Save finish. " + fileReply.FILES.size.ToString());*/
        }


    }

    public class FILES_REPLY
    {
        public string status { get; set; }
        public FILES FILES { get; set; }
    }

    public class FILES
    {
        public int dest { get; set; }
        public string name { get; set; }
        public int progress { get; set; }
        public int type { get; set; }
        public int size { get; set; }
    }

    public class HISTORY_DATA
    {
        public string TimeStamp { get; set; }
        public string STIRRER { get; set; }
        public string TEMP { get; set; }
        public string PH { get; set; }
        public string DO { get; set; }
        public string ACID { get; set; }
        public string BASE { get; set; }
        public string AFOAM { get; set; }
        public string LEVEL { get; set; }
        public string FOAM_LIMIT { get; set; }
        public string LEVEL_LIMIT { get; set; }
        public string AIR { get; set; }
        public string O2 { get; set; }
        public string N2 { get; set; }
        public string CO2 { get; set; }
        public string SUBS_A { get; set; }
        public string EC_PROBE { get; set; }
        public string CO2_PROBE { get; set; }
        public string TURBIDITY_PROBE { get; set; }
        public string LIGHT { get; set; }
        public string MARKER { get; set; }
    }
}


public class CHKPORT_REPLY
{
    public CHKPORT CHKPORT { get; set; }
}

public class CHKPORT
{
}
