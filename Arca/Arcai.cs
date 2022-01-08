using System.Diagnostics;
using System;
using System.Collections.Generic;

namespace Arca
{
	public class Arcai
	{
		private readonly BarcReader reader = new();
		private readonly BarcConstant[] constantTable;
		private readonly BarcValue[] variableTable = new BarcValue[256];
		private readonly Stack<BarcValue> stack = new();
		private readonly BarcReference[] heap = new BarcReference[256];


		public Arcai(byte[] bytecode)
		{
			reader.SetSource(bytecode);
			int constantTableSize = reader.GetByte();
			constantTable = new BarcConstant[constantTableSize];
			for (int i = 0; i < constantTableSize; i++)
			{
				BarcConstantType type = (BarcConstantType)reader.GetByte();
				int position = reader.Position;
				constantTable[i] = new BarcConstant(type, position);
				switch (type)
				{
					case BarcConstantType.String:
						reader.SkipBytes(reader.GetInt());
						break;
					case BarcConstantType.Int:
						reader.SkipBytes(4);
						break;
					case BarcConstantType.Byte:
						reader.SkipBytes(1);
						break;
				}
			}
		}

		public void Interpret()
		{
			Stopwatch sw = Stopwatch.StartNew();
			bool error = false;
			BarcOpcode errorOpcode = BarcOpcode.Noop;
			while (!reader.EndOfData && !error)
			{
				BarcOpcode opcode = (BarcOpcode)reader.GetByte();
				switch (opcode)
				{
					case BarcOpcode.Noop:
						break;
					case BarcOpcode.Load:


						break;
					case BarcOpcode.Print:
						Print();
						break;
					case BarcOpcode.DeclareConstant:
						DeclareConstant();
						break;
					case BarcOpcode.JumpRelativeSByte:
						reader.SetPosition(reader.Position + reader.GetSByte());
						break;
					case BarcOpcode.JumpRelativeShort:
						reader.SetPosition(reader.Position + reader.GetShort());
						break;
					case BarcOpcode.JumpAbsoluteUShort:
						reader.SetPosition(reader.GetUShort());
						break;
					case BarcOpcode.JumpAbsoluteInt:
						reader.SetPosition(reader.GetInt());
						break;
					default:
						error = true;
						errorOpcode = opcode;
						break;
				}
			}
			sw.Stop();
			if (error)
				Console.WriteLine($"Runtime Error:\n\tBefore Byte: {reader.Position}\n\tWhilst Executing: {errorOpcode}");
			//Console.WriteLine((sw.ElapsedTicks / 10000d) + "ms");
			Console.WriteLine((sw.ElapsedTicks / 1000000d) + "ms");
		}

		private void Print()
		{
			BarcConstant constant = constantTable[reader.GetByte()];
			switch (constant.type)
			{
				case BarcConstantType.String:
					Console.WriteLine(constant.GetString(reader));
					break;
				case BarcConstantType.Int:
					Console.WriteLine(constant.GetInt(reader));
					break;
				case BarcConstantType.Byte:
					Console.WriteLine(constant.GetByte(reader));
					break;
			}
		}

		private void DeclareConstant()
		{
			byte position = reader.GetByte();
			BarcConstant constant = constantTable[reader.GetByte()];
			BarcValue value = new();
			switch (constant.type)
			{
				case BarcConstantType.String:
					value.type = BarcValueType.Reference;
					break;
				case BarcConstantType.Int:
					value.type = BarcValueType.Int;
					value.Aint = constant.GetInt(reader);
					break;
				case BarcConstantType.Byte:
					value.type = BarcValueType.Byte;
					value.Abyte = constant.GetByte(reader);
					break;
			}
			variableTable[position] = value;
		}
	}
}