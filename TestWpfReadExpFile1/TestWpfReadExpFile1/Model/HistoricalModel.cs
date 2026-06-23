using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Measure;
using LiveChartsCore;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;

namespace TestWpfReadExpFile1.Model;

[ObservableObject]
public partial class HistoricalModel
{
    public DataTable dataTable;

    #region Properties
    [ObservableProperty] public bool _isPageEnabled = false;
    public bool IsDetailReadingComplete { get; set; } = false;
    [ObservableProperty] public ObservableCollection<EXPERIMENT> _experiments = new();
    private EXPERIMENT _selectedExperiment;
    public EXPERIMENT SelectedExperiment
    {
        get { return _selectedExperiment; }
        set
        {
            _selectedExperiment = value;
            OnPropertyChanged(nameof(SelectedExperiment));
        }
    }
    [ObservableProperty] public ObservableCollection<LineSeries<DateTimePoint>> _series=new();
    [ObservableProperty] public ObservableCollection<ICartesianAxis> _yAxes = new();
    [ObservableProperty] public ObservableCollection<Axis> _xAxes=new();
    [ObservableProperty] public LegendPosition _legendPosition = LegendPosition.Hidden;
    [ObservableProperty] public Visibility _filterVisible = Visibility.Collapsed;
    [ObservableProperty] public DataView _dataView;
    [ObservableProperty] public ObservableCollection<CheckboxData> _checkboxes = new();
    public bool IsPreview { get; set; } = false;
    #endregion
}
