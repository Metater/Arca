namespace Arca;

public abstract class Archetype
{
    public string? Name { get; protected set; }

    public virtual void Print()
    {
        Console.WriteLine(Name);
    }
}