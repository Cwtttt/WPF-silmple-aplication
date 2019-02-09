using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
                Model = txbModel.Text,
                Moc = Int32.Parse(txbMoc.Text),
                LiczbaMiejsc = Int32.Parse(txbLiczbaMiejsc.Text)
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
                txbMoc.Text = car.Moc.ToString();
                txbLiczbaMiejsc.Text = car.LiczbaMiejsc.ToString();
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
                a.Moc = Int32.Parse(txbMoc.Text);
                a.LiczbaMiejsc = Int32.Parse(txbLiczbaMiejsc.Text);
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
                else if ((sender as TextBox).Name == nameof(txbModel).ToString())
                    txbMoc.Focus();
                else if ((sender as TextBox).Name == nameof(txbMoc).ToString())
                    txbLiczbaMiejsc.Focus();
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
            txbLiczbaMiejsc.Text = String.Empty;
            txbMoc.Text = String.Empty;
        }

        public void UpdateList()
        {
            lviCarList.ItemsSource = context.Cars.ToList();
            lviCarList.Items.Refresh();
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFiltr.Text))
                return true;
            else
                return ((item as Car).Mark.IndexOf
                       (txtFiltr.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void txtFiltr_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionView widok = (CollectionView)CollectionViewSource.
                                    GetDefaultView(lviCarList.ItemsSource);
            widok.Filter = UserFilter;
            CollectionViewSource.GetDefaultView(lviCarList.ItemsSource).Refresh();
        }
    }
}


//obsluga wyjatkow try catch \

//TODO AK dodac cos tam


