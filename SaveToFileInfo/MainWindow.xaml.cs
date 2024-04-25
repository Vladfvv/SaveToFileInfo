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
using System.ComponentModel;
using System.Runtime.CompilerServices;






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
        ObservableCollection<Employee> listSerializable2;
        ObservableCollection<Employee> listSerializable3;
        ObservableCollection<String> positions;
        ObservableCollection<String> cities;
        ObservableCollection<String> streets;
        List<Employee> employeesList;
        string path = "D:\\Vlad\\СВПП\\20240413\\SaveToFileInfo\\user.json";

        public class Employee : INotifyPropertyChanged
        {

            public string surname;

            public string salary;

            public string position;

            public string city;

            public string street;

            public string house;


            public string Surname
            {
                get { return surname; }
                set
                {
                    surname = value;
                    OnPropertyChanged("Surname");
                }
            }
            public string Salary
            {
                get { return salary; }
                set
                {
                    salary = value;
                    OnPropertyChanged("Salary");
                }
            }
            public string Position
            {
                get { return position; }
                set
                {
                    position = value;
                    OnPropertyChanged("Position");
                }
            }
            public string City
            {
                get { return city; }
                set
                {
                    city = value;
                    OnPropertyChanged("City");
                }
            }
            public string Street
            {
                get { return street; }
                set
                {
                    street = value;
                    OnPropertyChanged("Street");
                }
            }
            public string House
            {
                get { return house; }
                set
                {
                    house = value;
                    OnPropertyChanged("House");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged([CallerMemberName] string prop = "")
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }


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
            listSerializable2 = new ObservableCollection<Employee>();

        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                results.Add(employee.surname + "\t" + employee.salary + "\t" + employee.position + "\t" + employee.city + "\t" + employee.street + "\t" + employee.house);
                listSerializable2.Add(employee);
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
                    MessageBox.Show("Список пуст. Добавьте в список", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void readButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Employee> employeeFromList = JsonSerializationCustomClass.LoadFromJson(path);//читаем из json-файла

                if (employeeFromList.Count > 0)//если в json файле есть объекты
                {
                    foreach (Employee person in employeeFromList)//проходим по объектам listBox-a  и добавляем его в список для последующей сериализации
                    {
                        listSerializable2.Add(person);//добавляем каждый объект
                        results.Add(employee.surname + "\t" + employee.salary + "\t" + employee.position + "\t" + employee.city + "\t" + employee.street + "\t" + employee.house);
                    }
                    MessageBox.Show("Список объектов успешно прочитан из файла JSON.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                
                }
                else
                {
                    MessageBox.Show("Файл пуст.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /*
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
        */


        private void clearFormButton_Click(object sender, RoutedEventArgs e)
        {
            surnameTextBox.Text = string.Empty;
            salaryTextBox.Text = string.Empty;
            positionCombobox.Text = string.Empty;
            cityCombobox.Text = string.Empty;
            streetСomboBox.Text = string.Empty;
            houseTextBox.Text = string.Empty;
        }

        private void clearListBoxButton_Click(object sender, RoutedEventArgs e)
        {
            results.Clear();
        }

        private void clearFileButton_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(path, "");
            MessageBox.Show("Файл очищен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}


