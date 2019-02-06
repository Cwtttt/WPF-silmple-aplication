﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Model;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private Guid selectedId;
        private CarContext context = new CarContext();

        public MainWindow()
        {
            InitializeComponent();
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
            MessageBox.Show("Dodano samochód do bazy");
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


