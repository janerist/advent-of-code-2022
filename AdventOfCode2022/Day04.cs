namespace AdventOfCode2022;

public static class Day04
{
    public static object Solve1()
    {
        bool Contains((int[] First, int[] Second) pair) =>
            pair.First.All(pair.Second.Contains) || pair.Second.All(pair.First.Contains);

        return File.ReadAllLines("Day04.txt")
            .Select(Pairs)
            .Count(Contains);
    }

    public static object Solve2()
    {
        bool Overlaps((int[] First, int[] Second) pair) =>
            pair.First.Intersect(pair.Second).Any();

        return File.ReadAllLines("Day04.txt")
            .Select(Pairs)
            .Count(Overlaps);
    }

    private static (int[], int[]) Pairs(string line)
    {
        var ranges = line.Split(",")
            .Select(range => range.Split("-").Select(int.Parse).ToArray())
            .Select(nums => Enumerable.Range(nums[0], nums[1] - nums[0] + 1).ToArray())
            .ToArray();

        return (ranges[0], ranges[1]);
    }
}