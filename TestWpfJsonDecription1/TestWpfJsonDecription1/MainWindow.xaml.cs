using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestWpfJsonDecription1;

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
        var options = new JsonSerializerOptions { WriteIndented = true };
        options.Converters.Add(new WithDescriptionConverter<Person>());
        var person = new Person
        {
            Name = "Alice",
            Age = 30,
            Address = new Address { Street = "Main St", City = "Springfield" }
        };
        string json = JsonSerializer.Serialize(person, options);
        File.WriteAllText("person.json", json, Encoding.UTF8);
    });
}

public class Address
{
    [Description("Street name")]
    public string Street { get; set; }

    [Description("City name")]
    public string City { get; set; }
}

public class Person
{
    [Description("Full name")]
    public string Name { get; set; }

    [Description("Age in years")]
    public int Age { get; set; }

    [Description("Home address")]
    public Address Address { get; set; }
}

public class WithDescriptionConverter<T> : JsonConverter<T>
{
    private void WriteObject(Utf8JsonWriter writer, object obj, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        foreach (var prop in obj.GetType().GetProperties())
        {
            var jsonName = prop.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? prop.Name;
            var descAttr = prop.GetCustomAttribute<DescriptionAttribute>();
            var propValue = prop.GetValue(obj);

            writer.WritePropertyName(jsonName);
            writer.WriteStartObject();

            writer.WritePropertyName("value");

            if (propValue != null && !IsSimpleType(prop.PropertyType))
            {
                WriteObject(writer, propValue, options); // recursion for nested classes
            }
            else
            {
                JsonSerializer.Serialize(writer, propValue, options);
            }

            if (descAttr != null)
            {
                writer.WriteString("description", descAttr.Description);
            }

            writer.WriteEndObject();
        }
        writer.WriteEndObject();
    }

    private static bool IsSimpleType(Type type) =>
        type.IsPrimitive || type.IsEnum || type == typeof(string) ||
        type == typeof(decimal) || type == typeof(DateTime) || type == typeof(Guid);

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        WriteObject(writer, value, options);
    }

    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<T>(ref reader, options);
    }
}