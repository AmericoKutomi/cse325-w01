using Newtonsoft.Json;
using System.Text;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");

var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);

var salesFiles = FindFiles(storesDirectory);

var salesTotal = CalculateSalesTotal(salesFiles); // Add this line of code

File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

GenerateSalesSummary(salesFiles);

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        // The file name will contain the full path, so only check the end of it
        var extension = Path.GetExtension(file);
        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);

        // Parse the contents as JSON
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        // Add the amount found in the Total field to the salesTotal variable
        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}

static void GenerateSalesSummary(IEnumerable<string> salesFiles)
{
    double totalSales = 0;
    // StringBuilders for accumulating details
    StringBuilder salesDetailsBuilder = new StringBuilder();
    salesDetailsBuilder.AppendLine("Sales Details:");
    salesDetailsBuilder.AppendLine("----------------------------");

    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);

        // Parse the contents as JSON
        SalesTotal? data = JsonConvert.DeserializeObject<SalesTotal?>(salesJson);

        // Add the amount found in the Total attribute or OverallTotal attribute to the totalSales variable
        double fileTotal = data?.Total ?? data?.OverallTotal ?? 0;
        totalSales += fileTotal;
        salesDetailsBuilder.AppendLine($"{file}: {fileTotal:C}");
    }

    // Declare a StringBuilder for the General Summary
    StringBuilder summaryBuilder = new StringBuilder();
    summaryBuilder.AppendLine("Sales Summary:");
    summaryBuilder.AppendLine("----------------------------");
    summaryBuilder.AppendLine($"  Total Sales: {totalSales:C}");
    summaryBuilder.AppendLine();

    File.WriteAllText(Path.Combine("salesTotalDir", "SalesSummary.txt"), summaryBuilder.ToString());
    File.AppendAllText(Path.Combine("salesTotalDir", "SalesSummary.txt"), salesDetailsBuilder.ToString());

}
record SalesTotal(double? Total, double? OverallTotal);
record SalesData(double Total);
