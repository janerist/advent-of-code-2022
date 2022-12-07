namespace AdventOfCode2022;

public static class Day06
{
    public static object Solve1()
    {
        return Solve(4);
    }
    
    public static object Solve2()
    {
        return Solve(14);
    }

    private static object Solve(int n)
    {
        var buffer = File.ReadAllText("Day06.txt").Trim();

        for (var i = n; i < buffer.Length; i++)
        {
            if (buffer[(i - n)..i].Distinct().Count() == n)
            {
                return i;
            }
        }

        throw new InvalidOperationException();
    }
}