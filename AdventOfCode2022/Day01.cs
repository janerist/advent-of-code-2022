namespace AdventOfCode2022;

public static class Day01
{
    public static object Solve1()
    {
        return GetElves().Max();
    }

    public static object Solve2()
    {
        return GetElves().OrderDescending().Take(3).Sum();
    }

    private static IEnumerable<int> GetElves() =>
        File.ReadAllText("Day01.txt")
            .Split("\n\n", StringSplitOptions.TrimEntries)
            .Select(l => l.Split("\n").Select(int.Parse).Sum());
}