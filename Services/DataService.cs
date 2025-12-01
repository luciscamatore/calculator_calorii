using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using CaloriesCalculator.Models;

namespace CaloriesCalculator.Services
{
    public class DataService
    {
        private const string FoodDatabaseFile = "food_database.xml";
        private const string DailyRecordsFile = "daily_records.xml";

        public ObservableCollection<FoodItem> LoadFoodDatabase()
        {
            if (!File.Exists(FoodDatabaseFile))
            {
                return new ObservableCollection<FoodItem>();
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<FoodItem>));
                using (StreamReader reader = new StreamReader(FoodDatabaseFile))
                {
                    return (ObservableCollection<FoodItem>)serializer.Deserialize(reader);
                }
            }
            catch (Exception)
            {
                return new ObservableCollection<FoodItem>();
            }
        }

        public void SaveFoodDatabase(ObservableCollection<FoodItem> foods)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<FoodItem>));
                using (StreamWriter writer = new StreamWriter(FoodDatabaseFile))
                {
                    serializer.Serialize(writer, foods);
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine("Error saving food database: " + ex.Message);
            }
        }

        // For daily records, we might want to store them by date or all in one file.
        // Given the scale (kindergarten), one file is probably fine for a few years.
        public List<DailyRecord> LoadDailyRecords()
        {
            if (!File.Exists(DailyRecordsFile))
            {
                return new List<DailyRecord>();
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<DailyRecord>));
                using (StreamReader reader = new StreamReader(DailyRecordsFile))
                {
                    return (List<DailyRecord>)serializer.Deserialize(reader);
                }
            }
            catch (Exception)
            {
                return new List<DailyRecord>();
            }
        }

        public void SaveDailyRecords(List<DailyRecord> records)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<DailyRecord>));
                using (StreamWriter writer = new StreamWriter(DailyRecordsFile))
                {
                    serializer.Serialize(writer, records);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving daily records: " + ex.Message);
            }
        }
    }
}
