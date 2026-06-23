using CommunityToolkit.Mvvm.ComponentModel;
using System.Configuration;

namespace TestWpfSaveAppSettings1
{
    [ObservableObject]
    public partial class UISettings:ConfigurationSection
    {
        /*[ObservableProperty] public string _language= "English";
        [ObservableProperty] public string _theme = "Light";*/


        [ConfigurationProperty("language", DefaultValue = "English")]
        public string Language
        {
            get { return (string)this["language"]; }
            set { this["language"] = value; }
        }

        [ConfigurationProperty("theme", DefaultValue = "Light")]
        public string Theme
        {
            get { return (string)this["theme"]; }
            set { this["theme"] = value; }
        }
    }
}
