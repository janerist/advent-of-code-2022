using System.Text.RegularExpressions;

namespace AdventOfCode2022;

public static class Day05
{
    public record Instruction(int Amount, int From, int To);
    
    public static object Solve1() =>
        Solve((stacks, instruction) =>
        {
            foreach(var _ in Enumerable.Range(0, instruction.Amount))
            {
                stacks[instruction.To - 1].Push(stacks[instruction.From - 1].Pop());
            }
        });

    public static object Solve2() =>
        Solve((stacks, instruction) =>
        {
            var moving = Enumerable.Range(0, instruction.Amount)
                .Select(_ => stacks[instruction.From - 1].Pop());

            foreach (var crate in moving.Reverse())
            {
                stacks[instruction.To - 1].Push(crate);    
            }
        });

    private static object Solve(Action<List<Stack<char>>, Instruction> applyInstruction)
    {
        var splits = File.ReadAllText("Day05.txt").Split("\n\n");
        
        var stacks = ParseStacks(splits[0]);
        var instructions = ParseInstructions(splits[1]);

        foreach (var instruction in instructions)
        {
            applyInstruction(stacks, instruction);
        }

        return string.Concat(stacks.Select(s => s.Pop()));
    }

    private static IEnumerable<Instruction> ParseInstructions(string text)
    {
        var regex = new Regex(@"move (\d+) from (\d+) to (\d+)");

        return text
            .Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
            {
                var groups = regex.Match(line).Groups;
                return new Instruction(int.Parse(groups[1].Value),
                    int.Parse(groups[2].Value),
                    int.Parse(groups[3].Value));
            });
    }

    private static List<Stack<char>> ParseStacks(string text)
    {
        var crates = text.Split("\n").SkipLast(1);

        return crates.Reverse().Aggregate(new List<Stack<char>>(),
            (stacks, line) =>
            {
                var current = 0;
                for (var i = 1; i < line.Length; i += 4, current++)
                {
                    if (stacks.Count <= current)
                    {
                        stacks.Add(new Stack<char>());
                    }

                    if (line[i] != ' ')
                    {
                        stacks[current].Push(line[i]);
                    }
                }

                return stacks;
            });
    }
}