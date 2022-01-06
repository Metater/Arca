using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Arca
{
	public class BarcReference
	{
		public BarcReferenceType type;

		public BarcData data;
	}

	public abstract class BarcData
	{

	}

	public class StringData : BarcData
	{
		public string value;
	}

	public enum BarcReferenceType : byte
	{
		String,
		Custom,
	}
}