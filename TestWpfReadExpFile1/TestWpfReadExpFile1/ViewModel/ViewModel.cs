using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using TestWpfReadExpFile1.Model;
using TestWpfReadExpFile1.Service;
using System.Windows.Media;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace TestWpfReadExpFile1.ViewModel;

[ObservableObject]
public partial class ViewModel
{
    #region Fields
    StringBuilder data = new();
    FILES_REPLY fileReply = new();
    DataTable dataTable = new();
    int interval = 15;
    #endregion

    #region Properties
    [ObservableProperty] public List<string> _items = new();
    [ObservableProperty] public Axis[] _xAxes;

    [ObservableProperty]
    public DrawMarginFrame _frame = new DrawMarginFrame()
    {
        Stroke = new SolidColorPaint
        {
            Color = SKColors.White.WithAlpha(80),
            StrokeThickness = 1
        }
    };
    [ObservableProperty]
    public SolidColorPaint _legendTextPaint = new SolidColorPaint
    {
        Color = new SKColor(50, 50, 50),
        SKTypeface = SKTypeface.FromFamilyName("Frutiger Next IKA")
    };
    [ObservableProperty] public SolidColorPaint _ledgendBackgroundPaint = new SolidColorPaint(new SKColor(240, 240, 240));

    public SolidColorPaint TooltipTextPaint { get; set; } = new SolidColorPaint
    {
        Color = new SKColor(242, 244, 195),
        SKTypeface = SKTypeface.FromFamilyName("Frutiger Next IKA"),
    };
    public SolidColorPaint TooltipBackgroundPaint { get; set; } = new SolidColorPaint(new SKColor(72, 0, 50));
    #endregion

    public ViewModel()
    {
        MainService.Init();
        XAxes = new Axis[] { new DateTimeAxis(TimeSpan.FromSeconds(15), d => d.ToString("yyyy-MM-dd HH:mm:ss")) { ForceStepToMin = true, LabelsRotation = 90 }, };
    }

    public ICommand ExperimentsCommand => new RelayCommand(() =>
    {
        MainService.port.Write("{\"FILES\":{\"type\":2,\"name\":\"experiments.csv\",\"size\":{ },\"dest\":0}}" + Environment.NewLine);
        Thread.Sleep(300);
        while (true)
        {
            if (new string[] { "success", "FILES", "experiments" }.Any(s => MainService.msg.Contains(s)))
            {
                fileReply = JsonConvert.DeserializeObject<FILES_REPLY>(MainService.msg.ToString());
                break;
            }
        }
        data.Clear();
        while (true)
        {
            if (MainService.msg.Contains("$$$$"))
            {
                data.Append(MainService.msg);
                break;
            }
        }
        string[] lines = data.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        AppData.HistoricalModel.Experiments.Clear();
        foreach (string line in lines)
        {
            if (!line.Contains("id,name") && !String.IsNullOrEmpty(line) && !line.Contains("$$$$"))
            {
                string[] ls = line.Split(",");
                AppData.HistoricalModel.Experiments.Add(new EXPERIMENT()
                {
                    id = int.Parse(ls[0]),
                    name = ls[1],
                    author = ls[2],
                    recipe = ls[3],
                    startdate = int.Parse(ls[4]),
                    cultivation = int.Parse(ls[5]),
                    stopdate = int.Parse(ls[6]),
                    sts = int.Parse(ls[7]),
                    note = ls[8],
                    enabled = 1
                });
            }
        }
        MainService.port.Write("\u0006" + Environment.NewLine);
    });

    public ICommand PreViewCommand => new RelayCommand(async () =>
    {
        if (AppData.HistoricalModel.SelectedExperiment is not null)
        {
            AppData.HistoricalModel.IsPreview = false;

            string filename = AppData.HistoricalModel.SelectedExperiment.name.Replace(" ", "`") + "_" + AppData.HistoricalModel.SelectedExperiment.startdate;
            MainService.port.Write("{\"FILES\":{\"type\":3,\"name\":\"" + filename + ".csv\",\"size\":{ },\"dest\":0,\"progress\":0}}" + Environment.NewLine);
            double filesize = 0.0;
            while (true)
            {
                if (new string[] { "success", "FILES", "filename" }.Any(s => MainService.msg.Contains(s)))
                {
                    fileReply = JsonConvert.DeserializeObject<FILES_REPLY>(MainService.msg);
                    filesize = (fileReply.FILES.size.HasValue) ? fileReply.FILES.size.Value : 0;
                    break;
                }
            }
            Thread.Sleep(300);
            data.Clear();
            string compare = String.Empty;
            int counter = 0;
            await Task.Run(() =>
            {
                while (true)
                {
                    if (data.Length.Equals(filesize)) break;
                    if (String.IsNullOrEmpty(MainService.msg)) break;
                    if (MainService.msg.Contains("$$$$"))
                        if (!compare.Equals(MainService.msg))
                        {
                            counter = 0;
                            compare = MainService.msg;
                            if (MainService.CheckSum(MainService.msg))
                                data.Append(MainService.msg.Substring(0, MainService.msg.Length - 8));
                            MainService.port.Write("\u0006");
                        }
                        else
                        {
                            counter++;
                            if (counter > 3) break;
                            MainService.port.Write("\u0006");
                        }
                    Thread.Sleep(300);
                }
                string[] lines = data.ToString().Split(Environment.NewLine);
                dataTable = new();
                string[] columns = lines[0].Split(",");
                int c = 0;
                foreach (string column in columns)
                    if (dataTable.Columns.Contains(column))
                    {
                        dataTable.Columns.Add(String.Concat(column, c.ToString()));
                        c++;
                    }
                    else
                        dataTable.Columns.Add(column);

                foreach (string line in lines)
                    if (!lines.Contains("TimeStamp"))
                    {
                        var str = line.Trim();
                        if (line.Contains("$$$$"))
                            str = line.Replace(String.Concat(line.Substring(line.IndexOf("$$$$") - 4, 4), "$$$$"), "").Trim();
                        if (!line.Contains("TimeStamp"))
                        {
                            string[] ls = str.Split(",");
                            if (ls.Length == dataTable.Columns.Count)
                            {
                                var row = dataTable.NewRow();
                                for (int i = 0; i < ls.Length; i++)
                                    row[i] = ls[i];
                                dataTable.Rows.Add(row);
                            }
                        }
                        else
                            interval = int.Parse(MainService.FindStringBetween(line, "interval=", ")"));
                    }
                for (int i = 0; i < dataTable.Rows.Count; i++)
                    if (i > 0)
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                            if (j < 1)
                            {
                                DateTime dateTime = DateTime.ParseExact((string)dataTable.Rows[i - 1][j], "d/M/yy H:mm:ss", CultureInfo.InvariantCulture);
                                dataTable.Rows[i][j] = dateTime.AddSeconds(interval).ToString("dd/M/yy H:mm:ss");
                            }
                            else
                                if (String.IsNullOrEmpty(dataTable.Rows[i][j].ToString()))
                                dataTable.Rows[i][j] = dataTable.Rows[i - 1][j];
               

                // Load data completed
                App.Current.Dispatcher.Invoke(() =>
                {
                    AppData.HistoricalModel.Checkboxes.Clear();
                    AppData.HistoricalModel.YAxes.Clear();
                    AppData.HistoricalModel.Series.Clear();
                    BrushConverter converter = new();
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        if (!column.ColumnName.Contains("TimeStamp") && !column.ColumnName.Contains("MARKER"))
                        {
                            AppData.HistoricalModel.Checkboxes.Add(new CheckboxData()
                            {
                                Label = column.ColumnName,
                                Checked = true,
                            });
                            ObservableCollection<DateTimePoint> points = new();
                            var selectedColumns = dataTable.AsEnumerable()
                                           .Select(row => new
                                           {
                                               DateTime = row.Field<string>(dataTable.Columns[0].ColumnName),
                                               Value = row.Field<string>(column.ColumnName)
                                           })
                                           .ToList();
                            foreach(var col in selectedColumns)
                                points.Add(new DateTimePoint()
                                {
                                    DateTime = DateTime.ParseExact(col.DateTime, "dd/M/yy HH:mm:ss", CultureInfo.InvariantCulture),
                                    Value = double.Parse(col.Value)
                                });
                            AppData.HistoricalModel.YAxes.Add(new LiveChartsCore.SkiaSharpView.Axis
                            {
                                DrawTicksPath = true,
                                // InLineNamePlacement = true,
                                LabelsAlignment = LiveChartsCore.Drawing.Align.Middle,
                                Name = column.ColumnName,
                                NamePadding = new LiveChartsCore.Drawing.Padding(0, 0),
                                NameTextSize = 16,
                                Padding = new LiveChartsCore.Drawing.Padding(4, 2, 4, 2),
                                TextSize = 16,
                            });
                            AppData.HistoricalModel.Series.Add(new LineSeries<DateTimePoint>() 
                            {
                                Tag = column.ColumnName,
                                IsVisible = true,
                                Values = points 
                            });
                        }


                        /*if (!column.ColumnName.Contains("TimeStamp") && !column.ColumnName.Contains("MARKER"))
                        {
                            AppData.HistoricalModel.Checkboxes.Add(new CheckboxData()
                            {
                                Id = c,
                                Label = column.ColumnName,
                                Checked = false,
                            });
                            AppData.HistoricalModel.YAxes.Add(new LiveChartsCore.SkiaSharpView.Axis
                            {
                                CrosshairLabelsBackground = SKColors.DarkOrange.AsLvcColor(),
                                CrosshairLabelsPaint = new SolidColorPaint(SKColors.DarkRed, 1),
                                CrosshairPaint = new SolidColorPaint(SKColors.DarkOrange, 1),
                                CrosshairSnapEnabled = true,
                                DrawTicksPath = true,
                                Name = column.ColumnName,
                                LabelsAlignment = LiveChartsCore.Drawing.Align.Middle,
                                NamePadding = new LiveChartsCore.Drawing.Padding(0, 0),
                                NameTextSize = 16,
                                Padding = new LiveChartsCore.Drawing.Padding(4, 2, 4, 2),
                                TextSize = 16,
                                ShowSeparatorLines = false,
                                IsVisible = false,
                            });
                            var points = new ObservableCollection<DateTimePoint>();
                            foreach (DataRow row in dataTable.Rows)
                            {
                                double tempVal = 0.0;
                                if (double.TryParse(row[column.ColumnName].ToString(), out tempVal))
                                {
                                    points.Add(new DateTimePoint()
                                    {
                                        DateTime = DateTime.ParseExact((string)row[dataTable.Columns[0].ColumnName], "d/M/yy H:mm:ss", null), // Work
                                                                                                                                              // DateTime = DateTime.ParseExact((string)row["TimeStamp"], "dd/M/yy H:mm:ss", null), // Work
                                        Value = Convert.ToDouble(row[column.ColumnName])
                                    });
                                }
                                else
                                {
                                    points.Add(new DateTimePoint()
                                    {
                                        DateTime = DateTime.ParseExact((string)row[dataTable.Columns[0].ColumnName], "d/M/yy H:mm:ss", null), // Work
                                                                                                                                              // DateTime = DateTime.ParseExact((string)row["TimeStamp"], "dd/M/yy H:mm:ss", null), // Work
                                        Value = 999.99
                                    });
                                }
                            }
                            AppData.HistoricalModel.Series.Add(new LineSeries<DateTimePoint>()
                            {
                                LineSmoothness = 0,
                                GeometrySize = 0,
                                Fill = null,
                                ScalesYAt = c,
                                Values = points,
                                IsVisible = false,
                            });

                        }*/
                        c++;
                    }
                    AppData.HistoricalModel.YAxes[0].ShowSeparatorLines = true;
                    Items.Clear();
                    // Items = dataTable.AsEnumerable().Select(r => r.Field<string>(dataTable.Columns[0].ColumnName)).ToList();
                });

             
                Thread.Sleep(500);

                AppData.HistoricalModel.DataView = dataTable.DefaultView;
                AppData.HistoricalModel.IsPreview = true;
            });
        }
    });
}
