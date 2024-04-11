// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;



string folderPath = @"C:\Users\AgbenorkuD\OneDrive - AECOM\Documents 1\DiRoots\FamilyReviser\Families\MP2ACCULC3001-BOF-BS00-AA-000-M3-AR-000003";

List<string> prefixes = new List<string> { "ARC","BHE", "KONE"};
ValidateFilesInFolder(folderPath, prefixes);


static void ValidateFilesInFolder(string folderPath, List<string> prefixes)
{
    if (Directory.Exists(folderPath))
    {
        string resultFolderPath = Path.Combine(folderPath, "ValidationResults");
        Directory.CreateDirectory(resultFolderPath);

        string[] files = Directory.GetFiles(folderPath);

        int totalFiles = files.Length;
        int validFiles = 0;
        int invalidFiles = 0;

        List<string> validFileNames = new List<string>();
        List<string> invalidFileNames = new List<string>();

        foreach (var file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            bool isValid = ValidateBS8541String(fileName, prefixes);

            if (isValid)
            {
                validFiles++;
                validFileNames.Add(fileName);
            }
            else
            {
                invalidFiles++;
                invalidFileNames.Add(fileName);
            }

            Console.WriteLine($"{fileName} - Is Valid: {isValid}");
        }

        // Calculate and display the percentage
        double passPercentage = ((double)validFiles / totalFiles) * 100;
        double failPercentage = ((double)invalidFiles / totalFiles) * 100;

        Console.WriteLine($"\nValidation Summary:");
        Console.WriteLine($"Total Files: {totalFiles}");
        Console.WriteLine($"Valid Files: {validFiles} ({Math.Round(passPercentage,2)}% passed)");
        Console.WriteLine($"Invalid Files: {invalidFiles} ({Math.Round(failPercentage,2)}% failed)");

        // Write results to files
        WriteToFile(resultFolderPath, $"{Path.GetFileName(folderPath)}_ValidItems.txt", validFileNames, Math.Round(passPercentage,2));
        WriteToFile(resultFolderPath, $"{Path.GetFileName(folderPath)}_InvalidItems.txt", invalidFileNames, Math.Round(failPercentage,2));
    }
    else
    {
        Console.WriteLine($"Folder not found: {folderPath}");
    }
}

static bool ValidateBS8541String(string input, List<string> prefixes)
{
    // Sample regex for basic validation with three or four parts, accepting "_" and "-"
    string pattern = @"^[A-Za-z0-9]+[_-][A-Za-z0-9]+[_-][A-Za-z0-9]+([_-][A-Za-z0-9]+)?([_-][A-Za-z0-9]+)?$";

    // Check if the input matches the pattern and starts with any of the specified prefixes
    return Regex.IsMatch(input, pattern) && prefixes.Any(prefix => input.StartsWith(prefix));
}

static void WriteToFile(string folderPath, string fileName, List<string> content, double percentage)
{
    string filePath = Path.Combine(folderPath, fileName);

    // Write content to the file and overwrite existing content
    File.WriteAllText(filePath, string.Join(Environment.NewLine, content));

    // Append percentage at the end of the file
    File.AppendAllText(filePath, $"\n\nPercentage: {percentage}%");
}