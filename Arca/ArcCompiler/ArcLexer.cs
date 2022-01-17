using System.Diagnostics;
using System;
using System.Collections.Generic;

namespace Arca.ArcCompiler
{
	public class ArcLexer
	{
		private readonly List<string> source;

		private int readPos = 0;
		private int line = 0;

		private char currentChar;

		private bool eol = false;
		private bool eof = false;

		public ArcLexer(List<string> source)
		{
			this.source = source;
			if (source.Count > 0)
				currentChar = Peek(0);
			else
				eof = true;
		}

		private void Advance(int amount = 1)
		{
			readPos += amount;
			if (source[line].Length > readPos)
				currentChar = source[line][readPos];
			else
				eol = true;
		}

		private char Peek(int depth = 1)
		{
			int peekPos = readPos + depth;
			if (source[line].Length > peekPos)
				return source[line][peekPos];
			return char.MinValue;
		}

		private void AdvanceLine()
		{
			line++;
			if (source.Count > line)
			{
				eol = false;
				readPos = 0;
				currentChar = Peek(0);
				return;
			}
			eof = true;
		}

		public ArcToken ReadIdentifier()
		{
			string result = "";
			while (char.IsLetterOrDigit(currentChar) && !eol)
            {
				result += currentChar;
				Advance();
            }
			return new ArcToken("identifier", result);
		}

		public ArcToken ReadNumeric()
		{
			string result = "";
			bool isDecimal = false;
			if (currentChar == '-')
            {
				result += currentChar;
				Advance();
            }
			else if (currentChar == '+')
				Advance();
			while ((char.IsDigit(currentChar) || (currentChar == '.' && !isDecimal)) && !eol)
            {
				if (currentChar == '.') isDecimal = true;
				result += currentChar;
				Advance();
            }
			if (isDecimal)
            {
                _ = double.TryParse(result, out double num);
				return new ArcToken("dec", num);
			}
			else
            {
				_ = int.TryParse(result, out int num);
				return new ArcToken("int", num);
			}
		}

		public ArcToken ReadEnclosed(char closing, string type)
		{
			string result = "";
			Advance();
			while (currentChar != closing && !eof)
			{
				if (eol)
				{
					result += '\n';
					AdvanceLine();
					continue;
				}
				result += currentChar;
				Advance();
			}
			Advance();
			return new ArcToken(type, result);
		}

		public ArcToken GetNextToken()
		{
			while (!eof)
			{
				while (!eol)
				{
					if (currentChar == ' ')
					{
						while (currentChar == ' ' && !eol)
							Advance();
						continue;
					}
					else if (char.IsLetter(currentChar))
						return ReadIdentifier();
					else if (char.IsDigit(currentChar) || ((currentChar == '+' || currentChar == '-') && char.IsDigit(Peek())))
						return ReadNumeric();
					else if (currentChar == '\"')
						return ReadEnclosed('\"', "literal");
					else
					{
						char c = currentChar;
						Advance();
						return new ArcToken(c.ToString());
					}
				}
				AdvanceLine();
			}
			return new ArcToken("eof");
		}
	}
}