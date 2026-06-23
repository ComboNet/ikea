using SkiaSharp;
using System.IO.Ports;
using System.Text;

namespace TestWpfReadExpFile1.Service;

public static class MainService
{
    public static SerialPort port;
    public static string msg = String.Empty;

    static MainService()
    {
        port = new SerialPort("COM15", 115200, Parity.None, 8);
        port.DataReceived += (s, e) => msg = port.ReadExisting().ToString();
        if(!port.IsOpen) port.Open();
    }

    public static void Init() { }

    public static bool CheckSum(string str)
    {
        string checksum = str.Substring(str.IndexOf("$$$$") - 4, 4);
        string compare = CalculateCheckSum(str.Substring(0, str.IndexOf(checksum))).PadLeft(4, '0');
        return checksum.Equals(compare);
    }

    public static string CalculateCheckSum(string str) => str.Sum(i => Encoding.UTF8.GetBytes(i.ToString())[0]).ToString("X");

    public static string FindStringBetween(string str, string str1, string str2) => str.Split(new string[] { str1 }, StringSplitOptions.None)[1].Split(str2)[0].Trim();

    public static SKColor HexToSKColor(string hexString)
    {
        SKColor color;
        SKColor.TryParse(hexString, out color);
        return color;
    }
}
