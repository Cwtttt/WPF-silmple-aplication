using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Model;

namespace WpfApp1
{


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

            using (var context = new CarContext())
            {
                var car = context.Cars.Where(c => c.Mark == "BMW").ToArray();

                context.Cars.RemoveRange(car);
                context.SaveChanges();
            }

            //LoadDataFromFile();

            ////list.Add(new Car()
            ////{
            ////    Id = _id++,
            ////    Model = "BMW",
            ////    Mark = "Seria 8"
            ////});

            //lviCarList.ItemsSource = list;


        }

        private void BtnNewCarClick(object sender, RoutedEventArgs e)
        {
            Car car1 = new Car
            {
                Id = _id++,
                Mark = txbMarka.Text,
                Model = txbModel.Text
            };
            try
            {
                list.Add(car1);
            }
            catch(NullReferenceException)
            {
                list = new List<Car>
                {
                    car1
                };
                lviCarList.ItemsSource = list;
                lviCarList.Items.Refresh();
            }

            lviCarList.Items.Refresh();
            Clear();
        }

        private void LviDoubleClick(object sender, MouseButtonEventArgs e)
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

        private void KeyEnter(object sender, KeyEventArgs e)
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
            try
            {
                using (StreamReader sr = new StreamReader(@"D:\file.txt"))
                {
                    list = JsonConvert.DeserializeObject<List<Car>>(sr.ReadToEnd().ToString());
                }
            }
            catch (FileNotFoundException)
            {
                using (FileStream fs = File.Create(@"D:\file.txt")) { };
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


