using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestWpfToastNotification1
{
    [ObservableObject]
    public partial class ViewModel
    {
        public ViewModel()
        {
            
        }

        [ObservableProperty] public ObservableCollection<string> _items = new();

        public ICommand OkCommand => new RelayCommand(() =>
        {
            var notificationManager = new NotificationManager();
            notificationManager.Show(new NotificationContent
            {
                Title = "Sample notification",
                Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Type = NotificationType.Information,
            }, expirationTime: TimeSpan.FromSeconds(0.5));
        });
    }
}
