using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWpfReadExpFile1.Model;

public static class AppData
{
    private static HistoricalModel historicalModel = new();
    public static HistoricalModel HistoricalModel
    {
        get { return historicalModel; }
        set { historicalModel = value; }
    }
}
