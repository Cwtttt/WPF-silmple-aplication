using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WpfApp1
{
    public class Car
    {
        public int Id { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int _id = 0;
        private int selectedId;
        List<Car> list = new List<Car>();

        public MainWindow()
        {
            InitializeComponent();
            //Car car1 = new Car();
            //car1.Mark = "BMW";
            //car1.Model = "series 8";
            //list.Add(car1);

            LoadDataFromFile();

            //list.Add(new Car()
            //{
            //    Id = _id++,
            //    Model = "BMW",
            //    Mark = "Seria 8"
            //});

            lviCarList.ItemsSource = list;


        }

        private void BtnNewCarClick(object sender, RoutedEventArgs e)
        {
            Car car1 = new Car();
            car1.Id = _id++;
            car1.Mark = txbMarka.Text;
            car1.Model = txbModel.Text;
            list.Add(car1);
            lviCarList.Items.Refresh();
            Clear();


        }

        private void lviDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lviCarList.SelectedItem is Car car)
            {
                selectedId = car.Id;
                txbMarka.Text = car.Mark;
                txbModel.Text = car.Model;
                btnEditCar.Visibility = Visibility.Visible;
            }
        }

        private void BtnEditCarClick(object sender, RoutedEventArgs e)
        {
            Car a = list.Find(x => x.Id == selectedId);
            a.Mark = txbMarka.Text;
            a.Model = txbModel.Text;
            lviCarList.Items.Refresh();
            Clear();
        }

        private void Clear()
        {
            txbMarka.Text = String.Empty;
            txbModel.Text = String.Empty;
        }

        private void BtnDeleteCarClick(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).DataContext is Car car)
            {
                Car a = list.Find(x => x.Id == car.Id);
                list.Remove(a);
                lviCarList.Items.Refresh();
            }
        }

        private void keyEnter(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                if ((sender as TextBox).Name == nameof(txbMarka).ToString())
                    txbModel.Focus();
                else
                    btnNewCar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void BtnClearAllClick(object sender, RoutedEventArgs e)
        {
            list.Clear();
            lviCarList.Items.Refresh();
        }

        public void LoadDataFromFile()
        {
            using (StreamReader sr = new StreamReader(@"D:\file.txt"))
            {
                list = JsonConvert.DeserializeObject<List<Car>>(sr.ReadToEnd().ToString());
            }

        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string json = JsonConvert.SerializeObject(list);
            using (StreamWriter sr = new StreamWriter(@"D:\file.txt"))
            {
                sr.Write(json);
            }
        }
    }
}


//obsluga wyjatkow try catch \

//TODO AK dodac cos tam


