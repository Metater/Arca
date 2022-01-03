using Arca;

Console.WriteLine("Hello, World!");

Console.WriteLine("What should be interpreted?");

string file = Directory.GetCurrentDirectory() + @"\" + Console.ReadLine() + ".arc";

Console.WriteLine($"Interpreting {file}");

Arcai arcai = new();

foreach (string line in File.ReadLines(file))
{
    string[] split = line.Split(' ', StringSplitOptions.TrimEntries);
    if (arcai.TryGetArchetype(split[0], out Archetype archetype))
    {

    }
}