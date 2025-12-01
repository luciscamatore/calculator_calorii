using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CaloriesCalculator.Models
{
    public class DailyRecord : INotifyPropertyChanged
    {
        private DateTime _date;
        private string _breakfastMenu;
        private string _snackMenu;
        private string _lunchMenu;
        private string _dinnerMenu;
        private ObservableCollection<FoodEntry> _ingredients;
        private int _kidsCount;

        public DailyRecord()
        {
            Ingredients = new ObservableCollection<FoodEntry>();
            Date = DateTime.Today;
            BreakfastMenu = "";
            SnackMenu = "";
            LunchMenu = "";
            DinnerMenu = "";
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged("Date"); }
        }

        public int KidsCount
        {
            get { return _kidsCount; }
            set { _kidsCount = value; OnPropertyChanged("KidsCount"); }
        }

        public string BreakfastMenu
        {
            get { return _breakfastMenu; }
            set { _breakfastMenu = value; OnPropertyChanged("BreakfastMenu"); }
        }

        public string SnackMenu
        {
            get { return _snackMenu; }
            set { _snackMenu = value; OnPropertyChanged("SnackMenu"); }
        }

        public string LunchMenu
        {
            get { return _lunchMenu; }
            set { _lunchMenu = value; OnPropertyChanged("LunchMenu"); }
        }

        public string DinnerMenu
        {
            get { return _dinnerMenu; }
            set { _dinnerMenu = value; OnPropertyChanged("DinnerMenu"); }
        }

        public ObservableCollection<FoodEntry> Ingredients
        {
            get { return _ingredients; }
            set { _ingredients = value; OnPropertyChanged("Ingredients"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class FoodEntry : INotifyPropertyChanged
    {
        private FoodItem _food;
        private double _quantity; // in grams

        public FoodItem Food
        {
            get { return _food; }
            set { _food = value; OnPropertyChanged("Food"); }
        }

        public double Quantity
        {
            get { return _quantity; }
            set { _quantity = value; OnPropertyChanged("Quantity"); }
        }

        // Calculated properties for display
        public double TotalCalories 
        { 
            get { return Food != null ? (Food.Calories * Quantity / 100.0) : 0; } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
