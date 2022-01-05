using Arca;

/*
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

*/

BarcWriter writer = new();

writer.Put((byte)4);

writer.Put((byte)BarcConstantType.String);
writer.Put("Hello");

writer.Put((byte)BarcConstantType.String);
writer.Put("Goodbye");

writer.Put((byte)BarcConstantType.String);
writer.Put("World");

writer.Put((byte)BarcConstantType.String);
writer.Put("Mars");

for (int i = 0; i < 10; i++)
{
	writer.Put((byte)BarcOpcode.Print);
	writer.Put((byte)0);

	writer.Put((byte)BarcOpcode.JumpRelativeSByte);
	writer.Put((sbyte)1);

	writer.Put((byte)BarcOpcode.Print);
	writer.Put((byte)1);

	writer.Put((byte)BarcOpcode.Print);
	writer.Put((byte)2);

	writer.Put((byte)BarcOpcode.Print);
	writer.Put((byte)3);
}

Console.WriteLine("Bytecode Size: " + writer.Length + " bytes");

Arcai arcai = new();
arcai.Load(writer.CopyData());
arcai.Interpret();