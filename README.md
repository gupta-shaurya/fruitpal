# Fruitpal

Fruitpal is a tool which allows the trader to understand the full cost of buying a fruit from various countires of origin.

## Build

Build the Console Application (Fruitpal.ConsoleApp)

```bash
dotnet build Fruitpal.ConsoleApp
```

## Usage

Sample Input
```bash
mango 53 405
```

Sample Output
```bash
BR 22060.10 | ((53 + 1.42) * 405) + 20.00
MX 21999.20 | ((53 + 1.24) * 405) + 32.00
```

## Important Notes

The solution of this tool has been designed to be as generic and extensible as possible. Some of the ways in which this has been achieved:

- Customizable Formula: The formula used for calculating the total cost is customizable to accomodate any new additions/updates both either to the data or the formula WITHOUT any code change in the business logic. For example, if a new data point, vendor_cost is added to the data, the same can be utilised by simply updating the calculation formula. This has been achieved using a mathematical expression parser (NCalc in this case).
- Data Provider: Currently a FileDataProvider has been created to fetch the sample data from a file. However, similar data provider provider can be plugged in, such as SQLDataProvider or PostgreSQLDataProvider with the current abstraction of IDataProvider to fetch data from the corresponding sources.

Unit Tests have also been added for the Service layer which takes into account both positive and negative scenarios. Similar test coverage may be extended in the future to the other application layers.

## Acknowledgments

This tool was made possible utilizing the below libraries (aside from .NET Core and .NET Standard):

- [NCalc](https://github.com/sklose/NCalc2)
- [Newtonsoft.JSON](https://github.com/JamesNK/Newtonsoft.Json)
- [xUnit](http://xunit.github.io/)
- [Moq](https://github.com/moq/moq4)

## License
[MIT](https://choosealicense.com/licenses/mit/)
