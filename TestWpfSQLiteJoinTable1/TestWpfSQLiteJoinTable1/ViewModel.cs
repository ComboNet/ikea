using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SQLite;

namespace TestWpfSQLiteJoinTable1
{
    [ObservableObject]
    public partial class ViewModel
    {
        public ViewModel()
        {
            
        }

        #region Fields
        string[] Parameters = { "ACID", "AIR", "BASE" };
        string strConn = "Data Source=C:\\Data\\habitat.db;Version=3;";
        StringBuilder sb = new StringBuilder();
        #endregion

        #region Properties
        [ObservableProperty] public DataView _dataView;
        #endregion

        #region Functions
        public DataSet QuerySelect(string strSQLCommand)
        {
            using (SQLiteConnection thisConnection = new SQLiteConnection(strConn))
            using (SQLiteCommand thisCommand = new SQLiteCommand(strSQLCommand, thisConnection))
            using (SQLiteDataAdapter thisAdapter = new SQLiteDataAdapter(thisCommand))
            {
                DataSet ds = new DataSet();
                thisAdapter.Fill(ds);
                return ds;
            }
        }
        #endregion

        #region Commands
        public ICommand OkCommand => new RelayCommand(() =>
        {
            sb.Clear();

            /*SELECT ACID."timestamp", ACID.flow_pv, BASE.flow_pv, AFOAM.flow_pv, AIR.flow_pv
FROM ACID
INNER JOIN BASE ON ACID."timestamp" = BASE."timestamp"
INNER JOIN AFOAM ON ACID."timestamp" = AFOAM."timestamp"
INNER JOIN AIR ON ACID."timestamp" = AIR."timestamp"
ORDER BY ACID."timestamp"*/

        }); 
        #endregion
    }
}
