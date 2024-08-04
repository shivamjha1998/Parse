// Constants
NHS_NUMBER_1 = "NHS Number:"
NHS_NUMBER_2 = "NHSNumber"
NAME_1 = "Name:"

// Initialize indices and pointers
nhsNumber1Index = 0
nhsNumber2Index = 0
nameIndex = 0

pointer = 0
input= "String"

currentNhsNumber = ""
currentName = ""

// List to store patient data
patientDataList = []

// Replace [[new-line]] with actual new lines
input = replaceNewLines(input)

// Function to read NHS Number
function readNHSNumber(pointer, input) {
    nhsNumber = ""
    while pointer < length of input and input[pointer] {
        nhsNumber = nhsNumber + input[pointer]
        pointer = pointer + 1
    }
    return pointer, nhsNumber
}

// Function to read Name
function readName(pointer, input) {
    name = ""
    while pointer < length of input and input[pointer] {
        name = name + input[pointer]
        pointer = pointer + 1
    }
    return pointer, name
}

// Main parsing loop
while pointer < length of input {
    currentChar = input[pointer]

    // Check if current position matches NHS_NUMBER_1
    if nhsNumber1Index == length of NHS_NUMBER_1 {
        if currentNhsNumber is not empty {
            patientDataList.append((currentName, currentNhsNumber))
            currentNhsNumber = ""
            currentName = ""
        }
        pointer, nhsNumber = readNHSNumber(pointer, input)
        currentNhsNumber = nhsNumber
        nhsNumber1Index = 0
    } else if currentChar == NHS_NUMBER_1[nhsNumber1Index] {
        nhsNumber1Index = nhsNumber1Index + 1
    } else {
        nhsNumber1Index = 0
    }

    // Check if current position matches NAME_1
    if nameIndex == length of NAME_1 {
        if currentName is not empty {
            patientDataList.append((currentName, "No NHS Number"))
            currentName = ""
        }
        pointer, name = readName(pointer, input)
        currentName = name
        nameIndex = 0
    } else if currentChar == NAME_1[nameIndex] {
        nameIndex = nameIndex + 1
    } else {
        nameIndex = 0
    }

    pointer = pointer + 1
}

// Add any remaining data
if currentNhsNumber is not empty or currentName is not empty {
    patientDataList.append((currentName, currentNhsNumber))
}

// Function to remove duplicates
function removeDuplicates(dataList) {
    uniqueData = []
    seen = set()
    for data in dataList {
        if data not in seen {
            uniqueData.append(data)
            seen.add(data)
    }
    return uniqueData
    }
}

// Remove duplicates from patient datap
uniquePatientData = removeDuplicates(patientDataList)

// Output results
print("Names and NHS Numbers:")
for entry in uniquePatientData {
    print("Name:", entry[0], "NHS Number:", entry[1])
}
