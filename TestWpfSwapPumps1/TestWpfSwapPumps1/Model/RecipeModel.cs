using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWpfSwapPumps1.Model.Resource;

namespace TestWpfSwapPumps1.Model;

[ObservableObject]
public partial class RecipeModel
{
    [ObservableProperty] public RecipePump _pump = new();
}

[ObservableObject]
public partial class RecipePump
{
    [ObservableProperty] public ObservableCollection<ReactorItem> items = new();
}
