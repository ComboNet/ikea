using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Notification.Wpf;
using Notification.Wpf.Controls;
using System.Windows.Controls;
using System.Windows.Input;

namespace TestWpfNotification1
{
    [ObservableObject]
    public partial class ViewModel
    {
        NotificationManager notificationManager = new();
        /*public void Notify(string notifytype, string message)
        {
            var notificationContent = notifytype.Equals("None")
                ? new NotificationContent { Title = notifytype, Message = message, Type = NotificationType.None } : notifytype.Equals("Success")
                ? new NotificationContent { Title = notifytype, Message = message, Type = NotificationType.Success } : notifytype.Equals("Error")
                ? new NotificationContent { Title = notifytype, Message = message, Type = NotificationType.Error } : notifytype.Equals("Warning")
                ? new NotificationContent { Title = notifytype, Message = message, Type = NotificationType.Warning }
                : new NotificationContent { Title = notifytype, Message = message, Type = NotificationType.Information };
            notificationManager.ShowFilePopUpMessage(@"C:\Backup", true, true, TimeSpan.FromSeconds(3));
        }*/
        public ICommand OkCommand => new RelayCommand(() =>
        {
            Grid grid = (Grid)App.Current.Windows[0].Content as Grid;
            foreach(var child in grid.Children)
                if (child is NotificationArea)
                    notificationManager.Show("Information", "Hello World", NotificationType.Information, ((NotificationArea)child).Name);
        });
    }
}
