using System.Numerics;

namespace AdventOfCode2022;

public static class Day11
{
    class Monkey
    {
        public List<long> Items { get; set; } = new();
        public Func<long, long> Operation { get; set; }
        public int Test { get; set; }
        public int TestTrue { get; set; }
        public int TestFalse { get; set; }

        public List<long> Inspected { get; } = new();
    }

    public static object Solve1()
    {
        return Solve(ParseMonkeys(), 20, n => n / 3);
    }

    public static object Solve2()
    {
        var monkeys = ParseMonkeys();
        var product = monkeys.Aggregate(1, (acc, m) => acc * m.Test);
        return Solve(monkeys, 10_000, n => n % product);
    }

    private static object Solve(List<Monkey> monkeys, int rounds, Func<long, long> worryManagementFunc)
    {
        for (var round = 0; round < rounds; round++)
        {
            foreach (var monkey in monkeys)
            {
                foreach (var item in monkey.Items)
                {
                    monkey.Inspected.Add(item);
                    var newWorryLevel = worryManagementFunc(monkey.Operation(item));

                    if (newWorryLevel % monkey.Test == 0)
                    {
                        monkeys[monkey.TestTrue].Items.Add(newWorryLevel);
                    }
                    else
                    {
                        monkeys[monkey.TestFalse].Items.Add(newWorryLevel);
                    }
                }

                monkey.Items.Clear();
            }
        }

        var ordered = monkeys
            .Select(m => m.Inspected.Count)
            .OrderDescending()
            .ToArray();

        return (long)ordered[0] * ordered[1];
    }

    private static List<Monkey> ParseMonkeys()
    {
        return File.ReadAllText("Day11.txt")
            .Split("\n\n")
            .Select(m => m.Split("\n"))
            .Aggregate(new List<Monkey>(), (monkeys, monkeyDesc) =>
            {
                var monkey = new Monkey();
                foreach (var line in monkeyDesc)
                {
                    switch (line.Trim().Split(":"))
                    {
                        case ["Starting items", var items]:
                            monkey.Items = items.Split(",").Select(long.Parse).ToList();
                            break;

                        case ["Operation", var operation]:
                            monkey.Operation = old =>
                            {
                                if (operation.Trim().Split(" ") is
                                    [
                                        "new", "=", "old", var op, var operand
                                    ])
                                {
                                    var operandParsed = operand switch
                                    {
                                        "old" => old,
                                        _ => int.Parse(operand)
                                    };

                                    return op switch
                                    {
                                        "*" => old * operandParsed,
                                        "+" => old + operandParsed
                                    };
                                }

                                return old;
                            };
                            break;

                        case ["Test", var test]:
                            monkey.Test = int.Parse(test.Split(" ").Last());
                            break;

                        case ["If true", var testTrue]:
                            monkey.TestTrue = int.Parse(testTrue.Split(" ").Last());
                            break;

                        case ["If false", var testFalse]:
                            monkey.TestFalse = int.Parse(testFalse.Split(" ").Last());
                            break;
                    }
                }

                monkeys.Add(monkey);
                return monkeys;
            });
    }
}