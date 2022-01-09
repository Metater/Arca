namespace Arca.BarcInterpreter
{
	public enum BarcOpcode : byte
	{
		Noop,
		Push,
		Print,
		DeclareConstant,
		Release,
		JumpRelativeSByte,
		JumpRelativeShort,
		JumpAbsoluteUShort,
		JumpAbsoluteInt,
	}
}