using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CaloriesCalculator.Models;
using CaloriesCalculator.Services;
using Microsoft.Win32;

namespace CaloriesCalculator
{
    public partial class MainWindow : Window
    {
        private DataService _dataService;
        private ObservableCollection<FoodItem> _foodDatabase;
        private List<DailyRecord> _dailyRecords;
        private DailyRecord _currentRecord;
        private bool _isLoading;

        public MainWindow()
        {
            InitializeComponent();
            _dataService = new DataService();
            LoadData();
            
            calendar.SelectedDate = DateTime.Today;
        }

        private void LoadData()
        {
            _foodDatabase = _dataService.LoadFoodDatabase();
            _dailyRecords = _dataService.LoadDailyRecords();
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate.HasValue)
            {
                LoadRecordForDate(calendar.SelectedDate.Value);
            }
        }

        private void LoadRecordForDate(DateTime date)
        {
            _isLoading = true;
            _currentRecord = _dailyRecords.FirstOrDefault(r => r.Date.Date == date.Date);
            if (_currentRecord == null)
            {
                _currentRecord = new DailyRecord { Date = date };
                _dailyRecords.Add(_currentRecord);
            }

            txtKidsCount.Text = _currentRecord.KidsCount > 0 ? _currentRecord.KidsCount.ToString() : "1";
            
            txtBreakfastMenu.Text = _currentRecord.BreakfastMenu;
            txtSnackMenu.Text = _currentRecord.SnackMenu;
            txtLunchMenu.Text = _currentRecord.LunchMenu;
            txtDinnerMenu.Text = _currentRecord.DinnerMenu;

            gridIngredients.ItemsSource = _currentRecord.Ingredients;

            UpdateTotals();
            _isLoading = false;
        }

        private void SaveData()
        {
            if (_isLoading) return;
            _dataService.SaveDailyRecords(_dailyRecords);
            _dataService.SaveFoodDatabase(_foodDatabase);
        }

        private void UpdateTotals()
        {
            if (_currentRecord == null) return;

            double p = 0, l = 0, c = 0, f = 0, cal = 0;

            foreach (var entry in _currentRecord.Ingredients)
            {
                double factor = entry.Quantity / 100.0;
                p += entry.Food.Proteins * factor;
                l += entry.Food.Lipids * factor;
                c += entry.Food.Carbohydrates * factor;
                f += entry.Food.Fibers * factor;
                cal += entry.Food.Calories * factor;
            }

            double kids = _currentRecord.KidsCount > 0 ? _currentRecord.KidsCount : 1;

            lblTotalProteins.Text = (p / kids).ToString("F2");
            lblTotalLipids.Text = (l / kids).ToString("F2");
            lblTotalCarbs.Text = (c / kids).ToString("F2");
            lblTotalFibers.Text = (f / kids).ToString("F2");
            lblTotalCalories.Text = (cal / kids).ToString("F2");
        }

        private void txtKidsCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_currentRecord != null && !_isLoading)
            {
                int count;
                if (int.TryParse(txtKidsCount.Text, out count))
                {
                    _currentRecord.KidsCount = count;
                    SaveData();
                }
            }
        }

        private void Menu_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_currentRecord != null && !_isLoading)
            {
                _currentRecord.BreakfastMenu = txtBreakfastMenu.Text;
                _currentRecord.SnackMenu = txtSnackMenu.Text;
                _currentRecord.LunchMenu = txtLunchMenu.Text;
                _currentRecord.DinnerMenu = txtDinnerMenu.Text;
                SaveData();
            }
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddFoodWindow(_foodDatabase);
            addWindow.Owner = this;
            if (addWindow.ShowDialog() == true)
            {
                _currentRecord.Ingredients.Add(addWindow.SelectedEntry);
                UpdateTotals();
                SaveData();
            }
        }

        private void btnRemoveIngredient_Click(object sender, RoutedEventArgs e)
        {
            var selected = gridIngredients.SelectedItem as FoodEntry;
            if (selected != null)
            {
                _currentRecord.Ingredients.Remove(selected);
                UpdateTotals();
                SaveData();
            }
        }

        private void btnAddDatabase_Click(object sender, RoutedEventArgs e)
        {
            var dbWindow = new ManageDatabaseWindow(_foodDatabase);
            dbWindow.Owner = this;
            dbWindow.ShowDialog();
            SaveData();
        }

        private void btnCreateExcel_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "Menu_" + _currentRecord.Date.ToString("yyyy-MM-dd");
            dlg.DefaultExt = ".xls";
            dlg.Filter = "Excel Documents (.xls)|*.xls";

            if (dlg.ShowDialog() == true)
            {
                var exporter = new ExcelExporter();
                try
                {
                    exporter.ExportToExcel(_currentRecord, dlg.FileName);
                    MessageBox.Show("Fișierul Excel a fost creat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare la crearea fișierului Excel: " + ex.Message, "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
