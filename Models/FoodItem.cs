using System;
using System.ComponentModel;

namespace CaloriesCalculator.Models
{
    [Serializable]
    public class FoodItem : INotifyPropertyChanged
    {
        private string _name;
        private double _proteins;
        private double _lipids;
        private double _carbohydrates;
        private double _fibers;
        private double _calories;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public double Proteins
        {
            get { return _proteins; }
            set { _proteins = value; OnPropertyChanged("Proteins"); }
        }

        public double Lipids
        {
            get { return _lipids; }
            set { _lipids = value; OnPropertyChanged("Lipids"); }
        }

        public double Carbohydrates
        {
            get { return _carbohydrates; }
            set { _carbohydrates = value; OnPropertyChanged("Carbohydrates"); }
        }

        public double Fibers
        {
            get { return _fibers; }
            set { _fibers = value; OnPropertyChanged("Fibers"); }
        }

        public double Calories
        {
            get { return _calories; }
            set { _calories = value; OnPropertyChanged("Calories"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
