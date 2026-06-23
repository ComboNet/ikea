using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TestReadHistoricalDataFile1.Models;

namespace TestReadHistoricalDataFile1
{
    [ObservableObject]
    public partial class ViewModel
    {
        public ViewModel()
        {
            var mapper = Mappers.Xy<ChartDataPoint>()
                .X(model => model.DateTime1.Ticks)   //use DateTime.Ticks as X
                .Y(model => model.Value);           //use the value property as Y

            Charting.For<ChartDataPoint>(mapper);
            Formatter = value => new DateTime((long)value).ToString("MMM dd,yyyy" + Environment.NewLine + " HH:mm:ss");

            var fileJson = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"Resources\parameters.json");
            Parameters = JsonConvert.DeserializeObject<List<Parameter>>(System.IO.File.ReadAllText(fileJson));

            dataTable = new DataTable();
            string[] lines = System.IO.File.ReadAllLines(filePath);
            int count = 0;
            foreach (string line in lines)
            {
                string[] columns = line.Split(",");
                if (count < 1)
                {
                    interval = int.Parse(FindStringBetween(columns[0], "=", ")"));
                    foreach (string column in columns)
                    {
                        string col = column.Contains('(') ? column.Substring(0, column.IndexOf("(")) : column;
                        dataTable.Columns.Add(col);
                    }
                    count = 1;
                }
                else
                {
                    string[] splits = line.Split(',');
                    DataRow row = dataTable.NewRow();
                    for (int i = 0; i < columns.Length; i++)
                        row[i] = splits[i];
                    dataTable.Rows.Add(row);
                }
            }
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (i > 0)
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                        if (j < 1)
                        {
                            DateTime dateTime = DateTime.ParseExact((string)dataTable.Rows[i - 1][j], "dd/M/yy H:mm:ss", CultureInfo.InvariantCulture);
                            dataTable.Rows[i][j] = dateTime.AddSeconds(interval).ToString("dd/M/yy H:mm:ss");
                        }
                        else
                            if (String.IsNullOrEmpty(dataTable.Rows[i][j].ToString()))
                            dataTable.Rows[i][j] = dataTable.Rows[i - 1][j];
            }
            if (dataTable != null)
            {
                DataView = dataTable.AsDataView();
                if (dataTable.Rows.Count > 0)
                {
                    Checkboxes = new ObservableCollection<CheckboxData>();
                    var converter = new System.Windows.Media.BrushConverter();
                    int cc = 0;
                    foreach (DataColumn column in dataTable.Columns)
                        if (!column.ColumnName.Contains("TimeStamp") && !column.ColumnName.Contains("MARKER"))
                        {
                            string col = column.ColumnName.Contains('(')
                                ? column.ColumnName.ToString().Substring(0, column.ColumnName.IndexOf('('))
                                : column.ColumnName.ToString();
                            Parameter p = Parameters.FirstOrDefault(p => p.Tag.Equals(col));
                            Checkboxes.Add(new CheckboxData()
                            {
                                Id = cc,
                                Label = col,
                                Checked = false,
                                Color = (System.Windows.Media.Brush)converter.ConvertFromString(p.Color)
                            });
                            cc++;
                        }
                }
            }
        }

        #region Fields
        int interval = 0;
        string filePath = @"C:\txt\data.txt";
        DataTable dataTable;
        List<Parameter> Parameters = new List<Parameter>();
        #endregion

        #region Properties
        [ObservableProperty] public DataView _dataView;
        [ObservableProperty] public SeriesCollection _series = new();
        [ObservableProperty] public AxesCollection _yAxes = new();
        [ObservableProperty] public AxesCollection _xAxes = new();
        [ObservableProperty] public Func<double, string> _formatter;
        [ObservableProperty] public ObservableCollection<CheckboxData> _checkboxes;
        #endregion

        #region Functions
        public Func<double, string> yAxisFormatFunc = (x) => string.Format("{0:0.00}", x);

        private string FindStringBetween(string str, string str1, string str2)
        {
            return str.Split(new string[] { str1 }, StringSplitOptions.None)[1].Split(str2)[0].Trim();
        }
        #endregion

        #region Commands

        public ICommand PlotCommand => new RelayCommand(() =>
        {
            var selectedCheckBoxes = from c in Checkboxes
                                     where c.Checked == true
                                     select c.Label;
            if (selectedCheckBoxes.Count() > 0)
            {
                var converter = new System.Windows.Media.BrushConverter();

                XAxes.Clear();
                YAxes.Clear();
                Series.Clear();

                int scaleAt = 0;
                foreach (var selectedCheckBox in selectedCheckBoxes)
                {
                    var p = Parameters.FirstOrDefault(p => p.Tag.Equals(selectedCheckBox));
                    var color = (Brush)converter.ConvertFromString(p.Color);
                    YAxes.Add(new Axis()
                    {
                        Name = selectedCheckBox,
                        Title = p.FullName,
                        LabelFormatter = yAxisFormatFunc,
                        Foreground = color,
                        ShowLabels = true,
                        Visibility = Visibility.Visible,
                        /*Separator = new Separator()
                        {
                            Stroke = color,
                            StrokeThickness = 1,
                            StrokeDashArray = new DoubleCollection() { 1 },
                            Opacity = 0.2,
                        },*/
                        Position = p.AxisPosition.Contains("Left") ? AxisPosition.LeftBottom : AxisPosition.RightTop,
                    });
                    var points = new ObservableCollection<ChartDataPoint>();
                    foreach (DataRow row in dataTable.Rows)
                        points.Add(new ChartDataPoint(DateTime.ParseExact((string)row["TimeStamp"], "dd/M/yy H:mm:ss", null), Convert.ToDouble(row[selectedCheckBox])));
                    Series.Add(new LineSeries()
                    {
                        Name = selectedCheckBox,
                        Title = p.FullName,
                        LineSmoothness = 0,
                        PointGeometry = null,
                        Stroke = color,
                        Fill = Brushes.Transparent,
                        ScalesYAt = scaleAt,
                        Values = points.AsChartValues(),
                    });
                    scaleAt++;
                }
            }
        });
        #endregion
    }

    
    #pragma warning disable format
    public class CheckboxData
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public bool Checked { get; set; }
        public System.Windows.Media.Brush Color { get; set; }
    }
    #pragma warning restore format
}
