using System.Diagnostics;
using System.Globalization;
using AdventOfCode2022;

new List<Func<object>>
    {
        Day01.Solve1,
        Day01.Solve2,
        
        Day02.Solve1,
        Day02.Solve2,
        
        Day03.Solve1,
        Day03.Solve2,
        
        Day04.Solve1,
        Day04.Solve2,
        
        Day05.Solve1,
        Day05.Solve2,
        
        Day06.Solve1,
        Day06.Solve2
    }
    .ForEach(solver =>
    {
        var sw = Stopwatch.StartNew();
        var solution = solver();
        Console.WriteLine(
            $"{solver.Method.DeclaringType.Name}.{solver.Method.Name} [{sw.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture)} ms]: {solution}");
    });