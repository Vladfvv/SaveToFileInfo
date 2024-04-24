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
using System.Xml.Serialization;
using System.Net.Http.Json;

using System.Collections.Generic;
using System;
using System.Collections.Generic;






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
        ObservableCollection<Employee> listSerializable2;
        ObservableCollection<Employee> listSerializable3;
        ObservableCollection<String> positions;
        ObservableCollection<String> cities;
        ObservableCollection<String> streets;
        List<Employee> employeesList;
        string path = "D:\\Vlad\\СВПП\\20240413\\SaveToFileInfo\\user.json";

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
            listSerializable2 = new ObservableCollection<Employee>();
            listSerializable3 = new ObservableCollection<Employee>();

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
                listSerializable2.Add(new Employee(surname, salary, position, city, street, house));

                employee.surname = string.Empty;
                employee.position = string.Empty;
                employee.salary = string.Empty;
                employee.city = string.Empty;
                employee.street = string.Empty;
                employee.house = string.Empty;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }





        public class JsonSerializationCustomClass
        {

            // Метод для сохранения списка объектов в файл JSON
            public static void SaveToJson(List<Employee> list, string path)
            {               
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true // Добавляем отступы для удобочитаемости
                    };

                    string json = JsonSerializer.Serialize(list, options);                   
                    File.AppendAllTextAsync(path, json);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении файла JSON: " + ex.Message);
                }
            }




            public static List<Employee> LoadFromJson(string path)
            {
                try
                {
                    string jsonString = File.ReadAllText(path);
                    return JsonSerializer.Deserialize<List<Employee>>(jsonString);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при чтении файла JSON: " + ex.Message);
                    return null;
                }
            }
            public static void ClearJsonFile(string filePath)
            {
                File.WriteAllText(filePath, string.Empty);
            }
        }





        private void saveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listSerializable2.Count > 0)//если список в Listbox не пуст
                {
                    List<Employee> employeeFromList = JsonSerializationCustomClass.LoadFromJson(path);//читаем из json-файла

                    if (employeeFromList != null)//если в json файле есть объекты
                    {
                        // Добавляем новые объекты к существующему списку                   

                        foreach (Employee person in listSerializable2)//проходим по объектам listBox-a  и добавляем его в список для последующей сериализации
                        {
                            employeeFromList.Add(person);//добавляем каждый объект
                        }
                        //чистим файл
                        JsonSerializationCustomClass.ClearJsonFile(path);
                        // Сохраняем обновленный список объектов обратно в файл
                        JsonSerializationCustomClass.SaveToJson(employeeFromList, path);
                    }
                    else
                    {
                        //чистим файл
                        JsonSerializationCustomClass.ClearJsonFile(path);
                        JsonSerializationCustomClass.SaveToJson(listSerializable2.ToList(), path);// Сохраняем список объектов в файл
                    }

                    MessageBox.Show("Список объектов успешно сохранен в файл JSON.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Список пуст.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        private async void readButton_Click(object sender, RoutedEventArgs e)
        {
            try { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        public List<Employee> ReadAndDeserialize(string path)
        {
            var serializer = new XmlSerializer(typeof(List<Employee>));
            using (var reader = new StreamReader(path))
            {
                return (List<Employee>)serializer.Deserialize(reader);
            }
        }


        public void SerializeAndSave(string path, List<Employee> data)
        {
            var serializer = new XmlSerializer(typeof(List<Employee>));
            using (var em = new StreamWriter(path))
            {
                serializer.Serialize(em, data);
            }
        }

        private void saveToListButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


