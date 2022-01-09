namespace Arca.BarcInterpreter
{
	public enum BarcOpcode : byte
	{
		Noop,
		Push,
		Pop,
		Call,
		Print,
		DeclareConstant,
		Release,
		JumpRelativeSByte,
		JumpRelativeShort,
		JumpAbsoluteUShort,
		JumpAbsoluteInt,
	}
}