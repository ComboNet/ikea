using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Input;

namespace TestWpfEventLog1;

// Need run as Administrator

[ObservableObject]
public partial class ViewModel
{
    #region Fields
    string sourceName = "SampleApplicationSource";
    string myLogName = "myNewLog";
    string messageFile = "customlog.log";
    EventLog eventLog;
    #endregion

    [ObservableProperty] public ObservableCollection<string> _items = new();

    public ViewModel()
    {
        if (!IsRunningAsAdministrator())
        {
            string fileExe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            ProcessStartInfo processStartInfo = new ProcessStartInfo(fileExe);
            processStartInfo.UseShellExecute = true;
            processStartInfo.Verb = "runas";
            Process.Start(processStartInfo);
            App.Current.Shutdown();
        }
        eventLog = new EventLog(myLogName, ".", sourceName);
        eventLog.Source = sourceName;
    }
    public bool IsRunningAsAdministrator()
    {
        WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
        WindowsPrincipal windowsPrincipal = new WindowsPrincipal(windowsIdentity);
        return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
    }
    private void WriteToApplicationEventLog(string logMessage, EventLogEntryType logType, int eventID)
    {
        if (!EventLog.SourceExists(sourceName, Environment.MachineName))
        {
            EventSourceCreationData sourceData = new EventSourceCreationData(sourceName, myLogName);
            sourceData.MessageResourceFile = sourceData.CategoryResourceFile = sourceData.ParameterResourceFile = messageFile;
            sourceData.CategoryCount = 1;
            EventLog.CreateEventSource(sourceData);
        }
        myLogName = EventLog.LogNameFromSourceName(sourceName, ".");
        eventLog.WriteEntry(logMessage, logType, eventID);
    }
    public ICommand AddLogCommand => new RelayCommand(() =>
    {
        WriteToApplicationEventLog("test", EventLogEntryType.Error, 123);
        WriteToApplicationEventLog("test", EventLogEntryType.Warning, 123);
        WriteToApplicationEventLog("test", EventLogEntryType.Information, 123);
    });
    public ICommand ClearLogsCommand => new RelayCommand(() => { eventLog.Clear(); });
    public ICommand DeleteEventLogCommand => new RelayCommand(() => { EventLog.Delete(myLogName, Environment.MachineName); });
    public ICommand ReadLogsCommand => new RelayCommand(() =>
    {
        Items.Clear();
        EventLog log = new EventLog("myNewLog");
        foreach (EventLogEntry entry in eventLog.Entries)
        {
            string msg = entry.Message.Trim();
            string data = msg.Substring(msg.IndexOf(':') + 2, msg.Length - msg.IndexOf(':') - 3);
            Items.Add(entry.TimeGenerated +","+ entry.EntryType + "," + data + "," + entry.EventID.ToString());
        }
    });
}
