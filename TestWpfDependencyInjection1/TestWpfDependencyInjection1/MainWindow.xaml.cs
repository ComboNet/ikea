using System.Windows;
using TestWpfDependencyInjection1.Data;

namespace TestWpfDependencyInjection1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EmployeeDbContext dbContext;

        public MainWindow(EmployeeDbContext dbContext)
        {
            this.dbContext = dbContext;
            InitializeComponent();
            GetEmployees();
        }

        private void GetEmployees()
        {
            var employees = dbContext.Employee.ToList();
            EmployeeDG.ItemsSource = employees;
        }
    }
}