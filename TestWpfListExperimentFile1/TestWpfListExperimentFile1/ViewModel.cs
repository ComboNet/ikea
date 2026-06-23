using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TestWpfListExperimentFile1
{
    [ObservableObject]
    public partial class ViewModel
    {
        #region Fields
        SerialPort serialPort;
        string Message = string.Empty;
        FILES_REPLY fileReply;
        StringBuilder block = new();
        StringBuilder data = new();
        bool isMasterReading = false;
        bool isDetailReading = false;
        #endregion

        public ViewModel()
        {
            try
            {
                if (SerialPort.GetPortNames().Count() > 0)
                {
                    serialPort = new SerialPort("COM10", 115200, Parity.None, 8);
                    serialPort.DataReceived += SerialPort_DataReceived;
                    if (!serialPort.IsOpen)
                        serialPort.Open();
                }
                else
                    MessageBox.Show("No comport.", "Exclamation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Message = serialPort.ReadExisting();
            if (isDetailReading)
                if (Message.Contains("success") && Message.Contains("FILE"))
                    fileReply = JsonConvert.DeserializeObject<FILES_REPLY>(Message);
        }

        #region Properties
        [ObservableProperty] public ObservableCollection<Experiment> _experiments = new();
        [ObservableProperty] public Experiment _selectedExperiment;
        [ObservableProperty] public ObservableCollection<Item1> _items1 = new();
        [ObservableProperty] public ObservableCollection<Item2> _items2 = new();
        #endregion

        #region Functions
        private DateTime UnixTimestampToDateTime(double unixTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
            return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
        }

        public bool CheckBlock(StringBuilder sb)
        {
            bool bState = false;
            string ss = sb.ToString();
            if (ss.Contains("$$$$"))
            {
                string checksum = ss.Substring(ss.IndexOf("$$$$") - 4, 4);
                if (CheckSum(block.ToString()))
                    bState = true;
            }
            return bState;
        }

        public string FindStringBetween(string str, string str1, string str2) { return str.Split(new string[] { str1 }, StringSplitOptions.None)[1].Split(str2)[0].Trim(); }
        private string CalculateCheckSum(string str) { return str.Sum(i => Encoding.UTF8.GetBytes(i.ToString())[0]).ToString("X"); }

        private bool CheckSum(string s)
        {
            string checksum = s.Substring(s.IndexOf("$$$$") - 4, 4);
            string calCheckupm = CalculateCheckSum(s.Substring(0, s.IndexOf(checksum))).PadLeft(4, '0');
            return checksum.Equals(calCheckupm);
        }
        #endregion

        #region Commands
        public ICommand LoadCommand => new RelayCommand(async () =>
        {
            try
            {
                data.Clear();
                isMasterReading = true;
                fileReply = new FILES_REPLY();
                if (!serialPort.IsOpen)
                    serialPort.Open();
                serialPort.Write("{\"FILES\":{\"dest\":0,\"name\":\"experiments.csv\",\"size\":{},\"type\":2}}\r\n");
                Thread.Sleep(200);
                while (isMasterReading)
                {
                    if (Message.Contains("success") && Message.Contains("FILE"))
                        fileReply = JsonConvert.DeserializeObject<FILES_REPLY>(Message);
                    else
                        if (Message.Contains("$$$$"))
                        {
                            data.Append(Message);
                            if (fileReply.FILES.size.Equals(data.ToString().Substring(0, data.ToString().IndexOf("$$$$") - 4).Length))
                                isMasterReading = false;
                            else
                            { }
                        }
                    Thread.Sleep(200);
                }
                await Task.Run(() =>
                {
                    App.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string[] lines = data.ToString().Split("\r\n");
                        Experiments.Clear();
                        foreach (string line in lines)
                            if (!line.Contains("id,name") && !String.IsNullOrEmpty(line) && !line.Contains("$$$$"))
                            {
                                string[] ls = line.Split(",");
                                Experiments.Add(new Experiment()
                                {
                                    Id = int.Parse(ls[0]),
                                    Name = ls[1],
                                    Author = ls[2],
                                    Recipe = ls[3],
                                    Creationdate = UnixTimestampToDateTime(Convert.ToDouble(ls[4])).ToString("hh:mm:ss dd/MM/yyyy"),
                                    CreationdateUnix = ls[4],
                                    Cultivation = ls[5],
                                    Stopdate = ls[6],
                                    Status = int.Parse(ls[7]),
                                    Note = ls[8],
                                    Enabled = int.Parse(ls[9])
                                });
                            }
                    }));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });

        public ICommand ReadCommand => new RelayCommand( () =>
        {
            if (Experiments.Count > 0)
            {
                Items1.Clear();
                Items2.Clear();
                if (!serialPort.IsOpen)
                    serialPort.Open();
                string fileName = Experiments[0].Name.Replace(" ", "`") + "_" + Experiments[0].CreationdateUnix + ".csv";
                block.Clear();
                data.Clear();
                fileReply = new FILES_REPLY();
                isDetailReading = true;
                serialPort.WriteLine("{\"FILES\":{\"type\":3,\"name\":\"" + fileName + "\",\"size\":{ },\"dest\":0,\"progress\":0}}\r\n");
                Thread.Sleep(400);
                while (isDetailReading)
                {
                    block.Clear();
                    if (Message.Contains("$$$$"))
                    {
                        Items1.Add(new Item1() { Message = Message });
                        block.Append(Message);
                        if(CheckBlock(block))
                        {
                            Items2.Add(new Item2() { Message = block.ToString().Trim().Substring(0, block.ToString().IndexOf("$$$$") - 4) });
                            data.Append(block.ToString().Trim().Substring(0, block.ToString().IndexOf("$$$$") - 4));
                            if (fileReply.FILES.size.Equals(data.Length))
                                isDetailReading = false;
                            else
                                serialPort.Write("\u0006");
                        }
                        else
                            serialPort.Write("\u0021");
                    }
                    else
                        serialPort.Write("\u0021");
                    Thread.Sleep(400);
                }
                // MessageBox.Show(data.ToString());
            }
        });

        public ICommand CloseCommand => new RelayCommand(() =>
        {
            if(SerialPort.GetPortNames().Count()>0)
            {
                if (serialPort.IsOpen)
                    serialPort.Close();
                serialPort.Dispose();
            }
            Application.Current.Shutdown();
        });
        #endregion
    }

    public class Experiment
    {
        public int Id;
        public string Name;
        public string Author;
        public string Recipe;
        public string CreationdateUnix;
        public string Creationdate;
        public string Cultivation;
        public string Stopdate;
        public int Status;
        public string Note;
        public int Enabled;
    }

    public class FILES_REPLY
    {
        public string status { get; set; }
        public FILES FILES { get; set; } = new();
    }

    public class FILES
    {
        public int type { get; set; }
        public string name { get; set; }
        public int? size { get; set; }
        public int dest { get; set; }
        public int progress { get; set; }
    }

    public class Item1
    {
        public string Message { get; set; }
    }

    public class Item2
    {
        public string Message { get; set; }
    }
}
