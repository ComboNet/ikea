using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using log4net;

namespace TestWpfLog4Net2
{
    public partial class MainWindow : Window
    {
        FileSystemWatcher watcher;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string fileName = @"C:\log\log.txt";

        public MainWindow()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
            ChangeLogLocation(fileName);
        }
        private void ChangeLogLocation(string filePath)
        {
            var rootRepository = log4net.LogManager.GetRepository();
            foreach (var appender in rootRepository.GetAppenders())
            {
                if (appender.Name.Equals("file") && appender is log4net.Appender.FileAppender)
                {
                    var fileAppender = appender as log4net.Appender.FileAppender;
                    fileAppender.File = filePath;
                    fileAppender.ActivateOptions();
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            log.Debug("Hello World");
        }
    }
}