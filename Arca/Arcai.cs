namespace Arca;

public class Arcai
{
    public Dictionary<string, Archetype> archetypes = new();

    public Dictionary<string, Member> members = new();

    public bool TryGetArchetype(string str, out Archetype archetype)
    {
        return archetypes.TryGetValue(str, out archetype!);
    }

    public void New(string archetype, string member)
    {
        switch (archetype)
        {
            case "int":
                members.Add(member, new Member(new IntArch(0)));
                break;
        }
    }

    public void Assign(string member, int value)
    {
        members[member].archetype = new IntArch(value);
    }


    public void Print(string member)
    {
        members[member].archetype.Print();
    }
}