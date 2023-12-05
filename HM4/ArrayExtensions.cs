namespace HM4;

public static class ArrayExtensions
{
    public static List<int[]> FindThreeSumCombinations(this int[] array, int target)
    {
        var input = array.OrderBy(i => i).ToArray();
        var result = new List<int[]>();

        for (int i = 0; i < input.Length; i++)
        {
            int left = i + 1;
            int right = input.Length - 1;

            while (left < right)
            {
                int current = input[i] + input[left] + input[right];
                if (current == target)
                {
                    result.Add([input[i], input[left], input[right]]);
                    left++;
                    right--;
                }
                else if (current < target)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }
        }
        return result;
    }

    public static int[] GetRandomArray(this int length)
    {
        var array = Enumerable.Range(1, length).ToArray();
        for (int i = 0; i < array.Length - 1; i++)
        {
            int rand = Random.Shared.Next(i + 1, array.Length);
            (array[i], array[rand]) = (array[rand], array[i]);
        }
        return array;
    }

    public static string GetString(this int[] array)
        => $"[{string.Join(" ", array)}]";
}
