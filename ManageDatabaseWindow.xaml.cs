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
    public partial class ManageDatabaseWindow : Window
    {
        private ObservableCollection<FoodItem> _database;
        private FoodItem _selectedItem;

        public ManageDatabaseWindow(ObservableCollection<FoodItem> database)
        {
            InitializeComponent();
            _database = database;
            lstFoods.ItemsSource = _database;

            // Setup filtering
            ICollectionView view = CollectionViewSource.GetDefaultView(lstFoods.ItemsSource);
            view.Filter = FilterFoods;
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
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
            CollectionViewSource.GetDefaultView(lstFoods.ItemsSource).Refresh();
        }

        private void lstFoods_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedItem = lstFoods.SelectedItem as FoodItem;
            if (_selectedItem != null)
            {
                txtName.Text = _selectedItem.Name;
                txtProteins.Text = _selectedItem.Proteins.ToString();
                txtLipids.Text = _selectedItem.Lipids.ToString();
                txtCarbs.Text = _selectedItem.Carbohydrates.ToString();
                txtFibers.Text = _selectedItem.Fibers.ToString();
                txtCalories.Text = _selectedItem.Calories.ToString();
            }
            else
            {
                ClearForm();
            }
        }

        private void ClearForm()
        {
            txtName.Text = "";
            txtProteins.Text = "";
            txtLipids.Text = "";
            txtCarbs.Text = "";
            txtFibers.Text = "";
            txtCalories.Text = "";
            lstFoods.SelectedItem = null;
            _selectedItem = null;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vă rugăm să introduceți o denumire.", "Eroare Validare", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            double proteins, lipids, carbs, fibers, calories;

            if (!double.TryParse(txtProteins.Text, out proteins) ||
                !double.TryParse(txtLipids.Text, out lipids) ||
                !double.TryParse(txtCarbs.Text, out carbs) ||
                !double.TryParse(txtFibers.Text, out fibers) ||
                !double.TryParse(txtCalories.Text, out calories))
            {
                MessageBox.Show("Vă rugăm să introduceți valori numerice valide.", "Eroare Validare", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_selectedItem == null)
            {
                // Add New
                var newItem = new FoodItem
                {
                    Name = txtName.Text,
                    Proteins = proteins,
                    Lipids = lipids,
                    Carbohydrates = carbs,
                    Fibers = fibers,
                    Calories = calories
                };
                _database.Add(newItem);
                MessageBox.Show("Aliment adăugat cu succes.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearForm();
            }
            else
            {
                // Update Existing
                _selectedItem.Name = txtName.Text;
                _selectedItem.Proteins = proteins;
                _selectedItem.Lipids = lipids;
                _selectedItem.Carbohydrates = carbs;
                _selectedItem.Fibers = fibers;
                _selectedItem.Calories = calories;
                MessageBox.Show("Aliment actualizat cu succes.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedItem != null)
            {
                if (MessageBox.Show("Sunteți sigur că doriți să ștergeți '" + _selectedItem.Name + "'?", "Confirmare Ștergere", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _database.Remove(_selectedItem);
                    ClearForm();
                }
            }
        }
    }
}
