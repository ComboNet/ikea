using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace TestWpfQtLangDeserialize1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }
    }

    [ObservableObject]
    public partial class MainWindowViewModel
    {
        public ICommand OkCommand => new RelayCommand(() =>
        {
            // Path to your XML file
            string filePath = "Resource/app_th.ts";

            // Create the serializer for the TS type
            XmlSerializer serializer = new XmlSerializer(typeof(TS));

            // Read and deserialize the XML
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                TS tsData = (TS)serializer.Deserialize(fs);

                // Example: Accessing deserialized data
                Console.WriteLine($"Version: {tsData.version}");
                Console.WriteLine($"Language: {tsData.language}");

                if (tsData.context != null)
                {
                    foreach (var ctx in tsData.context)
                    {
                        Console.WriteLine($"Context Name: {ctx.name}");
                        if (ctx.message != null)
                        {
                            foreach (var msg in ctx.message)
                            {
                                Console.WriteLine($"Source Text: {string.Join(", ", msg.source?.Text ?? Array.Empty<string>())}");
                                Console.WriteLine($"Translation Type: {msg.translation?.type}");
                            }
                        }
                    }
                }
            }
        });
    }
}