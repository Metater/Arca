using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arca.ArcCompiler
{
	public class Arcac
	{
		private ArcLexer lexer;

		public Arcac(string[] sourceLines)
		{
			List<string> source = new();
			foreach (string sourceLine in sourceLines)
			{
				string sourceLineTrimmed = sourceLine.Trim(' ');
				if (sourceLineTrimmed.Length >= 2 && sourceLineTrimmed.Substring(0, 2) == "//")
					continue;
				if (string.IsNullOrWhiteSpace(sourceLineTrimmed))
					continue;
				source.Add(sourceLineTrimmed);
			}
			lexer = new ArcLexer(source);
			ArcToken token = null;
			while (token == null || token.type != "eof")
			{
				token = lexer.GetNextToken();
				Console.WriteLine($"Type: {token.type} Value: {token.value}");
			}
		}
	}
}
