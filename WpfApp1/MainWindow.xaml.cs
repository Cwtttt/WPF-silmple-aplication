using System;
using System.Collections.Generic;
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

        private Guid selectedId;
        List<Car> list = new List<Car>();
        private CarContext context = new CarContext();

        public MainWindow()
        {

            InitializeComponent();

            //using (var context = new CarContext())
            //{
            //    var car = context.Cars.Where(c => c.Mark == "BMW").ToArray();

            //    context.Cars.RemoveRange(car);
            //    context.SaveChanges();
            //}

            UpdateList();



        }

        private void BtnNewCarClick(object sender, RoutedEventArgs e)
        {
            Car car1 = new Car
            {
                Id = Guid.NewGuid(),
                Mark = txbMarka.Text,
                Model = txbModel.Text
            };

            context.Cars.Add(car1);
            context.SaveChanges();

            UpdateList();
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
            Car a = context.Cars.SingleOrDefault(x => x.Id == selectedId);

            if (a != null)
            {
                a.Mark = txbMarka.Text;
                a.Model = txbModel.Text;
                context.SaveChanges();
            }
            UpdateList();
            Clear();
        }


        private void BtnDeleteCarClick(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).DataContext is Car car)
            {
                Car a = context.Cars.ToList().Find(x => x.Id == car.Id);
                context.Cars.Remove(a);
                context.SaveChanges();
            }
            UpdateList();
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
            var all = from a in context.Cars select a;
            context.Cars.RemoveRange(all);
            context.SaveChanges();
            UpdateList();
        }


        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Clear()
        {
            txbMarka.Text = String.Empty;
            txbModel.Text = String.Empty;
        }

        public void UpdateList()
        {
            lviCarList.ItemsSource = context.Cars.ToList();
            lviCarList.Items.Refresh();
        }
    }
}


//obsluga wyjatkow try catch \

//TODO AK dodac cos tam


