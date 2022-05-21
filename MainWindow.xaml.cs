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

        List<Guitar> availableGuitars = new List<Guitar>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            

            var query = from g in db.Guitars
                        select g.StringSize;

            dropdownStringSize.ItemsSource = query.ToList().Distinct();
        }


        //store selected StringSize from dropdown and enable search button if other inputs are filled
        private void dropdownStringSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var query = from g in db.Guitars
                        select g.StringSize;

            if (AreAllInputsFilled() && IsStartDateEarlierThanEndDate())
            {
                buttonSearch.IsEnabled = true;
            }

            else
            {
                buttonSearch.IsEnabled = false;
            }

        }

        //every time a start date is selected, check if the other inputs are filled to enable search button
        private void datePickerStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsStartDateEarlierThanEndDate())
            {
                textblockDateError.Visibility = Visibility.Hidden;

                if (AreAllInputsFilled())
                {
                    buttonSearch.IsEnabled = true;
                }
                
            }

            else
            {
                if (IsEndDateFilled() && IsStartDateEarlierThanEndDate() is false)
                {
                    textblockDateError.Visibility = Visibility.Visible;
                }
                buttonSearch.IsEnabled = false;
   
            }
        }

        //every time an end date is selected, check if the other inputs are filled to enable search button
        private void datePickerEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsStartDateEarlierThanEndDate())
            {
                textblockDateError.Visibility = Visibility.Hidden;

                if (AreAllInputsFilled())
                {
                    buttonSearch.IsEnabled = true;
                }
            }

            else
            {
                if (IsStartDateFilled() && IsStartDateEarlierThanEndDate() is false)
                {
                    textblockDateError.Visibility = Visibility.Visible;
                }
                buttonSearch.IsEnabled = false;
            }
        }

        //store selected start date from date picker

        //store selected end date from date picker

        //build query with data and return available guitars

        //feed available listbox with available guitars










        //guitars tab with all guitar models available
        public void buttonGetAllGuitars_Click(object sender, RoutedEventArgs e)
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

 







        //method to check if dropdown has any value selected
        public bool IsDropdownSelected()
        {
            if (dropdownStringSize.SelectedItem != null)
            {
                if (dropdownStringSize.SelectedItem.ToString() == "")
                {
                    return false;
                }

                else
                {
                    return true;
                }
            }

            else
            {
                return false;
            }
        }

        //method to check if startDate datepicker has any value selected
        public bool IsStartDateFilled()
        {
            if (datePickerStart.SelectedDate.HasValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //method to check if endDate datepicker has any value selected
        public bool IsEndDateFilled()
        {
            if (datePickerEnd.SelectedDate.HasValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //method to check if all inputs are filled
        public bool AreAllInputsFilled()
        {
            if (IsDropdownSelected() && IsStartDateFilled() && IsEndDateFilled())
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        //method to check if selected start date is later than selected end date
        public bool IsStartDateEarlierThanEndDate()
        {
            if (datePickerStart.SelectedDate < datePickerEnd.SelectedDate)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        //click event handler method to query the database for available guitars to book in selected date range
        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            //storing input values
            string selectedStringSize = dropdownStringSize.SelectedItem.ToString();
            DateTime? startDate = datePickerStart.SelectedDate;
            DateTime? endDate = datePickerEnd.SelectedDate;

            //first query to retrieve all guitar ids from selected string size     
            var selectedSizeGuitarIds = from g in db.Guitars
                               where g.StringSize.Equals(selectedStringSize)
                               select g.Id;

            //second query to retrieve all guitar ids that are already booked for that date range
            var bookedGuitarIds = from b in db.Bookings
                        where b.Guitar.StringSize.Equals(selectedStringSize)
                        && (startDate >= b.StartDate || startDate <= b.EndDate)
                        || (endDate >= b.StartDate || endDate <= b.EndDate)
                        select b.GuitarId;

            //the difference between both previous lists will be the available guitars
            //we achieve this through the IQueryable interface and its except method
            //reference https://stackoverflow.com/questions/3944803/use-linq-to-get-items-in-one-list-that-are-not-in-another-list
            
            IQueryable<int> availableGuitarIds = selectedSizeGuitarIds.Except<int>(bookedGuitarIds);
            //
            //third and last query to get the guitar objects that match the available guitar ids
            var availableGuitars = from g in db.Guitars
                                   where availableGuitarIds.Contains(g.Id)
                                   select g;

            //updating the available guitars listbox content
            listBoxAvailable.ItemsSource = availableGuitars.ToList();

        }


        //event handler method to react to selected guitars in the listbox
        private void listBoxAvailable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //displaying image of selected guitar

            //enabling booking button
            buttonBook.IsEnabled = true;

            Guitar selectedGuitar = listBoxAvailable.SelectedItem as Guitar;

            if (selectedGuitar != null)
            {
                textblockSelectedGuitar.Text = "Selected guitar: " + selectedGuitar.ToString();
                textblockSelectedGuitar.Visibility = Visibility.Visible;
            }
        }


        //make the booking if selected guitar is available at that date
        private void buttonBook_Click(object sender, RoutedEventArgs e)
        {
            //getting selected element from listbox and casting it as a guitar object
            Guitar selectedGuitar = listBoxAvailable.SelectedItem as Guitar;

            //getting information from user inputs
            string selectedStringSize = dropdownStringSize.SelectedItem.ToString();
            DateTime startDate = (DateTime)datePickerStart.SelectedDate;
            DateTime endDate = (DateTime)datePickerEnd.SelectedDate;

            //creating a new booking object with collected information
            Booking b = new Booking()
            {
                StartDate = startDate,
                EndDate = endDate,
                GuitarId = selectedGuitar.Id
            };

            //adding the booking object to the database and saving changes
            db.Bookings.Add(b);
            db.SaveChanges();
        }









    }
}
