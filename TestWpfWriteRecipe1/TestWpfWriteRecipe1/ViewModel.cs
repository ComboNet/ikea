using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace TestWpfWriteRecipe1
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
            catch (SocketException ex)
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
        [ObservableProperty] public ObservableCollection<Recipe> _recipes = new();
        [ObservableProperty] public Recipe _selectedRecipe = new();
        #endregion

        #region Functions
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            msg = serialPort.ReadExisting();
            while (true)
            {
                if (msg.Equals("!"))
                    serialPort.Write("{\"CHKPORT\":{}}" + Environment.NewLine);
                else
                    break;
                Thread.Sleep(800);
            }
        }

        public Int32 DateTimeToUnixTimeSecond(DateTime dateTime) => Convert.ToInt32(dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        public DateTime UnixTimestampToDateTime(double unixTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
            return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
        }

        public string CalculateCheckSum(string str) => str.Sum(i => Encoding.UTF8.GetBytes(i.ToString())[0]).ToString("X");

        public void Apply(object obj)
        {
            if (obj != null)
            {
                string jsonString = JsonConvert.SerializeObject(obj);
                string strCommand = "{" + quote + obj.GetType().Name + quote + ":" + jsonString + "}" + "\r\n";
                serialPort.Write(strCommand);
            }
        }

        private string RecipesToString()
        {
            StringBuilder sb = new();
            sb.Append("id,name,author,creationdate,enabled\r\n");
            foreach (Recipe recipe in Recipes)
                sb.Append(recipe.Id + "," + recipe.Name + "," + recipe.Author + "," + recipe.CreationdateUnix + "," + recipe.Enabled + "\r\n");
            return sb.ToString() + CalculateCheckSum(sb.ToString());
        }

        private void Refresh()
        {
            try
            {
                block.Clear();
                data.Clear();
                string compareString = String.Empty;
                if (!serialPort.IsOpen) serialPort.Open();
                serialPort.Write("{\"FILES\":{\"dest\":0,\"name\":\"recipes.csv\",\"size\":{},\"type\":1}}\r\n");
                while (true)
                {
                    if (msg.Contains("success") && msg.Contains("FILES"))
                    {
                        string[] lines = msg.Split('\n');
                        if (lines.Count() > 1)
                            fileReply = JsonConvert.DeserializeObject<FILES_REPLY>(lines[0]);
                        else
                        {
                            for (int i = 1; i < lines.Count(); i++)
                                if (!lines[i].Contains("id,name") && !String.IsNullOrEmpty(lines[i]) && !lines[i].Contains("$$$$"))
                                {
                                    string[] ls = lines[i].Split(",");
                                    Recipe recipe = new Recipe()
                                    {
                                        Id = int.Parse(ls[0]),
                                        Name = ls[1],
                                        Author = ls[2],
                                        Creationdate = UnixTimestampToDateTime(Convert.ToDouble(ls[3])).ToString("hh:mm:ss dd/MM/yyyy"),
                                        CreationdateUnix = ls[3],
                                        Enabled = int.Parse(ls[4])
                                    };
                                    Recipes.Add(recipe);
                                    string item = recipe.Id + "," + recipe.Name + "," + recipe.Author + "," + recipe.CreationdateUnix + "," + recipe.Enabled + Environment.NewLine;
                                    Items.Add(item);
                                }
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(msg))
                        {
                            if (!compareString.Equals(msg))
                            {
                                compareString = msg;
                                // block.Append(msg);
                                if (msg.Contains("$$$$"))
                                {
                                    data.Append(msg.ToString().Trim().Substring(0, msg.ToString().IndexOf("$$$$") - 4));
                                    if (fileReply.FILES.size.Equals(data.Length))
                                    {
                                        string[] lines = data.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                                        Items.Clear();
                                        string header = "id,name,author,creationdate,enabled" + Environment.NewLine;
                                        Items.Add(header);
                                        Recipes.Clear();
                                        foreach (var line in lines)
                                            if (!line.Contains("id,name") && !String.IsNullOrEmpty(line) && !line.Contains("$$$$"))
                                            {
                                                string[] ls = line.Split(",");
                                                Recipe recipe = new Recipe()
                                                {
                                                    Id = int.Parse(ls[0]),
                                                    Name = ls[1],
                                                    Author = ls[2],
                                                    Creationdate = UnixTimestampToDateTime(Convert.ToDouble(ls[3])).ToString("hh:mm:ss dd/MM/yyyy"),
                                                    CreationdateUnix = ls[3],
                                                    Enabled = int.Parse(ls[4])
                                                };
                                                Recipes.Add(recipe);
                                                string item = recipe.Id + "," + recipe.Name + "," + recipe.Author + "," + recipe.CreationdateUnix + "," + recipe.Enabled + Environment.NewLine;
                                                Items.Add(item);
                                            }
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                block.Clear();
                                data.Clear();
                                block.Append(msg);
                                if (msg.Contains("$$$$"))
                                {
                                    data.Append(block.ToString().Trim().Substring(0, block.ToString().IndexOf("$$$$") - 4));
                                    if (fileReply.FILES.size.Equals(data.Length))
                                    {
                                        string[] lines = data.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                                        Items.Clear();
                                        string header = "id,name,author,creationdate,enabled" + Environment.NewLine;
                                        Items.Add(header);
                                        Recipes.Clear();
                                        foreach (var line in lines)
                                            if (!line.Contains("id,name") && !String.IsNullOrEmpty(line) && !line.Contains("$$$$"))
                                            {
                                                string[] ls = line.Split(",");
                                                Recipe recipe = new Recipe()
                                                {
                                                    Id = int.Parse(ls[0]),
                                                    Name = ls[1],
                                                    Author = ls[2],
                                                    Creationdate = UnixTimestampToDateTime(Convert.ToDouble(ls[3])).ToString("hh:mm:ss dd/MM/yyyy"),
                                                    CreationdateUnix = ls[3],
                                                    Enabled = int.Parse(ls[4])
                                                };
                                                Recipes.Add(recipe);
                                                string item = recipe.Id + "," + recipe.Name + "," + recipe.Author + "," + recipe.CreationdateUnix + "," + recipe.Enabled + Environment.NewLine;
                                                Items.Add(item);
                                            }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    Thread.Sleep(500);
                }
                serialPort.Write("\u0006\r\n");
                serialPort.Write("{\"CHKPORT\":{}}" + Environment.NewLine);
                if (serialPort.IsOpen) serialPort.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SendData(string dataString)
        {
            byte[] MyMessage = System.Text.Encoding.UTF8.GetBytes(dataString);
            serialPort.Write(MyMessage, 0, MyMessage.Length);
            while (true)
                if (msg.Contains("\u0006")) break;
        }

        private void Add(Recipe recipe)
        {
            try
            {
                string jsonString = "{\"RECIPE\":{\"accessories\":{\"0\":0,\"1\":0,\"2\":0,\"3\":0,\"4\":0,\"5\":0,\"6\":0,\"7\":0,\"8\":0},\"cascade\":[{\"air\":0,\"n2\":500,\"o2\":0,\"stir\":280},{\"air\":50,\"n2\":0,\"o2\":0,\"stir\":280},{\"air\":50,\"n2\":0,\"o2\":0,\"stir\":280},{\"air\":50,\"n2\":0,\"o2\":0,\"stir\":280},{\"air\":50,\"n2\":0,\"o2\":0,\"stir\":280},{\"air\":50,\"n2\":0,\"o2\":0,\"stir\":280}],\"cellInfo\":{\"0\":\"\",\"1\":\"\",\"2\":\"\",\"3\":\"\",\"4\":\"\",\"5\":\"\",\"6\":\"\",\"7\":\"\",\"8\":\"\"},\"controller\":{\"0\":0,\"1\":0,\"2\":0,\"3\":0,\"4\":1,\"5\":1,\"6\":1,\"7\":0,\"8\":0,\"9\":0,\"A\":1,\"B\":6},\"gas\":{\"0\":0,\"1\":1,\"2\":1,\"3\":1,\"4\":0},\"general\":{\"0\":0,\"1\":\"\",\"2\":1,\"3\":1},\"pump\":{\"0\":1,\"1\":1,\"2\":1,\"3\":1,\"4\":0,\"5\":9,\"6\":10,\"7\":11,\"8\":12,\"9\":13},\"reactor\":{\"0\":0,\"1\":0,\"2\":0,\"3\":0,\"4\":0,\"5\":0,\"6\":0,\"7\":0,\"8\":0}}}9D8C";
                Recipes.Add(recipe);
                string dataString = RecipesToString();
                if (!serialPort.IsOpen) serialPort.Open();
                serialPort.Write("{\"CHKPORT\":{}}" + Environment.NewLine);
                serialPort.Write("{\"FILES\":{\"dest\":0,\"name\":\"" + recipe.Name + ".json\",\"size\":" + (jsonString.Length - 4).ToString() + ",\"type\":4}}\r\n");
                while (true)
                {
                    if (msg.Equals("!")) serialPort.Write("{\"SUBS\":{ \"sts\" : 0}}\r\n");
                    if (msg.Contains("success") && msg.Contains("FILES")) break;
                }
                while (true)
                {
                    if (msg.Equals("!"))
                        serialPort.Write("{\"CHKPORT\":{}}" + Environment.NewLine);
                    else
                        break;
                    Thread.Sleep(300);
                }
                SendData(jsonString);
                if (!serialPort.IsOpen) serialPort.Open();
                serialPort.Write("{\"FILES\":{\"dest\":0,\"name\":\"recipes.csv\",\"size\":" + (dataString.Length - 4).ToString() + ",\"type\":1}}\r\n");
                while (true)
                    if (msg.Contains("success") && msg.Contains("FILES")) break;
                    repeat:
                SendData(dataString);
                /*MyMessage = System.Text.Encoding.UTF8.GetBytes(dataString);
                serialPort.Write(MyMessage, 0, MyMessage.Length);
                while (true)
                {
                    if (String.IsNullOrEmpty(msg)) goto repeat;
                    if (msg.Equals("!")) serialPort.Write("{\"SUBS\":{ \"sts\" : 0}}\r\n");
                    if (msg.Contains("\u0006")) break;
                }*/
                serialPort.Write("{\"CHKPORT\":{}}" + Environment.NewLine);
                if (serialPort.IsOpen) serialPort.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete(Recipe recipe)
        {
            try
            {
                Recipes.RemoveAt(Recipes.IndexOf(recipe));
                string dataString = RecipesToString();
                if (!serialPort.IsOpen) serialPort.Open();
                serialPort.Write("{\"FILES\":{\"dest\":0,\"name\":\"recipes.csv\",\"size\":" + (dataString.Length - 4).ToString() + ",\"type\":1}}\r\n");
                while (true)
                    if (msg.Contains("success") && msg.Contains("FILES")) break;
                SendData(dataString);
                serialPort.Write("{\"CHKPORT\":{}}" + Environment.NewLine);
                if (serialPort.IsOpen) serialPort.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Commands
        public ICommand TestCommand => new RelayCommand(() =>
        {
            string str = "id,name,author,creationdate,enabled\r\n1,SW-05L-default,admin,1614072026,1\r\n2,SW-1L-default,admin,1614072073,1\r\n3,SW-2L-default,admin,1614072116,1\r\n4,SW-5L-default,admin,1614072164,1\r\n5,DW-05L-default,admin,1614072240,1\r\n6,DW-1L-default,admin,1614072293,1\r\n7,DW-2L-default,admin,1614072328,1\r\n8,DW-5L-default,admin,1614072532,1\r\n9,DW-10L-default,admin,1614072561,1\r\n10,IWG-DO-GAS,admin,1683001956,1\r\n11,IWT-BV-BIOR,admin,1683087661,1\r\n12,Narongsak-20240423,admin,1713406931,1\r\n";
            MessageBox.Show(str.Length.ToString() + "," + CalculateCheckSum(str));
        });

        public ICommand ListCommand => new RelayCommand(() =>
        {
            Refresh();
        });

        public ICommand ClearCommand => new RelayCommand(() =>
        {
            Recipes.Clear();
        });

        public ICommand ReadCommand => new RelayCommand(() =>
        {
            string jsonFileName = SelectedRecipe.Name;
            // string jsonFileName = "SW-05L-default";
            try
            {
                block.Clear();
                data.Clear();
                string compareString = String.Empty;
                if (!serialPort.IsOpen) 
                    serialPort.Open();
                serialPort.Write("{\"FILES\":{\"dest\":0,\"name\":\"" + jsonFileName + ".json\",\"size\":{},\"type\":4}}\r\n");
                Thread.Sleep(300);
                while (true)
                {
                    
                    if (msg.Contains("id,name")) { }
                    if (msg.Contains("success") && msg.Contains("FILES"))
                    {
                        string[] lines = msg.Split('\n');
                        if (lines.Count() > 1)
                            fileReply = JsonConvert.DeserializeObject<FILES_REPLY>(lines[0]);
                        else
                            MessageBox.Show(lines[1]);
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(msg))
                        {
                            if (!compareString.Equals(msg))
                            {
                                compareString = msg;
                                // block.Append(msg);
                                if (msg.Contains("$$$$"))
                                {
                                    data.Append(msg.ToString().Trim().Substring(0, msg.ToString().IndexOf("$$$$") - 4));
                                    if (fileReply.FILES.size.Equals(data.Length))
                                    {
                                        MessageBox.Show(data.ToString());
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                block.Clear();
                                data.Clear();
                                block.Append(msg);
                                if (msg.Contains("$$$$"))
                                {
                                    data.Append(block.ToString().Trim().Substring(0, block.ToString().IndexOf("$$$$") - 4));
                                    if (fileReply.FILES.size.Equals(data.Length))
                                    {
                                        MessageBox.Show(data.ToString());
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    Thread.Sleep(500);
                }
                serialPort.Write("\u0006\r\n");
                serialPort.Write("{\"CHKPORT\":{}}" + Environment.NewLine);
                if (serialPort.IsOpen) 
                    serialPort.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });

        public ICommand AddCommand => new RelayCommand(() =>
        {
            try
            {
                string fileName = "Narongsak-20240418";
                var now = DateTime.Now;
                Add(new Recipe()
                {
                    Id = Recipes.Max(x => x.Id) + 1,
                    Name = fileName,
                    Author = "admin",
                    Creationdate = now.ToString(),
                    CreationdateUnix = DateTimeToUnixTimeSecond(DateTime.Now).ToString(),
                    Enabled = 1
                });
                MessageBox.Show("Add data success.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });

        public ICommand EditCommand => new RelayCommand(() =>
        {
            try
            {
                string jsonFileName = "Narongsak-20240423";
                var now = DateTime.Now;
                Recipe editRecipe = SelectedRecipe;
                editRecipe.Name = jsonFileName;
                editRecipe.Creationdate = now.ToString();
                editRecipe.CreationdateUnix = DateTimeToUnixTimeSecond(now).ToString();
                Delete(SelectedRecipe);
                Add(editRecipe);
                MessageBox.Show("Edit data success.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });

        public ICommand DeleteCommand => new RelayCommand(() =>
        {
            Delete(SelectedRecipe);
            MessageBox.Show("Delete data success.");
        });
        #endregion
    }

    public class Recipe
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Creationdate { get; set; }
        public string CreationdateUnix { get; set; }
        public string Data { get; set; }
        public int Enabled { get; set; }
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
