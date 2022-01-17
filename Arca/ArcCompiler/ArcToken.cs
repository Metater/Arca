using System.Diagnostics;
using System;
using System.Collections.Generic;

namespace Arca.ArcCompiler
{
	public sealed class ArcToken
	{
		public string type;
		public object value;

		public ArcToken(string type, object value)
		{
			this.type = type;
			this.value = value;
		}

		public ArcToken(string type) : this(type, null) { }
	}
}