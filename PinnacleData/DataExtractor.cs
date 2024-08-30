using System;
using System.Data;
using System.IO;
using System.Linq;

namespace PinnacleData
{
    public class DataExtractor
    {
        public DataTable ExtractData(string inputText)
        {
            DataTable dataTable = new DataTable();

            // Add columns to the DataTable
            dataTable.Columns.Add("Measurement", typeof(string));
            dataTable.Columns.Add("Deviation", typeof(double));
            dataTable.Columns.Add("Actual", typeof(double));
            dataTable.Columns.Add("Nominal", typeof(double));
            dataTable.Columns.Add("- Tol", typeof(double));
            dataTable.Columns.Add("+ Tol", typeof(double));
            dataTable.Columns.Add("O/T", typeof(string));
            dataTable.Columns.Add("P/F", typeof(string));

            // Split the input text into lines
            string[] lines = inputText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            // Skip the header lines
            for (int i = 2; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("-")) continue;

                // Split the line into columns
                string[] columns = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Ensure we have the correct number of columns
                if (columns.Length >= 8)
                {
                    DataRow row = dataTable.NewRow();

                    // Extract measurement (which may contain spaces)
                    int measurementEndIndex = Array.FindIndex(columns, c => double.TryParse(c, out _));
                    row["Measurement"] = string.Join(" ", columns.Take(measurementEndIndex));

                    // Extract other columns
                    row["Deviation"] = Convert.ToDouble(columns[measurementEndIndex]);
                    row["Actual"] = Convert.ToDouble(columns[measurementEndIndex + 1]);
                    row["Nominal"] = Convert.ToDouble(columns[measurementEndIndex + 2]);
                    row["- Tol"] = Convert.ToDouble(columns[measurementEndIndex + 3]);
                    row["+ Tol"] = Convert.ToDouble(columns[measurementEndIndex + 4]);
                    row["O/T"] = columns[measurementEndIndex + 5];
                    row["P/F"] = columns[measurementEndIndex + 6];

                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }
    }
}