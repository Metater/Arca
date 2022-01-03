using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arca;

public class IntArch : Archetype
{
    public readonly int value;

    public IntArch(int value)
    {
        this.value = value;
    }

    public override void Print()
    {
        Console.WriteLine(value);
    }
}