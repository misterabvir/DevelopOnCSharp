using HM4;

int[] array = 10.GetRandomArray();
Console.WriteLine($"Original array: {array.GetString()}"); // Original array: [6 3 5 9 1 8 2 4 10 7]

array.FindThreeSumCombinations(12)
     .ForEach(item => Console.WriteLine($"Combination: {item.GetString()} = {item.Sum()}"));

/*  OUTPUT:
    Combination: [1 2 9] = 12
    Combination: [1 3 8] = 12
    Combination: [1 4 7] = 12
    Combination: [1 5 6] = 12
    Combination: [2 3 7] = 12
    Combination: [2 4 6] = 12
    Combination: [3 4 5] = 12
*/

array.FindThreeSumCombinations(7)
     .ForEach(item => Console.WriteLine($"Combination: {item.GetString()} = {item.Sum()}"));

/*  OUTPUT:
    Combination: [1 2 4] = 7
*/