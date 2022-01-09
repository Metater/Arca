using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Arca.BarcInterpreter
{
	public sealed class BarcReference
	{
		public BarcReferenceType type;

		public BarcData data;

		public BarcReference(BarcReferenceType type, BarcData data)
		{
			this.type = type;
			this.data = data;
		}
	}

	public abstract class BarcData
	{

	}

	public sealed class StringData : BarcData
	{
		public string value;

		public StringData(string value)
		{
			this.value = value;
		}

		public override string ToString()
		{
			return value;
		}
	}

	public enum BarcReferenceType : byte
	{
		String,
		Custom,
	}
}