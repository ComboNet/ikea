using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using System.Linq;

namespace TestLiveChartSetMinMaxLimit1
{
    [ObservableObject]
    public partial class ViewModel
    {
        public ViewModel()
        {
            Values = new ObservableCollection<DateTimePoint>() 
                {
                    new DateTimePoint(new DateTime(2021, 1, 1), 3),
                    new DateTimePoint(new DateTime(2021, 1, 2), 6),
                    new DateTimePoint(new DateTime(2021, 1, 3), 5),
                    new DateTimePoint(new DateTime(2021, 1, 4), 3),
                    new DateTimePoint(new DateTime(2021, 1, 5), 5),
                    new DateTimePoint(new DateTime(2021, 1, 6), 8),
                    new DateTimePoint(new DateTime(2021, 1, 7), 6),
                    new DateTimePoint(new DateTime(2021, 1, 8), 4),
                    new DateTimePoint(new DateTime(2021, 1, 9), 1),
                    new DateTimePoint(new DateTime(2021, 1, 10), 2),
                    new DateTimePoint(new DateTime(2021, 1, 11), 8),
                    new DateTimePoint(new DateTime(2021, 1, 12), 5)
                };
            Series.Add(new ColumnSeries<DateTimePoint>() { Values = Values });
        }

        [ObservableProperty] public ObservableCollection<ISeries> _series = new();

        public Axis[] XAxes { get; set; } =
        {
            new DateTimeAxis(TimeSpan.FromDays(1) , date => date.ToString("MMMM dd"))
        };

        [ObservableProperty] public ObservableCollection<DateTimePoint> _values = new();

        public ICommand Filter1Command => new RelayCommand(() =>
        {
            DateTime min = new DateTime(2021, 1, 1);
            DateTime max = new DateTime(2021, 1, 6);
            Series[0].Values = Values.Where(v => v.DateTime >= min && v.DateTime <= max);
        });

        public ICommand Filter2Command => new RelayCommand(() =>
        {
            DateTime min = new DateTime(2021, 1, 3);
            DateTime max = new DateTime(2021, 1, 8);
            Series[0].Values = Values.Where(v => v.DateTime >= min && v.DateTime <= max);
        });
    }
}
