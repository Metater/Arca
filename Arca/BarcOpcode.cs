
namespace Arca;

public enum BarcOpcode : byte
{
	Noop,
	Print,
	Declare,
	JumpRelativeSByte,
	JumpRelativeShort,
	JumpAbsolute,
}