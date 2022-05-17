using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CA3_s00220273
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GuitarsAndBookingsEntities db = new GuitarsAndBookingsEntities();

        ObservableCollection<Guitar> dropdownColorsList = new ObservableCollection<Guitar>();
        ObservableCollection<Guitar> availableGuitarsList = new ObservableCollection<Guitar>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            

            var query = from g in db.Guitars
                        select g.StringSize;

            dropdownBrand.ItemsSource = query.ToList();
        }


        //feed StringSize dropdown with guitar colors
        private void PopulateDropdown()
        {
            //var query = Guitar
        }



        //store selected StringSize from dropdown
        private void dropdownColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var query = from g in db.Guitars
                        select g.StringSize;

            
        }

        //store selected start date from date picker

        //store selected end date from date picker

        //build query with data and return available guitars

        //feed available listbox with available guitars

        //guitars tab with all guitar models available
        private void buttonGetAllGuitars_Click(object sender, RoutedEventArgs e)
        {
            var query = from g in db.Guitars
                        select g;

            datagridGuitars.ItemsSource = query.ToList();
        }

        //bookings tab with all guitar models available
        private void buttonGetAllBookings_Click(object sender, RoutedEventArgs e)
        {
            var query = from g in db.Bookings
                        select g;

            datagridBookings.ItemsSource = query.ToList();
        }

   
    }
}
