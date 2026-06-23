using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TestWpfListIp1
{
    [ObservableObject]
    public partial class ViewModel
    {
        [ObservableProperty] public ObservableCollection<string> _items = new();

        private async Task<List<PingReply>> PingAsync()
        {
            List<string> theListOfIPs = new List<string>();
            for (int i = 1; i <= 255; i++)
                theListOfIPs.Add("192.168.56." + i);
            var tasks = theListOfIPs.Select(ip => new Ping().SendPingAsync(ip, 200));
            var results = await Task.WhenAll(tasks);
            return results.ToList();
        }

        public ICommand OkCommand => new RelayCommand(async () =>
        {
            var  results = await PingAsync();
            if (results.Count > 0)
            {
                Items.Clear();
                int i = 1;
                foreach (var result in results)
                {
                    if (result.Status.Equals(IPStatus.Success))
                    {
                        string ip = "192.168.56." + i.ToString();
                        App.Current.Dispatcher.Invoke(() => { Items.Add(ip + " : " + result.Status.ToString()); });
                        /*using (TcpClient client = new TcpClient())
                        {
                            try
                            {
                                await client.ConnectAsync(IPAddress.Parse(ip), 40999).ConfigureAwait(false);
                                if (client.Connected)
                                    App.Current.Dispatcher.Invoke(() => { Items.Add(ip + " : " + result.Status.ToString()); });
                            }
                            catch (SocketException) { }

                        }*/
                    }
                    i++;
                }
                MessageBox.Show("Scan ip success.");
            }
        });
    }
}
