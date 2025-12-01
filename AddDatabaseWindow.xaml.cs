using System;
using System.Windows;
using CaloriesCalculator.Models;

namespace CaloriesCalculator
{
    public partial class AddDatabaseWindow : Window
    {
        public FoodItem NewFoodItem { get; private set; }

        public AddDatabaseWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            double proteins, lipids, carbs, fibers, calories;

            if (!double.TryParse(txtProteins.Text, out proteins) ||
                !double.TryParse(txtLipids.Text, out lipids) ||
                !double.TryParse(txtCarbs.Text, out carbs) ||
                !double.TryParse(txtFibers.Text, out fibers) ||
                !double.TryParse(txtCalories.Text, out calories))
            {
                MessageBox.Show("Please enter valid numeric values.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewFoodItem = new FoodItem
            {
                Name = txtName.Text,
                Proteins = proteins,
                Lipids = lipids,
                Carbohydrates = carbs,
                Fibers = fibers,
                Calories = calories
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
