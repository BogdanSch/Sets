# Set Class in C# with Expression Evaluator

## Overview

This project is an implementation of a mathematical **Set** class in C# using Object-Oriented Programming (OOP) principles. The project supports various set operations such as **union**, **intersection**, **difference**, and **complement**. Additionally, it includes an **Expression Evaluator** that processes set operations from a given string expression.

Unit tests have been created to ensure the correctness of all set operations, making the project reliable and easy to maintain.

## Features

- **Union**: Combines two sets to form a new set containing all unique elements.
- **Intersection**: Finds the common elements between two sets.
- **Difference**: Computes the difference between two sets, resulting in the elements present in one set but not the other.
- **Complement**: Finds the complement of a set relative to a universal set.
- **Expression Evaluator**: Evaluates string-based expressions of set operations.
- **Unit Testing**: Ensures the accuracy and robustness of the set operations using comprehensive unit tests.

## Example Usage

### Set Operations

```csharp
var setA = new Set(new List<int> {1, 2, 3});
var setB = new Set(new List<int> {3, 4, 5});
var setC = new Set(new List<int> {5, 6, 7});

var unionResult = setA.Union(setB);        // [1, 2, 3, 4, 5]
var intersectionResult = setA.Intersection(setB); // [3]
var differenceResult = setA.Difference(setB); // [1, 2]

var setsDict = new Dictionary<string, Set>
{
    {"A", setA},
    {"B", setB},
    {"C", setC}
};

var expression = "A intersection B union C";
Set resultSet = Set.EvaluateExpression(expression, setsDict); // [3, 5, 6, 7]


## Installation
Clone the repository:
git clone https://github.com/BogdanSch/Sets.git
Open the project in Visual Studio or your preferred C# development environment.
Restore dependencies:
dotnet restore
Build the project:
dotnet build
Running Unit Tests
Unit tests are included in the Sets.UnitTests project. You can run the tests using the following command:

dotnet test
The tests validate the functionality of the set operations and ensure that the expression evaluator behaves correctly.

Project Structure
├── Sets
│   ├── Set.cs          # Core implementation of the Set class and its operations
│   ├── SetOperations.cs # (Optional) Enums or helper classes for set operations
│   ├── ExpressionEvaluator.cs # Evaluates string-based set expressions
├── Sets.UnitTests
│   ├── SetTests.cs     # Unit tests for set operations
│   ├── ExpressionTests.cs  # Unit tests for the expression evaluator
Future Improvements
Optimization: Improve the efficiency of set operations with larger datasets.
Additional Operations: Support for more advanced set operations.
Expression Parsing: Enhance the expression evaluator to handle more complex operations or nested expressions.
Contributing
Feel free to fork this project and submit pull requests with improvements or bug fixes. Open an issue if you find a bug or have a suggestion for a feature!

License
This project is licensed under the MIT License. See the LICENSE file for details.
```
