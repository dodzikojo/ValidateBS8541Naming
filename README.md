# ValidateBS8541Naming

A robust console application designed to validate file names against the **BS8541** naming standard. This tool scans a specified directory, checks each file name against a configurable regex pattern and prefix list, and generates detailed validation reports.

## Features

- **BS8541 Validation**: Strictly validates file names using a customizable Regular Expression.
- **Prefix Support**: Checks for valid project-specific prefixes (e.g., "ARC").
- **Detailed Reporting**:
  - Console summary with pass/fail statistics.
  - Generates a `ValidationResults` folder containing:
    - `_ValidItems.txt`: List of compliant files.
    - `_InvalidItems.txt`: List of non-compliant files.
    - Percentage breakdowns in each report.

## Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later.

## Getting Started

### 1. Installation

Clone the repository to your local machine:

```bash
git clone https://github.com/your-username/ValidateBS8541Naming.git
cd ValidateBS8541Naming
```

### 2. Configuration

Currently, configuration is done by directly modifying `Program.cs`.

1.  Open `ValidateBS8541Naming/Program.cs` in your preferred editor.
2.  Update the `folderPath` variable to point to the directory you want to validate:
    ```csharp
    string folderPath = @"C:\Your\Target\Directory";
    ```
3.  Update the `prefixes` list with your allowed prefixes:
    ```csharp
    List<string> prefixes = new List<string> { "ARC", "STR", "MEP" };
    ```

## Usage

Run the application from the command line:

```bash
dotnet run --project ValidateBS8541Naming
```

### Output

The tool will output a summary to the console:

```text
Validation Summary:
Total Files: 50
Valid Files: 42 (84% passed)
Invalid Files: 8 (16% failed)
```

It will also create a folder named `ValidationResults` inside your target directory containing the detailed text reports.