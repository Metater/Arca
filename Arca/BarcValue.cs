using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Arca
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
	}

	public enum BarcValueType : byte
	{
		Invalid,
		Reference,
		Int,
		Byte,
	}
}