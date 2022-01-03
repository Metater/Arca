using Arca;

Console.WriteLine("Hello, World!");

Console.WriteLine("What should be compiled?");

string file = Console.ReadLine()!;

Console.WriteLine($"Compiling...");

string[] lines = File.ReadAllLines(file);
List<string> source = new();
foreach (string line in lines)
{
    string trimmed = line.Trim();
    if (trimmed[..2] == "//") continue;
    source.Add(trimmed);
}

BarcWriter writer = new();
foreach (string line in source)
{

}

string outputFile = file.Insert(file.Length - 3, "b");
Console.WriteLine(outputFile);
File.WriteAllBytes(outputFile, writer.CopyData());