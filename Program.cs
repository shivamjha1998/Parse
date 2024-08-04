using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class PatientData
{
    public string Name { get; set; }
    public string NHSNumber { get; set; }

    public PatientData(string name, string nhsNumber)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        NHSNumber = nhsNumber ?? throw new ArgumentNullException(nameof(nhsNumber));
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: Program <inputFile>");
            return;
        }

        string input = File.ReadAllText(args[0]);

        // Step 1: Replace new lines in the input string
        string modifiedInput = ReplaceNewLines(input);

        // Step 2: Extract patient data
        var data = ExtractPatientData(modifiedInput);

        // Step 3: Remove duplicates
        var uniqueData = RemoveDuplicates(data);

        // Output results
        Console.WriteLine("Modified Transcription:\n" + modifiedInput);
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine("Names and NHS Numbers:");
        foreach (var entry in uniqueData)
        {
            Console.WriteLine($"Name: {entry.Name}, NHS Number: {entry.NHSNumber}");
        }
    }

    // Function to replace [[new-line]] with actual newline characters
    public static string ReplaceNewLines(string text)
    {
        return Regex.Replace(text, @"\[\[new-line\]\]|\[\[New-Line\]\]", "\n", RegexOptions.IgnoreCase);
    }

    // Unified function to extract patient data from text
    public static List<PatientData> ExtractPatientData(string text)
    {
        const string NHS_NUMBER_PATTERN = @"NHS\s*Number[""]?\s*[:]\s*[""]?([A-Za-z0-9]+)[""]?";
        const string NAME_PATTERN = @"Name[""]?\s*[:]\s*[""]?([A-Za-z ]+?)[""]?(?=\s*NHS\s*Number|,\s*""NHSNumber|$)";

        var dataList = new List<PatientData>();
        string currentName = null;
        string currentNHSNumber = null;

        var nameMatches = Regex.Matches(text, NAME_PATTERN, RegexOptions.IgnoreCase);
        var nhsNumberMatches = Regex.Matches(text, NHS_NUMBER_PATTERN, RegexOptions.IgnoreCase);

        int nameIndex = 0, nhsIndex = 0;

        while (nameIndex < nameMatches.Count || nhsIndex < nhsNumberMatches.Count)
        {
            if (nameIndex < nameMatches.Count && (nhsIndex >= nhsNumberMatches.Count || nameMatches[nameIndex].Index < nhsNumberMatches[nhsIndex].Index))
            {
                currentName = nameMatches[nameIndex].Groups[1].Value.Trim();
                nameIndex++;
            }
            else if (nhsIndex < nhsNumberMatches.Count)
            {
                currentNHSNumber = nhsNumberMatches[nhsIndex].Groups[1].Value.Trim();
                nhsIndex++;
                if (currentName != null)
                {
                    dataList.Add(new PatientData(currentName, currentNHSNumber));
                    currentName = null;
                }
                else
                {
                    dataList.Add(new PatientData("No Name", currentNHSNumber));
                }
                currentNHSNumber = null;
            }
        }

        // If there's a leftover name or NHS number, handle it accordingly
        if (currentName != null)
        {
            dataList.Add(new PatientData(currentName, currentNHSNumber ?? "No NHS Number"));
        }

        return dataList;
    }

    // Function to remove duplicates from the patient data list
    public static List<PatientData> RemoveDuplicates(List<PatientData> data)
    {
        return data.GroupBy(p => new { p.Name, p.NHSNumber })
                   .Select(g => g.First())
                   .ToList();
    }
}
