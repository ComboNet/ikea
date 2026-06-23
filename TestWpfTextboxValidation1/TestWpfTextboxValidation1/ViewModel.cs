using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWpfTextboxValidation1
{
    [ObservableObject]
    public partial class ViewModel
    {
        private string _text=string.Empty;
        public string Text
        {
            get { return _text; }
            set 
            {
                _text = value;
                OnPropertyChanged(nameof(Text));

            }
        }

    }
}
