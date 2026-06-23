using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TestReadHistoricalDataFile1.Models
{
    public class ChartDataPoint
    {
        public DateTime DateTime1 { get; set; }
        public double Value { get; set; }
        public ChartDataPoint(DateTime datetime1, double value)
        {
            DateTime1 = datetime1;
            this.Value = value;
        }
    }
}
