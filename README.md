# Calculator Calorii (Calories Calculator)

A WPF application targeting .NET 4.0 designed for a kindergarten to manage daily menus and calculate nutritional values per child.

## Features

*   **Daily Menu Management**: Plan menus for Breakfast, Snack, Lunch, and Dinner.
*   **Ingredient Tracking**: Manage a single list of ingredients for the entire day.
*   **Nutritional Calculation**: Automatically calculates Proteins, Lipids, Carbohydrates, Fibers, and Calories per child based on total ingredient quantities and the number of children.
*   **Food Database**: Add, Edit, and Delete food items with their nutritional information.
*   **Excel Export**: Generate daily reports in a specific Excel format ready for printing.
*   **Localization**: Fully localized in Romanian.

## Getting Started

### Prerequisites

*   Windows OS
*   .NET Framework 4.0 or later

### Installation

1.  Clone the repository or download the source code.
2.  Open `CaloriesCalculator.sln` in Visual Studio (2010 or later).
3.  Build the solution.
4.  Run the application.

### Usage

1.  **Set Date**: Use the calendar to select the day you are planning for.
2.  **Set Kids Count**: Enter the number of children present.
3.  **Plan Menu**: Enter the text description for each meal.
4.  **Add Ingredients**:
    *   Click "Adaugă Ingredient".
    *   Search for a food item.
    *   Enter the **TOTAL** quantity for all children.
    *   Click "Adaugă".
5.  **Check Totals**: Verify the "Totaluri Zilnice (Per Copil)" on the left.
6.  **Export**: Click "Generare Excel" to save the daily sheet.

## Data Persistence

The application saves data locally in XML files within the application directory:
*   `food_database.xml`: Stores the food items.
*   `daily_records.xml`: Stores the daily menus and ingredient lists.

## License

[Add License Here]
