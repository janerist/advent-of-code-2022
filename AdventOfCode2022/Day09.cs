namespace AdventOfCode2022;

public static class Day09
{
    public record Position(int X, int Y);

    public static object Solve1()
    {
        return Solve(2);
    }

    public static object Solve2()
    {
        return Solve(10);
    }

    private static int Solve(int numKnots)
    {
        var knots = new List<List<Position>>(Enumerable.Range(0, numKnots)
            .Select(_ => new List<Position> { new(0, 0) }));
        
        foreach (var move in File.ReadAllLines("Day09.txt"))
        {
            if (move.Split(" ") is [var dir, var n])
            {
                foreach (var _ in Enumerable.Range(0, int.Parse(n)))
                {
                    var head = knots.First();
                    var oldHead = head.Last();

                    head.Add(dir switch
                    {
                        "L" => oldHead with { X = oldHead.X - 1 },
                        "R" => oldHead with { X = oldHead.X + 1 },
                        "U" => oldHead with { Y = oldHead.Y + 1 },
                        "D" => oldHead with { Y = oldHead.Y - 1 }
                    });

                    for (var i = 1; i < knots.Count; i++)
                    {
                        var knot = knots[i];
                        knot.Add(AdjustTail(knot.Last(), knots[i-1][^1]));
                    }
                }
            }
        }

        return knots.Last().Distinct().Count();
    }

    private static Position AdjustTail(Position tail, Position head)
    {
        if (Math.Abs(head.X - tail.X) > 1 || Math.Abs(head.Y - tail.Y) > 1)
        {
            if (tail.X == head.X)
            {
                return tail with { Y = tail.Y > head.Y ? tail.Y - 1 : tail.Y + 1 };
            }
            
            if (tail.Y == head.Y)
            {
                return tail with { X = tail.X > head.X ? tail.X - 1 : tail.X + 1 };
            }
            
            return new Position(tail.X > head.X ? tail.X - 1 : tail.X + 1, tail.Y > head.Y ? tail.Y - 1 : tail.Y + 1); 
        }

        return tail;
    }
}