namespace AdventOfCode2022;

public static class Extensions
{
    public static IEnumerable<T> TakeWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool inclusive)
    {
        foreach(var item in source)
        {
            if (predicate(item)) 
            {
                yield return item;
            }
            else
            {
                if (inclusive)
                {
                    yield return item;
                }

                yield break;
            }
        }
    } 
}