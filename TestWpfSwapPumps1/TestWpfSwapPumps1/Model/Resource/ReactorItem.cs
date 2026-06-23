using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWpfSwapPumps1.Model.Resource;

[ObservableObject]
public partial class ReactorItem
{
    public int Left { get; set; }
    [ObservableProperty] public int _top;
    public string Type { get; set; }
    public string? Tag { get; set; } = null;
    [ObservableProperty] private string? _name = null;
}

