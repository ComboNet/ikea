using CommunityToolkit.Mvvm.ComponentModel;

namespace TestWpfReadExpFile1.Model
{
    [ObservableObject]
    public partial class CheckboxData
    {
        #region Properties
        // public int Id { get; set; }
        public string Label { get; set; }
        // public string Tag { get; set; } = string.Empty;
        private bool _checked = false;
        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                OnPropertyChanged(nameof(Checked));
                if (AppData.HistoricalModel.IsPreview)
                {
                    AppData.HistoricalModel.YAxes.FirstOrDefault(y => y.Name.Equals(Label)).IsVisible = Checked;
                    AppData.HistoricalModel.Series.FirstOrDefault(s => s.Tag.Equals(Label)).IsVisible = Checked;
                }
            }
        }
        // public System.Windows.Media.Brush Color { get; set; }
        #endregion
    }
}
