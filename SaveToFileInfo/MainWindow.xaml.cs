using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static SaveToFileInfo.MainWindow;

namespace SaveToFileInfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
/*
    public List<Employee> GetEmployeesFromFile(string fileName)
    {
        try {
            List<Employee> lines = Parse(List<Employee>)File.ReadAllText(fileName); 
        } catch {
            MessageBox.Show("File not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        List<Employee> lines = File.ReadAllLines(fileName).ToList();

    }
*/


    public partial class MainWindow : Window
    {
        ObservableCollection<String> results;

        public class Employee
        {
            public  string surname { get; set; }
            public  double salary { get; set; }
            public  string position { get; set; }
            public  string city { get; set; }
            public  string street { get; set; }
            public  string house { get; set; }

        }

        Employee employee;

        public MainWindow()
        {
            InitializeComponent();
            employee = new Employee();
            Grid.DataContext = employee;
            results = new ObservableCollection<string>();
            //results
            lResults.DataContext = results;

        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            try {
            string surname = employee.surname.ToString();  
            string salary = employee.salary.ToString();  
            string position = employee.position.ToString();  
            string city = employee.city.ToString();  
            string street = employee.street.ToString();  
            string house = employee.house.ToString();

                results.Add(surname + "\t" + salary + "\t" + position + "\t" + city + "\t" + street + "\t" + house);   


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}


