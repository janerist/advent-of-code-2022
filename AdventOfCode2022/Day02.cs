namespace AdventOfCode2022;

using static RPS;
using static Outcome;

internal enum RPS { Rock, Paper, Scissors }
internal enum Outcome { Lose, Draw, Win }

public static class Day02
{
    public static object Solve1()
    {
        return File.ReadAllLines("Day02.txt")
            .Select(ParseLine1)
            .Select(GetPoints1)
            .Sum();
    }
    
    public static object Solve2()
    {
        return File.ReadAllLines("Day02.txt")
            .Select(ParseLine2)
            .Select(GetPoints2)
            .Sum();
    }

    private static (RPS, RPS) ParseLine1(string line)
    {
        RPS ParseLetter(string letter) =>
            letter switch
            {
                "A" or "X" => Rock,
                "B" or "Y" => Paper,
                "C" or "Z" => Scissors
            };

        if (line.Split(" ") is [var first, var second])
        {
            return (ParseLetter(first), ParseLetter(second));
        }

        throw new InvalidOperationException();
    }

    private static int GetPoints1((RPS, RPS) pair) =>
        pair switch
        {
            (Rock, Rock) => 1 + 3,
            (Rock, Paper) => 2 + 6,
            (Rock, Scissors) => 3 + 0,

            (Paper, Rock) => 1 + 0,
            (Paper, Paper) => 2 + 3,
            (Paper, Scissors) => 3 + 6,

            (Scissors, Rock) => 1 + 6,
            (Scissors, Paper) => 2 + 0,
            (Scissors, Scissors) => 3 + 3
        };

    private static (RPS, Outcome) ParseLine2(string line)
    {
        if (line.Split(" ") is [var first, var second])
        {
            return (first switch
            {
                "A" => Rock,
                "B" => Paper,
                "C" => Scissors
            }, second switch
            {
                "X" => Lose,
                "Y" => Draw,
                "Z" => Win
            });
        }

        throw new InvalidOperationException();
    }

    private static int GetPoints2((RPS, Outcome) pair) =>
        pair switch
        {
            (Rock, Lose) => GetPoints1((Rock, Scissors)),
            (Rock, Draw) => GetPoints1((Rock, Rock)),
            (Rock, Win) => GetPoints1((Rock, Paper)),

            (Paper, Lose) => GetPoints1((Paper, Rock)),
            (Paper, Draw) => GetPoints1((Paper, Paper)),
            (Paper, Win) => GetPoints1((Paper, Scissors)),

            (Scissors, Lose) => GetPoints1((Scissors, Paper)),
            (Scissors, Draw) => GetPoints1((Scissors, Scissors)),
            (Scissors, Win) => GetPoints1((Scissors, Rock))
        };
}