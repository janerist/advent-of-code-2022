namespace AdventOfCode2022;

public static class Day08
{
    public static object Solve1()
    {
        var trees = File.ReadAllLines("Day08.txt")
            .Select(n => n.Select(c => (int)char.GetNumericValue(c)).ToArray())
            .ToArray();

        var visible = 0;
        for (var row = 1; row < trees.Length - 1; row++)
        {
            for (var col = 1; col < trees[0].Length - 1; col++)
            {
                var height = trees[row][col];
                if (trees[row][..col].All(x => x < height) ||
                    trees[row][(col + 1)..].All(x => x < height) ||
                    trees[..row].Select(r => r[col]).All(x => x < height) ||
                    trees[(row+1)..].Select(r => r[col]).All(x => x < height))
                {
                    visible++;
                }
            }
        }

        return visible + trees.Length * 4 - 4;
    }
    
    public static object Solve2()
    {
        var trees = File.ReadAllLines("Day08.txt")
            .Select(n => n.Select(c => (int)char.GetNumericValue(c)).ToArray())
            .ToArray();
        
        var max = 0;
        for (var row = 1; row < trees.Length - 1; row++)
        {
            for (var col = 1; col < trees[0].Length - 1; col++)
            {
                var height = trees[row][col];
                var left = trees[row][..col].Reverse().TakeWhile(x => x < height, true).Count();
                var right = trees[row][(col + 1)..].TakeWhile(x => x < height, true).Count();
                var top = trees[..row].Select(r => r[col]).Reverse().TakeWhile(x => x < height, true).Count();
                var bottom = trees[(row + 1)..].Select(r => r[col]).TakeWhile(x => x < height, true).Count();
                var score = left * right * top * bottom;

                if (score > max)
                {
                    max = score;
                }
            }
        }

        return max;
    }
}