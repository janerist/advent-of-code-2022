using System.Text;

namespace AdventOfCode2022;

public static class Day10
{
    public static object Solve1()
    {
        var cycles = GetCycles();
        return cycles[19] * 20 +
               cycles[59] * 60 +
               cycles[99] * 100 +
               cycles[139] * 140 +
               cycles[179] * 180 +
               cycles[219] * 220;
    }

    public static object Solve2()
    {
        var cycles = GetCycles();

        var crt = new StringBuilder();
        var pos = 0;
        foreach (var x in cycles)
        {
            crt.Append(x == pos || x - 1 == pos || x + 1 == pos ? '#' : '.');
            pos++;

            if (pos % 40 == 0)
            {
                crt.Append('\n');
                pos = 0;
            }
        }

        return crt;
    }

    private static List<int> GetCycles() => File
        .ReadLines("Day10.txt")
        .Aggregate(new List<int> { 1 }, (cycles, inst) =>
        {
            if (inst.Split(" ") is ["addx", var n])
            {
                cycles.Add(cycles.Last());
                cycles.Add(cycles.Last() + int.Parse(n));
            }
            else
            {
                cycles.Add(cycles.Last());
            }

            return cycles;
        });
}