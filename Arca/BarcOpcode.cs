
namespace Arca
{
	public enum BarcOpcode : byte
	{
		Noop,
		Load,
		Print,
		DeclareConstant,
		JumpRelativeSByte,
		JumpRelativeShort,
		JumpAbsoluteUShort,
		JumpAbsoluteInt,
	}
}