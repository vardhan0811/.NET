using System;
using System.Collections.Generic;
using System.Globalization;

namespace Day29
{
    public class ForensicReport
    {
        // Attribute: Stores Officer -> Date
        private Dictionary<string, DateTime> reportMap = new Dictionary<string, DateTime>();

        // Add report details
        public void AddReportDetails(string officerName, DateTime filedDate)
        {
            reportMap[officerName] = filedDate;
        }

        // Get officers who filed on given date
        public List<string> GetOfficersWhoFiledReportsOnDate(DateTime date)
        {
            List<string> result = new List<string>();

            foreach (KeyValuePair<string, DateTime> entry in reportMap)
            {
                // Compare only date (ignore time)
                if (entry.Value.Date == date.Date)
                {
                    result.Add(entry.Key);
                }
            }

            return result;
        }
    }

    public class ForensicReportMain
    {
        public static void Run()
        {
            ForensicReport report = new ForensicReport();

            // Step 1: Get number of reports
            Console.WriteLine("Enter number of reports to be added");
            int n = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("Enter the Forensic reports (Reporting Officer: Report Filed Date)");

            // Step 2: Read reports
            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine() ?? "";

                // Split by :
                string[] parts = input.Split(':');

                string officerName = parts[0];
                string dateText = parts[1];

                // Convert string to DateTime
                DateTime filedDate = DateTime.ParseExact(
                    dateText,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture
                );

                // Store in Dictionary
                report.AddReportDetails(officerName, filedDate);
            }

            // Step 3: Read search date
            Console.WriteLine("Enter the filed date to identify the reporting officers");

            string searchText = Console.ReadLine() ?? "";

            DateTime searchDate = DateTime.ParseExact(
                searchText,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture
            );

            // Step 4: Filter
            List<string> officers =
                report.GetOfficersWhoFiledReportsOnDate(searchDate);

            // Step 5: Display result
            if (officers.Count == 0)
            {
                Console.WriteLine("No reporting officer filed the report");
            }
            else
            {
                Console.WriteLine("Reports filed on the " + searchDate.ToString("yyyy-MM-dd") + " are by");
                foreach (string name in officers)
                {
                    Console.WriteLine(name);
                }
            }
        }
    }
}