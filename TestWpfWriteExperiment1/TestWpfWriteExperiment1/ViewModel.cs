using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.Data;

namespace TestWpfWriteExperiment1
{
    [ObservableObject]
    public partial class ViewModel
    {
        public ViewModel()
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

        #region Fields
        SerialPort serialPort;
        StringBuilder block = new();
        StringBuilder data = new();
        StringBuilder sb = new();
        String msg = string.Empty;
        FILES_REPLY fileReply = new();
        const string quote = "\"";
        #endregion

        #region Properties
        [ObservableProperty] public ObservableCollection<string> _items = new();
        [ObservableProperty] public ObservableCollection<EXPERIMENT> _experiments = new();
        [ObservableProperty] public EXPERIMENT _selectedExperiment = new();
        [ObservableProperty] public DataView _dataView;
        #endregion

        #region Functions
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            msg = serialPort.ReadExisting();
        }

        public Int32 DateTimeToUnixTimeSecond(DateTime dateTime) => Convert.ToInt32(dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        public DateTime UnixTimestampToDateTime(double unixTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
            return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
        }

        public string CalculateCheckSum(string str) => str.Sum(i => Encoding.UTF8.GetBytes(i.ToString())[0]).ToString("X");
        private bool CheckSum(string s)
        {
            string checksum = s.Substring(s.IndexOf("$$$$") - 4, 4);
            bool b = (checksum.Equals(CalculateCheckSum(s.Substring(0, s.IndexOf(checksum))))) ? true : false;
            return b;
        }

        public void Apply(object obj)
        {
            if (obj != null)
            {
                string jsonString = JsonConvert.SerializeObject(obj);
                string strCommand = "{" + quote + obj.GetType().Name + quote + ":" + jsonString + "}" + "\r\n";
                serialPort.Write(strCommand);
            }
        }

        private void RefreshList()
        {
            try
            {
                string str = String.Empty;
                string compareString = String.Empty;
                if (!serialPort.IsOpen) serialPort.Open();
                msg = String.Empty;
                data.Clear();
                serialPort.Write("{\"FILES\":{\"dest\":0,\"name\":\"experiments.csv\",\"size\":{},\"type\":2}}\r\n");
                int count = 0;
                while (true)
                {
                    if (!String.IsNullOrEmpty(msg))
                    {
                        if (!compareString.Equals(msg))
                        {
                            count = 0;
                            if (data.ToString().Contains("$$$$")) break;
                            str += msg;
                            data.Append(msg);
                            compareString = msg;
                        }
                        else
                            if (count++ > 3) break;
                    }
                    Thread.Sleep(500);
                }
                string[] lines = str.Split(Environment.NewLine);
                Experiments.Clear();
                for (int i = 2; i < lines.Length - 1; i++)
                {
                    string[] ls = lines[i].Split(",");
                    Experiments.Add(new EXPERIMENT()
                    {
                        id = int.Parse(ls[0]),
                        name = ls[1],
                        author = ls[2],
                        recipe = ls[3],
                        startdate = int.Parse(ls[4]),
                        cultivation = int.Parse(ls[5]),
                        stopdate = int.Parse(ls[6]),
                        sts = int.Parse(ls[7]),
                        note = ls[8],
                    });
                }
                serialPort.Write("\u0006\r\n");
                serialPort.Write("{\"CHKPORT\":{}}" + Environment.NewLine);
                if (serialPort.IsOpen)
                    serialPort.Close();
                MessageBox.Show("Completed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string FindStringBetween(string str, string str1, string str2) { return str.Split(new string[] { str1 }, StringSplitOptions.None)[1].Split(str2)[0].Trim(); }

        private string ExperimentsToString()
        {
            StringBuilder sb = new();
            sb.Append("id,name,author,recipe,creationdate,cultivation,stopdate,status,note,enabled\r\n");
            foreach (EXPERIMENT experiment in Experiments)
                sb.Append(experiment.id + "," + experiment.name + "," + experiment.author + "," + experiment.recipe
                    + "," + experiment.startdate + "," + experiment.cultivation + "," + experiment.stopdate
                    + "," + experiment.sts + "," + experiment.note + "\r\n");
            return sb.ToString() + CalculateCheckSum(sb.ToString());
        }

        private async Task<DataTable> ReadCsvAsync(FILES_REPLY fileReply)
        {
            if (!serialPort.IsOpen) serialPort.Open();
            Thread.Sleep(500);
            DataTable dataTable = new();
            if (fileReply != null)
            {
                data.Clear();
                await Task.Run(() =>
                {
                    while (true)
                    {
                        if (msg.Contains("CHKPORT") || msg.Equals("Load completed.")) break;
                        block.Clear();
                        if (msg.Contains("$$$$"))
                        {
                            block.Append(msg);
                            if (CheckBlock(block))
                            {
                                data.Append(block.ToString().Trim().Substring(0, block.ToString().IndexOf("$$$$") - 4));
                                if (fileReply.FILES.size.Equals(data.Length))
                                {
                                    string[] lines = data.ToString().Split(Environment.NewLine);
                                    string[] cs = lines[0].Split(',');
                                    int index = 0;
                                    for (int i = 0; i < cs.Count(); i++)
                                    {
                                        string[] sss = cs.Where(s => s.Contains(cs[i])).ToArray();
                                        if (sss.Count() > 1)
                                        {
                                            cs[i] = cs[i] + index.ToString();
                                            index++;
                                        }
                                        dataTable.Columns.Add(new DataColumn(cs[i]));
                                    }
                                    foreach (string line in lines)
                                        if (!lines.Contains("TimeStamp"))
                                        {
                                            var str = line.Trim();
                                            if (line.Contains("$$$$"))
                                                str = line.Replace(String.Concat(line.Substring(line.IndexOf("$$$$") - 4, 4), "$$$$"), "").Trim();
                                            if (!line.Contains("TimeStamp"))
                                            {
                                                string[] ls = str.Split(",");
                                                if (ls.Length == dataTable.Columns.Count)
                                                {
                                                    var row = dataTable.NewRow();
                                                    for (int i = 0; i < ls.Length; i++)
                                                        row[i] = ls[i];
                                                    dataTable.Rows.Add(row);
                                                }
                                            }
                                        }
                                    /*for (int i = 0; i < dataTable.Rows.Count; i++)
                                        if (i > 0)
                                            for (int j = 0; j < dataTable.Columns.Count; j++)
                                                if (j < 1)
                                                {
                                                    DateTime dateTime = DateTime.ParseExact((string)dataTable.Rows[i - 1][j], "d/M/yy H:mm:ss", CultureInfo.InvariantCulture);
                                                    dataTable.Rows[i][j] = dateTime.AddSeconds(interval).ToString("dd/M/yy H:mm:ss");
                                                }
                                                else
                                                    if (String.IsNullOrEmpty(dataTable.Rows[i][j].ToString()))
                                                    dataTable.Rows[i][j] = dataTable.Rows[i - 1][j];
                                    DataView = dataTable.DefaultView; */
                                    break;
                                }
                                else
                                    serialPort.Write("\u0006");
                            }
                            else
                                serialPort.Write("\u0021");
                        }
                        else
                            serialPort.Write("\u0021");
                        Thread.Sleep(500);
                    }
                });
            }
            if (serialPort.IsOpen) serialPort.Close();
            return dataTable;
        }

        private void Add(EXPERIMENT experiment)
        {
            try
            {
                Experiments.Add(experiment);
                string dataString = ExperimentsToString();
                if (!serialPort.IsOpen)
                    serialPort.Open();
                serialPort.Write("{\"CHKPORT\":{}}\r\n");
                serialPort.Write("{\"FILES\":{\"dest\":0,\"name\":\"experiments.csv\",\"size\":" + (dataString.Length - 4).ToString() + ",\"type\":0}}\r\n");
                while (true)
                    if (msg.Contains("success") && msg.Contains("FILES")) break;
                byte[] MyMessage = System.Text.Encoding.UTF8.GetBytes(dataString);
                serialPort.Write(MyMessage, 0, MyMessage.Length);
                while (true)
                    if (msg.Contains("\u0006")) break;
                serialPort.Write("{\"CHKPORT\":{}}" + Environment.NewLine);
                if (serialPort.IsOpen)
                    serialPort.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete(EXPERIMENT experiment)
        {
            try
            {
                Experiments.RemoveAt(Experiments.IndexOf(experiment));
                string dataString = ExperimentsToString();
                if (!serialPort.IsOpen)
                    serialPort.Open();
                serialPort.Write("{\"FILES\":{\"dest\":0,\"name\":\"experiments.csv\",\"size\":" + (dataString.Length - 4).ToString() + ",\"type\":0}}\r\n");
                while (true)
                    if (msg.Contains("success") && msg.Contains("FILES")) break;
                byte[] MyMessage = System.Text.Encoding.UTF8.GetBytes(dataString);
                serialPort.Write(MyMessage, 0, MyMessage.Length);
                while (true)
                    if (msg.Contains("\u0006")) break;
                serialPort.Write("{\"CHKPORT\":{}}" + Environment.NewLine);
                if (serialPort.IsOpen)
                    serialPort.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        public void SendData(string dataString)
        {
            byte[] MyMessage = System.Text.Encoding.UTF8.GetBytes(dataString);
            serialPort.Write(MyMessage, 0, MyMessage.Length);
            while (true)
                if (msg.Contains("\u0006")) break;
        }
        #endregion

        public ICommand ClearCommand => new RelayCommand(() =>
        {
            Experiments.Clear();
        });

        public ICommand ListCommand => new RelayCommand(() =>
        {
            RefreshList();
        });

        public ICommand ReadCommand => new RelayCommand(async () =>
        {
            if (SelectedExperiment != null)
            {
                string filename = SelectedExperiment.name.Replace(" ", "`") + "_" + SelectedExperiment.startdate + ".csv";
                if (!serialPort.IsOpen) serialPort.Open();
                serialPort.Write("{\"FILES\":{\"type\":3,\"name\":\"" + filename + "\",\"size\":{ },\"dest\":0,\"progress\":0}}\r\n");
                while (true)
                    if (msg.Contains("success") && msg.Contains("FILES")) break;
                fileReply = JsonConvert.DeserializeObject<FILES_REPLY>(msg);
                DataTable dataTable = await ReadCsvAsync(fileReply);
                DataView = dataTable.DefaultView;
                if (serialPort.IsOpen) serialPort.Close();
            }
        });

        public ICommand AddCommand => new RelayCommand(() =>
        {
            try
            {
                var now = DateTime.Now;
                Add(new EXPERIMENT()
                {
                    id = Experiments.Max(u => u.id) + 1,
                    name = "Narongsak1",
                    author = "admin",
                    recipe = "nv_kku1@hotmail.com",
                    startdate = DateTimeToUnixTimeSecond(now),
                    cultivation = 0,
                    stopdate = 0,
                    note = String.Empty
                });
                MessageBox.Show("Add experiment success.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });

        public ICommand DeleteCommand => new RelayCommand(() =>
        {
            try
            {
                Delete(SelectedExperiment);
                MessageBox.Show("Delete selected experiment success.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });

        public ICommand StartCommand => new RelayCommand(() =>
        {
            if (!serialPort.IsOpen) serialPort.Open();
            EXPERIMENT experiment = new()
            {
                id = Experiments.Max(e => e.id) + 1,
                name = "Narongsak03",
                author = "admin",
                recipe = "SW-05L-default",
                vessel = 0,
                cultivation = -1,
                startdate = DateTimeToUnixTimeSecond(DateTime.Now),
                interval = 3,
                stopdate = -1,
                note = "Hello03"
            };
            Add(experiment);
            if (!serialPort.IsOpen) serialPort.Open();
            Apply(experiment);
            while (true)
                if (msg.Contains("success") && msg.Contains("EXPERIMENT")) break;
            serialPort.Write("{\"EXPERIMENT\":{\"sts\":1}}\r\n");
            while (true)
                if (msg.Contains("success") && msg.Contains("EXPERIMENT")) break;
            serialPort.Write("{\"EXPERIMENT\":{\"sts\":3}}\r\n");
            while (true)
                if (msg.Contains("success") && msg.Contains("EXPERIMENT")) break;
            int cultivation = DateTimeToUnixTimeSecond(DateTime.Now);
            EXPERIMENT editExperiment = experiment;
            Delete(experiment);
            if (!serialPort.IsOpen) serialPort.Open();
            editExperiment.cultivation = cultivation;   
            Add(editExperiment);
            if (!serialPort.IsOpen) serialPort.Open();
            serialPort.Write("{\"EXPERIMENT\":{\"cultivation\":" + cultivation + "}}\r\n");
            while (true)
                if (msg.Contains("success") && msg.Contains("EXPERIMENT")) break;
            serialPort.Write("{\"SAMPLE_DATA\":{\"desc\":\"\",\"label\":\"Cultivation`start\",\"time_stamp\":" + cultivation + "}}\r\n");
            while (true)
                if (msg.Contains("success") && msg.Contains("SAMPLE_DATA")) break;
            if (serialPort.IsOpen) serialPort.Close();
            MessageBox.Show("Start experiment successfully.");
        });

        public ICommand StopCommand => new RelayCommand(() =>
        {
            int stopdate = DateTimeToUnixTimeSecond(DateTime.Now);
            EXPERIMENT tempExperiment = Experiments.Last();
            Delete(Experiments.Last());
            if (!serialPort.IsOpen) serialPort.Open();
            tempExperiment.stopdate = stopdate;
            tempExperiment.sts = 4;
            Add(tempExperiment);
            if (!serialPort.IsOpen) serialPort.Open();
            serialPort.Write("{\"EXPERIMENT\":{\"stopdate\":" + stopdate.ToString() + "}}\r\n");
            while (true)
                if (msg.Contains("success") && msg.Contains("EXPERIMENT")) break;
            string dataString = ExperimentsToString();
            serialPort.Write("{\"FILES\":{\"dest\":0,\"name\":\"experiments.csv\",\"size\":" + (dataString.Length - 4).ToString() + ",\"type\":2}}\r\n");
            while (true)
                if (msg.Contains("success") && msg.Contains("FILES")) break;
            SendData(dataString);
            serialPort.Write("{\"EXPERIMENT\":{\"sts\":0}}\r\n");
            while (true)
                if (msg.Contains("success") && msg.Contains("EXPERIMENT")) break;
            if (serialPort.IsOpen) serialPort.Close();
            MessageBox.Show("Stop experiment successfully.");
        });

        public ICommand CsvCommand => new RelayCommand(async () =>
        {
            if (!serialPort.IsOpen) serialPort.Open();
            var experiment = Experiments.Last();
            string filename = experiment.name.Replace(" ", "`") + "_" + experiment.startdate + ".csv";
            serialPort.Write("{\"FILES\":{\"dest\":0,\"name\":\"" + filename + "\",\"size\":{},\"type\":3}}\r\n");
            while (true)
                if (msg.Contains("success") && msg.Contains("FILES") && msg.Contains(filename)) break;
            fileReply = JsonConvert.DeserializeObject<FILES_REPLY>(msg);
            DataTable dataTable = await ReadCsvAsync(fileReply);
            DataView = dataTable.DefaultView;
            if (serialPort.IsOpen) serialPort.Close();
            MessageBox.Show("Export CSV file successfully.");
        });

        public ICommand AlarmCommand => new RelayCommand(async () =>
        {
            if (!serialPort.IsOpen) serialPort.Open();
            var experiment = Experiments.Last();
            string filename = experiment.name.Replace(" ", "`") + "_" + experiment.startdate + "_alarm" + ".csv";
            serialPort.Write("{\"FILES\":{\"dest\":0,\"name\":\"" + filename + "\",\"size\":{},\"type\":5}}\r\n");
            while (true)
                if (msg.Contains("success") && msg.Contains("FILES") && msg.Contains(filename)) break;
            fileReply = JsonConvert.DeserializeObject<FILES_REPLY>(msg);
            DataTable dataTable = await ReadCsvAsync(fileReply);
            DataView = dataTable.DefaultView;
            if (serialPort.IsOpen) serialPort.Close();
            MessageBox.Show("Export alarm file successfully.");
        });

        public ICommand CheckLengthCommand => new RelayCommand(() =>
        {
            string str = "id,name,author,recipe,creationdate,cultivation,stopdate,status,note,enabled\r\n1,test DO,admin,IWG-DO-GAS,1683001992,-1,-1,8,,1\r\n2,Test 3mar 2023,admin,IWT-BV-BIOR,1683087701,1683087830,1683089331,4,,1\r\n3,test 4-may,admin,IWT-BV-BIOR,1683181907,-1,-1,8,,1\r\n4,Narongsak,admin,SW-05L-default,1711440698,1711440780,1711441088,4,Hello World,0\r\n5,Narongsak123,admin,SW-05L-default,1711445049,1711445049,1711445111,4,,0\r\n6,Narong666,admin,SW-05L-default,1712038436,1712038459,1712038544,4,,1\r\n";
            MessageBox.Show(str.Length.ToString());
        });
    }

    public class EXPERIMENT
    {
        #region Properties
        public int id { get; set; }
        public int sts { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public string recipe { get; set; }
        public int vessel { get; set; }
        public string log { get; set; }
        public int interval { get; set; }
        public int startdate { get; set; }
        public int cultivation { get; set; }
        public int stopdate { get; set; }
        public string note { get; set; }
        #endregion
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
}
