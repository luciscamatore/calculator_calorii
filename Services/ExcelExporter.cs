using System;
using System.IO;
using System.Text;
using CaloriesCalculator.Models;

namespace CaloriesCalculator.Services
{
    public class ExcelExporter
    {
        public void ExportToExcel(DailyRecord record, string filePath)
        {
            StringBuilder sb = new StringBuilder();
            
            // Using HTML format which Excel can open
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>");
            sb.AppendLine("<style>");
            sb.AppendLine("table { border-collapse: collapse; width: 100%; font-family: Arial, sans-serif; }");
            sb.AppendLine("th, td { border: 1px solid black; padding: 5px; text-align: center; font-size: 12px; }");
            sb.AppendLine(".header { font-weight: bold; background-color: #f0f0f0; }");
            sb.AppendLine(".title { font-size: 18px; font-weight: bold; text-align: center; margin-bottom: 20px; }");
            sb.AppendLine(".info { margin-bottom: 10px; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            
            sb.AppendLine("<div class='title'>Gradinita cu Program Prelungit Sangeorz-Bai</div>");
            
            sb.AppendLine("<div class='info'>");
            sb.AppendLine("<p><strong>Data:</strong> " + record.Date.ToString("dd.MM.yyyy") + "</p>");
            sb.AppendLine("<p><strong>Meniul zilei:</strong></p>");
            sb.AppendLine("<ul>");
            if (!string.IsNullOrEmpty(record.BreakfastMenu)) sb.AppendLine("<li>9:00 - " + record.BreakfastMenu + "</li>");
            if (!string.IsNullOrEmpty(record.SnackMenu)) sb.AppendLine("<li>10:00 - " + record.SnackMenu + "</li>");
            if (!string.IsNullOrEmpty(record.LunchMenu)) sb.AppendLine("<li>12:00 - " + record.LunchMenu + "</li>");
            if (!string.IsNullOrEmpty(record.DinnerMenu)) sb.AppendLine("<li>16:00 - " + record.DinnerMenu + "</li>");
            sb.AppendLine("</ul>");
            sb.AppendLine("<p><strong>TOTAL COPII:</strong> " + record.KidsCount + "</p>");
            sb.AppendLine("</div>");

            sb.AppendLine("<table>");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr class='header'>");
            sb.AppendLine("<th>Nr. crt.</th>");
            sb.AppendLine("<th>Denumire</th>");
            sb.AppendLine("<th>Cant/gram</th>");
            sb.AppendLine("<th>P/ 100g</th>");
            sb.AppendLine("<th>Total P</th>");
            sb.AppendLine("<th>L/ 100g</th>");
            sb.AppendLine("<th>Total L</th>");
            sb.AppendLine("<th>G/ 100g</th>");
            sb.AppendLine("<th>Total G</th>");
            sb.AppendLine("<th>F/ 100g</th>");
            sb.AppendLine("<th>Total F</th>");
            sb.AppendLine("<th>Cal/ 100g</th>");
            sb.AppendLine("<th>Total calorii</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");

            int index = 1;
            double totalProteins = 0, totalLipids = 0, totalCarbs = 0, totalFibers = 0, totalCalories = 0;

            foreach (var entry in record.Ingredients)
            {
                double factor = entry.Quantity / 100.0;
                double p = entry.Food.Proteins * factor;
                double l = entry.Food.Lipids * factor;
                double c = entry.Food.Carbohydrates * factor;
                double f = entry.Food.Fibers * factor;
                double cal = entry.Food.Calories * factor;

                totalProteins += p;
                totalLipids += l;
                totalCarbs += c;
                totalFibers += f;
                totalCalories += cal;

                sb.AppendLine("<tr>");
                sb.AppendLine("<td>" + index++ + "</td>");
                sb.AppendLine("<td>" + entry.Food.Name + "</td>");
                sb.AppendLine("<td>" + entry.Quantity + "</td>");
                
                sb.AppendLine("<td>" + entry.Food.Proteins.ToString("F1") + "</td>");
                sb.AppendLine("<td>" + p.ToString("F1") + "</td>");
                
                sb.AppendLine("<td>" + entry.Food.Lipids.ToString("F1") + "</td>");
                sb.AppendLine("<td>" + l.ToString("F1") + "</td>");
                
                sb.AppendLine("<td>" + entry.Food.Carbohydrates.ToString("F1") + "</td>");
                sb.AppendLine("<td>" + c.ToString("F1") + "</td>");

                sb.AppendLine("<td>" + entry.Food.Fibers.ToString("F1") + "</td>");
                sb.AppendLine("<td>" + f.ToString("F1") + "</td>");
                
                sb.AppendLine("<td>" + entry.Food.Calories.ToString("F0") + "</td>");
                sb.AppendLine("<td>" + cal.ToString("F1") + "</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");
            
            sb.AppendLine("<br/>");
            
            // Footer with per-kid calculations
            double kids = record.KidsCount > 0 ? record.KidsCount : 1;
            
            sb.AppendLine("<table>");
            sb.AppendLine("<tr><td><strong>Proteine / copil</strong></td><td>" + (totalProteins / kids).ToString("F2") + "</td><td></td><td></td><td><strong>Asistent Medical</strong></td></tr>");
            sb.AppendLine("<tr><td><strong>Lipide / copil</strong></td><td>" + (totalLipids / kids).ToString("F2") + "</td><td></td><td></td><td><strong>Bungardean Ana</strong></td></tr>");
            sb.AppendLine("<tr><td><strong>Glucide / copil</strong></td><td>" + (totalCarbs / kids).ToString("F2") + "</td><td></td><td></td><td></td></tr>");
            sb.AppendLine("<tr><td><strong>Fibre / copil</strong></td><td>" + (totalFibers / kids).ToString("F2") + "</td><td></td><td></td><td></td></tr>");
            sb.AppendLine("<tr><td><strong>Calorii / copil</strong></td><td>" + (totalCalories / kids).ToString("F2") + "</td><td></td><td></td><td></td></tr>");
            sb.AppendLine("</table>");

            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }
    }
}
