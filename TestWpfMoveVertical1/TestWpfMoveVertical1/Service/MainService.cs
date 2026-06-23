using LiveChartsCore.Defaults;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace TestWpfMoveVertical1.Service;

public static class MainService
{
    public static SKColor HexToSKColor(string hexString)
    {
        SKColor color;
        SKColor.TryParse(hexString, out color);
        return color;
    }

    public static ObservableCollection<TimeSpanPoint> TimeSpanPoint2TimeSerie(ObservableCollection<TimeSpanPoint> tsps)
    {
        ObservableCollection<TimeSpanPoint> ts = new();
        TimeSpan newTimeSpan = new();
        if (tsps is not null)
        {
            for (int i = 0; i < tsps.Count; i++)
            {
                if (i == 0)
                    ts.Add(new TimeSpanPoint(TimeSpan.Zero, tsps[i].Value));
                else
                    ts.Add(new TimeSpanPoint(newTimeSpan, tsps[i].Value));
                newTimeSpan += tsps[i].TimeSpan;
            }
            if (tsps.Count != 0)
                ts.Add(new TimeSpanPoint(newTimeSpan, tsps.Last().Value));
        }
        return ts;
    }
}
