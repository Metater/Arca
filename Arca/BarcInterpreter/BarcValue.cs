using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Arca.BarcInterpreter
{
	[StructLayout(LayoutKind.Explicit)]
	public struct BarcValue
	{
		[FieldOffset(0)]
		public BarcValueType type;

		[FieldOffset(1)]
		public int Aint;

		[FieldOffset(1)]
		public byte Abyte;

		[FieldOffset(1)]
		public bool Abool;
	}

	public enum BarcValueType : byte
	{
		Invalid,
		Reference,
		Int,
		Byte,
		Bool,
	}
}