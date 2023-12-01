namespace Labyrinth;

public static class LabyrinthExtensions
{
    private static readonly int wall = 1;
    private static readonly int entrance = 2;
    private static readonly int wasChecked = 3;

    public static void SetChecked(this int[,] labyrinth, (int x, int y) position)
        => labyrinth[position.y, position.x] = wasChecked;

    public static bool IsExit(this int[,] labyrinth, (int x, int y) position)
        => labyrinth[position.y, position.x] == entrance;

    public static bool CheckDown(this int[,] labyrinth, (int x, int y) position) =>
        position.y + 1 < labyrinth.GetLength(0) &&
        labyrinth[position.y + 1, position.x] != wall &&
        labyrinth[position.y + 1, position.x] != wasChecked;

    public static bool CheckUp(this int[,] labyrinth, (int x, int y) position) =>
        position.y > 0 &&
        labyrinth[position.y - 1, position.x] != wall &&
        labyrinth[position.y - 1, position.x] != wasChecked;

    public static bool CheckRight(this int[,] labyrinth, (int x, int y) position) =>
        position.x + 1 < labyrinth.GetLength(1) &&
        labyrinth[position.y, position.x + 1] != wall &&
        labyrinth[position.y, position.x + 1] != wasChecked;

    public static bool CheckLeft(this int[,] labyrinth, (int x, int y) position) =>
        position.x > 0 &&
        labyrinth[position.y, position.x - 1] != wall &&
        labyrinth[position.y, position.x - 1] != wasChecked;

    public static void Print(this int[,] labyrinth, List<(int x, int y)> exits)
    {
        Console.Clear();
        for (int y = 0; y < labyrinth.GetLength(0); y++)
        {
            for (int x = 0; x < labyrinth.GetLength(1); x++)
            {
                string cell = labyrinth[y, x] switch
                {
                    0 => "  ",
                    1 => "██",
                    2 => "►◄",
                    3 => "..",
                    _ => "",
                };
                var color = Console.ForegroundColor;
                if (exits.Contains((x, y)))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write(cell);
                Console.ForegroundColor = color;
            }
            Console.WriteLine();
        }
        Console.WriteLine($"count of exits -> {exits.Count}");
    }

    public static (int x, int y) GetRandomPosition(this int[,] labyrinth)
    {
        (int x, int y) position;
        do
        {
            position = (
                Random.Shared.Next(0, labyrinth.GetLength(1)),
                Random.Shared.Next(0, labyrinth.GetLength(0)));
        } while (labyrinth[position.y, position.x] != 0);
        return position;
    }
}