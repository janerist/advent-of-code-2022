namespace AdventOfCode2022;

public static class Day07
{
    public static object Solve1()
    {
        var root = GetRoot();

        long sum = 0;
        VisitDir(root, current =>
        {
            var size = GetSize(current);
            if (size <= 100_000)
            {
                sum += size;
            }
        });

        return sum;
    }

    public static object Solve2()
    {
        var root = GetRoot();
        var totalSize = GetSize(root);
        var free = 70_000_000 - totalSize;

        var smallest = totalSize;
        VisitDir(root, current =>
        {
            var size = GetSize(current);
            if (free + size >= 30_000_000 && size < smallest)
            {
                smallest = size;
            }
        });

        return smallest;
    }

    private static Directory GetRoot()
    {
        var commands = System.IO.File
            .ReadAllText("Day07.txt")
            .Split("$ ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim());

        var root = new Directory("/");
        commands.Aggregate(root, (current, command) => command switch
        {
            ['c', 'd', ' ', '/'] => root,
            ['c', 'd', ' ', '.', '.'] => current.Parent,
            ['c', 'd', ' ', .. var dir] => AddDirectory(current, dir),
            ['l', 's', '\n', .. var contents] => AddFiles(current, contents)
        });
        
        return root;
    }

    private static Directory AddDirectory(Directory currentDir, string name)
    {
        var dir = new Directory(name, currentDir);
        currentDir.Contents.Add(dir);
        return dir;
    }

    private static Directory AddFiles(Directory currentDir, string contents)
    {
        var nodes = contents
            .Split("\n")
            .Select(line => line.Split(" ") switch
            {
                ["dir", var name] => new Directory(name, currentDir) as Node,
                [var size, var name] => new File(name, Convert.ToInt64(size))
            });
        
        currentDir.Contents.AddRange(nodes);
        return currentDir;
    }

    private static long GetSize(Node node) =>
        node switch
        {
            File f => f.Size,
            Directory d => d.Contents.Sum(GetSize)
        };

    private static void VisitDir(Directory current, Action<Directory> action)
    {
        action(current);
        
        foreach (var dir in current.Contents.OfType<Directory>())
        {
            VisitDir(dir, action);
        }
    }

    private abstract class Node
    {
        public string Name { get; set; }
    }

    private class Directory : Node
    {
        public List<Node> Contents { get; } = new();
        public Directory? Parent { get; }

        public Directory(string name, Directory? parent = null)
        {
            Name = name;
            Parent = parent;
        }
    }

    private class File : Node
    {
        public long Size { get; }

        public File(string name, long size)
        {
            Name = name;
            Size = size;
        }
    }
}