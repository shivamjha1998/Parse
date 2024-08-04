# Patient Data Parser

## Overview

This project contains a program to parse patient data from a text file and extract names and NHS numbers. The project includes the following files:
- `Program.cs`: Main program file.
- `string1.txt`: First input file with patient data.
- `string2.txt`: Second input file with patient data.
- `pseudoCode.txt`: File containing the pseudocode for the program.

## How to Run

To run the program, use the following command with the path to your input file as an argument:

~~~sh
dotnet run <inputFile>
~~~

## What the Program Does

The program performs the following tasks:

1.  Replaces occurrences of `[[new-line]]` and `[[New-Line]]` with actual newline characters.
2.  Extracts all instances of `Name` and `NHS Number` from the text transcription.
3.  Creates a table of all the names with their corresponding NHS numbers.
4.  Removes any duplicate entries from the list.
5.  Outputs the modified transcription and the list of names and NHS numbers.

## Technique Used for Parsing

I used regular expressions (regex) to parse the input strings. The regex patterns were designed to handle different formats and ensure the correct extraction of names and NHS numbers, even when there were variations in the input format. This approach allowed for flexible and robust parsing, ensuring that the program could handle both quoted and unquoted formats as well as variations in the placement of the `NHS Number`.

## Learning Experience

This project was an exciting opportunity to delve deeper into text processing and regular expressions. It led me down a fascinating rabbit hole where I learned about various string manipulation techniques and best practices for handling text data. During my research, I also came across interesting articles on microprocesses in Intel's chips and their bug in 13th and 14th gen processors just because in 1 transistors's current was faulty among trillions of transistors.

## Challenges and Future Improvements

Given more time, I would:

-   Implement more comprehensive error handling to manage unexpected input formats.
-   Add unit tests to ensure the robustness of the parsing logic.
-   Optimize the regex patterns further for performance improvements.

One of the main hurdles I faced was ensuring that the regex patterns were flexible enough to handle variations in the input format while maintaining accuracy in the extraction process.

## Conclusion

Thank you for providing this opportunity. It was a valuable learning experience that allowed me to enhance my skills in text processing and regex. I look forward to any feedback and further opportunities to improve my coding abilities.