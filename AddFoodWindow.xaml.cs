using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CaloriesCalculator.Models;

namespace CaloriesCalculator
{
    public partial class AddFoodWindow : Window
    {
        private ObservableCollection<FoodItem> _allFoods;
        public FoodEntry SelectedEntry { get; private set; }

        public AddFoodWindow(ObservableCollection<FoodItem> foods)
        {
            InitializeComponent();
            _allFoods = foods;
            gridFoods.ItemsSource = _allFoods;
            
            // Setup filtering
            ICollectionView view = CollectionViewSource.GetDefaultView(gridFoods.ItemsSource);
            view.Filter = FilterFoods;
        }

        private bool FilterFoods(object item)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
                return true;

            var food = item as FoodItem;
            return food.Name.IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(gridFoods.ItemsSource).Refresh();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var selectedFood = gridFoods.SelectedItem as FoodItem;
            if (selectedFood == null)
            {
                MessageBox.Show("Vă rugăm să selectați un aliment.", "Eroare Validare", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            double quantity;
            if (!double.TryParse(txtQuantity.Text, out quantity) || quantity <= 0)
            {
                MessageBox.Show("Vă rugăm să introduceți o cantitate validă.", "Eroare Validare", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SelectedEntry = new FoodEntry
            {
                Food = selectedFood,
                Quantity = quantity
            };

            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
