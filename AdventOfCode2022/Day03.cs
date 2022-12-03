namespace AdventOfCode2022;

public static class Day03
{
    public static object Solve1()
    {
        return File.ReadAllLines("Day03.txt")
            .Select(x => x.Chunk(x.Length / 2))
            .Select(CommonItem)
            .Select(Priority)
            .Sum();
    }

    public static object Solve2()
    {
        return File.ReadAllLines("Day03.txt")
            .Chunk(3)
            .Select(CommonItem)
            .Select(Priority)
            .Sum();
    }

    private static char CommonItem(IEnumerable<IEnumerable<char>> rucksacks) =>
        rucksacks
            .Aggregate((prev, next) => string.Concat(prev.Intersect(next)))
            .First();

    private static int Priority(char item) =>
        char.IsLower(item)
            ? item - 96
            : item - 64 + 26;
}