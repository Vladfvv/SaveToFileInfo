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
using System.Text.Json;
using System.Printing;
using System;
using System.Text.Json.Serialization;

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
        ObservableCollection<String> resultsSerializable;
        ObservableCollection<String> positions;
        ObservableCollection<String> cities;
        ObservableCollection<String> streets;

        public class Employee
        {
            public string surname { get; set; }
            public string salary { get; set; }
            public string position { get; set; }
            public string city { get; set; }
            public string street { get; set; }
            public string house { get; set; }

            public Employee() { }

            public Employee(string surname, string salary, string position, string city, string street, string house)
            {
                this.surname = surname;
                this.salary = salary;
                this.position = position;
                this.city = city;
                this.street = street;
                this.house = house;
            }

        }




        Employee employee;

        public MainWindow()
        {
            InitializeComponent();
            employee = new Employee();
            Grid.DataContext = employee;
            results = new ObservableCollection<string>();
            lResults.DataContext = results;
            positions = new ObservableCollection<string> { "Директор", "Менеджер", "Водитель" };
            positionCombobox.ItemsSource = positions;
            cities = new ObservableCollection<string> { "Минск", "Брест", "Гродно", "Гомель", "Витебск" };
            cityCombobox.ItemsSource = cities;
            streets = new ObservableCollection<string> { "Центральная", "Московская", "Минская", "Независимости" };
            streetСomboBox.ItemsSource = streets;
            resultsSerializable = new ObservableCollection<string>();
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string surname = employee.surname.ToString();
                string salary = employee.salary.ToString();
                string position = employee.position.ToString();
                string city = employee.city.ToString();
                string street = employee.street.ToString();
                string house = employee.house.ToString();
                Employee employee1 = new Employee(surname, salary, position, city, street, house);

                using (FileStream fs = new FileStream("D:\\Vlad\\СВПП\\20240413\\SaveToFileInfo\\user.json", FileMode.OpenOrCreate))
                {
                    JsonSerializer.SerializeAsync<Employee>(fs, employee1);
                    MessageBox.Show("Data has been saved to file");
                }                               /*

                string path = "D:\\Vlad\\СВПП\\20240413\\SaveToFileInfo\\myText.txt";
                string text = "This is ny text";
                using (StreamWriter writer = new StreamWriter(path, true))
                { 
                    writer.WriteAsync(text); 
                }*/

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }





        private async void readButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FileStream fs = new FileStream("D:\\Vlad\\СВПП\\20240413\\SaveToFileInfo\\user.json", FileMode.OpenOrCreate))
                {
                    Employee employee = await JsonSerializer.DeserializeAsync<Employee>(fs);
                    Console.WriteLine(employee.surname, employee.salary, employee.position, employee.city, employee.street, employee.house);
                    resultsSerializable.Add(employee.surname + "\t" + employee.salary + "\t" + employee.position + "\t" + employee.city + "\t" + employee.street + "\t" + employee.house);
                    lResults.DataContext = resultsSerializable;
                }
            }
            /* 
             string path = "D:\\Vlad\\СВПП\\20240413\\SaveToFileInfo\\myText.txt";
             using (StreamReader reader = new StreamReader(path)) 
             {
                 string text = reader.ReadToEnd();
                 Console.WriteLine(text);
                 results.Add(text);

             }*/
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}


