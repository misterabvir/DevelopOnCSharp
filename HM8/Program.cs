namespace HM8;

public class Program
{


    public static void Main(string[] args)
    {
        string directory = Directory.GetCurrentDirectory();
        string substring = string.Empty;
        string extension = string.Empty;

        if (CheckArgs(args, ref extension, ref substring))
        {
            Console.WriteLine($"extension: {extension}");
            Console.WriteLine($"substring: {substring}");
            Console.WriteLine("----------------------------------");

            List<string> lines = ReadFrom(directory, extension, substring);
            Print(lines, substring);
        }
    }

    private static void Print(List<string> lines, string substring)
    {
        foreach (string line in lines)
            {
                int index = -1;
                int lastIndex = 0;
                while ((index = line.IndexOf(substring, index + 1, StringComparison.OrdinalIgnoreCase)) >= 0)
                {
                    ReadOnlySpan<char> span = line.AsSpan();

                    Console.Write(span[lastIndex..index].ToArray());

                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Green;

                    Console.Write(span.Slice(index, substring.Length).ToArray());

                    Console.ResetColor();

                    lastIndex = index + substring.Length;
                }

                if (lastIndex < line.Length)
                {
                    Console.WriteLine(line[lastIndex..]);
                }
                else
                {
                    Console.WriteLine();
                }
            }
    }

  

    private static List<string> ReadFrom(string currentDirectory, string extension, string substring)
    {
        List<string> strings = [];

        string[] files = Directory.GetFiles(currentDirectory);
        foreach (string file in files)
        {
            FileInfo info = new(file);
            if (info.Extension.EndsWith(extension))
            {
                strings.AddRange(SearchIn(file, substring));
            }
        }
        string[] directories = Directory.GetDirectories(currentDirectory);
        foreach (string directory in directories)
        {
            strings.AddRange(ReadFrom(directory, extension, substring));
        }
        return strings;
    }

    private static IEnumerable<string> SearchIn(string file, string substring)
    {
        List<string> lines = [];
        using StreamReader reader = new(file);
        while (!reader.EndOfStream)
        {
            string? line = reader.ReadLine();
            if (line is not null && line.Contains(substring, StringComparison.CurrentCultureIgnoreCase))
                lines.Add(line.Trim());
        }
        return lines;
    }


    private static bool CheckArgs(string[] args, ref string extension, ref string substring)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Enter two arguments: extension and the substring you are looking for.");
            return false;
        }
        extension = args[0];
        substring = args[1];
        return true;
    }
}


