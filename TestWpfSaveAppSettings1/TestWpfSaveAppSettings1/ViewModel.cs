using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using System.Windows.Input;

namespace TestWpfSaveAppSettings1
{
    [ObservableObject]
    public partial class ViewModel
    {
        #region Fields
        Configuration AppConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        UISettings uiSetting; 
        #endregion

        #region Properties
        [ObservableProperty] public ObservableCollection<string> _languages = new() { "English", "Chinese" };
        [ObservableProperty] public ObservableCollection<string> _themes = new() { "Dark", "Light" };
        [ObservableProperty] public string _selectedLanguage = String.Empty;
        [ObservableProperty] public string _selectedTheme = String.Empty;
        #endregion

        public ViewModel()
        {
            uiSetting = new UISettings();
            if (AppConfig.Sections["UISettings"] is null)
                AppConfig.Sections.Add("UISettings", new UISettings());
            var uiSettings = (UISettings)AppConfig.GetSection("UISettings");
            SelectedLanguage = uiSettings.Language;
            SelectedTheme = uiSettings.Theme;
        }

        public ICommand SaveCommand => new RelayCommand(() =>
        {
            if (AppConfig.Sections["UISettings"] is not null)
                AppConfig.Sections.Remove("UISettings");
            uiSetting.Language = SelectedLanguage;
            uiSetting.Theme = SelectedTheme;
            AppConfig.Sections.Add("UISettings", uiSetting);
            AppConfig.Save();
        });
    }
}
