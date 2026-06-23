using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.IO.Ports;
using System.Reflection.Metadata;
using System.Text;

namespace LoadExperimentFile1
{
    public partial class Form1 : Form
    {
        public SerialPort serialPort;
        ArrayList row = new();
        bool IsReading = false;
        StringBuilder SbBlock = new();
        StringBuilder SbData = new();
        DataTable dataTable;
        int interval;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cbPortName.Items.Clear();
            foreach (string port in ports)
                cbPortName.Items.Add(port);

            dataGridView1.ColumnCount = 1;
            dataGridView1.Columns[0].Name = "Name";

            row = new ArrayList();
            row.Add("One");
            dataGridView1.Rows.Add(row.ToArray());

            row = new ArrayList();
            row.Add("Two");
            dataGridView1.Rows.Add(row.ToArray());
            dataGridView1.Columns[0].Width = 480;

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Click Me";
            btn.Name = "button";
            btn.Text = "Click Me";
            btn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btn);
            dataGridView1.AutoResizeColumn(0);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort != null)
                if (serialPort.IsOpen)
                    serialPort.Close();
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            serialPort = new SerialPort(cbPortName.Text, 115200, Parity.None, 8);
            serialPort.ReadTimeout = 500;
            serialPort.WriteTimeout = 500;
            serialPort.DataReceived += SerialPort_DataReceived;
            if (!serialPort.IsOpen)
                serialPort.Open();
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (IsReading)
            {
                string msg = serialPort.ReadExisting();
                row = new ArrayList();
                row.Add(msg);
                dataGridView1.Rows.Add(row.ToArray());
                if (msg.Contains("DATA_LOGGER") && msg.Contains("success"))
                    IsReading = false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                int count = 0;
                while(true)
                {

                }
            }
        }

        private async Task PreviewAsync()
        {
            try
            {
                if (AppData.MainModel.IsConnect)
                {
                    AppData.MainModel.Client.WriteLine("{\"SUBS\":{ \"sts\": 0}}\r\n");
                    AppData.HistoricalModel.SbData.Clear();
                    AppData.Bioreactor.FILES.dest = 0;
                    AppData.Bioreactor.FILES.name = AppData.HistoricalModel.SelectedItem.Name + "_" + AppData.HistoricalModel.SelectedItem.CreationdateUnix + ".csv";
                    AppData.Bioreactor.FILES.size = null;
                    AppData.Bioreactor.FILES.type = 3;
                    AppData.MainModel.IsLoadingOpen = true;
                    AppData.HistoricalModel.IsLoading = true;
                    await LoadDataAsync("{ \"FILES\":" + JsonConvert.SerializeObject(AppData.Bioreactor.FILES).Replace("null", "{ }") + "}\r\n");
                    while (true)
                    {
                        await LoadDataAsync("\u0006");
                        if (SbBlock.Length == 0)
                            break;
                    }
                    // await LoadDataAsync("\u0021");
                    string[] lines = SbData.ToString().Split(Environment.NewLine);

                    if (lines[0].Contains("TimeStamp"))
                    {
                        dataTable = new DataTable();
                        string[] columns = lines[0].Split(',');
                        interval = int.Parse(AppData.MainModel.FindStringBetween(columns[0], "=", ")"));
                        foreach (string column in columns)
                        {
                            string col = column.Contains('(') ? column.Substring(0, column.IndexOf("(")) : column;
                            if (col.Equals("SUBS_A")) col = "SUBA";
                            dataTable.Columns.Add(col);
                        }

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
                                    var row = dataTable.NewRow();
                                    for (int i = 0; i < ls.Length; i++)
                                        row[i] = ls[i];
                                    dataTable.Rows.Add(row);
                                }
                            }
                        }

                        if (dataTable != null)
                        {
                            for (int i = 0; i < dataTable.Rows.Count; i++)
                                if (i > 0)
                                    for (int j = 0; j < dataTable.Columns.Count; j++)
                                        if (j < 1)
                                        {
                                            DateTime dateTime = DateTime.ParseExact((string)dataTable.Rows[i - 1][j], "dd/M/yy H:mm:ss", CultureInfo.InvariantCulture);
                                            dataTable.Rows[i][j] = dateTime.AddSeconds(interval).ToString("dd/M/yy H:mm:ss");
                                        }
                                        else
                                            if (String.IsNullOrEmpty(dataTable.Rows[i][j].ToString()))
                                            dataTable.Rows[i][j] = dataTable.Rows[i - 1][j];

                            AppData.HistoricalModel.DataView = dataTable.AsDataView();
                            int c = 0;
                        }
                    }
                    AppData.MainModel.Client.WriteLineAndGetReply("{\"SUBS\":{ \"sts\": 1}}\r\n", TimeSpan.FromSeconds(3));
                }
            }
            catch (Exception) { }
        }

        private async Task LoadDataAsync(string strCommand)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    if (AppData.MainModel.IsConnect)
                    {
                        AppData.HistoricalModel.SbBlock.Clear();
                        AppData.MainModel.Client.WriteLineAndGetReply(strCommand, TimeSpan.FromSeconds(3));
                        if (AppData.HistoricalModel.SbBlock.Length == 0) return;
                        string line = AppData.HistoricalModel.SbBlock.ToString().Split(Environment.NewLine).LastOrDefault();
                        if (line.Contains("$$$$"))
                        {
                            string checksum = line.Substring(line.IndexOf("$$$$") - 4, 4);
                            if (checksum.Equals(CheckSum(AppData.HistoricalModel.SbBlock.ToString().Substring(0, AppData.HistoricalModel.SbBlock.ToString().IndexOf(checksum)))))
                                return;
                        }
                        System.Threading.Thread.Sleep(300);
                    }
                }
                catch (Exception) { }
            });
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            IsReading = true;
            serialPort.Write("{\"DATA_LOGGER\":{\"saving_location\":3}}\r\n");
        }
    }
}
